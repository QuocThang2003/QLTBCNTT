using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLTBCNTT.Models;

namespace QLTBCNTT.Controllers
{
    public class MaintenanceController : Controller
    {
        private readonly QltbcnttContext _context;

        public MaintenanceController(QltbcnttContext context)
        {
            _context = context;
        }

        // GET: Maintenance
        public async Task<IActionResult> Index()
        {
            var maintenances = _context.BaoTris
                .Include(m => m.MaThietbiNavigation)
                .Include(m => m.NguoiPhuTrachNavigation);
            return View(await maintenances.ToListAsync());
        }

        // GET: Maintenance/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var maintenance = await _context.BaoTris
                .Include(m => m.MaThietbiNavigation)
                .Include(m => m.NguoiPhuTrachNavigation)
                .FirstOrDefaultAsync(m => m.MaBaotri == id);

            if (maintenance == null) return NotFound();

            return View(maintenance);
        }

        // GET: Maintenance/Create
        public IActionResult Create()
        {
            ViewData["DeviceId"] = new SelectList(_context.ThietBis, "MaThietbi", "TenThietbi");
            ViewData["TechnicianId"] = new SelectList(_context.NguoiDungs, "MaNguoidung", "HoTen");
            return View();
        }

        // POST: Maintenance/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaThietbi,NgayBaotri,NgayHoanthanh,LoaiBaotri,TrangThai,NguoiPhuTrach")] BaoTri maintenance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(maintenance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeviceId"] = new SelectList(_context.ThietBis, "MaThietbi", "TenThietbi", maintenance.MaThietbi);
            ViewData["TechnicianId"] = new SelectList(_context.NguoiDungs, "MaNguoidung", "HoTen", maintenance.NguoiPhuTrach);
            return View(maintenance);
        }

        // GET: Maintenance/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var maintenance = await _context.BaoTris.FindAsync(id);
            if (maintenance == null) return NotFound();

            ViewData["DeviceId"] = new SelectList(_context.ThietBis, "MaThietbi", "TenThietbi", maintenance.MaThietbi);
            ViewData["TechnicianId"] = new SelectList(_context.NguoiDungs, "MaNguoidung", "HoTen", maintenance.NguoiPhuTrach);
            return View(maintenance);
        }

        // POST: Maintenance/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaBaotri,MaThietbi,NgayBaotri,NgayHoanthanh,LoaiBaotri,TrangThai,NguoiPhuTrach")] BaoTri maintenance)
        {
            if (id != maintenance.MaBaotri) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maintenance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaintenanceExists(maintenance.MaBaotri)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["DeviceId"] = new SelectList(_context.ThietBis, "MaThietbi", "TenThietbi", maintenance.MaThietbi);
            ViewData["TechnicianId"] = new SelectList(_context.NguoiDungs, "MaNguoidung", "HoTen", maintenance.NguoiPhuTrach);
            return View(maintenance);
        }

        // GET: Maintenance/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var maintenance = await _context.BaoTris
                .Include(m => m.MaThietbiNavigation)
                .Include(m => m.NguoiPhuTrachNavigation)
                .FirstOrDefaultAsync(m => m.MaBaotri == id);

            if (maintenance == null) return NotFound();

            return View(maintenance);
        }

        // POST: Maintenance/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var maintenance = await _context.BaoTris.FindAsync(id);
            if (maintenance != null)
            {
                _context.BaoTris.Remove(maintenance);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool MaintenanceExists(int id)
        {
            return _context.BaoTris.Any(e => e.MaBaotri == id);
        }
    }
}
