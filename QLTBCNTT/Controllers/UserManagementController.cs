using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLTBCNTT.Models;

namespace QLTBCNTT.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly QltbcnttContext _context;

        public UserManagementController(QltbcnttContext context)
        {
            _context = context;
        }

        // Xem danh sách người dùng
        public async Task<IActionResult> Index()
        {
            var users = await _context.NguoiDungs
                .Include(u => u.MaPhongbanNavigation)
                .ToListAsync();

            return View(users);
        }

        // Xem chi tiết người dùng
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.NguoiDungs
                .Include(u => u.MaPhongbanNavigation)
                .FirstOrDefaultAsync(u => u.MaNguoidung == id);

            if (user == null) return NotFound();

            return View(user);
        }

        // Hiển thị form tạo user mới
        public IActionResult Create()
        {
            ViewBag.PhongBans = new SelectList(_context.PhongBans, "MaPhongban", "TenPhongban");
            return View();
        }

        // Xử lý tạo user mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string hoTen, string email, string soDienThoai, string matKhau, string vaiTro, int? maPhongban)
        {
            var existingUser = await _context.NguoiDungs.FirstOrDefaultAsync(u => u.Email == email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Email đã được sử dụng.");
                ViewBag.PhongBans = new SelectList(_context.PhongBans, "MaPhongban", "TenPhongban");
                return View();
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(matKhau);
            var user = new NguoiDung
            {
                HoTen = hoTen,
                Email = email,
                SoDienthoai = soDienThoai,
                MatKhau = hashedPassword,
                VaiTro = vaiTro,
                MaPhongban = maPhongban
            };

            _context.NguoiDungs.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Hiển thị form sửa người dùng
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.NguoiDungs.FindAsync(id);
            if (user == null) return NotFound();

            ViewBag.PhongBans = new SelectList(_context.PhongBans, "MaPhongban", "TenPhongban", user.MaPhongban);
            return View(user);
        }

        // Xử lý cập nhật thông tin người dùng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string hoTen, string email, string soDienThoai, string vaiTro, int? maPhongban)
        {
            var user = await _context.NguoiDungs.FindAsync(id);
            if (user == null) return NotFound();

            var existingUser = await _context.NguoiDungs.FirstOrDefaultAsync(u => u.Email == email && u.MaNguoidung != id);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Email đã được sử dụng.");
                ViewBag.PhongBans = new SelectList(_context.PhongBans, "MaPhongban", "TenPhongban", maPhongban);
                return View(user);
            }

            var validRoles = new List<string> { "admin", "quanly", "nhanvien" };
            var normalizedRole = vaiTro?.ToLower();
            if (!validRoles.Contains(normalizedRole))
            {
                ModelState.AddModelError("VaiTro", "Vai trò không hợp lệ.");
                ViewBag.PhongBans = new SelectList(_context.PhongBans, "MaPhongban", "TenPhongban", maPhongban);
                return View(user);
            }

            user.HoTen = hoTen;
            user.Email = email;
            user.SoDienthoai = soDienThoai;
            user.VaiTro = normalizedRole;
            user.MaPhongban = maPhongban;

            _context.NguoiDungs.Update(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        // Hiển thị form xác nhận xóa
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var user = await _context.NguoiDungs
                .Include(u => u.MaPhongbanNavigation)
                .FirstOrDefaultAsync(u => u.MaNguoidung == id);
            if (user == null) return NotFound();

            return View(user);
        }

        // Xử lý xóa người dùng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.NguoiDungs.FindAsync(id);
            if (user != null)
            {
                _context.NguoiDungs.Remove(user);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
