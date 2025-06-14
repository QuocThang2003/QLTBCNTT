using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLTBCNTT.Models;

namespace QLTBCNTT.Controllers
{
    public class StaffDeviceController : Controller
    {
        private readonly QltbcnttContext _context;

        public StaffDeviceController(QltbcnttContext context)
        {
            _context = context;
        }

        // GET: Danh sách thiết bị
        public async Task<IActionResult> Index()
        {
            var devices = await _context.ThietBis
                .Include(t => t.MaDanhmucNavigation)
                .Include(t => t.MaNccNavigation)
                .Include(t => t.ChiTietPhieuMuons)
                    .ThenInclude(ct => ct.MaPhieumuonNavigation)
                .ToListAsync();

            return View(devices);
        }


        // GET: Form mượn thiết bị
        public IActionResult Borrow(int id)
        {
            var device = _context.ThietBis.FirstOrDefault(t => t.MaThietbi == id);
            if (device == null) return NotFound();

            ViewBag.MaThietbi = id;
            ViewBag.TenThietbi = device.TenThietbi;
            return View();
        }

        // POST: Xử lý mượn thiết bị
        [HttpPost]
        public async Task<IActionResult> Borrow(int maThietbi, DateTime ngayMuon, DateTime ngayTraDuKien, int soLuong)
        {
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int maNguoiDung))
            {
                return RedirectToAction("Login", "Auth");
            }

            var phieu = new PhieuMuon
            {
                NguoiMuon = maNguoiDung,
                NgayMuon = DateOnly.FromDateTime(ngayMuon),
                NgayTraDuKien = DateOnly.FromDateTime(ngayTraDuKien),
                TrangThai = "cho_duyet"
            };

            _context.PhieuMuons.Add(phieu);
            await _context.SaveChangesAsync();

            var chitiet = new ChiTietPhieuMuon
            {
                MaPhieumuon = phieu.MaPhieumuon,
                MaThietbi = maThietbi,
                SoLuong = 1
            };

            _context.ChiTietPhieuMuons.Add(chitiet);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Yêu cầu mượn thiết bị đã được gửi và đang chờ duyệt.";
            return RedirectToAction("Index");
        }
    }
}
