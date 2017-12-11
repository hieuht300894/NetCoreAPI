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

            EventInfo eInfo = grv1.GetType().GetEvent("CellValueChanged");
            MethodInfo mInfo = grv1.GetType().GetMethods().FirstOrDefault(x => x.Name.Contains("CellValueChanged"));
            //mInfo.Invoke(grv1, new object[] { new CellValueChangedEventArgs(0, new GridColumn(), 0) });
            Delegate del = Delegate.CreateDelegate(typeof(CellMergeEventHandler), mInfo);
            eInfo.RemoveEventHandler(grv1, del);

        }

        private void Grv1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
        }

        public IEnumerable<MethodInfo> GetSubscribedMethods(object obj)
        {
            Func<EventInfo, FieldInfo> ei2fi = ei => obj.GetType().GetField(ei.Name, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField);

            return from eventInfo in obj.GetType().GetEvents()
                   let eventFieldInfo = ei2fi(eventInfo)
                   let eventFieldValue = (Delegate)eventFieldInfo.GetValue(obj)
                   from subscribedDelegate in eventFieldValue.GetInvocationList()
                   select subscribedDelegate.Method;
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
