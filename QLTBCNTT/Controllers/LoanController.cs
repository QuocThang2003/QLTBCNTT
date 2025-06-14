using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLTBCNTT.Models;

namespace QLTBCNTT.Controllers
{
    public class LoanController : Controller
    {
        private readonly QltbcnttContext _context;

        public LoanController(QltbcnttContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Cập nhật trạng thái "trễ hạn"
            var phieuQuahan = await _context.PhieuMuons
                .Where(p => p.TrangThai != "da_tra")
                .Where(p => p.NgayTraDuKien < DateOnly.FromDateTime(DateTime.Now) && p.NgayTra == null)
                .ToListAsync();

            foreach (var phieu in phieuQuahan)
            {
                phieu.TrangThai = "tre_han";
            }
            await _context.SaveChangesAsync();

            // Lấy tất cả phiếu mượn, không lọc trạng thái
            var danhSach = await _context.PhieuMuons
                .Include(p => p.NguoiMuonNavigation)
                .Include(p => p.ChiTietPhieuMuons)
                    .ThenInclude(ct => ct.MaThietbiNavigation)
                .ToListAsync();

            return View(danhSach);
        }

        // Duyệt phiếu mượn
        [HttpPost]
        public async Task<IActionResult> Approve(int id)
        {
            var phieu = await _context.PhieuMuons
                .Include(p => p.ChiTietPhieuMuons)
                .FirstOrDefaultAsync(p => p.MaPhieumuon == id);

            if (phieu == null) return NotFound();

            // Lấy ID của người duyệt (admin) từ session
            var userIdStr = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdStr) || !int.TryParse(userIdStr, out int maNguoiDuyet))
            {
                return RedirectToAction("Login", "Auth");
            }

            // Cập nhật trạng thái và người duyệt
            phieu.TrangThai = "da_duyet";
            phieu.NguoiDuyet = maNguoiDuyet;

            // Cập nhật trạng thái thiết bị: "dang_muon"
            foreach (var ct in phieu.ChiTietPhieuMuons)
            {
                var thietBi = await _context.ThietBis.FindAsync(ct.MaThietbi);
                if (thietBi != null)
                {
                    thietBi.TrangThai = "dang_muon";
                }
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Phiếu mượn đã được duyệt.";
            return RedirectToAction("Index");
        }

        // Từ chối phiếu mượn
        [HttpPost]
        public async Task<IActionResult> Reject(int id)
        {
            var phieu = await _context.PhieuMuons.FindAsync(id);
            if (phieu == null) return NotFound();

            _context.PhieuMuons.Remove(phieu);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Phiếu mượn đã bị từ chối và xóa.";
            return RedirectToAction("Index");
        }

        // Đánh dấu phiếu là đã trả
        [HttpPost]
        public async Task<IActionResult> MarkReturned(int id)
        {
            var phieu = await _context.PhieuMuons
                .Include(p => p.ChiTietPhieuMuons)
                    .ThenInclude(ct => ct.MaThietbiNavigation)
                .FirstOrDefaultAsync(p => p.MaPhieumuon == id);

            if (phieu == null)
                return NotFound();

            phieu.TrangThai = "da_tra";
            phieu.NgayTra = DateOnly.FromDateTime(DateTime.Now);

            foreach (var ct in phieu.ChiTietPhieuMuons)
            {
                ct.MaThietbiNavigation.TrangThai = "con_hang";
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Phiếu mượn đã được cập nhật là ĐÃ TRẢ.";

            return RedirectToAction("Index");
        }
    }
}
