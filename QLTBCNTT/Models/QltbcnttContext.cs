using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QLTBCNTT.Models;

public partial class QltbcnttContext : DbContext
{
    public QltbcnttContext()
    {
    }

    public QltbcnttContext(DbContextOptions<QltbcnttContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BaoTri> BaoTris { get; set; }

    public virtual DbSet<ChiTietPhieuMuon> ChiTietPhieuMuons { get; set; }

    public virtual DbSet<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; }

    public virtual DbSet<ChiTietPhieuXuat> ChiTietPhieuXuats { get; set; }

    public virtual DbSet<DanhMuc> DanhMucs { get; set; }

    public virtual DbSet<NguoiDung> NguoiDungs { get; set; }

    public virtual DbSet<NhaCungCap> NhaCungCaps { get; set; }

    public virtual DbSet<PhieuMuon> PhieuMuons { get; set; }

    public virtual DbSet<PhieuNhap> PhieuNhaps { get; set; }

    public virtual DbSet<PhieuXuat> PhieuXuats { get; set; }

    public virtual DbSet<PhongBan> PhongBans { get; set; }

    public virtual DbSet<ThietBi> ThietBis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-4Q0P439\\MSSQLSERVER01;Initial Catalog=QLTBCNTT;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BaoTri>(entity =>
        {
            entity.HasKey(e => e.MaBaotri).HasName("PK__BaoTri__71BAECF074DACC30");

            entity.ToTable("BaoTri");

            entity.Property(e => e.MaBaotri).HasColumnName("ma_baotri");
            entity.Property(e => e.LoaiBaotri)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("loai_baotri");
            entity.Property(e => e.MaThietbi).HasColumnName("ma_thietbi");
            entity.Property(e => e.NgayBaotri).HasColumnName("ngay_baotri");
            entity.Property(e => e.NgayHoanthanh).HasColumnName("ngay_hoanthanh");
            entity.Property(e => e.NguoiPhuTrach).HasColumnName("nguoi_phu_trach");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("da_len_ke_hoach")
                .HasColumnName("trang_thai");

            entity.HasOne(d => d.MaThietbiNavigation).WithMany(p => p.BaoTris)
                .HasForeignKey(d => d.MaThietbi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BaoTri__ma_thiet__693CA210");

            entity.HasOne(d => d.NguoiPhuTrachNavigation).WithMany(p => p.BaoTris)
                .HasForeignKey(d => d.NguoiPhuTrach)
                .HasConstraintName("FK__BaoTri__nguoi_ph__6A30C649");
        });

        modelBuilder.Entity<ChiTietPhieuMuon>(entity =>
        {
            entity.HasKey(e => e.MaChitietmuon).HasName("PK__ChiTietP__003784C759017AB8");

            entity.ToTable("ChiTietPhieuMuon");

            entity.Property(e => e.MaChitietmuon).HasColumnName("ma_chitietmuon");
            entity.Property(e => e.MaPhieumuon).HasColumnName("ma_phieumuon");
            entity.Property(e => e.MaThietbi).HasColumnName("ma_thietbi");
            entity.Property(e => e.SoLuong)
                .HasDefaultValue(1)
                .HasColumnName("so_luong");

            entity.HasOne(d => d.MaPhieumuonNavigation).WithMany(p => p.ChiTietPhieuMuons)
                .HasForeignKey(d => d.MaPhieumuon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietPh__ma_ph__619B8048");

            entity.HasOne(d => d.MaThietbiNavigation).WithMany(p => p.ChiTietPhieuMuons)
                .HasForeignKey(d => d.MaThietbi)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChiTietPh__ma_th__628FA481");
        });

        modelBuilder.Entity<ChiTietPhieuNhap>(entity =>
        {
            entity.HasKey(e => e.MaChitietnhap).HasName("PK__ChiTietP__52F43C0EC4B4B3BA");

            entity.ToTable("ChiTietPhieuNhap");

            entity.Property(e => e.MaChitietnhap).HasColumnName("ma_chitietnhap");
            entity.Property(e => e.MaPhieunhap).HasColumnName("ma_phieunhap");
            entity.Property(e => e.MaThietbi).HasColumnName("ma_thietbi");
            entity.Property(e => e.SoLuong)
                .HasDefaultValue(1)
                .HasColumnName("so_luong");

            entity.HasOne(d => d.MaPhieunhapNavigation).WithMany(p => p.ChiTietPhieuNhaps)
                .HasForeignKey(d => d.MaPhieunhap)
                .HasConstraintName("FK__ChiTietPh__ma_ph__4E88ABD4");

            entity.HasOne(d => d.MaThietbiNavigation).WithMany(p => p.ChiTietPhieuNhaps)
                .HasForeignKey(d => d.MaThietbi)
                .HasConstraintName("FK__ChiTietPh__ma_th__4F7CD00D");
        });

        modelBuilder.Entity<ChiTietPhieuXuat>(entity =>
        {
            entity.HasKey(e => e.MaChitietxuat).HasName("PK__ChiTietP__7ABE9F9DE996CCB9");

            entity.ToTable("ChiTietPhieuXuat");

            entity.Property(e => e.MaChitietxuat).HasColumnName("ma_chitietxuat");
            entity.Property(e => e.MaPhieuxuat).HasColumnName("ma_phieuxuat");
            entity.Property(e => e.MaThietbi).HasColumnName("ma_thietbi");
            entity.Property(e => e.SoLuong)
                .HasDefaultValue(1)
                .HasColumnName("so_luong");

            entity.HasOne(d => d.MaPhieuxuatNavigation).WithMany(p => p.ChiTietPhieuXuats)
                .HasForeignKey(d => d.MaPhieuxuat)
                .HasConstraintName("FK__ChiTietPh__ma_ph__571DF1D5");

            entity.HasOne(d => d.MaThietbiNavigation).WithMany(p => p.ChiTietPhieuXuats)
                .HasForeignKey(d => d.MaThietbi)
                .HasConstraintName("FK__ChiTietPh__ma_th__5812160E");
        });

        modelBuilder.Entity<DanhMuc>(entity =>
        {
            entity.HasKey(e => e.MaDanhmuc).HasName("PK__DanhMuc__498F58413BFE2CE3");

            entity.ToTable("DanhMuc");

            entity.Property(e => e.MaDanhmuc).HasColumnName("ma_danhmuc");
            entity.Property(e => e.TenDanhmuc)
                .HasMaxLength(100)
                .HasColumnName("ten_danhmuc");
        });

        modelBuilder.Entity<NguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaNguoidung).HasName("PK__NguoiDun__48823C2E269D1C6E");

            entity.ToTable("NguoiDung");

            entity.HasIndex(e => e.Email, "UQ__NguoiDun__AB6E6164A988638B").IsUnique();

            entity.Property(e => e.MaNguoidung).HasColumnName("ma_nguoidung");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.HoTen)
                .HasMaxLength(100)
                .HasColumnName("ho_ten");
            entity.Property(e => e.MaPhongban).HasColumnName("ma_phongban");
            entity.Property(e => e.MatKhau)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("mat_khau");
            entity.Property(e => e.SoDienthoai)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("so_dienthoai");
            entity.Property(e => e.VaiTro)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("nhanvien")
                .HasColumnName("vai_tro");

