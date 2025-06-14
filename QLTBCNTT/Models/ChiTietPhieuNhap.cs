using System;
using System.Collections.Generic;

namespace QLTBCNTT.Models;

public partial class ChiTietPhieuNhap
{
    public int MaChitietnhap { get; set; }

    public int? MaPhieunhap { get; set; }

    public int? MaThietbi { get; set; }

    public int? SoLuong { get; set; }

    public virtual PhieuNhap? MaPhieunhapNavigation { get; set; }

    public virtual ThietBi? MaThietbiNavigation { get; set; }
}
