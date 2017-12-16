﻿using Client.BLL.Common;
using Client.GUI.Common;
using DevExpress.XtraGrid.Views.Grid;
using EntityModel.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.GUI.DauKy
{
    public partial class frmSoDuDauKyNhaCungCap : frmBase
    {
        BindingList<eSoDuDauKyNhaCungCap> lstEntries = new BindingList<eSoDuDauKyNhaCungCap>();
        BindingList<eSoDuDauKyNhaCungCap> lstEdited = new BindingList<eSoDuDauKyNhaCungCap>();

        public frmSoDuDauKyNhaCungCap()
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

        public async void LoadRepository()
        {
            IList<eNhaCungCap> lstNhaCungCap = await clsFunction.GetAll<eNhaCungCap>("");
            await RunMethodAsync(() => { rlokNhaCungCap.DataSource = lstNhaCungCap; });
        }
        public async override void LoadDataForm()
        {
            lstEdited = new BindingList<eSoDuDauKyNhaCungCap>();
            lstEntries = new BindingList<eSoDuDauKyNhaCungCap>(await clsFunction.GetAll<eSoDuDauKyNhaCungCap>("sodudaukynhacungcap"));
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
                eNhaCungCap NhaCungCap = (eNhaCungCap)rlokNhaCungCap.GetDataSourceRowByKeyValue(x.IDNhaCungCap) ?? new eNhaCungCap();
                x.MaNhaCungCap = NhaCungCap.Ma;
                x.TenNhaCungCap = NhaCungCap.Ten;
            });

            Tuple<bool, List<eSoDuDauKyNhaCungCap>> Res = await clsFunction.Post<eSoDuDauKyNhaCungCap, eSoDuDauKyNhaCungCap>("sodudaukynhacungcap", lstEdited.ToList());
            return Res.Item1;
        }
        public override void CustomForm()
        {
            rlokNhaCungCap.ValueMember = "KeyID";
            rlokNhaCungCap.DisplayMember = "Ten";

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
            if (!lstEdited.Any(x => x.KeyID == ((eSoDuDauKyNhaCungCap)e.Row).KeyID)) lstEdited.Add((eSoDuDauKyNhaCungCap)e.Row);
        }
    }
}
