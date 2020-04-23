using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace dtolBowling
{
    public partial class Form2 : Form
    {
        int user = 0; 
        int userCnt = 0;
        int cellCnt = 1; 
        int frameCnt = 0; 
        int first = 0;
        int second = 0; 
        List<int> frameSum1 = new List<int>();
        List<int> frameSum2 = new List<int>();
        List<int> frameSum3 = new List<int>();
        List<int> frameSum4 = new List<int>();
        List<int> frameSum5 = new List<int>();
        List<int> frameSum6 = new List<int>();
        List<int> frameSum7 = new List<int>();
        List<int> frameSum8 = new List<int>();

        bool[] spare = new bool[8] { false, false, false, false, false, false, false, false };
        bool[] strike = new bool[8] { false, false, false, false, false, false, false, false };
        bool isFirst = true;

        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (txtUser.Text.Length > 0)
            {
                int cnt = Convert.ToInt32(txtUser.Text);

                if (cnt < 1 || cnt > 8)
                {
                    MessageBox.Show("1 ~ 8 사이의 숫자 입력");
                    txtUser.Text = "";
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtUser.TextLength < 1) //사용자 수 입력없으면 입력안내
            {
                MessageBox.Show("사용자수 입력");
                return;
            }
            DataRow row = null;
            DataTable dt = new DataTable();
            DataColumn[] frame = { new DataColumn("1"), new DataColumn("2"), new DataColumn("3"), new DataColumn("4"), new DataColumn("5"), new DataColumn("6"), new DataColumn("7"), new DataColumn("8"), new DataColumn("9"), new DataColumn("10") };

            dt.Columns.Add(new DataColumn("레일"));
            dt.Columns.AddRange(frame);

            dataGridView1.DataSource = dt;
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            user = int.Parse(txtUser.Text);
            int rail = 1;
            for (int i = 1; i < user * 2 + 1; i++)
            {
                if (i % 2 != 0)
                {
                    row = dt.NewRow();
                    row.ItemArray = new object[] { $"{rail}번 레일" };
                    dt.Rows.Add(row);
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource == null)
            {
                return;
            }

            if (frameCnt < 11)
            {
                if (userCnt < user * 2)
                {
                    switch (userCnt)
                    {
                        case 0:
                            rollData(frameSum1, userCnt, spare[0], strike[0]);
                            break;
                        case 2:
                            rollData(frameSum2, userCnt, spare[1], strike[1]);
                            break;
                        case 4:
                            rollData(frameSum3, userCnt, spare[2], strike[2]);
                            break;
                        case 6:
                            rollData(frameSum4, userCnt, spare[3], strike[3]);
                            break;
                        case 8:
                            rollData(frameSum5, userCnt, spare[4], strike[4]);
                            break;
                        case 10:
                            rollData(frameSum6, userCnt, spare[5], strike[5]);
                            break;
                        case 12:
                            rollData(frameSum7, userCnt, spare[6], strike[6]);
                            break;
                        case 14:
                            rollData(frameSum8, userCnt, spare[7], strike[7]);
                            break;
                        default:
                            MessageBox.Show("GG");
                            break;
                    }
                }
                else
                {
                    frameCnt++;
                    userCnt = 0;
                    cellCnt++;
                    button2.PerformClick();
                }
            }
        }

        private void rollData(List<int> framelist, int userrail, bool cSpare, bool cStrike)
        {
            Random rnd = new Random();
            string rollNum = string.Empty;
            if (isFirst)
            {
                first = rnd.Next(0, 11);
                if (first.Equals(10))
                {
                    rollNum = "X";
                    framelist.Add(first);
                    userCnt += 2;
                    isFirst = true;
                    strike[userrail / 2] = true;
                }
                else
                {
                    rollNum = first.ToString();
                    framelist.Add(first);
                    isFirst = false;
                }
                if(cSpare)
                {
                    scoreSum(framelist, userCnt, cSpare, cStrike);
                }
                dataGridView1.Rows[userrail].Cells[cellCnt].Value = rollNum;
            }
            else
            {
                second = rnd.Next(0, 11);
                if (first + second >= 10)
                {
                    rollNum = "/";
                    spare[userrail/2] = true;
                    second = 10;
                    framelist.RemoveAt(framelist.Count - 1);
                    framelist.Add(second);
                }
                else
                {
                    rollNum = second.ToString();
                    framelist.Add(second);
                    scoreSum(framelist, userCnt, cSpare, cStrike);
                }
                dataGridView1.Rows[userrail].Cells[cellCnt].Value = first.ToString() + "  |  " + rollNum;
                userCnt += 2;
                isFirst = true;
            }
        }

        private void scoreSum(List<int> framelist, int userrail, bool cSpare, bool cStrike)
        {
            int sum = 0;
            if (cSpare)
            {
                for (int i = 0; i < framelist.Count; i++)
                {
                    sum += framelist[i];
                    dataGridView1.Rows[userrail + 1].Cells[cellCnt - 1].Value = sum.ToString();
                    spare[userrail / 2] = false;
                }
            }
            else if(cStrike)
            {
                for (int i = 0; i < framelist.Count; i++)
                {
                    sum += framelist[i];
                    dataGridView1.Rows[userrail + 1].Cells[cellCnt - 1].Value = sum.ToString();
                    strike[userrail / 2] = false;
                }
                sum = framelist[framelist.Count - 1] + framelist[framelist.Count - 2];
                dataGridView1.Rows[userrail + 1].Cells[cellCnt].Value = sum.ToString();
            }
            else
            {
                for (int i = 0; i < framelist.Count; i++)
                {
                    sum += framelist[i];
                    dataGridView1.Rows[userrail + 1].Cells[cellCnt].Value = sum.ToString();
                }
            }
            //for (int i = 0; i < framelist.Count; i++)
            //{
            //    if(cSpare)
            //    {
            //        sum += framelist[i];
            //        dataGridView1.Rows[userrail + 1].Cells[cellCnt - 1].Value = sum.ToString();
            //        spare[userrail/2] = false;
            //    }
            //    else if(cStrike)
            //    {
            //        sum += framelist[i];
            //        dataGridView1.Rows[userrail + 1].Cells[cellCnt - 1].Value = sum.ToString();
            //    }
            //    sum += framelist[i];
            //    dataGridView1.Rows[userrail + 1].Cells[cellCnt].Value = sum.ToString();
            //}
            framelist.Clear();
            framelist.Add(sum);
        }
    }
}
