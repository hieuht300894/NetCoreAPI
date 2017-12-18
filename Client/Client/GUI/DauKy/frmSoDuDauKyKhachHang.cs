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
    public partial class frmSoDuDauKyKhachHang : frmBase
    {
        BindingList<eSoDuDauKyKhachHang> lstEntries = new BindingList<eSoDuDauKyKhachHang>();
        BindingList<eSoDuDauKyKhachHang> lstEdited = new BindingList<eSoDuDauKyKhachHang>();

        public frmSoDuDauKyKhachHang()
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
            IList<eKhachHang> lstKhachHang = await clsFunction.GetAll<eKhachHang>("khachhang");
            await RunMethodAsync(() => { rlokKhachHang.DataSource = lstKhachHang; });
        }
        public async override void LoadDataForm()
        {
            lstEdited = new BindingList<eSoDuDauKyKhachHang>();
            lstEntries = new BindingList<eSoDuDauKyKhachHang>(await clsFunction.GetAll<eSoDuDauKyKhachHang>("sodudaukykhachhang"));
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
                eKhachHang khachHang = (eKhachHang)rlokKhachHang.GetDataSourceRowByKeyValue(x.IDKhachHang) ?? new eKhachHang();
                x.MaKhachHang = khachHang.Ma;
                x.TenKhachHang = khachHang.Ten;
            });

            Tuple<bool, List<eSoDuDauKyKhachHang>> Res = await clsFunction.Post("sodudaukykhachhang", lstEdited.ToList());
            return Res.Item1;
        }
        public override void CustomForm()
        {
            rlokKhachHang.ValueMember = "KeyID";
            rlokKhachHang.DisplayMember = "Ten";

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
            if (!lstEdited.Any(x => x.KeyID == ((eSoDuDauKyKhachHang)e.Row).KeyID)) lstEdited.Add((eSoDuDauKyKhachHang)e.Row);
        }
    }
}
