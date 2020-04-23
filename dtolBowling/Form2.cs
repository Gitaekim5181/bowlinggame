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
        int jumsu = 0;
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
        int[] spareCnt = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] strikeCnt = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
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
            allClear();
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
                            rollData(frameSum1, userCnt, spare[0], strike[0], spareCnt[0], strikeCnt[0]);
                            break;
                        case 2:
                            rollData(frameSum2, userCnt, spare[1], strike[1], spareCnt[1], strikeCnt[1]);
                            break;
                        case 4:
                            rollData(frameSum3, userCnt, spare[2], strike[2], spareCnt[2], strikeCnt[2]);
                            break;
                        case 6:
                            rollData(frameSum4, userCnt, spare[3], strike[3], spareCnt[3], strikeCnt[3]);
                            break;
                        case 8:
                            rollData(frameSum5, userCnt, spare[4], strike[4], spareCnt[4], strikeCnt[4]);
                            break;
                        case 10:
                            rollData(frameSum6, userCnt, spare[5], strike[5], spareCnt[5], strikeCnt[5]);
                            break;
                        case 12:
                            rollData(frameSum7, userCnt, spare[6], strike[6], spareCnt[6], strikeCnt[6]);
                            break;
                        case 14:
                            rollData(frameSum8, userCnt, spare[7], strike[7], spareCnt[7], strikeCnt[7]);
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
            else
            {
                MessageBox.Show("게임종료");
            }
        }

        private void rollData(List<int> framelist, int userrail, bool cSpare, bool cStrike, int sparecount, int strikecount)
        {
            Random rnd = new Random();
            string rollNum = string.Empty;
            int sum = 0;
            if (isFirst)
            {
                first = rnd.Next(0, 11);
                //first = 10;
                if (first.Equals(10))
                {
                    rollNum = "X";
                    framelist.Add(first);
                    userCnt += 2;
                    isFirst = true;
                    strike[userrail / 2] = true;
                    if (strikecount >= 2)
                    {
                        dataGridView1.Rows[userrail + 1].Cells[cellCnt - 2].Value = first + 20;
                        framelist.Add(20);
                    }
                    else if (strikecount == 1)
                    {
                        dataGridView1.Rows[userrail + 1].Cells[cellCnt - 1].Value = first + 10;
                        framelist.Add(10);
                    }
                    strikeCnt[userrail / 2]++;
                }
                else
                {
                    rollNum = first.ToString();
                    framelist.Add(first);
                    isFirst = false;
                }
                if(cSpare)
                {
                    sum = scoreSum(framelist, userCnt, cSpare, cStrike);
                    framelist.Add(first);
                    spare[userrail / 2] = false;
                    dataGridView1.Rows[userrail + 1].Cells[cellCnt - 1].Value = sum.ToString();
                }
                dataGridView1.Rows[userrail].Cells[cellCnt].Value = rollNum;
            }
            else
            {
                second = rnd.Next(0, 11);
                if (first + second >= 10)
                {
                    rollNum = "/";
                    spare[userrail / 2] = true;
                    spareCnt[userrail/2]++;
                    second = 10;
                    framelist.RemoveAt(framelist.Count - 1);
                    framelist.Add(second);
                    if(cStrike)
                    {
                        sum = scoreSum(framelist, userCnt, cSpare, cStrike);
                        dataGridView1.Rows[userrail + 1].Cells[cellCnt].Value = sum.ToString();
                        dataGridView1.Rows[userrail + 1].Cells[cellCnt].Value = "";
                    }
                }
                else
                {
                    rollNum = second.ToString();
                    framelist.Add(second);
                    sum = scoreSum(framelist, userCnt, cSpare, cStrike);
                    dataGridView1.Rows[userrail + 1].Cells[cellCnt].Value = sum.ToString();
                    spare[userrail / 2] = false;
                    spareCnt[userrail / 2] = 0;
                    strikeCnt[userrail / 2] = 0;
                }
                dataGridView1.Rows[userrail].Cells[cellCnt].Value = first.ToString() + "  |  " + rollNum;
                userCnt += 2;
                isFirst = true;
            }
        }

        private int scoreSum(List<int> framelist, int userrail, bool cSpare, bool cStrike)
        {
            int sum = 0;
            if (cSpare)
            {
                for (int i = 0; i < framelist.Count; i++)
                {
                    sum += framelist[i];
                    //dataGridView1.Rows[userrail + 1].Cells[cellCnt - 1].Value = sum.ToString();
                }
            }
            else if(cStrike)
            {
                for (int i = 0; i < framelist.Count; i++)
                {
                    sum += framelist[i];
                    dataGridView1.Rows[userrail + 1].Cells[cellCnt - 1].Value = sum.ToString();
                }
                strike[userrail / 2] = false;
                sum = sum + framelist[framelist.Count - 1] + framelist[framelist.Count - 2];
                //dataGridView1.Rows[userrail + 1].Cells[cellCnt].Value = sum.ToString();
                framelist.Clear();
                framelist.Add(sum);
            }
            else
            {
                for (int i = 0; i < framelist.Count; i++)
                {
                    sum += framelist[i];
                    //dataGridView1.Rows[userrail + 1].Cells[cellCnt].Value = sum.ToString();
                }
            }
            return sum;
        }

        private void allClear()
        {
            userCnt = 0;
            cellCnt = 1;
            frameCnt = 1;
            frameSum1.Clear();
            frameSum2.Clear();
            frameSum3.Clear();
            frameSum4.Clear();
            frameSum5.Clear();
            frameSum6.Clear();
            frameSum7.Clear();
            frameSum8.Clear();
            isFirst = true;
            spare = new bool[8] { false, false, false, false, false, false, false, false };
            strike = new bool[8] { false, false, false, false, false, false, false, false };
            spareCnt = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
            strikeCnt = new int[8] { 0, 0, 0, 0, 0, 0, 0, 0 };
        }
    }
}
