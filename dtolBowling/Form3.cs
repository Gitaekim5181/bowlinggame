using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Data.OracleClient;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace dtolBowling
{
    public partial class Form3 : Form
    {
        bool isDate = false;
        string firstdate = string.Empty;
        string seconddate = string.Empty;

        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;

            OracleConnection oraConn = new OracleConnection(connStr);

            OracleCommand oraCmd = oraConn.CreateCommand();
            oraCmd.CommandText = @"select g.name 이름, Max(score) 최고점수, round(avg(score)) 평균점수, RTRIM(count(g.name),'0') 게임수,
NVL(Round(((select count(record) from framedata f where record like '%/%' and g.name=f.name group by f.name) / count(record))*100,2),0)||'%' Spare ,
NVL(Round(((select count(record) from framedata f where record like 'X' and g.name=f.name group by f.name) / count(record))*100,2),0)||'%' Strike ,
NVL(Round(((select count(record) from framedata f where (record not like 'X' and record not like '%/%') and g.name=f.name group by f.name) / count(record))*100,2),0)||'%' Open,
NVL(Round(((select count(record) from framedata f where record like '%0%' and g.name=f.name group by f.name) / count(record))*100,2),0)||'%' Gutter
 from gameData g inner join framedata f on g.name=f.name and g.dated=f.dated group by g.name";
            oraConn.Open();

            DataSet ds = new DataSet();
            OracleDataAdapter adapt = new OracleDataAdapter(oraCmd);
            adapt.SelectCommand = oraCmd;
            adapt.Fill(ds);

            oraConn.Close();
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql = string.Empty;
            if(!isDate && txtPlayer.Text.Length < 1)
            {
                MessageBox.Show("조건을 입력해 주세요");
                return;
            }
            if (txtPlayer.Text.Length >= 1 && isDate)
            {
                sql = @"select g.name, Max(score) 최고점수, round(avg(score))  평균점수, RTRIM(count(g.name),'0') 게임수,
NVL(Round(((select count(record) from frame_view f where record like '%/%' and g.name=f.name group by f.name) / count(record))*100,2),0)||'%' Spare ,
NVL(Round(((select count(record) from frame_view f where record like 'X' and g.name=f.name group by f.name) / count(record))*100,2),0)||'%' Strike ,
NVL(Round(((select count(record) from frame_view f where (record not like 'X' and record not like '%/%') and g.name=f.name group by f.name) / count(record))*100,2),0)||'%' Open,
NVL(Round(((select count(record) from frame_view f where record like '%0%' and g.name=f.name group by f.name) / count(record))*100,2),0)||'%' Gutter
 from gameData g inner join frame_view f on g.name=f.name and g.dated=f.dated where g.name like '%'||:name||'%' and g.dated between :date1 and :date2 group by g.name";
                searchData(sql, 3);
            }
            else if(isDate)
            {
                sql = @"select g.name 이름, Max(score) 최고점수, round(avg(score))  평균점수, RTRIM(count(g.name),'0') 게임수,
NVL(Round(((select count(record) from frame_view f where record like '%/%' and g.name=f.name group by f.name) / count(record))*100,2),0)||'%' Spare ,
NVL(Round(((select count(record) from frame_view f where record like 'X' and g.name=f.name group by f.name) / count(record))*100,2),0)||'%' Strike ,
NVL(Round(((select count(record) from frame_view f where (record not like 'X' and record not like '%/%') and g.name=f.name group by f.name) / count(record))*100,2),0)||'%' Open,
NVL(Round(((select count(record) from frame_view f where record like '%0%' and g.name=f.name group by f.name) / count(record))*100,2),0)||'%' Gutter
 from gameData g inner join frame_view f on g.name=f.name and g.dated=f.dated where g.dated between :date1 and :date2 group by g.name";
                searchData(sql,2);
            }
            else if (txtPlayer.Text.Length >= 1)
            {
                sql = @"select g.name 이름, Max(score) 최고점수, round(avg(score))  평균점수, RTRIM(count(g.name),'0') 게임수,
NVL(Round(((select count(record) from frame_view f where record like '%/%' and g.name=f.name group by f.name) / count(record))*100,2),0)||'%' Spare ,
NVL(Round(((select count(record) from frame_view f where record like 'X' and g.name=f.name group by f.name) / count(record))*100,2),0)||'%' Strike ,
NVL(Round(((select count(record) from frame_view f where (record not like 'X' and record not like '%/%') and g.name=f.name group by f.name) / count(record))*100,2),0)||'%' Open,
NVL(Round(((select count(record) from frame_view f where record like '%0%' and g.name=f.name group by f.name) / count(record))*100,2),0)||'%' Gutter
 from gameData g inner join frame_view f on g.name=f.name and g.dated=f.dated where g.name like '%'||:name||'%' group by g.name";
                searchData(sql,1);
            }
            isDate = false;
            txtPlayer.Text = "";
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            firstdate = dateTimePicker1.Value.ToString("yyyy/MM/dd");
            firstdate = firstdate.Replace("-", "/");
            seconddate = dateTimePicker2.Value.AddDays(1).ToString("yyyy/MM/dd");
            seconddate = seconddate.Replace("-", "/");
            isDate = true;
        }

        private void searchData(string cmdtxt,int type)
        {
            string connStr = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            OracleConnection oraConn = new OracleConnection(connStr);
            DataSet ds = new DataSet();
            try 
            {
                OracleCommand oraCmd = oraConn.CreateCommand();
                oraCmd.CommandText = cmdtxt;
                oraCmd.Parameters.Clear();
                if (type == 1)
                {
                    oraCmd.Parameters.Add(new OracleParameter("name", txtPlayer.Text));
                }
                else if (type == 2)
                {
                    oraCmd.Parameters.Add(new OracleParameter("date1", firstdate));
                    oraCmd.Parameters.Add(new OracleParameter("date2", seconddate));
                }
                else
                {
                    oraCmd.Parameters.Add(new OracleParameter("name", txtPlayer.Text));
                    oraCmd.Parameters.Add(new OracleParameter("date1", firstdate));
                    oraCmd.Parameters.Add(new OracleParameter("date2", seconddate));
                }
                oraConn.Open();
                OracleDataAdapter adapt = new OracleDataAdapter(oraCmd);
                adapt.SelectCommand = oraCmd;
                adapt.Fill(ds);
            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
            finally
            {
                oraConn.Close();
            }
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string player = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
            Form4 frm = new Form4(player, firstdate, seconddate);
            frm.ShowDialog();
        }
    }
}
