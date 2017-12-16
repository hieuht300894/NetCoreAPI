using Client.BLL.Common;
using Client.GUI.Common;
using Client.Module;
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
        BindingList<eNhaCungCap> lstEntries = new BindingList<eNhaCungCap>();
        BindingList<eNhaCungCap> lstEdited = new BindingList<eNhaCungCap>();

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
            IList<eTinhThanh> lstTinhThanh = new List<eTinhThanh>(await clsFunction.GetAll<eTinhThanh>("tinhthanh"));
            await RunMethodAsync(() => { rlokTinhThanh.DataSource = lstTinhThanh; });
        }
        public async override void LoadDataForm()
        {
            lstEdited = new BindingList<eNhaCungCap>();
            lstEntries = new BindingList<eNhaCungCap>(await clsFunction.GetAll<eNhaCungCap>("nhacungcap"));
            await RunMethodAsync(() => { gctDanhSach.DataSource = lstEntries; });
        }
        public override bool ValidationForm()
        {
            grvDanhSach.CloseEditor();
            grvDanhSach.UpdateCurrentRow();
            return base.ValidationForm();
        }
        public async override Task<bool> SaveData()
        {
            DateTime time = DateTime.Now.ServerNow();

            lstEdited.ToList().ForEach(x =>
            {
                eTinhThanh tinhThanh = (eTinhThanh)rlokTinhThanh.GetDataSourceRowByKeyValue(x.IDTinhThanh) ?? new eTinhThanh();
                x.TinhThanh = tinhThanh.Ten;

                if (x.KeyID > 0)
                {
                    x.NguoiCapNhat = clsGeneral.curPersonnel.KeyID;
                    x.MaNguoiCapNhat = clsGeneral.curPersonnel.Ma;
                    x.TenNguoiCapNhat = clsGeneral.curPersonnel.Ten;
                    x.NgayCapNhat = time;
                }
                else
                {
                    x.NguoiTao = clsGeneral.curPersonnel.KeyID;
                    x.MaNguoiTao = clsGeneral.curPersonnel.Ma;
                    x.TenNguoiTao = clsGeneral.curPersonnel.Ten;
                    x.NgayTao = time;
                }
            });

            Tuple<bool, List<eNhaCungCap>> Res = await clsFunction.Post<eNhaCungCap, eNhaCungCap>("nhacungcap", lstEdited.ToList());
            return Res.Item1;
        }
        public override void CustomForm()
        {
            rlokTinhThanh.ValueMember = "KeyID";
            rlokTinhThanh.DisplayMember = "Ten";

            base.CustomForm();

            gctDanhSach.MouseClick += gctDanhSach_MouseClick;
            grvDanhSach.RowUpdated += grvDanhSach_RowUpdated;
        }

        private void gctDanhSach_MouseClick(object sender, MouseEventArgs e)
        {
            ShowGridPopup(sender, e, true, false, true, true, true, true);
        }
        private void grvDanhSach_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            if (!lstEdited.Any(x => x.KeyID == ((eNhaCungCap)e.Row).KeyID)) lstEdited.Add((eNhaCungCap)e.Row);
        }
    }
}
