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
            else
            {

                DataTable table = new DataTable();

                // column을 추가합니다.
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

         
                int player = int.Parse(textBox1.Text);
                for(int i = 0; i < player - 1; i++)
                {
                    table.Rows.Add();
                }
            
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           Random r = new Random();
           int score = r.Next(0, 10);
           
        }
    }
}
