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
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
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
                for(int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    DataGridViewColumn column = dataGridView1.Columns[i];
                    column.Width = 20;
                }
                
        
                table.Rows.Add();
                table.Rows.Add();
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            int firstscore = r.Next(0, 11);
            int secondscore = r.Next(0, 11);

            if (rollcnt < 11)
            {
                if(frameCnt == 0)
                {
                
                    dataGridView1.Rows[frameCnt].Cells[rollcnt].Value = firstscore;
                    dataGridView1.Rows[2].Cells[rollcnt].Value = firstscore;
                    frameCnt++;
                } 
                else
                {
                    dataGridView1.Rows[frameCnt].Cells[rollcnt].Value = secondscore;
                    
                    String sscroe = dataGridView1.Rows[frameCnt-1].Cells[rollcnt].Value.ToString();
                    int fscore = Int32.Parse(sscroe);
                    dataGridView1.Rows[2].Cells[rollcnt].Value = fscore +  secondscore;
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
