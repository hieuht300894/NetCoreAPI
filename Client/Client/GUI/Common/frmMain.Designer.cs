namespace Client.GUI.Common
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.rcMain = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.bhiNhanVien = new DevExpress.XtraBars.BarHeaderItem();
            this.rpTaiKhoan = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpThietLap = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpDanhMuc = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rpChucNang = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup4 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.rsbBottom = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.docManager = new DevExpress.XtraBars.Docking2010.DocumentManager(this.components);
            this.tbvMain = new DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView(this.components);
            this.frmTinhThanh = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.rcMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.docManager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbvMain)).BeginInit();
            this.SuspendLayout();
            // 
            // rcMain
            // 
            this.rcMain.ExpandCollapseItem.Id = 0;
            this.rcMain.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.rcMain.ExpandCollapseItem,
            this.bhiNhanVien,
            this.frmTinhThanh});
            this.rcMain.Location = new System.Drawing.Point(0, 0);
            this.rcMain.MaxItemId = 12;
            this.rcMain.Name = "rcMain";
            this.rcMain.PageHeaderItemLinks.Add(this.bhiNhanVien);
            this.rcMain.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.rpTaiKhoan,
            this.rpThietLap,
            this.rpDanhMuc,
            this.rpChucNang});
            this.rcMain.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.rcMain.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.True;
            this.rcMain.ShowToolbarCustomizeItem = false;
            this.rcMain.Size = new System.Drawing.Size(884, 116);
            this.rcMain.StatusBar = this.rsbBottom;
            this.rcMain.Toolbar.ShowCustomizeItem = false;
            this.rcMain.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            // 
            // bhiNhanVien
            // 
            this.bhiNhanVien.Caption = "Nhân viên";
            this.bhiNhanVien.Id = 5;
            this.bhiNhanVien.Name = "bhiNhanVien";
            // 
            // rpTaiKhoan
            // 
            this.rpTaiKhoan.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup1});
            this.rpTaiKhoan.Name = "rpTaiKhoan";
            this.rpTaiKhoan.Text = "Tài khoản";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "Nhân viên";
            // 
            // rpThietLap
            // 
            this.rpThietLap.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup2});
            this.rpThietLap.Name = "rpThietLap";
            this.rpThietLap.Text = "Thiết lập";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "ribbonPageGroup2";
            // 
            // rpDanhMuc
            // 
            this.rpDanhMuc.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup3});
            this.rpDanhMuc.Name = "rpDanhMuc";
            this.rpDanhMuc.Text = "Danh mục";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.frmTinhThanh);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.Text = "ribbonPageGroup3";
            // 
            // rpChucNang
            // 
            this.rpChucNang.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup4});
            this.rpChucNang.Name = "rpChucNang";
            this.rpChucNang.Text = "Chức năng";
            // 
            // ribbonPageGroup4
            // 
            this.ribbonPageGroup4.Name = "ribbonPageGroup4";
            this.ribbonPageGroup4.Text = "ribbonPageGroup4";
            // 
            // rsbBottom
            // 
            this.rsbBottom.Location = new System.Drawing.Point(0, 384);
            this.rsbBottom.Name = "rsbBottom";
            this.rsbBottom.Ribbon = this.rcMain;
            this.rsbBottom.Size = new System.Drawing.Size(884, 27);
            // 
            // docManager
            // 
            this.docManager.MdiParent = this;
            this.docManager.MenuManager = this.rcMain;
            this.docManager.View = this.tbvMain;
            this.docManager.ViewCollection.AddRange(new DevExpress.XtraBars.Docking2010.Views.BaseView[] {
            this.tbvMain});
            // 
            // tbvMain
            // 
            this.tbvMain.RootContainer.Element = null;
            // 
            // frmTinhThanh
            // 
            this.frmTinhThanh.Caption = "Tỉnh thành";
            this.frmTinhThanh.Id = 11;
            this.frmTinhThanh.Name = "frmTinhThanh";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 411);
            this.Controls.Add(this.rsbBottom);
            this.Controls.Add(this.rcMain);
            this.IsMdiContainer = true;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmMain";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.rcMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.docManager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbvMain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl rcMain;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpTaiKhoan;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarHeaderItem bhiNhanVien;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpThietLap;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpDanhMuc;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.Ribbon.RibbonPage rpChucNang;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup4;
        private DevExpress.XtraBars.Docking2010.DocumentManager docManager;
        private DevExpress.XtraBars.Docking2010.Views.Tabbed.TabbedView tbvMain;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar rsbBottom;
        private DevExpress.XtraBars.BarButtonItem frmTinhThanh;
    }
}