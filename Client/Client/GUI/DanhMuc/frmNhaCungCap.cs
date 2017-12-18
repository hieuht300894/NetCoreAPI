using Client.BLL.Common;
using Client.GUI.Common;
using Client.Module;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using EntityModel.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.GUI.DanhMuc
{
    public partial class frmNhaCungCap : frmBase
    {
        eNhaCungCap _iEntry;
        eNhaCungCap _aEntry;
        IList<eTinhThanh> lstTinhThanh = new List<eTinhThanh>();

        public frmNhaCungCap()
        {
            InitializeComponent();
        }
        protected override void frmBase_Load(object sender, EventArgs e)
        {
            base.frmBase_Load(sender, e);

            LoadRepository();
            LoadDataForm();
            CustomForm();
        }

        async void LoadRepository()
        {
            lstTinhThanh = await clsFunction.GetAll<eTinhThanh>("TinhThanh/DanhSach63TinhThanh");
            await RunMethodAsync(() => {/* slokTinhThanh.Properties.DataSource = lstTinhThanh;*/ });
        }
        public async override void LoadDataForm()
        {
            _iEntry = _iEntry ?? new eNhaCungCap();
            _aEntry = await clsFunction.GetByID<eNhaCungCap>("NhaCungCap", _iEntry.KeyID);

            SetControlValue();
        }
        public override void SetControlValue()
        {
            txtMa.DataBindings.Add("EditValue", _aEntry, "Ma", true, DataSourceUpdateMode.OnPropertyChanged);
            txtTen.DataBindings.Add("EditValue", _aEntry, "Ten", true, DataSourceUpdateMode.OnPropertyChanged);
            mmeGhiChu.DataBindings.Add("EditValue", _aEntry, "GhiChu", true, DataSourceUpdateMode.OnPropertyChanged);
            //txtDiaChi.DataBindings.Add("EditValue", _aEntry, "DiaChi", true, DataSourceUpdateMode.OnPropertyChanged);
            //txtSDT.DataBindings.Add("EditValue", _aEntry, "DienThoai", true, DataSourceUpdateMode.OnPropertyChanged);
            //txtNguoiLienHe.DataBindings.Add("EditValue", _aEntry, "NguoiLienHe", true, DataSourceUpdateMode.OnPropertyChanged);
            //lokTinhThanh.DataBindings.Add("EditValue", _aEntry, "IDTinhThanh", true, DataSourceUpdateMode.OnPropertyChanged);
            //slokTinhThanh.DataBindings.Add("Text", _aEntry, "TinhThanh", true, DataSourceUpdateMode.OnPropertyChanged);
        }
        public override bool ValidationForm()
        {
            return base.ValidationForm();
        }
        public async override Task<bool> SaveData()
        {
            if (_aEntry.KeyID > 0)
            {
                _aEntry.NguoiCapNhat = clsGeneral.curPersonnel.KeyID;
                _aEntry.MaNguoiCapNhat = clsGeneral.curPersonnel.Ma;
                _aEntry.TenNguoiCapNhat = clsGeneral.curPersonnel.Ten;
                _aEntry.NgayCapNhat = DateTime.Now.ServerNow();
                _aEntry.TrangThai = 2;
            }
            else
            {
                _aEntry.NguoiTao = clsGeneral.curPersonnel.KeyID;
                _aEntry.MaNguoiTao = clsGeneral.curPersonnel.Ma;
                _aEntry.TenNguoiTao = clsGeneral.curPersonnel.Ten;
                _aEntry.NgayTao = DateTime.Now.ServerNow();
                _aEntry.TrangThai = 1;
            }

            Tuple<bool, eNhaCungCap> Res = await (_aEntry.KeyID > 0 ?
                clsFunction.Put<eNhaCungCap, eNhaCungCap>("NhaCungCap", _aEntry) :
                clsFunction.Post<eNhaCungCap, eNhaCungCap>("NhaCungCap", _aEntry));

            if (Res.Item1)
                _ReloadData?.Invoke(Res.Item2.KeyID);
            return Res.Item1;
        }
        public override void CustomForm()
        {
            //lokTinhThanh.Properties.ValueMember = "KeyID";
            //lokTinhThanh.Properties.DisplayMember = "Ten";

            base.CustomForm();
        }
    }
}
