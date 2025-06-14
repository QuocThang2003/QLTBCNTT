using System;
using System.Collections.Generic;

namespace QLTBCNTT.Models;

public partial class ChiTietPhieuMuon
{
    public int MaChitietmuon { get; set; }

    public int MaPhieumuon { get; set; }

    public int MaThietbi { get; set; }

    public int SoLuong { get; set; }

    public virtual PhieuMuon MaPhieumuonNavigation { get; set; } = null!;

    public virtual ThietBi MaThietbiNavigation { get; set; } = null!;
}
