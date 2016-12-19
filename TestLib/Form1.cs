using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using OpenLib.DbHelper;
namespace TestLib
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqliteHelper hp = new SqliteHelper();
            hp.ConnectionString = String.Format("Data Source={0};Version=3;", @"E:\GitCode\VB6\vb6-novel-getter\storys.db");
            DataTable mh = hp.ExecParamQuery("select title from novels where id between ? and ?", 9, 20);
            dataGridView1.DataSource = mh;
            foreach (DataGridViewColumn item in dataGridView1.Columns)
            {
                item.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //var dict = new Dictionary<string, object> { { "Property", "foo" } };
            //System.Diagnostics.Debugger.Break();
            SqliteHelper hp = new SqliteHelper();
            hp.ConnectionString = String.Format("Data Source={0};Version=3;", @"E:\GitCode\VB6\vb6-novel-getter\storys.db");
            DataTable mh = hp.ExecNamedQuery("select title from novels where id between @begin and @end", new Dictionary<string, object> { { "begin", 9 }, { "end", 20 } });
            dataGridView1.DataSource = mh;
            foreach (DataGridViewColumn item in dataGridView1.Columns)
            {
                item.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }
    }
}
