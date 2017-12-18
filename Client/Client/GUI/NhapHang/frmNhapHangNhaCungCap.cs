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

namespace Client.GUI.NhapHang
{
    public partial class frmNhapHangNhaCungCap : frmBase
    {
        IList<eSanPham> lstSanPham = new List<eSanPham>();
        IList<eKho> lstKho = new List<eKho>();
        IList<eNhomSanPham> lstNhomSanPham = new List<eNhomSanPham>();
        eNhapHangNhaCungCap _iEntry;
        eNhapHangNhaCungCap _aEntry;
        BindingList<eNhapHangNhaCungCapChiTiet> lstDetail = new BindingList<eNhapHangNhaCungCapChiTiet>();

        public frmNhapHangNhaCungCap()
        {
            InitializeComponent();
        }
        protected override void frmBase_Load(object sender, EventArgs e)
        {
            base.frmBase_Load(sender, e);

            LoadRepository();
            LoadNhaCungCap(0);
            LoadDataForm();
            CustomForm();
        }
        protected override void frmBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            base.frmBase_FormClosing(sender, e);
            _ReloadData?.Invoke(0);
        }

        async void LoadRepository()
        {
            lstNhomSanPham = await clsFunction.GetAll<eNhomSanPham>("NhomSanPham");
            lstSanPham = await clsFunction.GetAll<eSanPham>("SanPham");
            lstKho = await clsFunction.GetAll<eKho>("Kho");

            await RunMethodAsync(() =>
            {
                dteNgayNhap.DateTime = DateTime.Now.ServerNow();
                slokNhomSanPham.Properties.DataSource = lstNhomSanPham;
                rlokSanPham.DataSource = lstSanPham;
                rlokKho.DataSource = lstKho;
                gctSanPham.DataSource = lstSanPham;

                var qSanPham = lstSanPham.Select(x => new { x.Ma, x.Ten });
                foreach (var rSanPham in qSanPham)
                {
                    srcMaSanPham.Properties.Items.Add(rSanPham.Ma);
                    srcTenSanPham.Properties.Items.Add(rSanPham.Ten);
                }
            });
        }
        async void LoadNhaCungCap(object KeyID)
        {
            IList<eNhaCungCap> lstNhaCungCap = await clsFunction.GetAll<eNhaCungCap>("NhaCungCap");
            await RunMethodAsync(() =>
            {
                slokNhaCungCap.Properties.DataSource = lstNhaCungCap;
                slokNhaCungCap.EditValue = KeyID;
            });
        }
        public async override void LoadDataForm()
        {
            lstDetail = new BindingList<eNhapHangNhaCungCapChiTiet>();

            _iEntry = _iEntry ?? new eNhapHangNhaCungCap();
            _aEntry = await clsFunction.GetByID<eNhapHangNhaCungCap>("NhapHangNhaCungCap", _iEntry.KeyID);
            SetControlValue();
            SetDataSource();
        }
        public override void SetControlValue()
        {
            if (_aEntry.KeyID > 0)
            {
                slokNhaCungCap.LockButton();
            }
            else
            {
                _aEntry.NgayNhap = dteNgayNhap.DateTime;
                slokNhaCungCap.UnlockButton();
            }

            ResetControlValue();

            slokNhaCungCap.DataBindings.Add("EditValue", _aEntry, "IDNhaCungCap", true, DataSourceUpdateMode.OnPropertyChanged);
            txtMaPhieu.DataBindings.Add("EditValue", _aEntry, "Ma", true, DataSourceUpdateMode.OnPropertyChanged);
            txtSoLoHang.DataBindings.Add("EditValue", _aEntry, "MaLoHang", true, DataSourceUpdateMode.OnPropertyChanged);
            dteNgayNhap.DataBindings.Add("DateTime", _aEntry, "NgayNhap", true, DataSourceUpdateMode.OnPropertyChanged);
            mmeGhiChu.DataBindings.Add("EditValue", _aEntry, "GhiChu", true, DataSourceUpdateMode.OnPropertyChanged);
            spnSoTien.DataBindings.Add("Value", _aEntry, "TongTien", true, DataSourceUpdateMode.OnPropertyChanged);
            spnSoLuong.DataBindings.Add("Value", _aEntry, "SoLuong", true, DataSourceUpdateMode.OnPropertyChanged);
        }
        public override void SetDataSource()
        {
            lstDetail = new BindingList<eNhapHangNhaCungCapChiTiet>(_aEntry.eNhapHangNhaCungCapChiTiet.ToList());
            gctChiTiet.DataSource = lstDetail;
        }
        public override bool ValidationForm()
        {
            return base.ValidationForm();
        }
        public override Task<bool> SaveData()
        {
            return base.SaveData();
        }
        public override void CustomForm()
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

            slokNhomSanPham.PreviewKeyDown -= LokNhomSanPham_PreviewKeyDown;
            srcMaSanPham.PreviewKeyDown -= SrcMaSanPham_PreviewKeyDown;
            srcTenSanPham.PreviewKeyDown -= SrcTenSanPham_PreviewKeyDown;
            grvChiTiet.CellValueChanged -= GrvChiTiet_CellValueChanged;
            rbtnXoa.ButtonClick -= RbtnXoa_ButtonClick;
            spnThanhToan.EditValueChanged -= SpnThanhToan_EditValueChanged;
            grvSanPham.DoubleClick -= GrvSanPham_DoubleClick;

