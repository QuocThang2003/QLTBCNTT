using System;
using System.Collections.Generic;

namespace QLTBCNTT.Models;

public partial class ChiTietPhieuXuat
{
    public int MaChitietxuat { get; set; }

    public int? MaPhieuxuat { get; set; }

    public int? MaThietbi { get; set; }

    public int? SoLuong { get; set; }

    public virtual PhieuXuat? MaPhieuxuatNavigation { get; set; }

    public virtual ThietBi? MaThietbiNavigation { get; set; }
}
