using System;
using System.Collections.Generic;

namespace QLTBCNTT.Models;

public partial class PhieuXuat
{
    public int MaPhieuxuat { get; set; }

    public DateOnly NgayXuat { get; set; }

    public int? MaPhongban { get; set; }

    public int? NguoiNhan { get; set; }

    public virtual ICollection<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; } = new List<ChiTietPhieuXuat>();

    public virtual PhongBan? MaPhongbanNavigation { get; set; }

    public virtual NguoiDung? NguoiNhanNavigation { get; set; }
}
