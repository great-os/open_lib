using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

using OpenLib.DbHelper;
namespace TestLib
{
    public partial class Form1 : Form
    {
        SqliteHelper hp = new SqliteHelper();
        string plainSql = "select title from novels where id between 9 and 20;";
        string testSql = "select title from novels where id between ? and ?;";
        string testNamedSql = "select title from novels where id between @begin and @end;";
        string textPlainInsertSql = "insert into novels(title,content,url,site_url) values('demo','demo content','url','site_url')";
        string textInsertSql = "insert into novels(title,content,url,site_url) values(?,?,?,?)";
        string textNamedInsertSql = "insert into novels(title,content,url,site_url) values(@title,@content,@url,@site_url)";
        public Form1()
        {
            InitializeComponent();
        }

        #region Query Methods
        private void button1_Click(object sender, EventArgs e)
        {
            DataTable result = hp.ExecParamQuery(testSql, 9, 20);
            dataGridView1.DataSource = result;
            foreach (DataGridViewColumn item in dataGridView1.Columns)
            {
                item.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DataTable result = hp.ExecNamedQuery(testNamedSql, new Dictionary<string, object> { { "begin", 9 }, { "end", 20 } });
            dataGridView1.DataSource = result;
            foreach (DataGridViewColumn item in dataGridView1.Columns)
            {
                item.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            DataTable result = hp.ExecQuery(plainSql);
            dataGridView1.DataSource = result;
            foreach (DataGridViewColumn item in dataGridView1.Columns)
            {
                item.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            }
        }
        #endregion

        #region Scalar Methods
        private void button4_Click(object sender, EventArgs e)
        {
            object result = hp.ExecParamScalar(testSql, 9, 20);
            MessageBox.Show(result.ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            object result = hp.ExecNamedScalar(testNamedSql, new Dictionary<string, object> { { "begin", 9 }, { "end", 20 } });
            MessageBox.Show(result.ToString());
        }
        private void button6_Click(object sender, EventArgs e)
        {
            object result = hp.ExecScalar(plainSql);
            MessageBox.Show(result.ToString());
        }
        #endregion

        #region Create Methods
        private void button7_Click(object sender, EventArgs e)
        {
            object result = hp.ExecParamCreate(textInsertSql, "paramCreate", "paramCreateContent", "url", "url_site");
            MessageBox.Show(result.ToString());
        }
        private void button8_Click(object sender, EventArgs e)
        {
            object result = hp.ExecNamedCreate(textNamedInsertSql, new Dictionary<string, object> {
                { "title", "namedInsert" },
                { "content", "namedInsertContent" },
                { "url", "url" },
                { "site_url", "site_url" }
            });
            MessageBox.Show(result.ToString());
        }
        private void button9_Click(object sender, EventArgs e)
        {
            object result = hp.ExecCreate(textPlainInsertSql);
            MessageBox.Show(result.ToString());
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            hp.ConnectionString = String.Format("Data Source={0};Version=3;", @"E:\GitCode\VB6\vb6-novel-getter\storys.db");
        }
    }
}
