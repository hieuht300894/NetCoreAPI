using Client.BLL.Common;
using Client.GUI.Common;
using Client.GUI.DanhMuc;
using Client.Module;
using DevExpress.XtraGrid.Views.Grid;
using EntityModel.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : frmBase
    {
        public Form1()
        {
            InitializeComponent();
        }
        protected override void frmBase_Load(object sender, EventArgs e)
        {
            base.frmBase_Load(sender, e);

            LoadNhaCungCap(0);
            LoadRepository();
            LoadDataForm();
            CustomForm();
        }
        protected override void frmBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.frmBase_FormClosing(sender, e);
            _ReloadData?.Invoke(0);
        }

        void LoadRepository()
        {
        }
        void LoadNhaCungCap(object KeyID)
        {
        }
        public override void LoadDataForm()
        {
            DisableEvents();
            SetDataSource();
            SetControlValue();
            EnableEvents();
        }
        public override void SetControlValue()
        {
        }
        public override void SetDataSource()
        {

        }
        public override bool ValidationForm()
        {
            return base.ValidationForm();
        }
        public override bool SaveData()
        {
            return base.SaveData();
        }
        public async override void CustomForm()
        {
            await Task.Run(() =>
            {
                Invoke(new Action(() =>
                {
                    slokNhaCungCap.Properties.ValueMember = "KeyID";
                    slokNhaCungCap.Properties.DisplayMember = "Ten";
                    slokNhomSanPham.Properties.ValueMember = "KeyID";
                    slokNhomSanPham.Properties.DisplayMember = "Ten";
                    rlokKho.ValueMember = "KeyID";
                    rlokKho.DisplayMember = "Ten";
                    rlokSanPham.ValueMember = "KeyID";
                    rlokSanPham.DisplayMember = "Ten";

                    base.CustomForm();

                    grvChiTiet.OptionsView.ColumnAutoWidth = false;
                    grvChiTiet.ShowFooter(
                        grvChiTiet.Columns["SoLuongSi"], grvChiTiet.Columns["SoLuongLe"], grvChiTiet.Columns["SoLuong"],
                        grvChiTiet.Columns["ThanhTien"], grvChiTiet.Columns["TongTien"]);

                    frmNhaCungCap frm = new frmNhaCungCap();
                    frm.fType = Module.QuanLyBanHang.eFormType.Add;
                    frm.Text = "Thêm mới nhà cung cấp";
                    frm._ReloadData = LoadNhaCungCap;
                    slokNhaCungCap.AddNewItem(frm);
                }));
            });
        }
        public override void EnableEvents()
        {
            slokNhomSanPham.PreviewKeyDown += LokNhomSanPham_PreviewKeyDown;
            srcMaSanPham.PreviewKeyDown += SrcMaSanPham_PreviewKeyDown;
            srcTenSanPham.PreviewKeyDown += SrcTenSanPham_PreviewKeyDown;
            grvChiTiet.CellValueChanged += GrvChiTiet_CellValueChanged;
            rbtnXoa.ButtonClick += RbtnXoa_ButtonClick;
            spnThanhToan.EditValueChanged += SpnThanhToan_EditValueChanged;
            grvSanPham.DoubleClick += GrvSanPham_DoubleClick;
            slokNhaCungCap.EditValueChanged += SlokNhaCungCap_EditValueChanged;
            dteNgayNhap.EditValueChanged += DteNgayNhap_EditValueChanged;
        }
        public override void DisableEvents()
        {
            slokNhomSanPham.PreviewKeyDown -= LokNhomSanPham_PreviewKeyDown;
            srcMaSanPham.PreviewKeyDown -= SrcMaSanPham_PreviewKeyDown;
            srcTenSanPham.PreviewKeyDown -= SrcTenSanPham_PreviewKeyDown;
            grvChiTiet.CellValueChanged -= GrvChiTiet_CellValueChanged;
            rbtnXoa.ButtonClick -= RbtnXoa_ButtonClick;
            spnThanhToan.EditValueChanged -= SpnThanhToan_EditValueChanged;
            grvSanPham.DoubleClick -= GrvSanPham_DoubleClick;
            slokNhaCungCap.EditValueChanged -= SlokNhaCungCap_EditValueChanged;
            dteNgayNhap.EditValueChanged -= DteNgayNhap_EditValueChanged;
        }
        void TimKiemSanPham()
        {

        }
        void CapNhatSoTien()
        {

        }
        void CongNoHienTai()
        {

        }

        private void SpnThanhToan_EditValueChanged(object sender, EventArgs e)
        {
            spnConLai.Value = spnTongNo.Value - spnThanhToan.Value;
        }
        private void RbtnXoa_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {

        }
        private void GrvChiTiet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            GridView view = (GridView)sender;
            eNhapHangNhaCungCapChiTiet item = (eNhapHangNhaCungCapChiTiet)view.GetFocusedRow();
            if (item == null) return;

            view.CellValueChanged -= GrvChiTiet_CellValueChanged;
            if (e.Column.FieldName.Equals("SoLuongSi") || e.Column.FieldName.Equals("SoLuongLe"))
            {
                item.SoLuong = item.SoLuongSi + item.SoLuongLe;
                item.ThanhTien = item.SoLuong * item.DonGia;
                item.TienChietKhau = item.ThanhTien * (item.ChietKhau / 100);
                item.TongTien = item.ThanhTien - item.TienChietKhau;
                //item.TongTien = item.ThanhTien * ((100 - item.ChietKhau) / 100);
                CapNhatSoTien();
            }
            if (e.Column.FieldName.Equals("DonGia"))
            {
                item.ThanhTien = item.SoLuong * item.DonGia;
                item.TienChietKhau = item.ThanhTien * (item.ChietKhau / 100);
                item.TongTien = item.ThanhTien - item.TienChietKhau;
                //item.TongTien = item.ThanhTien * ((100 - item.ChietKhau) / 100);
                CapNhatSoTien();
            }
            if (e.Column.FieldName.Equals("ChietKhau"))
            {
                item.TienChietKhau = item.ThanhTien * (item.ChietKhau / 100);
                item.TongTien = item.ThanhTien - item.TienChietKhau;
                //item.TongTien = item.ThanhTien * ((100 - item.ChietKhau) / 100);
                CapNhatSoTien();
            }

            view.CellValueChanged += GrvChiTiet_CellValueChanged;
        }
        private void SrcTenSanPham_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab) { TimKiemSanPham(); }
        }
        private void SrcMaSanPham_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab) { TimKiemSanPham(); }
        }
        private void LokNhomSanPham_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab) { TimKiemSanPham(); }
        }
        private void GrvSanPham_DoubleClick(object sender, EventArgs e)
        {

        }
        private void DteNgayNhap_EditValueChanged(object sender, EventArgs e)
        {
            CongNoHienTai();
        }
        private void SlokNhaCungCap_EditValueChanged(object sender, EventArgs e)
        {
            CongNoHienTai();
        }
    }
}
