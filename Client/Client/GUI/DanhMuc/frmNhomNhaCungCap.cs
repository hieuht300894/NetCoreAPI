﻿using DevExpress.XtraGrid.Views.Grid;
using EntityModel.DataModel;
using Client.BLL.Common;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.GUI.Common;
using Client.Module;

namespace Client.GUI.DanhMuc
{
    public partial class frmNhomNhaCungCap : frmBase
    {
        BindingList<eNhomNhaCungCap> lstEntries = new BindingList<eNhomNhaCungCap>();
        BindingList<eNhomNhaCungCap> lstEdited = new BindingList<eNhomNhaCungCap>();

        public frmNhomNhaCungCap()
        {
            InitializeComponent();
        }
        protected override void frmBase_Load(object sender, EventArgs e)
        {
            base.frmBase_Load(sender, e);
            LoadDataForm();
            CustomForm();
        }

        public async override void LoadDataForm()
        {
            lstEdited = new BindingList<eNhomNhaCungCap>();
            lstEntries = new BindingList<eNhomNhaCungCap>(await clsFunction.GetAll<eNhomNhaCungCap>("nhomnhacungcap"));
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

            bool chk = false;
            chk = await clsFunction.Post("nhomnhacungcap", lstEdited.ToList());
            return chk;
        }
        public override void CustomForm()
        {
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
            if (!lstEdited.Any(x => x.KeyID == ((eNhomNhaCungCap)e.Row).KeyID)) lstEdited.Add((eNhomNhaCungCap)e.Row);
        }
    }
}
