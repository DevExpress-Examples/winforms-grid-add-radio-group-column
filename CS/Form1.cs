using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsApplication1
{
    public partial class Form1 : Form
    {
                private DataTable CreateTable(int RowCount)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Name", typeof(string));
            tbl.Columns.Add("ID", typeof(int));
            tbl.Columns.Add("Number", typeof(int));
            tbl.Columns.Add("Date", typeof(DateTime));
            for (int i = 0; i < RowCount; i++)
                tbl.Rows.Add(new object[] { String.Format("Name{0}", i), i, 3 - i, DateTime.Now.AddDays(i) });
            return tbl;
        }


        private GridRadioGroupColumnHelper _Helper;
        public Form1()
        {
            InitializeComponent();
            gridControl1.DataSource = CreateTable(20);
            _Helper = new GridRadioGroupColumnHelper(gridView1);
            _Helper.SelectedRowChanged += new EventHandler(_Helper_SelectedRowChanged);

        }

        void _Helper_SelectedRowChanged(object sender, EventArgs e)
        {
            Text = _Helper.SelectedDataSourceRowIndex.ToString();
        }


    }
}