using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
//using System.Data.OracleClient;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace dtolBowling
{
    public partial class Form2 : Form
    {
        bool isFirst = true;
        bool bStrike = false;
        int user = 0; 
        int userCnt = 0;
        int cellCnt = 1; 
        int frameCnt = 0; 
        int first = 0;
        int second = 0;
        string bonusroll1 = string.Empty;
        string bonusroll2 = string.Empty;
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

        List<Panel> pnllist = new List<Panel>();
        List<TextBox> txtlist = new List<TextBox>();
        //List<TextBox> txtmaillist = new List<TextBox>();

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DataClear();
            btnSave.Visible = false;
            button2.Enabled = false;
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
            DataClear();
            if (txtUser.TextLength < 1) //사용자 수 입력없으면 입력안내
            {
                MessageBox.Show("사용자수 입력");
                txtUser.Focus();
                return;
            }

            user = int.Parse(txtUser.Text);

            groupBox1.Visible = true;
            for (int i = 0; i < user; i++)
            {
                pnllist[i].Visible = true;
            }
            button3.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtUser.Text.Length < 1)
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
            else if(frameCnt == 11)
            {
                switch (userCnt)
                {
                    case 0:
                        bonusGame(frameSum1, userCnt, spare[0], strike[0], strikeCnt[0]);
                        break;
                    case 2:
                        bonusGame(frameSum2, userCnt, spare[1], strike[1], strikeCnt[1]);
                        break;
                    case 4:
                        bonusGame(frameSum3, userCnt, spare[2], strike[2], strikeCnt[2]);
                        break;
                    case 6:
                        bonusGame(frameSum4, userCnt, spare[3], strike[3], strikeCnt[3]);
                        break;
                    case 8:
                        bonusGame(frameSum5, userCnt, spare[4], strike[4], strikeCnt[4]);
                        break;
                    case 10:
                        bonusGame(frameSum6, userCnt, spare[5], strike[5], strikeCnt[5]);
                        break;
                    case 12:
                        bonusGame(frameSum7, userCnt, spare[6], strike[6], strikeCnt[6]);
                        break;
                    case 14:
                        bonusGame(frameSum8, userCnt, spare[7], strike[7], strikeCnt[7]);
                        break;
                }
            }
            else
            {
                MessageBox.Show("게임종료");
                btnSave.Visible = true;
            }
        }

        private void rollData(List<int> framelist, int userrail, bool cSpare, bool cStrike, int sparecount, int strikecount) //볼링 게임점수 생성
        {
            Random rnd = new Random();
            string rollNum = string.Empty;
            int sum = 0;
            if (isFirst)//첫번째 공 칠경우
            {
                first = rnd.Next(0, 11);
                //first = 10;
                if (first.Equals(10))
                {
                    rollNum = "X";
                    framelist.Add(first);
                    isFirst = true;
                    strike[userrail / 2] = true;
                    if (strikecount == 2)
                    {
                        sum = scoreSum(framelist, userCnt, cSpare, false);
                        dataGridView1.Rows[userrail + 1].Cells[cellCnt - 2].Value = sum;
                    }
                    else if (strikecount >= 3)
                    {
                        framelist.Add(20);
                        sum = scoreSum(framelist, userCnt, cSpare, false);
                        dataGridView1.Rows[userrail + 1].Cells[cellCnt - 2].Value = sum;
                    }
                    userCnt += 2;
                    strikeCnt[userrail / 2]++;
                    if(frameCnt == 10)
                    {
                        userCnt -= 2;
                        frameCnt++;
                    }
                    if(frameCnt == 9)
                    {
                        bStrike = true;
                    }
                }
                else
                {
                    rollNum = first.ToString();
                    framelist.Add(first);
                    isFirst = false;
                    if (strikecount == 2)
                    {
                        sum = scoreSum(framelist, userCnt, cSpare, false);
                        dataGridView1.Rows[userrail + 1].Cells[cellCnt - 2].Value = sum;
                    }
                    if (strikecount >= 3)
                    {
                        framelist.Add(20);
                        sum = scoreSum(framelist, userCnt, cSpare, false);
                        dataGridView1.Rows[userrail + 1].Cells[cellCnt - 2].Value = sum;
                    }
                }
                if(cSpare)
                {
                    sum = scoreSum(framelist, userCnt, cSpare, false);
                    framelist.Add(first);
                    spare[userrail / 2] = false;
                    spareCnt[userrail / 2] = 0;
                    dataGridView1.Rows[userrail + 1].Cells[cellCnt - 1].Value = sum.ToString();
                }
                dataGridView1.Rows[userrail].Cells[cellCnt].Value = rollNum;
            }
            else
            {
                second = rnd.Next(0, 11);
                //second = 10;
                if (first + second >= 10)
                {
                    rollNum = "/";
                    spare[userrail / 2] = true;
                    spareCnt[userrail/2]++;
                    second = 10;
                    framelist.RemoveAt(framelist.Count - 1);
                    framelist.Add(second);
                    if (strikecount == 1)
                    {
                        sum = scoreSum(framelist, userCnt, true, false);
                        dataGridView1.Rows[userrail + 1].Cells[cellCnt - 1].Value = sum.ToString();
                        framelist.Add(10);
                    }
                    else if (strikecount == 2)
                    {
                        framelist.Add(10);
                        framelist.Add(first);
                        sum = scoreSum(framelist, userCnt, true, cStrike);
                        dataGridView1.Rows[userrail + 1].Cells[cellCnt - 1].Value = sum.ToString();
                        framelist.Add(10);
                    }
                    else if (strikecount >= 3)
                    {
                        framelist.Add(30);
                        sum = scoreSum(framelist, userCnt, true, cStrike);
                        dataGridView1.Rows[userrail + 1].Cells[cellCnt - 1].Value = sum.ToString();
                        framelist.Add(10);
                    }
                    strikeCnt[userrail / 2] = 0;
                    strike[userrail / 2] = false;
                    if (frameCnt == 10)
                    {
                        userCnt -= 2;
                        frameCnt++;
                    }
                }
                else
                {
                    if (strikecount == 2)
                    {
                        framelist.Add(10);
                        framelist.Add(first);
                    }
                    else if (strikecount >= 3)
                    {
                       framelist.Add(30);
                    }
                    rollNum = second.ToString();
                    framelist.Add(second);
                    sum = scoreSum(framelist, userCnt, cSpare, cStrike);
                    dataGridView1.Rows[userrail + 1].Cells[cellCnt].Value = sum.ToString();

                    spare[userrail / 2] = false;
                    spareCnt[userrail / 2] = 0;
                    strikeCnt[userrail / 2] = 0;
                    strike[userrail / 2] = false;
                }
                dataGridView1.Rows[userrail].Cells[cellCnt].Value = first.ToString() + "  |  " + rollNum;
                userCnt += 2;
                isFirst = true;
            }
        }

        private int scoreSum(List<int> framelist, int userrail, bool cSpare, bool cStrike) //점수계산
        {
            int sum = 0;
            if(cStrike) //스트라이크일 경우
            {
                for (int i = 0; i < framelist.Count; i++)
                {
                    sum += framelist[i];
                    dataGridView1.Rows[userrail + 1].Cells[cellCnt - 1].Value = sum.ToString();
                }
                sum = sum + framelist[framelist.Count-1] + framelist[framelist.Count - 2];
                framelist.Clear();
                framelist.Add(sum);
            }
            else //스트라이크 아닐 경우
            {
                for (int i = 0; i < framelist.Count; i++)
                {
                    sum += framelist[i];
                }
            }
            return sum;
        }

        private void bonusGame(List<int> framelist, int userrail, bool cSpare, bool cStrike, int strikecount) //10프레임 스트라이크 / 스페어일 경우 보너스 게임
        {
            int sum = 0;
            Random rnd = new Random();
            string txtroll = string.Empty;
            string txtroll2 = string.Empty;
            bonusroll1 = second.ToString();
            int bonus1 = 0;
            if (cStrike)
            {
                bonus1 = rnd.Next(0, 11);
                //bonus1 = 10;
                if (bonus1.Equals(10))
                {
                    bonusroll1 = "X";
                    bonusroll2 = bonusroll1;
                    framelist.Add(bonus1);
                }
                else
                {
                    bonusroll1 = bonus1.ToString();
                    framelist.Add(bonus1);
                    bonusroll2 = bonusroll1;
                }
                if(first.Equals(10))
                {
                    txtroll = "X";
                }
                else
                {
                    txtroll = first.ToString();
                }
                dataGridView1.Rows[userrail].Cells[cellCnt].Value = txtroll + "  |  " + bonusroll1;
                if(bStrike)
                {
                    sum = scoreSum(framelist, userrail, cSpare, false);
                    dataGridView1.Rows[userrail + 1].Cells[cellCnt - 1].Value = sum.ToString();
                    framelist.Clear();
                    framelist.Add(sum);
                    bStrike = false;
                }
                if(strikecount >= 8)
                {
                    sum = scoreSum(framelist, userrail, cSpare, false) + 20;
                    dataGridView1.Rows[userrail + 1].Cells[cellCnt - 1].Value = sum.ToString();
                    framelist.Clear();
                    framelist.Add(sum);
                    framelist.Add(10);
                    framelist.Add(bonus1);
                }
                strike[userrail / 2] = false;
                spare[userrail / 2] = true;
            }
            else if (cSpare)
            {
                string bonusroll = string.Empty;
                int bonus = rnd.Next(0, 11);
                //bonus = 10;
                if (bonus.Equals(10))
                {
                    bonusroll = "X";
                    framelist.Add(bonus);
                    if(strikecount >= 8)
                    {
                        framelist.Add(20);
                    }
                }
                else
                {
                    bonusroll = bonus.ToString();
                    framelist.Add(bonus);
                }
                if(first + second >= 10 && first != 10)
                {
                    bonusroll2 = "/";
                }
                if(first.Equals(10))
                {
                    txtroll = "X";
                }
                else
                {
                    txtroll = first.ToString();
                }
                dataGridView1.Rows[userrail].Cells[cellCnt].Value = txtroll + "  |  " + bonusroll2 + "  |  " + bonusroll;
                sum = scoreSum(framelist, userrail, cSpare, cStrike);
                dataGridView1.Rows[userrail + 1].Cells[cellCnt].Value = sum.ToString();
                userCnt += 2;
                frameCnt--;
                strike[userrail / 2] = false;
                spare[userrail / 2] = false;
            }
            else
            {
                MessageBox.Show("게임 종료");
                btnSave.Visible = true;
            }
        }

        private void allClear() //초기화
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
        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtFrame1.Text.Length < 1)
            {
                MessageBox.Show("플레이어명을 입력해주세요.");
                return;
            }
            List<string> name = new List<string>();
            List<int> score = new List<int>();
            //List<string> mail = new List<string>();

            //mail.Add(txtMail1.Text);
            name.Add(txtFrame1.Text);
            score.Add(Convert.ToInt32(dataGridView1[10, 1].Value));
            if (user > 1)
            {
                for (int i = 2; i <= user; i++)
                {
                    name.Add(txtlist[i - 1].Text);
                    //mail.Add(txtmaillist[i - 1].Text);
                    score.Add(Convert.ToInt32(dataGridView1[10, (i+1)*2-3].Value));
                }
            }
            
            string connStr = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            OracleConnection oraConn = new OracleConnection(connStr);
            oraConn.Open();
            OracleTransaction sTrans = oraConn.BeginTransaction();
            OracleCommand oraCmd = new OracleCommand();
            try
            {
                oraCmd.Transaction = sTrans;

                oraCmd.CommandText = "INSERT INTO GameData (name, score, dated) VALUES (:name, :score, to_char(sysdate,'yyyy/mm/dd HH:MI'))";
                oraCmd.Connection = oraConn;
                
                for (int i = 0; i < user; i++)
                {
                    oraCmd.Parameters.Clear();
                    //oraCmd.Parameters.Add(new OracleParameter("mail", mail[i]));
                    oraCmd.Parameters.Add(new OracleParameter("name", name[i]));
                    oraCmd.Parameters.Add(new OracleParameter("score", score[i]));
                    oraCmd.ExecuteNonQuery();
                }
                //oraCmd.CommandText = "INSERT INTO FrameData (name, frame, record, dated) VALUES (:name, :frame, :record, to_char(sysdate,'yyyy/mm/dd HH:MI'))";
                //for (int i = 0; i < user; i++)
                //{
                //    List<string> record = new List<string>();
                //    for (int j = 1; j < 11; j++)
                //    {
                //        record.Add(dataGridView1[j, i*2].Value.ToString());
                //        oraCmd.Parameters.Clear();
                //        oraCmd.Parameters.Add(new OracleParameter("name", name[i]));
                //        oraCmd.Parameters.Add(new OracleParameter("frame", j));
                //        oraCmd.Parameters.Add(new OracleParameter("record", record[j - 1]));
                //        oraCmd.ExecuteNonQuery();
                //    }
                //}
                ///테스트
                oraCmd.CommandText = "INSERT INTO TestTable (name,frame1,frame2,frame3,frame4,frame5,frame6,frame7,frame8,frame9,frame10, dated) VALUES (:name,:frame1,:frame2,:frame3,:frame4,:frame5,:frame6,:frame7,:frame8,:frame9,:frame10, to_char(sysdate,'yyyy/mm/dd HH:MI'))";
                for (int i = 0; i < user; i++)
                {
                    List<string> record = new List<string>();
                    for (int j = 1; j < 11; j++)
                    {
                        record.Add(dataGridView1[j, i * 2].Value.ToString());
                    }
                    oraCmd.Parameters.Clear();
                    oraCmd.Parameters.Add(new OracleParameter("name", name[i]));
                    oraCmd.Parameters.Add(new OracleParameter("frame1", record[0]));
                    oraCmd.Parameters.Add(new OracleParameter("frame2", record[1]));
                    oraCmd.Parameters.Add(new OracleParameter("frame3", record[2]));
                    oraCmd.Parameters.Add(new OracleParameter("frame4", record[3]));
                    oraCmd.Parameters.Add(new OracleParameter("frame5", record[4]));
                    oraCmd.Parameters.Add(new OracleParameter("frame6", record[5]));
                    oraCmd.Parameters.Add(new OracleParameter("frame7", record[6]));
                    oraCmd.Parameters.Add(new OracleParameter("frame8", record[7]));
                    oraCmd.Parameters.Add(new OracleParameter("frame9", record[8]));
                    oraCmd.Parameters.Add(new OracleParameter("frame10", record[9]));
                    oraCmd.ExecuteNonQuery();
                }
                ///
                oraCmd.Transaction.Commit();
                
                MessageBox.Show("기록저장완료");
                DataClear();
                txtUser.Text = "";
                button2.Enabled = false;
            }
            catch(Exception err)
            {
                oraCmd.Transaction.Rollback();
                MessageBox.Show(err.Message);
            }
            finally
            {
                oraConn.Close();
            }
        }

        private void 조회하기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 frm = new Form3();
            frm.Show();
        }

        private void DataClear()
        {
            dataGridView1.DataSource = "";
            groupBox1.Visible = false;
            btnSave.Visible = false;
            pnllist.Clear();
            txtlist.Clear();
            //txtmaillist.Clear();

            pnllist.Add(panel1);
            pnllist.Add(panel2);
            pnllist.Add(panel3);
            pnllist.Add(panel4);
            pnllist.Add(panel5);
            pnllist.Add(panel6);
            pnllist.Add(panel7);
            pnllist.Add(panel8);
            txtlist.Add(txtFrame1);
            txtlist.Add(txtFrame2);
            txtlist.Add(txtFrame3);
            txtlist.Add(txtFrame4);
            txtlist.Add(txtFrame5);
            txtlist.Add(txtFrame6);
            txtlist.Add(txtFrame7);
            txtlist.Add(txtFrame8);
            //txtmaillist.Add(txtMail1);
            //txtmaillist.Add(txtMail2);
            //txtmaillist.Add(txtMail3);
            //txtmaillist.Add(txtMail4);
            //txtmaillist.Add(txtMail5);
            //txtmaillist.Add(txtMail6);
            //txtmaillist.Add(txtMail7);
            //txtmaillist.Add(txtMail8);

            foreach (var item in pnllist)
            {
                item.Visible = false;
            }
            foreach (var txt in txtlist)
            {
                txt.Text = "";
            }
            //foreach (var txt in txtmaillist)
            //{
            //    txt.Text = "";
            //}
            for (int i = 0; i < txtlist.Count; i++)
            {
                txtlist[i].Enabled = true;
            }
            //for (int i = 0; i < txtmaillist.Count; i++)
            //{
            //    txtmaillist[i].Enabled = true;
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataRow row = null;
            DataTable dt = new DataTable();
            DataColumn[] frame = { new DataColumn("1"), new DataColumn("2"), new DataColumn("3"), new DataColumn("4"), new DataColumn("5"), new DataColumn("6"), new DataColumn("7"), new DataColumn("8"), new DataColumn("9"), new DataColumn("10") };

            dt.Columns.Add(new DataColumn("레일"));
            dt.Columns.AddRange(frame);

            dataGridView1.DataSource = dt;
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            int rail = 0;
            for (int i = 1; i < user * 2 + 1; i++)
            {
                if (i % 2 != 0)
                {
                    row = dt.NewRow();
                    row.ItemArray = new object[] { $"{txtlist[rail].Text} 레일" };
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

            for (int i = 0; i < txtlist.Count; i++)
            {
                txtlist[i].Enabled = false;
            }
            //for (int i = 0; i < txtmaillist.Count; i++)
            //{
            //    txtmaillist[i].Enabled = false;
            //}

            button3.Visible = false;
            button2.Enabled = true;
        }
    }
}