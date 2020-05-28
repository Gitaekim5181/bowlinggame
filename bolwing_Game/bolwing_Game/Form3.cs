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
    public partial class Form3 : Form
    {
        string pname = string.Empty;
        string pmail = string.Empty;
        

        public Form3()
        {
            InitializeComponent();
        }

        public Form3(string name, string mail)
        {
            InitializeComponent();
            pname = name;
            pmail = mail;

        }
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

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            OracleConnection con = conn();
            con.Open();
            DataSet ds = new DataSet();
         
            try
            {
                OracleCommand comm = new OracleCommand(@"SELECT NAME ,MAIL,max(TO_NUMBER(HAP)) AS 고득점,min(TO_NUMBER(HAP)) AS 저득점
                ,ROUND(avg(HAP),2) AS 총평균 ,sum(HAP) AS 총합, ROUND(AVG(STRIKE_LOG)*100 ,2)||'%' AS 스트라이크
                , ROUND(AVG(SPARE_LOG) * 100, 2) || '%' AS 스페어, ROUND(AVG(OPEN_LOG) * 100, 2) || '%' 
                 AS 오픈, count(MAIL) AS 게임수
                FROM DEV_USER1.GAME_SAVE
                WHERE 1 = 1
                AND NAME LIKE '%'||:NAME||'%'
                AND MAIL LIKE '%'||:MAIL||'%'
                GROUP BY MAIL, NAME", con);

                comm.Parameters.Clear();

                OracleParameter parameter_name = new OracleParameter("NAME", OracleDbType.Varchar2, 20);
                parameter_name.Value = pname.Trim();
                comm.Parameters.Add(parameter_name);
                OracleParameter parameter_mail = new OracleParameter("MAIL", OracleDbType.Varchar2, 100);
                parameter_mail.Value = pmail.Trim();
                comm.Parameters.Add(parameter_mail);

                comm.ExecuteNonQuery();

                OracleDataAdapter adapt = new OracleDataAdapter(comm);
                adapt.SelectCommand=comm;
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
    }
}
