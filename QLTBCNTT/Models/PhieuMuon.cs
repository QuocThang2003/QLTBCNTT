using System;
using System.Collections.Generic;

namespace QLTBCNTT.Models;

public partial class PhieuMuon
{
    public int MaPhieumuon { get; set; }

    public int NguoiMuon { get; set; }

    public int? NguoiDuyet { get; set; }

    public DateOnly NgayMuon { get; set; }

    public DateOnly NgayTraDuKien { get; set; }

    public DateOnly? NgayTra { get; set; }

    public string TrangThai { get; set; } = null!;

    public virtual ICollection<ChiTietPhieuMuon> ChiTietPhieuMuons { get; set; } = new List<ChiTietPhieuMuon>();

    public virtual NguoiDung? NguoiDuyetNavigation { get; set; }

    public virtual NguoiDung NguoiMuonNavigation { get; set; } = null!;
}
