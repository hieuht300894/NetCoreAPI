using Client.BLL.Common;
using Client.GUI.Common;
using Client.Module;
using DevExpress.XtraGrid.Views.Grid;
using EntityModel.DataModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.GUI.DanhMuc
{
    public partial class frmKho : frmBase
    {
        BindingList<eKho> lstEntries = new BindingList<eKho>();
        BindingList<eKho> lstEdited = new BindingList<eKho>();

        public frmKho()
        {
            InitializeComponent();
        }
        protected override void frmBase_Load(object sender, EventArgs e)
        {
            base.frmBase_Load(sender, e);
            LoadData(0);
            CustomForm();
        }

        public async override void LoadData(object KeyID)
        {
            lstEdited = new BindingList<eKho>();
            lstEntries = new BindingList<eKho>(await clsFunction.GetAll<eKho>("kho"));
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
            bool chk = false;
            chk = await clsFunction.Post("kho", lstEdited.ToList());
            return chk;
        }
        public override void CustomForm()
        {
            base.CustomForm();

            gctDanhSach.MouseClick += gctDanhSach_MouseClick;
            grvDanhSach.RowUpdated += grvDanhSach_RowUpdated;
            //grvDanhSach.InitNewRow += grvDanhSach_InitNewRow;
        }
        public async override void DeleteEntry()
        {
            IList<eKho> lstDeleted = await grvDanhSach.GetItems<eKho>();
            await clsFunction.Delete("kho", lstDeleted.ToList());
        }

        private void grvDanhSach_InitNewRow(object sender, InitNewRowEventArgs e)
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
            if (!lstEdited.Any(x => x.KeyID == ((eKho)e.Row).KeyID)) lstEdited.Add((eKho)e.Row);
        }
    }
}
