using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLTBCNTT.Models;

public class AuthController : Controller
{
    private readonly QltbcnttContext _context;

    public AuthController(QltbcnttContext context)
    {
        _context = context;
    }

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

        // Điều hướng theo vai trò
        switch (user.VaiTro.ToLower())
        {
            case "admin":
                return RedirectToAction("Index", "Home"); 
            case "quanly":
                return RedirectToAction("Index", "Quanlys"); 
            case "nhanvien":
                return RedirectToAction("Index", "StaffDevice"); 
            default:
                return RedirectToAction("Login"); 
        }
    }


    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
