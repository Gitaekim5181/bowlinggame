﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bolwing_Game
{
    public partial class Form1 : Form
    {
        Boolean Handled_txt = false;
        int frm = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void txt1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))

            {
                e.Handled = true;
                Handled_txt = true;
            }
            else
            {
                Handled_txt = false;
            }


        }

        private void txt1_TextChanged(object sender, EventArgs e)
        {
            if (Handled_txt == false && txt1.Text!="")
            {
                int ipno = Convert.ToInt32(txt1.Text);
                if (ipno <= 0 || ipno >8 )
                {

                        MessageBox.Show("1-8사이만입력하세여");

                        txt1.Text = "1";

                        txt1.Focus();

      
                }

                
            }
            grid_1.Rows.Clear();
            Handled_txt = true;
        }

        private void Bnt_Start_Click(object sender, EventArgs e)
        {
            frm = int.Parse(txt1.Text);

            for(int i=1;i<=frm;i++)
            {
               
                grid_1.Rows.Add(i+"프레임");
                grid_1.Rows.Add("점수");
                grid_1.Rows.Add("합계");
            }
            
        }

        private void btn_Roll_Click(object sender, EventArgs e)
        {
            int count = 1;

            if(grid_1.Columns.Contains("col1"))
            {
              

            }
        }
    }
}
