using System;
using System.Collections.Generic;

namespace QLTBCNTT.Models;

public partial class DanhMuc
{
    public int MaDanhmuc { get; set; }

    public string TenDanhmuc { get; set; } = null!;

    public virtual ICollection<ThietBi> ThietBis { get; set; } = new List<ThietBi>();
}
