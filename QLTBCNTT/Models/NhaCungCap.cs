using System;
using System.Collections.Generic;

namespace QLTBCNTT.Models;

public partial class NhaCungCap
{
    public int MaNcc { get; set; }

    public string TenNcc { get; set; } = null!;

    public string? Sdt { get; set; }

    public virtual ICollection<PhieuNhap> PhieuNhaps { get; set; } = new List<PhieuNhap>();

    public virtual ICollection<ThietBi> ThietBis { get; set; } = new List<ThietBi>();
}
