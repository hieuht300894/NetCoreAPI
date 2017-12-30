using Client.BLL.Common;
using Client.GUI.Common;
using Client.GUI.DanhMuc;
using Client.Module;
using DevExpress.XtraGrid.Views.Grid;
using EntityModel.DataModel;
using EntityModel.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : frmBase
    {
        public Form1()
        {
            InitializeComponent();
        }
        protected override void frmBase_Load(object sender, EventArgs e)
        {
            base.frmBase_Load(sender, e);

            clsFunction.Login<UserInfo>("module/login", "admin", "admin");
        }
    }
}