            entity.HasOne(d => d.MaPhongbanNavigation).WithMany(p => p.NguoiDungs)
                .HasForeignKey(d => d.MaPhongban)
                .HasConstraintName("FK__NguoiDung__ma_ph__3C69FB99");
        });

        modelBuilder.Entity<NhaCungCap>(entity =>
        {
            entity.HasKey(e => e.MaNcc).HasName("PK__NhaCungC__04FFEEB9F6BFF4FF");

            entity.ToTable("NhaCungCap");

            entity.Property(e => e.MaNcc).HasColumnName("ma_ncc");
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("sdt");
            entity.Property(e => e.TenNcc)
                .HasMaxLength(100)
                .HasColumnName("ten_ncc");
        });

        modelBuilder.Entity<PhieuMuon>(entity =>
        {
            entity.HasKey(e => e.MaPhieumuon).HasName("PK__PhieuMuo__57BB09F9356571CF");

            entity.ToTable("PhieuMuon");

            entity.Property(e => e.MaPhieumuon).HasColumnName("ma_phieumuon");
            entity.Property(e => e.NgayMuon).HasColumnName("ngay_muon");
            entity.Property(e => e.NgayTra).HasColumnName("ngay_tra");
            entity.Property(e => e.NgayTraDuKien).HasColumnName("ngay_tra_du_kien");
            entity.Property(e => e.NguoiDuyet).HasColumnName("nguoi_duyet");
            entity.Property(e => e.NguoiMuon).HasColumnName("nguoi_muon");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("cho_duyet")
                .HasColumnName("trang_thai");

            entity.HasOne(d => d.NguoiDuyetNavigation).WithMany(p => p.PhieuMuonNguoiDuyetNavigations)
                .HasForeignKey(d => d.NguoiDuyet)
                .HasConstraintName("FK__PhieuMuon__nguoi__5DCAEF64");

            entity.HasOne(d => d.NguoiMuonNavigation).WithMany(p => p.PhieuMuonNguoiMuonNavigations)
                .HasForeignKey(d => d.NguoiMuon)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PhieuMuon__nguoi__5CD6CB2B");
        });

        modelBuilder.Entity<PhieuNhap>(entity =>
        {
            entity.HasKey(e => e.MaPhieunhap).HasName("PK__PhieuNha__B3BEEFE977818995");

            entity.ToTable("PhieuNhap");

            entity.Property(e => e.MaPhieunhap).HasColumnName("ma_phieunhap");
            entity.Property(e => e.MaNcc).HasColumnName("ma_ncc");
            entity.Property(e => e.NgayNhap).HasColumnName("ngay_nhap");
            entity.Property(e => e.NguoiNhan).HasColumnName("nguoi_nhan");

            entity.HasOne(d => d.MaNccNavigation).WithMany(p => p.PhieuNhaps)
                .HasForeignKey(d => d.MaNcc)
                .HasConstraintName("FK__PhieuNhap__ma_nc__4AB81AF0");

            entity.HasOne(d => d.NguoiNhanNavigation).WithMany(p => p.PhieuNhaps)
                .HasForeignKey(d => d.NguoiNhan)
                .HasConstraintName("FK__PhieuNhap__nguoi__49C3F6B7");
        });

        modelBuilder.Entity<PhieuXuat>(entity =>
        {
            entity.HasKey(e => e.MaPhieuxuat).HasName("PK__PhieuXua__36EE59B4BAC0BB96");

            entity.ToTable("PhieuXuat");

            entity.Property(e => e.MaPhieuxuat).HasColumnName("ma_phieuxuat");
            entity.Property(e => e.MaPhongban).HasColumnName("ma_phongban");
            entity.Property(e => e.NgayXuat).HasColumnName("ngay_xuat");
            entity.Property(e => e.NguoiNhan).HasColumnName("nguoi_nhan");

            entity.HasOne(d => d.MaPhongbanNavigation).WithMany(p => p.PhieuXuats)
                .HasForeignKey(d => d.MaPhongban)
                .HasConstraintName("FK__PhieuXuat__ma_ph__52593CB8");

            entity.HasOne(d => d.NguoiNhanNavigation).WithMany(p => p.PhieuXuats)
                .HasForeignKey(d => d.NguoiNhan)
                .HasConstraintName("FK__PhieuXuat__nguoi__534D60F1");
        });

        modelBuilder.Entity<PhongBan>(entity =>
        {
            entity.HasKey(e => e.MaPhongban).HasName("PK__PhongBan__4418CABEE5F418E3");

            entity.ToTable("PhongBan");

            entity.Property(e => e.MaPhongban).HasColumnName("ma_phongban");
            entity.Property(e => e.TenPhongban)
                .HasMaxLength(100)
                .HasColumnName("ten_phongban");
        });

        modelBuilder.Entity<ThietBi>(entity =>
        {
            entity.HasKey(e => e.MaThietbi).HasName("PK__ThietBi__9B953F6012202AC0");

            entity.ToTable("ThietBi");

            entity.HasIndex(e => e.SoSerial, "UQ__ThietBi__27AAA9B2A05B4E3A").IsUnique();

            entity.Property(e => e.MaThietbi).HasColumnName("ma_thietbi");
            entity.Property(e => e.GiaMua)
                .HasColumnType("decimal(12, 2)")
                .HasColumnName("gia_mua");
            entity.Property(e => e.MaDanhmuc).HasColumnName("ma_danhmuc");
            entity.Property(e => e.MaNcc).HasColumnName("ma_ncc");
            entity.Property(e => e.NgayMua).HasColumnName("ngay_mua");
            entity.Property(e => e.SoSerial)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("so_serial");
            entity.Property(e => e.TenThietbi)
                .HasMaxLength(100)
                .HasColumnName("ten_thietbi");
            entity.Property(e => e.ThoiGianBaohanh).HasColumnName("thoi_gian_baohanh");
            entity.Property(e => e.TrangThai)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("con_hang")
                .HasColumnName("trang_thai");

            entity.HasOne(d => d.MaDanhmucNavigation).WithMany(p => p.ThietBis)
                .HasForeignKey(d => d.MaDanhmuc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ThietBi__ma_danh__45F365D3");

            entity.HasOne(d => d.MaNccNavigation).WithMany(p => p.ThietBis)
                .HasForeignKey(d => d.MaNcc)
                .HasConstraintName("FK__ThietBi__ma_ncc__46E78A0C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
