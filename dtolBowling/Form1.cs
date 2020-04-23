using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace dtolBowling
{
    public partial class Form1 : Form
    {
        int frameCnt = 1;
        int rowFrame1 = 1;
        int rowFrame2 = 1;
        int rowFrame3 = 1;
        int rowFrame4 = 1;
        int rowFrame5 = 1;
        int rowFrame6 = 1;
        int rowFrame7 = 1;
        int rowFrame8 = 1;
        bool isFirst = true;
        int strike = 0;
        bool spare = false;
        string first = string.Empty;
        string second = string.Empty;
        int jumsu = 0;
        int result = 0;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn item in dataGridView1.Columns)
            {
                item.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dataGridView1.ReadOnly = true;
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) //키 입력시 숫자만 입력가능
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) //입력한 숫자가 1~8 인지 확인
        {
            if (txtMember.Text.Length > 0)
            {
                int cnt = Convert.ToInt32(txtMember.Text);

                if (cnt < 1 || cnt > 8)
                {
                    MessageBox.Show("1 ~ 8 사이의 숫자 입력");
                    txtMember.Text = "";
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e) //볼링 게임 시작
        {
            frameCnt = 1;
            jumsu = 0;
            if (txtMember.TextLength < 1) //사용자 수 입력없으면 입력안내
            {
                MessageBox.Show("사용자수 입력");
                return;
            }
            DataRow row = null;
            DataTable dt = new DataTable();
            DataColumn[] frame = { new DataColumn("1"), new DataColumn("2"), new DataColumn("3"), new DataColumn("4"), new DataColumn("5"), new DataColumn("6"), new DataColumn("7"), new DataColumn("8"), new DataColumn("9"), new DataColumn("10") };

            dt.Columns.Add(new DataColumn("레일"));
            dt.Columns.AddRange(frame);
            dt.Columns.Add(new DataColumn("게임시작"));

            dataGridView1.DataSource = dt;
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            int member = int.Parse(txtMember.Text);
            int rail = 1;
            for (int i = 1; i < member * 2 + 1; i++)
            {
                if (i % 2 != 0)
                {
                    row = dt.NewRow();
                    row.ItemArray = new object[] { $"{rail}번 레일" };
                    dt.Rows.Add(row);

                    dataGridView1.Rows[i - 1].Cells[11] = new DataGridViewButtonCell();
                    dataGridView1.Rows[i - 1].Cells[11].Value = "Roll";

                    rail++;
                }
                else
                {
                    row = dt.NewRow();
                    row.ItemArray = new object[] { "점수합계" };
                    dt.Rows.Add(row);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(!dataGridView1.Rows[e.RowIndex].Cells[11].Selected || e.RowIndex < 0)
            {
                return;
            }
            else
            {
                if (frameCnt < 11)
                {
                    if (isFirst)
                    {
                        isFirst = false;
                        rollData(null, e, frameCnt, isFirst);
                    }
                    else
                    {
                        isFirst = true;
                        rollData(null, e, frameCnt, isFirst);
                        frameCnt++;
                    }
                }
                else
                {
                    if (frameCnt == 11 && spare)
                    {
                        jumsu = jumsu + 10;
                        bonusGame(null, e, frameCnt-1,true);
                        spare = false;
                    }
                    else if (frameCnt == 11 && strike > 0)
                    {
                        jumsu = jumsu + 10;
                        bonusGame(null, e, frameCnt - 1, false);
                    }
                    else
                    {
                        MessageBox.Show($"레일 게임 종료");
                        strike = 0;
                        frameCnt = 1;
                        jumsu = 0;
                    }
                }
            }
        }

        private void rollData(object sender, DataGridViewCellEventArgs e, int frame, bool firstRoll)
        {
            Random rnd = new Random();

            if (!firstRoll)
            {
                string roll = Convert.ToString(rnd.Next(0, 11));
                roll = "10";
                if (roll.Equals("10"))
                {
                    dataGridView1.Rows[e.RowIndex].Cells[frame].Value = "X";
                    strike++;
                    frameCnt++;
                    isFirst = true;
                    first = roll;
                    if (strike >= 3)
                    {
                        dataGridView1.Rows[e.RowIndex + 1].Cells[frame - 2].Value = jumsu + 20;
                        jumsu = jumsu + 30;
                    }
                    else if (strike == 2)
                    {
                        dataGridView1.Rows[e.RowIndex + 1].Cells[frame - 1].Value = jumsu + 20;
                        jumsu = jumsu + 10;
                    }
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].Cells[frame].Value = roll;
                    first = roll;
                }
                if (spare)
                {
                    jumsu = jumsu + 10 + Convert.ToInt32(first);
                    dataGridView1.Rows[e.RowIndex + 1].Cells[frame - 1].Value = jumsu;
                    spare = false;
                }
            }
            else
            {
                second = Convert.ToString(rnd.Next(0, 11));
                result = Convert.ToInt32(first) + Convert.ToInt32(second);

                if (result >= 10)
                {
                    second = "/";
                    spare = true;
                    result = 10;
                    strikeCounting(null,e,frame,strike,true);
                }
                else
                {
                    strikeCounting(null, e, frame, strike, false);
                }
                dataGridView1.Rows[e.RowIndex].Cells[frame].Value = first + "  |  " + second;
            }
        }
        private void bonusGame(object sender, DataGridViewCellEventArgs e, int frame, bool bFlag)
        {
            Random rnd = new Random();
            if (bFlag)
            {
                string bonusroll = string.Empty;
                int bonus = rnd.Next(0, 11);
                //int bonus = 10;
                if (bonus.Equals(10))
                {
                    bonusroll = "X";
                }
                else
                {
                    bonusroll = bonus.ToString();
                }
                dataGridView1.Rows[e.RowIndex].Cells[frame].Value = first + "  |  " + second + "  |  " + bonusroll;
                dataGridView1.Rows[e.RowIndex + 1].Cells[frame].Value = jumsu + bonus;
                strike = 0;
            }
            else
            {
                spare = true;
                string bonusroll1 = string.Empty;
                int bonus1 = rnd.Next(0, 11);
                //int bonus1 = 10;
                if(bonus1.Equals(10))
                {
                    bonusroll1 = "X";
                    if(strike > 8)
                    {
                        dataGridView1.Rows[e.RowIndex + 1].Cells[frame - 1].Value = jumsu + 10;
                    }
                    jumsu = jumsu + 10;
                }
                else
                {
                    bonusroll1 = bonus1.ToString();
                }
                first = "X";
                second = bonusroll1;
                dataGridView1.Rows[e.RowIndex].Cells[frame].Value = first + "  |  " + bonusroll1;
                dataGridView1.Rows[e.RowIndex + 1].Cells[frame].Value = jumsu + bonus1;
                jumsu = jumsu + bonus1;
            }
        }

        private void strikeCounting(object sender, DataGridViewCellEventArgs e, int frame, int strikeCnt, bool cFlag)
        {
            if (strikeCnt >= 3)
            {
                dataGridView1.Rows[e.RowIndex + 1].Cells[frame - 2].Value = jumsu + 30 + result;
                jumsu = jumsu + 30 + result;
                if (!cFlag)
                {
                    dataGridView1.Rows[e.RowIndex + 1].Cells[frame - 1].Value = jumsu + result + 10;
                    jumsu = jumsu + result + 10;
                    dataGridView1.Rows[e.RowIndex + 1].Cells[frame].Value = jumsu + result;
                    jumsu = jumsu + result;
                }
            }
            else if (strikeCnt == 2)
            {
                dataGridView1.Rows[e.RowIndex + 1].Cells[frame - 1].Value = jumsu + 20 + result;
                jumsu = jumsu + 20 + result;
                if (!cFlag)
                {
                    dataGridView1.Rows[e.RowIndex + 1].Cells[frame].Value = jumsu + result;
                    jumsu = jumsu + result;
                }
            }
            else if (strikeCnt == 1)
            {
                dataGridView1.Rows[e.RowIndex + 1].Cells[frame - 1].Value = jumsu + 10 + result;
                jumsu = jumsu + 10 + result;
                if (!cFlag)
                {
                    dataGridView1.Rows[e.RowIndex + 1].Cells[frame].Value = jumsu + result;
                    jumsu = jumsu + result;

                }
            }
            else
            {
                if(!cFlag)
                {
                    dataGridView1.Rows[e.RowIndex + 1].Cells[frame].Value = jumsu + result;
                    jumsu = Convert.ToInt32(dataGridView1.Rows[e.RowIndex + 1].Cells[frame].Value);
                }
                else
                {
                    return;
                }
            }
            strike = 0;
        }
    }
}
