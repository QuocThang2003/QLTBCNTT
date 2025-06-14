using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLTBCNTT.Models;

namespace QLTBCNTT.Controllers
{
    public class DeviceController : Controller
    {
        private readonly QltbcnttContext _context;

        public DeviceController(QltbcnttContext context)
        {
            _context = context;
        }

        // GET: Device
        public async Task<IActionResult> Index()
        {
            var devices = _context.ThietBis
                .Include(d => d.MaDanhmucNavigation)
                .Include(d => d.MaNccNavigation);
            return View(await devices.ToListAsync());
        }

        // GET: Device/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var device = await _context.ThietBis
                .Include(d => d.MaDanhmucNavigation)
                .Include(d => d.MaNccNavigation)
                .FirstOrDefaultAsync(m => m.MaThietbi == id);

            if (device == null) return NotFound();

            return View(device);
        }

        // GET: Device/Create
        public IActionResult Create()
        {
            ViewData["MaDanhmuc"] = new SelectList(_context.DanhMucs, "MaDanhmuc", "TenDanhmuc");
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "TenNcc");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenThietbi,SoSerial,MaDanhmuc,MaNcc,NgayMua,GiaMua,ThoiGianBaohanh,TrangThai")] ThietBi thietBi)
        {
            ModelState.Remove("MaDanhmucNavigation");
            ModelState.Remove("MaNccNavigation");

            if (ModelState.IsValid)
            {
                _context.Add(thietBi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaDanhmuc"] = new SelectList(_context.DanhMucs, "MaDanhmuc", "TenDanhmuc", thietBi.MaDanhmuc);
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "TenNcc", thietBi.MaNcc);
            return View(thietBi);
        }
        // GET: Device/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var thietBi = await _context.ThietBis.FindAsync(id);
            if (thietBi == null) return NotFound();

            ViewData["MaDanhmuc"] = new SelectList(_context.DanhMucs, "MaDanhmuc", "TenDanhmuc", thietBi.MaDanhmuc);
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "TenNcc", thietBi.MaNcc);
            return View(thietBi);
        }

        // POST: Device/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaThietbi,TenThietbi,SoSerial,MaDanhmuc,MaNcc,NgayMua,GiaMua,ThoiGianBaohanh,TrangThai")] ThietBi thietBi)
        {
            if (id != thietBi.MaThietbi) return NotFound();

            ModelState.Remove("MaDanhmucNavigation");
            ModelState.Remove("MaNccNavigation");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thietBi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThietBiExists(thietBi.MaThietbi)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["MaDanhmuc"] = new SelectList(_context.DanhMucs, "MaDanhmuc", "TenDanhmuc", thietBi.MaDanhmuc);
            ViewData["MaNcc"] = new SelectList(_context.NhaCungCaps, "MaNcc", "TenNcc", thietBi.MaNcc);
            return View(thietBi);
        }


        // GET: Device/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var device = await _context.ThietBis
                .Include(d => d.MaDanhmucNavigation)
                .Include(d => d.MaNccNavigation)
                .FirstOrDefaultAsync(m => m.MaThietbi == id);

            if (device == null) return NotFound();

            return View(device);
        }

        // POST: Device/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var device = await _context.ThietBis.FindAsync(id);
            if (device != null)
            {
                _context.ThietBis.Remove(device);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ThietBiExists(int id)
        {
            return _context.ThietBis.Any(e => e.MaThietbi == id);
        }
    }
}
