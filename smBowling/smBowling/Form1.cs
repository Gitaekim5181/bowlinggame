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
                int player = int.Parse(textBox1.Text);
                for (int i = 0; i < player; i++)
                {
                    dataGridView1.Rows.Add();
                }
            }
        }
    }
}
