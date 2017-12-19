using Client.Module;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.Internal;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.GUI.Common
{
    public partial class frmMain : XtraForm
    {
        #region Variable
        #endregion

        #region Form
        public frmMain()
        {
            InitializeComponent();

            Load -= FrmMain_Load;
            Load += FrmMain_Load;
        }
        #endregion

        #region Method
        async void LoadDataForm()
        {
            await Task.Factory.StartNew(() =>
            {
                clsGeneral.CallWaitForm(this);

                clsCallForm.InitFormCollection();
                AddItemClick();

                clsGeneral.CloseWaitForm();
            });
        }
        async void AddDocument(XtraForm _xtrForm)
        {
            await Task.Factory.StartNew(() =>
            {

                BaseDocument document = tbvMain.Documents.FirstOrDefault(x => x.Control.Name.Equals(_xtrForm.Name));

                if (document != null)
                    tbvMain.Controller.Activate(document);
                else
                {
                    Invoke(new Action(() =>
                    {
                        _xtrForm.Invoke(new Action(() =>
                        {
                            _xtrForm.MdiParent = this;
                            _xtrForm.Show();
                        }));
                    }));
                }

            });
        }
        async void AddItemClick()
        {
            // Duyệt từng page trong ribbon
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    foreach (RibbonPage page in rcMain.Pages)
                    {
                        foreach (RibbonPageGroup group in page.Groups)
                        {
                            foreach (var item in group.ItemLinks)
                            {
                                if (item is BarButtonItemLink)
                                {
                                    BarButtonItemLink bbi = item as BarButtonItemLink;
                                    if (bbi.Item.Name.StartsWith("frm"))
                                    {
                                        bbi.Item.ItemClick -= bbi_ItemClick;
                                        bbi.Item.ItemClick += bbi_ItemClick;
                                    }
                                }
                            }
                        }
                    }
                });
            }
            catch { }
        }
        #endregion

        #region Event
        private void FrmMain_Load(object sender, EventArgs e)
        {
            LoadDataForm();
        }
        async void bbi_ItemClick(object sender, ItemClickEventArgs e)
        {
            await Task.Factory.StartNew(() =>
            {
                clsGeneral.CallWaitForm(this);
                FormItem fi = clsCallForm.CreateNewForm(e.Item.Name);
                if (fi != null) AddDocument(fi.xForm);
                clsGeneral.CloseWaitForm();
            });
        }
        #endregion
    }
}
