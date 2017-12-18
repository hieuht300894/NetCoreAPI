using Client.BLL.Common;
using Client.GUI.Common;
using Client.Module;
using DevExpress.XtraEditors;
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
    //public partial class frmKho : frmBase
    //{
    //    BindingList<eKho> lstEntries = new BindingList<eKho>();
    //    BindingList<eKho> lstEdited = new BindingList<eKho>();

    //    public frmKho()
    //    {
    //        InitializeComponent();
    //    }
    //    protected override void frmBase_Load(object sender, EventArgs e)
    //    {
    //        base.frmBase_Load(sender, e);
    //        LoadDataForm();
    //        CustomForm();
    //    }

    //    public async override void LoadDataForm()
    //    {
    //        lstEdited = new BindingList<eKho>();
    //        lstEntries = new BindingList<eKho>(await clsFunction.GetAll<eKho>("kho"));
    //        await RunMethodAsync(() => { gctDanhSach.DataSource = lstEntries; });
    //    }
    //    public override bool ValidationForm()
    //    {
    //        grvDanhSach.CloseEditor();
    //        grvDanhSach.UpdateCurrentRow();
    //        return base.ValidationForm();
    //    }
    //    public async override Task<bool> SaveData()
    //    {
    //        DateTime time = DateTime.Now.ServerNow();

    //        lstEdited.ToList().ForEach(x =>
    //        {
    //            if (x.KeyID > 0)
    //            {
    //                x.NguoiCapNhat = clsGeneral.curPersonnel.KeyID;
    //                x.MaNguoiCapNhat = clsGeneral.curPersonnel.Ma;
    //                x.TenNguoiCapNhat = clsGeneral.curPersonnel.Ten;
    //                x.NgayCapNhat = time;
    //            }
    //            else
    //            {
    //                x.NguoiTao = clsGeneral.curPersonnel.KeyID;
    //                x.MaNguoiTao = clsGeneral.curPersonnel.Ma;
    //                x.TenNguoiTao = clsGeneral.curPersonnel.Ten;
    //                x.NgayTao = time;
    //            }
    //        });

    //        bool chk = false;
    //        chk = await clsFunction.Post("kho", lstEdited.ToList());
    //        return chk;
    //    }
    //    public override void CustomForm()
    //    {
    //        base.CustomForm();

    //        gctDanhSach.MouseClick += gctDanhSach_MouseClick;
    //        grvDanhSach.RowUpdated += grvDanhSach_RowUpdated;
    //    }
    //    public async override void DeleteEntry()
    //    {
    //        IList<eKho> lstDeleted = await grvDanhSach.GetItems<eKho>();
    //        await clsFunction.Delete("kho", lstDeleted.ToList());
    //    }

    //    private void gctDanhSach_MouseClick(object sender, MouseEventArgs e)
    //    {
    //        ShowGridPopup(sender, e, true, false, true, true, true, true);
    //    }
    //    private void grvDanhSach_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
    //    {
    //        if (!lstEdited.Any(x => x.KeyID == ((eKho)e.Row).KeyID)) lstEdited.Add((eKho)e.Row);
    //    }
    //}

    public partial class frmKho : XtraForm
    {
        public frmKho()
        {
            InitializeComponent();
        }

        //    eKho _iEntry;
        //    eKho _aEntry;

        //    public frmKho()
        //    {
        //        InitializeComponent();
        //    }
        //    protected override void frmBase_Load(object sender, EventArgs e)
        //    {
        //        base.frmBase_Load(sender, e);

        //        LoadDataForm();
        //        CustomForm();
        //    }

        //    public async override void LoadDataForm()
        //    {
        //        _iEntry = _iEntry ?? new eKho();
        //        _aEntry = await clsFunction.GetByID<eKho>("Kho", _iEntry.KeyID);

        //        SetControlValue();
        //    }
        //    public override void SetControlValue()
        //    {
        //        //txtMa.DataBindings.Add("EditValue", _aEntry, "Ma", true, DataSourceUpdateMode.OnPropertyChanged);
        //        //txtTen.DataBindings.Add("EditValue", _aEntry, "Ten", true, DataSourceUpdateMode.OnPropertyChanged);
        //        //mmeGhiChu.DataBindings.Add("EditValue", _aEntry, "GhiChu", true, DataSourceUpdateMode.OnPropertyChanged);
        //    }
        //    public override bool ValidationForm()
        //    {
        //        return base.ValidationForm();
        //    }
        //    public async override Task<bool> SaveData()
        //    {
        //        if (_aEntry.KeyID > 0)
        //        {
        //            _aEntry.NguoiCapNhat = clsGeneral.curPersonnel.KeyID;
        //            _aEntry.MaNguoiCapNhat = clsGeneral.curPersonnel.Ma;
        //            _aEntry.TenNguoiCapNhat = clsGeneral.curPersonnel.Ten;
        //            _aEntry.NgayCapNhat = DateTime.Now.ServerNow();
        //            _aEntry.TrangThai = 2;

        //        }
        //        else
        //        {
        //            _aEntry.NguoiTao = clsGeneral.curPersonnel.KeyID;
        //            _aEntry.MaNguoiTao = clsGeneral.curPersonnel.Ma;
        //            _aEntry.TenNguoiTao = clsGeneral.curPersonnel.Ten;
        //            _aEntry.NgayTao = DateTime.Now.ServerNow();
        //            _aEntry.TrangThai = 1;
        //        }

        //        Tuple<bool, eKho> Res = await (_aEntry.KeyID > 0 ? clsFunction.Put<eKho, eKho>("Kho", _aEntry) : clsFunction.Post<eKho, eKho>("Kho", _aEntry));
        //        if (Res.Item1)
        //            _ReloadData?.Invoke(Res.Item2.KeyID);
        //        return Res.Item1;
        //    }
    }
}
