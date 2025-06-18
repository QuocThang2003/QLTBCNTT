using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLTBCNTT.Models;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Net;

public class AuthController : Controller
{
    private readonly QltbcnttContext _context;

    public AuthController(QltbcnttContext context)
    {
        _context = context;
    }

    // ========== LOGIN ==========
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string matKhau)
    {
        var user = await _context.NguoiDungs.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(matKhau, user.MatKhau))
        {
            ViewBag.LoginError = "Email hoặc mật khẩu không đúng.";
            return View("Login");
        }

        HttpContext.Session.SetString("UserId", user.MaNguoidung.ToString());
        HttpContext.Session.SetString("UserName", user.HoTen);
        HttpContext.Session.SetString("UserRole", user.VaiTro);

        return user.VaiTro.ToLower() switch
        {
            "admin" => RedirectToAction("Index", "Home"),
            "quanly" => RedirectToAction("Index", "Quanlys"),
            "nhanvien" => RedirectToAction("Index", "StaffDevice"),
            _ => RedirectToAction("Login")
        };
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }


    // ========== QUÊN MẬT KHẨU ==========
    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        var user = await _context.NguoiDungs.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
        {
            ViewBag.Error = "Email không tồn tại.";
            ViewBag.ShowForgot = true; // 
            return View("Login");
        }

        // Tạo token
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        user.ResetToken = token;
        user.ResetTokenExpiry = DateTime.Now.AddMinutes(30);
        await _context.SaveChangesAsync();

        var resetUrl = Url.Action("ResetPassword", "Auth", new { token }, Request.Scheme);
        var logoCid = "cid:logoCID"; // dùng CID thay vì URL

        var emailBody = $@"
<div style='font-family:Poppins,sans-serif;padding:20px;max-width:600px;margin:auto;background:#fff;border-radius:10px;box-shadow:0 5px 20px rgba(0,0,0,0.1);'>
    <div style='text-align:center;margin-bottom:20px;'>
        <img src='{logoCid}' alt='Logo' style='max-height:80px;' />
        <h2 style='color:#4CAF50;margin:10px 0 5px;'>HỆ THỐNG QL TBCNTT</h2>
        <p style='margin:0;color:#777;'>Bệnh viện Ung Bướu TP.HCM</p>
    </div>

    <p>Xin chào <strong>{user.HoTen}</strong>,</p>
    <p>Hệ thống ghi nhận yêu cầu khôi phục mật khẩu cho tài khoản của bạn.<br />
    Nếu bạn là người thực hiện, vui lòng nhấn nút bên dưới để đặt lại mật khẩu:</p>

    <div style='text-align:center;margin:30px 0;'>
        <a href='{resetUrl}' style='display:inline-block;background:#4CAF50;color:#fff;padding:12px 24px;text-decoration:none;border-radius:8px;font-weight:bold;'>Đặt lại mật khẩu</a>
    </div>

    <p style='font-size:0.9em;color:#777;'>Liên kết này có hiệu lực trong vòng 30 phút. Nếu bạn không thực hiện, vui lòng bỏ qua email.</p>

    <div style='border-top:1px solid #eee;margin-top:20px;padding-top:10px;font-size:0.8em;color:#999;text-align:center;'>
        Email được gửi tự động từ hệ thống. Vui lòng không trả lời.
    </div>
</div>";



        await SendEmail(user.Email!, "Yêu cầu khôi phục mật khẩu - QL TBCNTT", emailBody);

        ViewBag.Message = "Đã gửi liên kết đặt lại mật khẩu đến email của bạn.";
        ViewBag.ShowForgot = true; // 
        return View("Login");
    }


    // ========== RESET MẬT KHẨU ==========
    [HttpGet]
    public IActionResult ResetPassword(string token)
    {
        var user = _context.NguoiDungs.FirstOrDefault(u => u.ResetToken == token && u.ResetTokenExpiry > DateTime.Now);
        if (user == null)
            return BadRequest("Liên kết không hợp lệ hoặc đã hết hạn.");

        ViewBag.Token = token;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(string token, string newPassword, string confirmPassword)
    {
        if (newPassword != confirmPassword)
        {
            ViewBag.Error = "Mật khẩu xác nhận không khớp.";
            ViewBag.Token = token;
            return View();
        }

        var user = await _context.NguoiDungs.FirstOrDefaultAsync(u => u.ResetToken == token && u.ResetTokenExpiry > DateTime.Now);
        if (user == null)
            return BadRequest("Token không hợp lệ hoặc đã hết hạn.");

        user.MatKhau = BCrypt.Net.BCrypt.HashPassword(newPassword);
        user.ResetToken = null;
        user.ResetTokenExpiry = null;
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Đặt lại mật khẩu thành công. Vui lòng đăng nhập.";
        return RedirectToAction("Login");
    }

    // ========== GỬI MAIL ==========
    private async Task SendEmail(string to, string subject, string body)
    {
        using var client = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("kaquach62@gmail.com", "sogm wjwu cmtw yugd"),
            EnableSsl = true
        };

        var mail = new MailMessage("kaquach62@gmail.com", to, subject, body)
        {
            IsBodyHtml = true
        };

        await client.SendMailAsync(mail);
    }

}
