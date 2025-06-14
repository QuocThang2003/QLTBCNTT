using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLTBCNTT.Models;

namespace QLTBCNTT.Controllers.Admin
{
    public class DepartmentController : Controller
    {
        private readonly QltbcnttContext _context;

        public DepartmentController(QltbcnttContext context)
        {
            _context = context;
        }

        // List all
        public async Task<IActionResult> Index()
        {
            return View(await _context.PhongBans.ToListAsync());
        }

        // Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var department = await _context.PhongBans.FirstOrDefaultAsync(m => m.MaPhongban == id);
            if (department == null) return NotFound();

            return View(department);
        }

        // Show create form
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TenPhongban")] PhongBan department)
        {
            if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // Show edit form
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var department = await _context.PhongBans.FindAsync(id);
            if (department == null) return NotFound();

            return View(department);
        }

        // Handle edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaPhongban,TenPhongban")] PhongBan department)
        {
            if (id != department.MaPhongban) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.PhongBans.Any(e => e.MaPhongban == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // Show delete confirmation
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var department = await _context.PhongBans.FirstOrDefaultAsync(m => m.MaPhongban == id);
            if (department == null) return NotFound();

            return View(department);
        }

        // Handle delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var department = await _context.PhongBans.FindAsync(id);
            if (department != null)
            {
                _context.PhongBans.Remove(department);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
