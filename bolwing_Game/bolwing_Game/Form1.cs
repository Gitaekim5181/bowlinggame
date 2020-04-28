
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
        int btncount = 0;
        int game = 0;
        int[] result = new int[16];
        int[] strike = new int[16];
        int[] spare = new int[16];
        int gcnt = 0;
        int fcnt = 0;
        int clear = 0;
        int game_Frm = 0;
        int game_Rows = 1;
        int bonus = 1;
        int strike_bonus = 1;
        int a = 0;
        int b = 0;
        int c = 0;
        int game_Boll = 1;
        int[] game_Roll = new int[16];//20
        int[] strike_j = new int[16];
        int[] spare_j = new int[16];

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
          
            if (Handled_txt == false && txt1.Text != "")
            {
                int ipno = Convert.ToInt32(txt1.Text);
                if (ipno <= 0 || ipno > 8)
                {
                    MessageBox.Show("1-8사이만입력하세여");
                    txt1.Text = "1";
                    txt1.Focus();
                }


            }
            Restart_Game();
            grid_1.Rows.Clear();
            grid_1.Refresh();
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
                    grid_1.Rows.Add(i + "번 핀수");
                    grid_1.Rows.Add(i + "번 점수");
                }
                clear++;

            }
            if (clear >= 2)
            {

                grid_1.Rows.Clear();
                grid_1.Refresh();
                clear = 0;
            }



        }

        private void btn_Roll_Click(object sender, EventArgs e)
        {
            Random val = new Random();
            Random val_B = new Random();



            if (grid_1.Rows.Count > 0)
            {
                if (game_Roll[gcnt] == 20 && bonus == 0)
                {
                    game_Boll = 3;
                }

                if (game == btncount)
                {
                    c = 10 - a;
                    btncount = 0;
                }
                if (grid_1.Rows.Count == gcnt)
                {
                    gcnt = 0;
                    game_Frm++;
                    game_Rows++;


                }
                if (game_Frm < 1)
                {
                    fcnt = 0;

                }
                if (game_Frm % 2 == 0)
                {
                    fcnt = game_Frm + game_Rows;
                    if (game_Frm == 10 && strike_bonus == 0)
                    {
                        fcnt--;
                    }
                    if (game_Frm >= 11 && strike_bonus == 0)
                    {
                        fcnt = fcnt - 3;
                    }


                }
                if (game_Frm % 2 == 1)
                {
                    fcnt = game_Frm + game_Rows;
                    if (game_Frm >= 11 && strike_bonus == 0)
                    {
                        fcnt = fcnt - 2;
                    }

                }

                btncount++;

                if (game_Frm < 10)//20까지 함.
                {

                    if (strike[gcnt] > 1)
                    {
                        strike[gcnt]--;

                    }
                    if (spare[gcnt] >= 1)
                    {
                        spare[gcnt]--;

                    }

                    if (game >= btncount)
                    {
                        if (game_Boll == 1) //(game_Count<1)
                        {
                            a = val.Next(10);

                            //a = 10;

                            //a = 5;
                            c = 10 - a;
                            strike_bonus = 10 - a;
                            //strike_j[gcnt] -= 10;

                            grid_1.Rows[gcnt].Cells[fcnt].Value = a;

                            if (a < 10) //스트라이크가 아닐때
                            {
                                game_Roll[gcnt]++;
                                game_Boll = 2;//game_Count++;
                                if (strike[gcnt] > 1)
                                {

                                    result[gcnt] += a + strike_j[gcnt];

                                    strike[gcnt] -= 1;
                                    strike_j[gcnt] -= 10;
                                    
                                    grid_1.Rows[gcnt + 1].Cells[game_Frm * 2 - 2].Value = result[gcnt];
                                }

                                else if (spare[gcnt] >= 1)
                                {
                                    spare_j[gcnt] = 10;
                                    //grid_1.Rows[gcnt].Cells[fcnt].Value = a + spare_j;
                                    result[gcnt] += a + spare_j[gcnt];
                                    spare_j[gcnt] -= 10;
                                    grid_1.Rows[gcnt + 1].Cells[fcnt - 1].Value = result[gcnt];
                                }



                            }

                            if (a == 10) //스트라이크 일때
                            {
                                game_Roll[gcnt] += 2;
                                strike[gcnt] += 2;
                                strike_j[gcnt] += 10;

                                if (strike[gcnt] > 3)
                                {

                                    strike_j[gcnt] -= 10;

                                    result[gcnt] += a + strike_j[gcnt];

                                    grid_1.Rows[gcnt + 1].Cells[game_Roll[gcnt] - 4].Value = result[gcnt];

                                }
                                if (spare[gcnt] >= 1)
                                {
                                    result[gcnt] += a + spare_j[gcnt];
                                    spare_j[gcnt] -= 10;
                                    grid_1.Rows[gcnt + 1].Cells[fcnt - 1].Value = result[gcnt];
                                }

                                if (game_Roll[gcnt] == 20 && strike_bonus == 0)
                                {
                                    game_Roll[gcnt] -= 1;

                                    game_Boll = 2;
                                    strike_j[gcnt] -= 10;

                                    result[gcnt] += a + strike_j[gcnt];
                                    //grid_1.Rows[gcnt + 1].Cells[fcnt - strike[gcnt] + 1].Value = result[gcnt];
                                }
                                else
                                {
                                    gcnt += 2;
                                    game_Boll = 1;//game_Count = 0;
                                }



                            }
                        }

                        else if (game_Boll == 2)
                        {
                            if (grid_1.Rows[gcnt].Cells[fcnt].Value.ToString()=="10")
                            {
                                if(strike[gcnt] >= 1)
                                {
                                    c = 10;
                                }
                                else
                                {
                                    c = 10 - a;
                                }
                                
                            }

                            b = val_B.Next(c);

                            //b = 5;
                            //b = 10;
                            bonus = c - b;
                            game_Roll[gcnt]++;
                            grid_1.Rows[gcnt].Cells[fcnt + 1].Value = b;

                            if (strike[gcnt] >= 1)
                            {


                                if (game_Roll[gcnt] == 20 && strike_bonus == 0)
                                {
                                    grid_1.Rows[gcnt].Cells[fcnt + 1].Value = b;
                                    strike_j[gcnt] -= 10;
                                    result[gcnt] += b + strike_j[gcnt];
                                    grid_1.Rows[gcnt + 1].Cells[fcnt - 1].Value = result[gcnt];
                                }
                                else
                                {
                                    if (grid_1.Rows[gcnt].Cells[fcnt - 1].Value == null && bonus != 0)
                                    {

                                        result[gcnt] += a + b + strike_j[gcnt];
                                        strike_j[gcnt] -= 10;
                                        grid_1.Rows[gcnt + 1].Cells[fcnt - 1].Value = result[gcnt];
                                        strike[gcnt]--;
                                    }
                                    if (grid_1.Rows[gcnt].Cells[fcnt - 1].Value == null && bonus == 0)
                                    {

                                        result[gcnt] += a + b + strike_j[gcnt];
                                        strike_j[gcnt] -= 10;
                                        grid_1.Rows[gcnt + 1].Cells[fcnt - 1].Value = result[gcnt];
                                        strike[gcnt]--;
                                    }


                                }
                                

                                //strike_j[gcnt]+=10;  //마지막프레임 보너스 경기에 10점이 필요함
                                
                            }
                            if (bonus == 0 && strike_bonus != 0)
                            {
                                spare[gcnt] += 2;
                                spare_j[gcnt] += 10;
                                
                                //strike[gcnt] -= 1;
                                //strike_j[gcnt] -= 10;
                            }
                            if (game_Roll[gcnt] == 20 && (strike_bonus==0 ||bonus == 0))
                            {
                                game_Boll = 3;
                            }

                            else
                            {
                                //if(bonus != 0 && game_Roll[gcnt] == 20)
                                //{
                                //    result[gcnt] += a + b;
                                //    grid_1.Rows[gcnt + 1].Cells[fcnt + 1].Value = result[gcnt];
                                //}
                                
                                if (grid_1.Rows[gcnt + 1].Cells[fcnt - 1].Value != null && bonus != 0)
                                {
                                    result[gcnt] += a + b;
                                    grid_1.Rows[gcnt + 1].Cells[fcnt + 1].Value = result[gcnt]; //스트라이크 후 +1 할필요가 없어서 확인중

                                }
                                gcnt += 2;
                                game_Boll = 1;//game_Count = 0;
                            }




                        }
                        else if (game_Boll == 3)
                        {
                            Random val_Boll = new Random();
                            int bonus_boll = val_Boll.Next(10);
                            int hap = 0;

                            hap = 10 - bonus_boll;

                            grid_1.Rows[gcnt].Cells[fcnt + 2].Value = bonus_boll;
                            if (strike_bonus == 0)
                            {
                                strike_j[gcnt] = 10;
                                result[gcnt] += bonus_boll + a + b;

                                grid_1.Rows[gcnt + 1].Cells[fcnt + 2].Value = result[gcnt];
                                game_Roll[gcnt] = 21;
                                game_Boll = 1;
                                gcnt += 2;
                            }
                            else
                            {
                                result[gcnt] += bonus_boll + spare_j[gcnt];
                                spare_j[gcnt] -= 10;
                                grid_1.Rows[gcnt + 1].Cells[fcnt + 2].Value = result[gcnt];
                                game_Roll[gcnt] = 21;
                                game_Boll = 1;
                                gcnt += 2;
                            }

                        }
                    }

                }


                else
                {
                    if (strike_bonus == 0)
                    {
                        if (grid_1.Columns.Count == fcnt + 3)
                        {
                            grid_1.Rows[gcnt].Cells[fcnt + 2].Value = result[gcnt];
                            gcnt += 2;

                            MessageBox.Show(gcnt + "번 게임이 완료되었습니다.!");

                        }

                        else
                        {
                            Reset_Game();
                        }
                    }
                    else if (bonus != 0)
                    {
                        if (grid_1.Columns.Count == fcnt + 2)
                        {
                            grid_1.Rows[gcnt + 1].Cells[fcnt + 1].Value = result[gcnt];
                            gcnt += 2;

                            MessageBox.Show("게임이 완료되었습니다.!");

                        }

                        else
                        {
                            Reset_Game();
                        }
                    }
                    else if (bonus == 0)
                    {
                        if (grid_1.Columns.Count == fcnt + 2)
                        {
                            grid_1.Rows[gcnt + 1].Cells[fcnt + 1].Value = result[gcnt];
                            gcnt += 2;

                            MessageBox.Show("게임이 완료되었습니다.!");

                        }

                        else
                        {
                            Reset_Game();
                        }
                            
                    }
                    else
                    {
                        if (grid_1.Columns.Count == fcnt)
                        {
                            grid_1.Rows[gcnt].Cells[fcnt - 1].Value = result[gcnt];
                            gcnt++;

                            MessageBox.Show(gcnt + "번 게임이 완료되었습니다.!");

                        }

                        else
                        {
                            Reset_Game();
                        }
                    }


                }

            }
        }
        private void Reset_Game()
        {
           
            MessageBox.Show("새로 시작하세요 모든 게임이 완료되었습니다.!");
            txt1.Text = "";
            Restart_Game();


        }
        private void Restart_Game()
        {
            for (int i = 0; i < grid_1.Columns.Count; i++)
            {
                for (int j = 0; j < grid_1.Rows.Count; j++)
                {
                    grid_1.Rows[j].Cells[i].DataGridView.Rows.Clear();

                }
            }
            
            fcnt = 0;
            btncount = 0;
            game_Frm = 0;
            game_Rows = 1;
            btncount = 0;
            game = 0;
            gcnt = 0;
            fcnt = 0;
            game_Frm = 0;
            game_Rows = 1;
            bonus = 1;
            strike_bonus = 1;
            a = 0;
            b = 0;
            c = 0;
            game_Boll = 1;
            for (int j = 0; j < result.Length; j++)
            {
                result[j] = 0;
                strike[j] = 0;
                spare[j] = 0;
                game_Roll[j] = 0;
                strike_j[j] = 0;
                spare_j[j] = 0;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.grid_1.Columns.Add(" ", "");
            this.grid_1.Columns.Add("1번프레임", "첫번째");
            this.grid_1.Columns.Add("1번프레임", "두번째");
            this.grid_1.Columns.Add("2번프레임", "첫번째");
            this.grid_1.Columns.Add("2번프레임", "두번째");
            this.grid_1.Columns.Add("3번프레임", "첫번째");
            this.grid_1.Columns.Add("3번프레임", "두번째");
            this.grid_1.Columns.Add("4번프레임", "첫번째");
            this.grid_1.Columns.Add("4번프레임", "두번째");
            this.grid_1.Columns.Add("5번프레임", "첫번째");
            this.grid_1.Columns.Add("5번프레임", "두번째");
            this.grid_1.Columns.Add("6번프레임", "첫번째");
            this.grid_1.Columns.Add("6번프레임", "두번째");
            this.grid_1.Columns.Add("7번프레임", "첫번째");
            this.grid_1.Columns.Add("7번프레임", "두번째");
            this.grid_1.Columns.Add("8번프레임", "첫번째");
            this.grid_1.Columns.Add("8번프레임", "두번째");
            this.grid_1.Columns.Add("9번프레임", "첫번째");
            this.grid_1.Columns.Add("9번프레임", "두번째");
            this.grid_1.Columns.Add("10번프레임", "첫번째");
            this.grid_1.Columns.Add("10번프레임", "두번째");
            this.grid_1.Columns.Add("10번프레임", "보너스");
            this.grid_1.Columns.Add(" ", "합계");

            for (int i = 0; i < this.grid_1.ColumnCount; i++)
            {

                this.grid_1.Columns[i].Width = 70;

            }



            this.grid_1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            this.grid_1.ColumnHeadersHeight = this.grid_1.ColumnHeadersHeight * 2;

            this.grid_1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;



            this.grid_1.ColumnWidthChanged += grid_1_ColumnWidthChanged;

            this.grid_1.Scroll += grid_1_Scroll;

            this.grid_1.Paint += grid_1_Paint;

            this.grid_1.CellPainting += grid_1_CellPainting;

        }
        private void grid_1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex > -1)
            {
                Rectangle cellRectangle = e.CellBounds;
                cellRectangle.Y += e.CellBounds.Height / 2;
                cellRectangle.Height = e.CellBounds.Height / 2;
                e.PaintBackground(cellRectangle, true);
                e.PaintContent(cellRectangle);
                e.Handled = true;

            }

        }

        private void grid_1_Paint(object sender, PaintEventArgs e)
        {
            string[] parentHeaderArray = { "1번프레임", "2번프레임", "3번프레임", "4번프레임", "5번프레임", "6번프레임", "7번프레임", "8번프레임", "9번프레임", "10번프레임" };



            for (int i = 1; i < 21;)
            {

                Rectangle cellRectangle = this.grid_1.GetCellDisplayRectangle(i, -1, true);



                int nextCellWidth = this.grid_1.GetCellDisplayRectangle(i + 1, -1, true).Width;



                cellRectangle.X += 1;

                cellRectangle.Y += 1;



                cellRectangle.Width = cellRectangle.Width + nextCellWidth - 2;

                cellRectangle.Height = cellRectangle.Height / 2 - 2;



                e.Graphics.FillRectangle(new SolidBrush(this.grid_1.ColumnHeadersDefaultCellStyle.BackColor), cellRectangle);



                StringFormat stringFormat = new StringFormat();



                stringFormat.Alignment = StringAlignment.Center;

                stringFormat.LineAlignment = StringAlignment.Center;



                e.Graphics.DrawString

                (

                    parentHeaderArray[i / 2],

                    this.grid_1.ColumnHeadersDefaultCellStyle.Font,

                    new SolidBrush(this.grid_1.ColumnHeadersDefaultCellStyle.ForeColor),

                    cellRectangle,

                    stringFormat

                );



                i += 2;

            }

        }

        private void grid_1_Scroll(object sender, ScrollEventArgs e)
        {
            Rectangle parentHeaderRectangle = this.grid_1.DisplayRectangle;
            parentHeaderRectangle.Height = this.grid_1.ColumnHeadersHeight / 2;
            this.grid_1.Invalidate(parentHeaderRectangle);

        }

        private void grid_1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            Rectangle headerRectangle = this.grid_1.DisplayRectangle;
            headerRectangle.Height = this.grid_1.ColumnHeadersHeight / 2;
            this.grid_1.Invalidate(headerRectangle);
        }
    }
}
