using System;
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
        int fcount = 1;
        int gcount = 1;
        int btncount = 0;
        int game = 0;
        int result = 0;
        int gcnt = 0;
        int fcnt = 0;
        int clear = 0;

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
            if (txt1.Text == "" || txt1.Text == null)
            {
                MessageBox.Show("참가인원을 입력해주세요 인원은 최대 8명까지 입니다.");

                txt1.Focus();
            }
            else
            {
                
                frm = int.Parse(txt1.Text); //프레임 수
                game = int.Parse(txt1.Text);// 게임참여 수
               
                    for (int i = 1; i <= frm; i++)
                    {

                        grid_1.Rows.Add(i + "프레임 점수");

                        grid_1.Rows.Add("합계");
                    }
                clear++;

            }
            if(clear>=2)
            {
             
              grid_1.Rows.Clear();
              clear=0;
            }
            
            

        }

        private void btn_Roll_Click(object sender, EventArgs e)
        {
            Random val = new Random();
            

            int a = val.Next(10);
            int b = 0;
            
            

            if (game <= btncount)
            {
                fcnt++;
                btncount = 0;
                gcnt = 0;
                fcount = 1;
                gcount = 1;
             
            }

            btncount++;

            
            if (fcnt < 10)
            {
                if (game >= btncount)
                {
                    
                    for (int j = fcount; j <= gcount; j++)
                    {
                        grid_1.Rows[gcnt].Cells[fcnt + 1].Value = a;
                        
                        grid_1.Rows[gcnt + 1].Cells[fcnt + 1].Value = a;
                        


                    }
                    gcnt += 2;
                    fcount++;
                    gcount++;
                }
            }
           else if (fcnt == 10 && a == 10)
            {
                if (game >= btncount)
                {

                    for (int j = fcount; j <= gcount; j++)
                    {
                        grid_1.Rows[gcnt].Cells[fcnt + 1].Value = a;
                        grid_1.Rows[gcnt + 1].Cells[fcnt + 1].Value = a;
                        //result = a + b;


                    }
                    gcnt += 2;
                    fcount++;
                    gcount++;
                }
            }
            else 
            {
                MessageBox.Show("게임이 완료되었습니다.!");
            }
 
        }

       
    }
}
