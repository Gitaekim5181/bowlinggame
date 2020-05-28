using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace bolwing_Game
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

        }

        //데이터베이스 연결 클래스 소스
        private OracleConnection conn()
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = "Data Source = XE; User ID = dev_user1; Password = dev_user1; ";
            return con;
        }
        //데이터베이스 연결 닫는 클래스 소스
        private void connClse(OracleConnection con)
        {
            con.Close();
        }

        private void bnt_sel2_Click(object sender, EventArgs e)
        {
            OracleConnection con = conn();
            con.Open();
            DataSet ds = new DataSet();
            DateTime dt1 = dateTimePicker1.Value;
            DateTime dt2 = dateTimePicker2.Value;
            dt1.ToString("yyyy.MM.dd 00:00");
            dt2.ToString("yyyy.MM.dd 24:00");
            string strname = txtName.Text;
            string strmail = txtMAIL.Text;
            try
            {
                StringBuilder str_sqlstr = new StringBuilder();
                str_sqlstr.Append("select * from DEV_USER1.GAME_SAVE where 1=1");
                str_sqlstr.Append("\n");
                if (txtName.Text != "".ToString().Trim() && txtName.Text != null)
                {

                    str_sqlstr.Append("and name like '%'||:name||'%'");
                    str_sqlstr.Append("\n");
                }
                if (txtMAIL.Text != "".ToString().Trim() && txtMAIL.Text != null)
                {

                    str_sqlstr.Append(" and mail llike '%'||:mail||'%' ");
                    str_sqlstr.Append("\n");

                }
                str_sqlstr.Append("and GAMEDATE between :dt1 and :dt2 order by GAMEDATE");

                OracleCommand comm = new OracleCommand(str_sqlstr.ToString(), con);

                comm.Parameters.Clear();
                if (txtName.Text != "".ToString().Trim() && txtName.Text != null)
                {
                    OracleParameter parameter_name = new OracleParameter("NAME", OracleDbType.Varchar2, 20);
                    parameter_name.Value = strname.Trim();
                    comm.Parameters.Add(parameter_name);
                }
                if (txtMAIL.Text != "".ToString().Trim() && txtMAIL.Text != null)
                {
                    OracleParameter parameter_mail = new OracleParameter("MAIL", OracleDbType.Varchar2, 100);
                    parameter_mail.Value = strmail.Trim();
                    comm.Parameters.Add(parameter_mail);

                }
                OracleParameter parameter_dt1 = new OracleParameter("dt1", OracleDbType.Varchar2, 200);
                parameter_dt1.Value = dt1.ToString("yyyy.MM.dd 00:00").Trim();
                comm.Parameters.Add(parameter_dt1);

                OracleParameter parameter_dt2 = new OracleParameter("dt2", OracleDbType.Varchar2, 200);
                parameter_dt2.Value = dt2.ToString("yyyy.MM.dd 24:00").Trim();
                comm.Parameters.Add(parameter_dt2);

                comm.ExecuteNonQuery();

                OracleDataAdapter adapt = new OracleDataAdapter(comm);

                adapt.Fill(ds);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                connClse(con);
            }
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            connClse(con);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            dateTimePicker1.Value.AddDays(-3);
            dateTimePicker2.Value = DateTime.Now;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string name = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            string mail = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString();
            Form3 frm = new Form3(name, mail);
            frm.ShowDialog();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
