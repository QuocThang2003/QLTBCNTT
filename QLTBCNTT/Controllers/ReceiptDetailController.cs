using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLTBCNTT.Models;

namespace QLTBCNTT.Controllers
{
    public class ReceiptDetailController : Controller
    {
        private readonly QltbcnttContext _context;

        public ReceiptDetailController(QltbcnttContext context)
        {
            _context = context;
        }

        // GET: ReceiptDetail
        public async Task<IActionResult> Index()
        {
            var data = _context.ChiTietPhieuNhaps
                .Include(c => c.MaPhieunhapNavigation)
                .Include(c => c.MaThietbiNavigation);
            return View(await data.ToListAsync());
        }

        // GET: ReceiptDetail/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var detail = await _context.ChiTietPhieuNhaps
                .Include(c => c.MaPhieunhapNavigation)
                .Include(c => c.MaThietbiNavigation)
                .FirstOrDefaultAsync(m => m.MaChitietnhap == id);

            if (detail == null) return NotFound();

            return View(detail);
        }

        // GET: ReceiptDetail/Create
        public IActionResult Create()
        {
            ViewData["MaPhieunhap"] = new SelectList(_context.PhieuNhaps, "MaPhieunhap", "MaPhieunhap");
            ViewData["MaThietbi"] = new SelectList(_context.ThietBis, "MaThietbi", "TenThietbi");
            return View();
        }

        // POST: ReceiptDetail/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaChitietnhap,MaPhieunhap,MaThietbi,SoLuong")] ChiTietPhieuNhap chiTietPhieuNhap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chiTietPhieuNhap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaPhieunhap"] = new SelectList(_context.PhieuNhaps, "MaPhieunhap", "MaPhieunhap", chiTietPhieuNhap.MaPhieunhap);
            ViewData["MaThietbi"] = new SelectList(_context.ThietBis, "MaThietbi", "TenThietbi", chiTietPhieuNhap.MaThietbi);
            return View(chiTietPhieuNhap);
        }

        // GET: ReceiptDetail/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var item = await _context.ChiTietPhieuNhaps.FindAsync(id);
            if (item == null) return NotFound();

            ViewData["MaPhieunhap"] = new SelectList(_context.PhieuNhaps, "MaPhieunhap", "MaPhieunhap", item.MaPhieunhap);
            ViewData["MaThietbi"] = new SelectList(_context.ThietBis, "MaThietbi", "TenThietbi", item.MaThietbi);
            return View(item);
        }

        // POST: ReceiptDetail/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaChitietnhap,MaPhieunhap,MaThietbi,SoLuong")] ChiTietPhieuNhap item)
        {
            if (id != item.MaChitietnhap) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceiptDetailExists(item.MaChitietnhap)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["MaPhieunhap"] = new SelectList(_context.PhieuNhaps, "MaPhieunhap", "MaPhieunhap", item.MaPhieunhap);
            ViewData["MaThietbi"] = new SelectList(_context.ThietBis, "MaThietbi", "TenThietbi", item.MaThietbi);
            return View(item);
        }

        // GET: ReceiptDetail/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var item = await _context.ChiTietPhieuNhaps
                .Include(c => c.MaPhieunhapNavigation)
                .Include(c => c.MaThietbiNavigation)
                .FirstOrDefaultAsync(m => m.MaChitietnhap == id);

            if (item == null) return NotFound();

            return View(item);
        }

        // POST: ReceiptDetail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.ChiTietPhieuNhaps.FindAsync(id);
            if (item != null)
            {
                _context.ChiTietPhieuNhaps.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ReceiptDetailExists(int id)
        {
            return _context.ChiTietPhieuNhaps.Any(e => e.MaChitietnhap == id);
        }
    }
}
