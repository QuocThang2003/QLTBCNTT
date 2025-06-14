using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLTBCNTT.Models;

public class ExportDetailController : Controller
{
    private readonly QltbcnttContext _context;

    public ExportDetailController(QltbcnttContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var exportDetails = _context.ChiTietPhieuXuats
            .Include(c => c.MaPhieuxuatNavigation)
            .Include(c => c.MaThietbiNavigation);
        return View(await exportDetails.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var detail = await _context.ChiTietPhieuXuats
            .Include(c => c.MaPhieuxuatNavigation)
            .Include(c => c.MaThietbiNavigation)
            .FirstOrDefaultAsync(m => m.MaChitietxuat == id);

        if (detail == null) return NotFound();

        return View(detail);
    }

    public IActionResult Create()
    {
        ViewData["MaPhieuxuat"] = new SelectList(_context.PhieuXuats, "MaPhieuxuat", "MaPhieuxuat");
        ViewData["MaThietbi"] = new SelectList(_context.ThietBis, "MaThietbi", "TenThietbi");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ChiTietPhieuXuat detail)
    {
        if (ModelState.IsValid)
        {
            _context.Add(detail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["MaPhieuxuat"] = new SelectList(_context.PhieuXuats, "MaPhieuxuat", "MaPhieuxuat", detail.MaPhieuxuat);
        ViewData["MaThietbi"] = new SelectList(_context.ThietBis, "MaThietbi", "TenThietbi", detail.MaThietbi);
        return View(detail);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var detail = await _context.ChiTietPhieuXuats.FindAsync(id);
        if (detail == null) return NotFound();

        ViewData["MaPhieuxuat"] = new SelectList(_context.PhieuXuats, "MaPhieuxuat", "MaPhieuxuat", detail.MaPhieuxuat);
        ViewData["MaThietbi"] = new SelectList(_context.ThietBis, "MaThietbi", "TenThietbi", detail.MaThietbi);
        return View(detail);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ChiTietPhieuXuat detail)
    {
        if (id != detail.MaChitietxuat) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(detail);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExportDetailExists(detail.MaChitietxuat)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }

        ViewData["MaPhieuxuat"] = new SelectList(_context.PhieuXuats, "MaPhieuxuat", "MaPhieuxuat", detail.MaPhieuxuat);
        ViewData["MaThietbi"] = new SelectList(_context.ThietBis, "MaThietbi", "TenThietbi", detail.MaThietbi);
        return View(detail);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var detail = await _context.ChiTietPhieuXuats
            .Include(c => c.MaPhieuxuatNavigation)
            .Include(c => c.MaThietbiNavigation)
            .FirstOrDefaultAsync(m => m.MaChitietxuat == id);

        if (detail == null) return NotFound();

        return View(detail);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var detail = await _context.ChiTietPhieuXuats.FindAsync(id);
        if (detail != null)
        {
            _context.ChiTietPhieuXuats.Remove(detail);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction(nameof(Index));
    }

    private bool ExportDetailExists(int id)
    {
        return _context.ChiTietPhieuXuats.Any(e => e.MaChitietxuat == id);
    }
}
