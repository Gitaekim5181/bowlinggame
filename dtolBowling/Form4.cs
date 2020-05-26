using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace dtolBowling
{
    public partial class Form4 : Form
    {
        string player = string.Empty;
        string fdate = string.Empty;
        string sdate = string.Empty;

        public Form4()
        {
            InitializeComponent();
        }

        public Form4(string name, string date1, string date2)
        {
            InitializeComponent();
            player = name;
            fdate = date1;
            sdate = date2;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            OracleConnection oraConn = new OracleConnection(connStr);
            DataSet ds = new DataSet();
            try
            {
                OracleCommand oraCmd = oraConn.CreateCommand();
                oraCmd.CommandText = "select * from testtable where name like '%'||:name||'%' and dated between :date1 and :date2 order by dated";
                oraCmd.Parameters.Clear();
               
                oraCmd.Parameters.Add(new OracleParameter("name", player));
                oraCmd.Parameters.Add(new OracleParameter("date1", fdate));
                oraCmd.Parameters.Add(new OracleParameter("date2", sdate));
                oraConn.Open();
                OracleDataAdapter adapt = new OracleDataAdapter(oraCmd);
                adapt.SelectCommand = oraCmd;
                adapt.Fill(ds);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                oraConn.Close();
            }
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
