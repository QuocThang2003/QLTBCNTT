using System;
using System.Collections.Generic;

namespace QLTBCNTT.Models;

public partial class PhieuNhap
{
    public int MaPhieunhap { get; set; }

    public DateOnly NgayNhap { get; set; }

    public int? NguoiNhan { get; set; }

    public int? MaNcc { get; set; }

    public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; } = new List<ChiTietPhieuNhap>();

    public virtual NhaCungCap? MaNccNavigation { get; set; }

    public virtual NguoiDung? NguoiNhanNavigation { get; set; }
}
