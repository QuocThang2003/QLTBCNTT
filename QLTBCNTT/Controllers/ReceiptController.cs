using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLTBCNTT.Models;

namespace QLTBCNTT.Controllers
{
    public class ReceiptController : Controller
    {
        private readonly QltbcnttContext _context;

        public ReceiptController(QltbcnttContext context)
        {
            _context = context;
        }

        // GET: Receipt
        public async Task<IActionResult> Index()
        {
            var receipts = _context.PhieuNhaps
                .Include(p => p.NguoiNhanNavigation)
                .Include(p => p.MaNccNavigation);
            return View(await receipts.ToListAsync());
        }

        // GET: Receipt/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PhieuNhaps == null)
                return NotFound();

            var receipt = await _context.PhieuNhaps
                .Include(p => p.NguoiNhanNavigation)
                .Include(p => p.MaNccNavigation)
                .FirstOrDefaultAsync(m => m.MaPhieunhap == id);

            if (receipt == null)
                return NotFound();

            return View(receipt);
        }

        // GET: Receipt/Create
        public IActionResult Create()
        {
            ViewData["NguoiNhan"] = new SelectList(_context.NguoiDungs, "MaNguoidung", "HoTen");
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "TenNcc");
            return View();
        }

        // POST: Receipt/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NgayNhap,NguoiNhan,MaNcc")] PhieuNhap phieuNhap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phieuNhap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["NguoiNhan"] = new SelectList(_context.NguoiDungs, "MaNguoidung", "HoTen", phieuNhap.NguoiNhan);
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "TenNcc", phieuNhap.MaNcc);
            return View(phieuNhap);
        }

        // GET: Receipt/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PhieuNhaps == null)
                return NotFound();

            var receipt = await _context.PhieuNhaps.FindAsync(id);
            if (receipt == null)
                return NotFound();

            ViewData["NguoiNhan"] = new SelectList(_context.NguoiDungs, "MaNguoidung", "HoTen", receipt.NguoiNhan);
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "TenNcc", receipt.MaNcc);
            return View(receipt);
        }

        // POST: Receipt/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaPhieunhap,NgayNhap,NguoiNhan,MaNcc")] PhieuNhap phieuNhap)
        {
            if (id != phieuNhap.MaPhieunhap)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phieuNhap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhieuNhapExists(phieuNhap.MaPhieunhap))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["NguoiNhan"] = new SelectList(_context.NguoiDungs, "MaNguoidung", "HoTen", phieuNhap.NguoiNhan);
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "TenNcc", phieuNhap.MaNcc);
            return View(phieuNhap);
        }

        // GET: Receipt/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PhieuNhaps == null)
                return NotFound();

            var receipt = await _context.PhieuNhaps
                .Include(p => p.NguoiNhanNavigation)
                .Include(p => p.MaNccNavigation)
                .FirstOrDefaultAsync(m => m.MaPhieunhap == id);

            if (receipt == null)
                return NotFound();

            return View(receipt);
        }

        // POST: Receipt/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var receipt = await _context.PhieuNhaps.FindAsync(id);
            if (receipt != null)
            {
                _context.PhieuNhaps.Remove(receipt);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool PhieuNhapExists(int id)
        {
            return _context.PhieuNhaps.Any(e => e.MaPhieunhap == id);
        }
    }
}
