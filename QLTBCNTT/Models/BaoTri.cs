using System;
using System.Collections.Generic;

namespace QLTBCNTT.Models;

public partial class BaoTri
{
    public int MaBaotri { get; set; }

    public int MaThietbi { get; set; }

    public DateOnly NgayBaotri { get; set; }

    public DateOnly? NgayHoanthanh { get; set; }

    public string LoaiBaotri { get; set; } = null!;

    public string TrangThai { get; set; } = null!;

    public int? NguoiPhuTrach { get; set; }

    public virtual ThietBi MaThietbiNavigation { get; set; } = null!;

    public virtual NguoiDung? NguoiPhuTrachNavigation { get; set; }
}
