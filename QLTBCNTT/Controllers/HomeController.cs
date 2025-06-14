using Microsoft.AspNetCore.Mvc;
using QLTBCNTT.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.Json;

namespace QLTBCNTT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly QltbcnttContext _context;

        public HomeController(ILogger<HomeController> logger, QltbcnttContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var totalDevices = _context.ThietBis.Count();
            var deviceStatusCounts = new
            {
                ConHang = _context.ThietBis.Count(t => t.TrangThai == "con_hang"),
                DangSuDung = _context.ThietBis.Count(t => t.TrangThai == "dang_su_dung"),
                DangMuon = _context.ThietBis.Count(t => t.TrangThai == "dang_muon"),
                DangBaoTri = _context.ThietBis.Count(t => t.TrangThai == "dang_baotri"),
                DaThanhLy = _context.ThietBis.Count(t => t.TrangThai == "da_thanhly")
        
            };
            var deviceTypeCounts = _context.DanhMucs
                .Select(d => new
                {
                    TenDanhmuc = d.TenDanhmuc,
                    Count = _context.ThietBis.Count(t => t.MaDanhmuc == d.MaDanhmuc)
                })
                .ToList();

            ViewBag.TotalDevices = totalDevices;
            ViewBag.DeviceStatusCounts = deviceStatusCounts;
            ViewBag.DeviceTypeCounts = JsonSerializer.Serialize(deviceTypeCounts); 

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}