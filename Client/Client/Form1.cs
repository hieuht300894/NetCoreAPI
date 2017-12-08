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

namespace Client
{
    public partial class Form1 : XtraForm
    {
        BindingList<Person> persons = new BindingList<Person>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            persons = new BindingList<Person>();
            int i = 1;
            int l = 10;
            for (i = 1; i <= l; i++) { persons.Add(new Person() { KeyID = i, UserName = $"U{i}", Password = $"P{i}" }); }
            gct1.DataSource = persons;

            gct1.Format();
            grv1.CellValueChanged += Grv1_CellValueChanged;
        }

        private void Grv1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //List<MyGridCell> cells = new List<MyGridCell>();
            //grv1.GetSelectedCells().ToList().ForEach(x =>
            //{
            //    cells.Add(new MyGridCell()
            //    {
            //        FormName = Name,
            //        GridName = grv1.Name,
            //        ColumnName = x.Column.Name,
            //        RowHandle = x.RowHandle,
            //        Value = grv1.GetRowCellValue(x.RowHandle, x.Column)
            //    });
            //});

            //Clipboard.SetData("ABC", cells);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //var item = Clipboard.GetData("ABC");
            //List<MyGridCell> cells = new List<MyGridCell>();
            //if (item != null)
            //    cells = (List<MyGridCell>)item;

            //var qColumn = cells.
            //     GroupBy(x => new
            //     {
            //         x.ColumnName
            //     }).
            //     Select(x => new
            //     {
            //         ColumnName = x.Key.ColumnName,
            //         Cells = x.ToList(),
            //         Start = x.Select(y => y.RowHandle).DefaultIfEmpty().Min(),
            //         Total = x.Select(y => y.RowHandle).Count()
            //     });

            //int CurrentRow = grv1.FocusedRowHandle > 0 ? grv1.FocusedRowHandle : grv1.GetRowCount();
            //int MinRow = cells.Select(y => y.RowHandle).DefaultIfEmpty().Min();
            //int MaxRow = cells.Select(y => y.RowHandle).DefaultIfEmpty().Max();
            //int TotalRow = MaxRow - MinRow + 1;
            //grv1.AddNewItemRow(CurrentRow + TotalRow);

            //foreach (var r in qColumn)
            //{
            //    foreach (MyGridCell cell in r.Cells)
            //    {
            //        GridColumn column = grv1.Columns.FirstOrDefault(x => x.Name.Equals(cell.ColumnName));
            //        if (column != null)
            //        {
            //            int rowHandle = CurrentRow + cell.RowHandle - MinRow;
            //            grv1.SetRowCellValue(rowHandle, column, cell.Value);//Hiện tại + vị trí bắt đầu cửa cell - vị trí bắt đầu của row
            //            grv1.SelectCell(rowHandle, column);
            //        }
            //    }
            //}
        }
    }

    [Serializable()]
    public class Person
    {
        public int KeyID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    //[Serializable()]
    //public class MyGridCell
    //{
    //    public string FormName { get; set; }
    //    public string GridName { get; set; }
    //    public string ColumnName { get; set; }
    //    public int RowHandle { get; set; }
    //    public Object Value { get; set; }
    //}

    //public static class FormatControl
    //{
    //    public static void Format2(this GridView grvMain)
    //    {
    //        grvMain.TopRowChanged -= grv_TopRowChanged;

    //        grvMain.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;

    //        int InitNewRowCount = 25;
    //        int CurrentRowCount = grvMain.GetRowCount();
    //        int RemainRowCount = InitNewRowCount - CurrentRowCount;

    //        for (int i = 0; i < RemainRowCount; i++)
    //        {
    //            grvMain.AddNewRow();
    //        }

    //        grvMain.RefreshData();
    //        grvMain.BestFitColumns();


    //        grvMain.TopRowChanged += grv_TopRowChanged;
    //    }
    //    public static void AddNewItemRow(this GridView grvMain, int Total = 0)
    //    {
    //        grvMain.TopRowChanged -= grv_TopRowChanged;

    //        int RemainRowCount = Total - grvMain.GetRowCount();

    //        for (int i = 0; i < RemainRowCount; i++) { grvMain.AddNewRow(); }

    //        grvMain.RefreshData();
    //        grvMain.BestFitColumns();

    //        grvMain.TopRowChanged += grv_TopRowChanged;
    //    }

    //    static void grv_TopRowChanged(object sender, EventArgs e)
    //    {
    //        GridView view = sender as GridView;
    //        GridViewInfo vi = view.GetViewInfo() as GridViewInfo;
    //        List<GridRowInfo> lstRowsInfo = new List<GridRowInfo>(vi.RowsInfo.Where(x => x.VisibleIndex != -1));
    //        for (int i = lstRowsInfo.Count - 1; i >= 0; i--)
    //        {
    //            if (view.IsRowVisible(lstRowsInfo[i].VisibleIndex) != RowVisibleState.Visible || view.IsNewItemRow(lstRowsInfo[i].VisibleIndex))
    //                lstRowsInfo.RemoveAt(i);
    //        }
    //        int LastRow = view.GetLastRow();
    //        int RowCount = view.GetRowCount();

    //        if (LastRow == RowCount)
    //        {
    //            view.AddNewRow();
    //        }
    //    }
    //    public static int GetLastRow(this GridView grvMain)
    //    {
    //        GridViewInfo vi = grvMain.GetViewInfo() as GridViewInfo;
    //        List<GridRowInfo> lstRowsInfo = new List<GridRowInfo>(vi.RowsInfo.Where(x => x.VisibleIndex != -1));
    //        for (int i = lstRowsInfo.Count - 1; i >= 0; i--)
    //        {
    //            if (grvMain.IsRowVisible(lstRowsInfo[i].VisibleIndex) != RowVisibleState.Visible || grvMain.IsNewItemRow(lstRowsInfo[i].VisibleIndex))
    //                lstRowsInfo.RemoveAt(i);
    //        }
    //        return lstRowsInfo.Select(x => x.VisibleIndex).ToList().DefaultIfEmpty().Max() + 1;
    //    }
    //    public static int GetRowCount(this GridView grvMain)
    //    {
    //        if (grvMain.OptionsView.NewItemRowPosition == NewItemRowPosition.None)
    //            return grvMain.RowCount;
    //        return grvMain.RowCount - 1;
    //    }
    //}
}