            slokNhomSanPham.PreviewKeyDown += LokNhomSanPham_PreviewKeyDown;
            srcMaSanPham.PreviewKeyDown += SrcMaSanPham_PreviewKeyDown;
            srcTenSanPham.PreviewKeyDown += SrcTenSanPham_PreviewKeyDown;
            grvChiTiet.CellValueChanged += GrvChiTiet_CellValueChanged;
            rbtnXoa.ButtonClick += RbtnXoa_ButtonClick;
            spnThanhToan.EditValueChanged += SpnThanhToan_EditValueChanged;
            grvSanPham.DoubleClick += GrvSanPham_DoubleClick;

            frmNhaCungCap frm = new frmNhaCungCap();
            frm.fType = Module.QuanLyBanHang.eFormType.Add;
            frm.Text = "Thêm mới nhà cung cấp";
            frm._ReloadData = LoadNhaCungCap;
            slokNhaCungCap.AddNewItem(frm);
        }

        void TimKiemSanPham()
        {
            List<eSanPham> lstTimKiem = new List<eSanPham>(lstSanPham);

            if (!string.IsNullOrEmpty(srcMaSanPham.Text.Trim()))
                lstTimKiem = lstTimKiem.Where(x => x.Ma.ToLower().Contains(srcMaSanPham.Text.Trim().ToLower())).ToList();

            if (!string.IsNullOrEmpty(srcTenSanPham.Text.Trim()))
                lstTimKiem = lstTimKiem.Where(x => x.Ten.ToLower().Contains(srcTenSanPham.Text.Trim().ToLower())).ToList();

            gctSanPham.DataSource = lstTimKiem;
        }
        void CapNhatSoTien()
        {
            spnSoLuong.Value = spnSoTien.Value = 0;
            spnTongNo.Value = spnNoCu.Value;

            _aEntry.SoLuong = _aEntry.ThanhTien = _aEntry.TongTien = 0;

            foreach (eNhapHangNhaCungCapChiTiet item in lstDetail)
            {
                spnSoLuong.Value += item.SoLuong;
                spnSoTien.Value += item.TongTien;
                spnTongNo.Value += item.TongTien;
            }
            spnConLai.Value = spnTongNo.Value - spnThanhToan.Value;
        }

        private void SpnThanhToan_EditValueChanged(object sender, EventArgs e)
        {
            spnConLai.Value = spnTongNo.Value - spnThanhToan.Value;
        }
        private void RbtnXoa_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            eNhapHangNhaCungCapChiTiet item = (eNhapHangNhaCungCapChiTiet)grvChiTiet.GetFocusedRow();
            if (item != null)
            {
                lstDetail.Remove(item);
                CapNhatSoTien();
            }
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
                item.TongTien = item.ThanhTien * ((100 - item.ChietKhau) / 100);
                CapNhatSoTien();
            }
            if (e.Column.FieldName.Equals("SoLuong") )
            {
                item.ThanhTien = item.SoLuong * item.DonGia;
                item.TongTien = item.ThanhTien * ((100 - item.ChietKhau) / 100);
                CapNhatSoTien();
            }
            if (e.Column.FieldName.Equals("DonGia"))
            {
                item.ThanhTien = item.SoLuong * item.DonGia;
                item.TongTien = item.ThanhTien * ((100 - item.ChietKhau) / 100);
                CapNhatSoTien();
            }
            if (e.Column.FieldName.Equals("ThanhTien"))
            {
                item.TongTien = item.ThanhTien * ((100 - item.ChietKhau) / 100);
                CapNhatSoTien();
            }
            if (e.Column.FieldName.Equals("ChietKhau"))
            {
                item.TongTien = item.ThanhTien * ((100 - item.ChietKhau) / 100);
                CapNhatSoTien();
            }

            view.UpdateCurrentRow();
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
            GridView view = (GridView)sender;
            eSanPham sp = (eSanPham)view.GetFocusedRow();
            if (sp != null)
            {
                int index = lstDetail.FindIndex(x => x.IDSanPham == sp.KeyID);
                if (index != -1)
                {
                    grvChiTiet.FocusedRowHandle = index;
                }
                else
                {
                    eNhapHangNhaCungCapChiTiet iDT = new eNhapHangNhaCungCapChiTiet();
                    iDT.IDSanPham = sp.KeyID;
                    iDT.MaSanPham = sp.Ma;
                    iDT.TenSanPham = sp.Ten;
                    iDT.IDDonViTinh = sp.IDDonViTinh;
                    iDT.MaDonViTinh = sp.MaDonViTinh;
                    iDT.TenDonViTinh = sp.TenDonViTinh;
                    iDT.SoLuongLe = 1;
                    iDT.SoLuong = 1;
                    lstDetail.Add(iDT);
                }
            }
        }
    }
}
