using System;
using System.Collections.Generic;

namespace QLTBCNTT.Models;

public partial class ThietBi
{
    public int MaThietbi { get; set; }

    public string TenThietbi { get; set; } = null!;

    public string? SoSerial { get; set; }

    public int MaDanhmuc { get; set; }

    public int? MaNcc { get; set; }

    public DateOnly? NgayMua { get; set; }

    public decimal? GiaMua { get; set; }

    public int? ThoiGianBaohanh { get; set; }

    public string TrangThai { get; set; } = null!;

    public virtual ICollection<BaoTri> BaoTris { get; set; } = new List<BaoTri>();

    public virtual ICollection<ChiTietPhieuMuon> ChiTietPhieuMuons { get; set; } = new List<ChiTietPhieuMuon>();

    public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; } = new List<ChiTietPhieuNhap>();

    public virtual ICollection<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; } = new List<ChiTietPhieuXuat>();

    public virtual DanhMuc MaDanhmucNavigation { get; set; } = null!;

    public virtual NhaCungCap? MaNccNavigation { get; set; }
}
