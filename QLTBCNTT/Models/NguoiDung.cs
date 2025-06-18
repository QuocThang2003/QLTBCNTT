using System;
using System.Collections.Generic;

namespace QLTBCNTT.Models;

public partial class NguoiDung
{
    public int MaNguoidung { get; set; }

    public string HoTen { get; set; } = null!;

    public string? Email { get; set; }

    public string? SoDienthoai { get; set; }

    public string MatKhau { get; set; } = null!;

    public int? MaPhongban { get; set; }

    public string VaiTro { get; set; } = null!;
    public string? ResetToken { get; set; }
  public DateTime? ResetTokenExpiry { get; set; }

    public virtual ICollection<BaoTri> BaoTris { get; set; } = new List<BaoTri>();

    public virtual PhongBan? MaPhongbanNavigation { get; set; }

    public virtual ICollection<PhieuMuon> PhieuMuonNguoiDuyetNavigations { get; set; } = new List<PhieuMuon>();

    public virtual ICollection<PhieuMuon> PhieuMuonNguoiMuonNavigations { get; set; } = new List<PhieuMuon>();

    public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; } = new List<PhieuNhap>();

    public virtual ICollection<PhieuXuat> PhieuXuats { get; set; } = new List<PhieuXuat>();
}
