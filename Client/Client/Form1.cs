using Client.Module;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using System.Reflection;

namespace Client
{
    public partial class Form1 : XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Order> list = new List<Order>();
            int i = 1;
            int l = 5;
            for (i = 1; i <= l; i++)
            {
                list.Add(new Order() { KeyID = i, Price = i, ID = i });
            }

            var r1 = list.Sum<Order, decimal>("ID");
            var r2 = list.OrderBy<Order, decimal>("ID");
            var r3 = list.OrderByDescending<Order, decimal>("ID");
            var r4 = list.Min<Order, decimal>("ID");
            var r5 = list.Max<Order, decimal>("ID");
        }
    }

    public class Order
    {
        public int KeyID { get; set; }
        public decimal Price { get; set; }
        public int? ID { get; set; }
    }
}
