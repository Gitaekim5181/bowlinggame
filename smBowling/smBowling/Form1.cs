using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smBowling
{
    public partial class Form1 : Form
    {
        int rollcnt = 1;
        int frameCnt = 0;
        int totalScore = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            dataGridView1.Refresh();
            rollcnt = 1;
            frameCnt = 0;
            if(textBox1.Text == "")
            {
                MessageBox.Show("참가인원을 입력해주세요");
            }
            else if (int.Parse(textBox1.Text) > 9)
            {
                MessageBox.Show("0~8 사이를 입력해주세요.");
            } 
            else
            {
                DataTable table = new DataTable();
                table.Columns.Add("-", typeof(string));
                table.Columns.Add("1", typeof(string));
                table.Columns.Add("2", typeof(string));
                table.Columns.Add("3", typeof(string));
                table.Columns.Add("4", typeof(string));
                table.Columns.Add("5", typeof(string));
                table.Columns.Add("6", typeof(string));
                table.Columns.Add("7", typeof(string));
                table.Columns.Add("8", typeof(string));
                table.Columns.Add("9", typeof(string));
                table.Columns.Add("10", typeof(string));
                dataGridView1.DataSource = table;
                dataGridView1.ReadOnly = true;
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    DataGridViewColumn column = dataGridView1.Columns[i];
                    column.Width = 40;
                }
                table.Rows.Add();
                table.Rows.Add();
                table.Rows.Add();
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            int firstscore = r.Next(0, 11);
            int fscore = 0;
            int secondscore = r.Next(0, 11 - fscore);
            if (rollcnt < 11)
            {                  
                if (frameCnt == 0)
                {                 
                    
                    
                        dataGridView1.Rows[frameCnt].Cells[rollcnt].Value = firstscore;
                  
                    
                    //dataGridView1.Rows[2].Cells[rollcnt].Value = firstscore;
                    frameCnt++;
                    if (rollcnt == 10)
                    {
                        if (firstscore == 10)
                        {
                           secondscore = r.Next(0, 11);
                           dataGridView1.Rows[frameCnt].Cells[rollcnt].Value = secondscore;
                        }
                    }
                } 
                else 
                {
                    String sscore= dataGridView1.Rows[frameCnt - 1].Cells[rollcnt].Value.ToString();
                    if(sscore == "10")
                    {
                        fscore = 10;
                        dataGridView1.Rows[frameCnt].Cells[rollcnt].Value = 0;
                        dataGridView1.Rows[3].Cells[rollcnt].Value = fscore;
                    }
                    else
                    {
                        fscore = Int32.Parse(sscore);
                        secondscore = r.Next(0, 11 - fscore);
                       
                       
                            dataGridView1.Rows[frameCnt].Cells[rollcnt].Value = secondscore;
                            dataGridView1.Rows[3].Cells[rollcnt].Value = fscore + secondscore;
                       
                    }
                   if(rollcnt == 10)
                    {
                        if(fscore + secondscore == 10)
                        {
                            int bonusScore = r.Next(0, 11);
                            dataGridView1.Rows[2].Cells[rollcnt].Value = bonusScore;
                            dataGridView1.Rows[3].Cells[rollcnt].Value = fscore + secondscore + bonusScore;
                        }
                        else
                        {
                            dataGridView1.Rows[3].Cells[rollcnt].Value = fscore + secondscore;
                        }
                    }
                    if(rollcnt > 1)
                    {
                        string lastScore = dataGridView1.Rows[3].Cells[rollcnt - 1].Value.ToString();
                        totalScore = Int32.Parse(lastScore);
                        dataGridView1.Rows[3].Cells[rollcnt].Value = fscore + secondscore + totalScore;
                   
                        int strikeScore = Int32.Parse(dataGridView1.Rows[0].Cells[rollcnt - 1].Value.ToString());
                        int spareScore = Int32.Parse(dataGridView1.Rows[1].Cells[rollcnt - 1].Value.ToString());
                        int lasyFrameScore = Int32.Parse(dataGridView1.Rows[3].Cells[rollcnt - 1].Value.ToString());
                    
                        if (strikeScore == 10)
                        {
                            dataGridView1.Rows[3].Cells[rollcnt - 1].Value = fscore + secondscore + lasyFrameScore;
                            dataGridView1.Rows[3].Cells[rollcnt].Value = fscore + secondscore + totalScore;
                        }
                        else if(strikeScore != 10 && strikeScore + spareScore == 10)
                        {
                            dataGridView1.Rows[3].Cells[rollcnt - 1].Value = fscore + lasyFrameScore;
                            dataGridView1.Rows[3].Cells[rollcnt].Value = fscore + lasyFrameScore + fscore + secondscore;
                            //dataGridView1.Rows[3].Cells[rollcnt].Value = fscore + secondscore + totalScore;
                        }
                    }
                    frameCnt--;
                    rollcnt++;
                    
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

    }
}
