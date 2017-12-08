using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Module
{
    public class TempHelper
    {
        public static List<MyGridCell> ListCell { get; set; } = new List<MyGridCell>();
    }

    public class MyGridCell
    {
        public XtraForm frmMain { get; set; }
        public GridView grvMain { get; set; }
        public GridCell Cell { get; set; }
        public Object Value { get; set; }
    }
}
