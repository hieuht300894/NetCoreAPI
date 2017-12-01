using EntityModel.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Server.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Model
{
    public class zModel : DbContext
    {
        /*
         * 1. Add new DB
         * Add-Migration "Name not exist"
         * Enable-Migrations 
         * Update-Database –Verbose
         * -----------------------------------
         * 2. Update DB
         * Add-Migration "Name not exist"
         * Update-Database –Verbose
         */


        public zModel() : base()
        {
        }
        public zModel(DbContextOptions<zModel> options) : base(options)
        {

        }

        #region Cấu hình
        public virtual DbSet<eHienThi> eHienThi { get; set; }
        public virtual DbSet<eQuyDoiDonVi> eQuyDoiDonVi { get; set; }
        public virtual DbSet<eQuyDoiTienTe> eQuyDoiTienTe { get; set; }
        #endregion

        #region Hệ thống
        public virtual DbSet<xAccount> xAccount { get; set; }
        public virtual DbSet<xAgency> xAgency { get; set; }
        public virtual DbSet<xPersonnel> xPersonnel { get; set; }
        public virtual DbSet<xAppConfig> xAppConfig { get; set; }
        public virtual DbSet<xDisplay> xDisplay { get; set; }
        public virtual DbSet<xFeature> xFeature { get; set; }
        public virtual DbSet<xLayoutItemCaption> xLayoutItemCaption { get; set; }
        public virtual DbSet<xMsgDictionary> xMsgDictionary { get; set; }
        public virtual DbSet<xPermission> xPermission { get; set; }
        public virtual DbSet<xUserFeature> xUserFeature { get; set; }
        public virtual DbSet<xLog> xLog { get; set; }
        #endregion

        #region Danh mục
        public virtual DbSet<eDonViTinh> eDonViTinh { get; set; }
        public virtual DbSet<eKhachHang> eKhachHang { get; set; }
        public virtual DbSet<eKho> eKho { get; set; }
        public virtual DbSet<eNhaCungCap> eNhaCungCap { get; set; }
        public virtual DbSet<eNhomDonViTinh> eNhomDonViTinh { get; set; }
        public virtual DbSet<eNhomKhachHang> eNhomKhachHang { get; set; }
        public virtual DbSet<eNhomNhaCungCap> eNhomNhaCungCap { get; set; }
        public virtual DbSet<eNhomSanPham> eNhomSanPham { get; set; }
        public virtual DbSet<eSanPham> eSanPham { get; set; }
        public virtual DbSet<eTienTe> eTienTe { get; set; }
        public virtual DbSet<eTinhThanh> eTinhThanh { get; set; }
        #endregion

        #region Khai báo đầu kỳ
        public virtual DbSet<eTonKhoDauKy> eTonKhoDauKy { get; set; }
        public virtual DbSet<eSoDuDauKyKhachHang> eSoDuDauKyKhachHang { get; set; }
        public virtual DbSet<eSoDuDauKyNhaCungCap> eSoDuDauKyNhaCungCap { get; set; }
        #endregion

        #region Công nợ
        public virtual DbSet<eCongNoNhaCungCap> eCongNoNhaCungCap { get; set; }
        #endregion

        #region Chức năng
        public virtual DbSet<eNhapHangNhaCungCap> eNhapHangNhaCungCap { get; set; }
        public virtual DbSet<eNhapHangNhaCungCapChiTiet> eNhapHangNhaCungCapChiTiet { get; set; }
        public virtual DbSet<eTonKho> eTonKho { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Cấu hình
            modelBuilder.Entity<eHienThi>().HasKey(x => x.KeyID);
            modelBuilder.Entity<eQuyDoiDonVi>().HasKey(x => x.KeyID);
            modelBuilder.Entity<eQuyDoiTienTe>().HasKey(x => x.KeyID);
            #endregion

            #region Hệ thống
            modelBuilder.Entity<xAccount>().HasKey(x => x.KeyID);
            modelBuilder.Entity<xAccount>().Property(x => x.KeyID).ValueGeneratedNever();
            modelBuilder.Entity<xAgency>().HasKey(x => x.KeyID);
            modelBuilder.Entity<xPersonnel>().HasKey(x => x.KeyID);
            modelBuilder.Entity<xAppConfig>().HasKey(x => x.KeyID);
            modelBuilder.Entity<xDisplay>().HasKey(x => x.KeyID);
            modelBuilder.Entity<xFeature>().HasKey(x => x.KeyID);
            modelBuilder.Entity<xLayoutItemCaption>().HasKey(x => x.KeyID);
            modelBuilder.Entity<xMsgDictionary>().HasKey(x => x.KeyID);
            modelBuilder.Entity<xPermission>().HasKey(x => x.KeyID);
            modelBuilder.Entity<xUserFeature>().HasKey(x => x.KeyID);
            modelBuilder.Entity<xLog>().HasKey(x => x.KeyID);
            #endregion

            #region Danh mục
            modelBuilder.Entity<eDonViTinh>().HasKey(x => x.KeyID);
            modelBuilder.Entity<eKhachHang>().HasKey(x => x.KeyID);
            modelBuilder.Entity<eKho>().HasKey(x => x.KeyID);
            modelBuilder.Entity<eNhaCungCap>().HasKey(x => x.KeyID);
            modelBuilder.Entity<eNhomDonViTinh>().HasKey(x => x.KeyID);
            modelBuilder.Entity<eNhomKhachHang>().HasKey(x => x.KeyID);
            modelBuilder.Entity<eNhomNhaCungCap>().HasKey(x => x.KeyID);
            modelBuilder.Entity<eNhomSanPham>().HasKey(x => x.KeyID);
            modelBuilder.Entity<eSanPham>().HasKey(x => x.KeyID);
            modelBuilder.Entity<eTienTe>().HasKey(x => x.KeyID);
            modelBuilder.Entity<eTinhThanh>().HasKey(x => x.KeyID);
            #endregion

            #region Khai báo đầu kỳ
            modelBuilder.Entity<eTonKhoDauKy>().HasKey(x => x.KeyID);
            modelBuilder.Entity<eSoDuDauKyKhachHang>().HasKey(x => x.KeyID);
            modelBuilder.Entity<eSoDuDauKyNhaCungCap>().HasKey(x => x.KeyID);
            #endregion

            #region Công nợ
            modelBuilder.Entity<eCongNoNhaCungCap>().HasKey(x => x.KeyID);
            #endregion

            #region Chức năng
            modelBuilder.Entity<eNhapHangNhaCungCap>().HasKey(x => x.KeyID);
            modelBuilder.Entity<eNhapHangNhaCungCapChiTiet>().HasKey(x => x.KeyID);
            modelBuilder.Entity<eTonKho>().HasKey(x => x.KeyID);
            #endregion
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(ModuleHelper.ConnectionString);
        }
    }
}
