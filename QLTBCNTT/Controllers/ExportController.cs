using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLTBCNTT.Models;

public class ExportController : Controller
{
    private readonly QltbcnttContext _context;

    public ExportController(QltbcnttContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var exports = _context.PhieuXuats
            .Include(p => p.MaPhongbanNavigation)
            .Include(p => p.NguoiNhanNavigation);
        return View(await exports.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var export = await _context.PhieuXuats
            .Include(p => p.MaPhongbanNavigation)
            .Include(p => p.NguoiNhanNavigation)
            .FirstOrDefaultAsync(m => m.MaPhieuxuat == id);

        if (export == null) return NotFound();

        return View(export);
    }

    public IActionResult Create()
    {
        ViewData["MaPhongban"] = new SelectList(_context.PhongBans, "MaPhongban", "TenPhongban");
        ViewData["NguoiNhan"] = new SelectList(_context.NguoiDungs, "MaNguoidung", "HoTen");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PhieuXuat export)
    {
        if (ModelState.IsValid)
        {
            _context.Add(export);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["MaPhongban"] = new SelectList(_context.PhongBans, "MaPhongban", "TenPhongban", export.MaPhongban);
        ViewData["NguoiNhan"] = new SelectList(_context.NguoiDungs, "MaNguoidung", "HoTen", export.NguoiNhan);
        return View(export);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var export = await _context.PhieuXuats.FindAsync(id);
        if (export == null) return NotFound();

        ViewData["MaPhongban"] = new SelectList(_context.PhongBans, "MaPhongban", "TenPhongban", export.MaPhongban);
        ViewData["NguoiNhan"] = new SelectList(_context.NguoiDungs, "MaNguoidung", "HoTen", export.NguoiNhan);
        return View(export);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, PhieuXuat export)
    {
        if (id != export.MaPhieuxuat) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(export);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExportExists(export.MaPhieuxuat)) return NotFound();
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["MaPhongban"] = new SelectList(_context.PhongBans, "MaPhongban", "TenPhongban", export.MaPhongban);
        ViewData["NguoiNhan"] = new SelectList(_context.NguoiDungs, "MaNguoidung", "HoTen", export.NguoiNhan);
        return View(export);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var export = await _context.PhieuXuats
            .Include(p => p.MaPhongbanNavigation)
            .Include(p => p.NguoiNhanNavigation)
            .FirstOrDefaultAsync(m => m.MaPhieuxuat == id);

        if (export == null) return NotFound();

        return View(export);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var export = await _context.PhieuXuats.FindAsync(id);
        if (export != null)
        {
            _context.PhieuXuats.Remove(export);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private bool ExportExists(int id)
    {
        return _context.PhieuXuats.Any(e => e.MaPhieuxuat == id);
    }
}
