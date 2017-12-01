using Client.BLL.Common;
using Client.GUI.Common;
using DevExpress.XtraEditors;
using EntityModel.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.GUI.DanhMuc
{
    public partial class frmTinhThanh : frmBase
    {
        public frmTinhThanh()
        {
            InitializeComponent();

            clsFunction.GetAll<eTienTe>("tiente");
        }
    }
}
