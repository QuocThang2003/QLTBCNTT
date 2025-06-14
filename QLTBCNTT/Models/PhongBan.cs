using System;
using System.Collections.Generic;

namespace QLTBCNTT.Models;

public partial class PhongBan
{
    public int MaPhongban { get; set; }

    public string TenPhongban { get; set; } = null!;

    public virtual ICollection<NguoiDung> NguoiDungs { get; set; } = new List<NguoiDung>();

    public virtual ICollection<PhieuXuat> PhieuXuats { get; set; } = new List<PhieuXuat>();
}
