using Client.BLL.Common;
using Client.GUI.Common;
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
            LoadData(0);
            CustomForm();
        }

        async void LoadRepository()
        {
            IList<eTinhThanh> lstTinhThanh = new List<eTinhThanh>(await clsFunction.GetAll<eTinhThanh>(""));
            await RunMethodAsync(() => { rlokTinhThanh.DataSource = lstTinhThanh; });
        }
        public async override void LoadData(object KeyID)
        {
            lstEdited = new BindingList<eNhaCungCap>();
            lstEntries = new BindingList<eNhaCungCap>(await clsFunction.GetAll<eNhaCungCap>(""));
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
            lstEdited.ToList().ForEach(x =>
            {
                eTinhThanh tinhThanh = (eTinhThanh)rlokTinhThanh.GetDataSourceRowByKeyValue(x.IDTinhThanh) ?? new eTinhThanh();

                x.TinhThanh = tinhThanh.Ten;
            });

            bool chk = false;
            await RunMethodAsync(() => { return false; });
            return chk;
        }
        public override void CustomForm()
        {
            rlokTinhThanh.ValueMember = "KeyID";
            rlokTinhThanh.DisplayMember = "Ten";

            base.CustomForm();

            gctDanhSach.MouseClick += gctDanhSach_MouseClick;
            grvDanhSach.RowUpdated += grvDanhSach_RowUpdated;
            grvDanhSach.InitNewRow += grvDanhSach_InitNewRow;
        }

        private void grvDanhSach_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            GridView view = (GridView)sender;
            view.SetRowCellValue(e.RowHandle, colKeyID, -lstEdited.Count);
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
