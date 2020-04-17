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
        int game_Count = 0;

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

            
            if (fcnt < 20)
            {
                if (game*2 >= btncount)
                {
                    if (game_Count < 2)
                    {
                        for (int j = fcount; j <= gcount; j++)
                        {
                            grid_1.Rows[gcnt].Cells[fcnt + 1].Value = a;

                            grid_1.Rows[gcnt + 1].Cells[fcnt + 1].Value = a;
                        }
                        
                        fcount++;
                        gcount++;
                        fcnt++;
                        game_Count++;
                    }
                    
                    else if(game_Count >2)
                    {
                        fcnt = 0;
                        for (int j = fcount; j <= gcount; j++)
                        {
                            grid_1.Rows[gcnt].Cells[fcnt + 1].Value = a;

                            grid_1.Rows[gcnt + 1].Cells[fcnt + 1].Value = a;



                        }
                        gcnt += 2;
                        fcount++;
                        gcount++;
                        game_Count = 1;
                    }
                    
                }
                

            }
           else if (fcnt == 20 && a == 10)
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
            this.grid_1.Columns.Add("보너스게임", "합계");

            for (int i = 0; i < this.grid_1.ColumnCount; i++)

            {

                this.grid_1.Columns[i].Width = 70;

            }



            this.grid_1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;

            this.grid_1.ColumnHeadersHeight = this.grid_1.ColumnHeadersHeight*2;

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
            string[] parentHeaderArray = { "1번프레임", "2번프레임", "3번프레임", "4번프레임", "5번프레임", "6번프레임", "7번프레임", "8번프레임", "9번프레임", "10번프레임"};



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
