
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace bolwing_Game
{


    public partial class Form1 : Form
    {
        Boolean save_t = false;
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

        double[] strike_log = new double[16];
        double[] spare_log = new double[16];
        double[] open_log = new double[16];
        string[,] save = new string[8,23];

     

        //데이터베이스 연결 클래스 소스
        private OracleConnection conn()
        {
            OracleConnection con = new OracleConnection();
            con.ConnectionString = "Data Source = XE; User ID = dev_user1; Password = dev_user1; ";
            return con;
        }
        //데이터베이스 연결 닫는 클래스 소스
        private void connClse(OracleConnection con)
        {
            con.Close();
        }


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
            Label[] label = new Label[8] { label3, label4, label5, label6, label7, label8, label9, label10 };
            TextBox[] textBox = new TextBox[8] { txtp1, txtp2, txtp3, txtp4, txtp5, txtp6, txtp7, txtp8 };
            TextBox[] textBox1 = new TextBox[8] { txtpd1, txtpd2, txtpd3, txtpd4, txtpd5, txtpd6, txtpd7, txtpd8 };
            
            //if (txt1.Text == "")
            //{
                for (int i = 0; i <= textBox.Length - 1; i++)
                {
                    textBox[i].Visible = false;
                    textBox1[i].Visible = false;
                    label[i].Visible = false;
                }
            //}
            if (Handled_txt == false && txt1.Text != "")
            {
                int ipno = Convert.ToInt32(txt1.Text);
                if (ipno <= 0 || ipno > 8)
                {
                    MessageBox.Show("1-8사이만입력하세여");
                    txt1.Text = "1";
                    txt1.Focus();
                }
                for(int i=0;i<= Convert.ToInt32(txt1.Text)-1;i++)
                {
                    textBox[i].Visible = true;
                    textBox1[i].Visible = true;
                    label[i].Visible = true;
                }
            }
            Restart_Game();
            grid_1.Rows.Clear();
            grid_1.Refresh();
            Handled_txt = true;
        }
        private void Bnt_Start_Click(object sender, EventArgs e)
        {
            OracleConnection con = conn();

            con.Open();

            //OracleCommand comm = new OracleCommand("INSERT INTO DEV_USER1.USER_TABLE(NAME,MAIL) VALUES(:NAME,:MAIL)", con);

            OracleCommand comm = new OracleCommand("MERGE INTO DEV_USER1.USER_TABLE USING DUAL ON(MAIL = :MAIL) WHEN NOT MATCHED THEN INSERT(NAME, MAIL ) VALUES(:NAME,:MAIL)", con);
            //NAME..중복 MAIL 중복불가

            if (txt1.Text == "" || txt1.Text == null)
            {
                MessageBox.Show("참가인원을 입력해주세요 인원은 최대 8명까지 입니다.");

                txt1.Focus();
            }
            else
            {
                txt1.Enabled = false;
              
                frm = int.Parse(txt1.Text); //프레임 수
                game = int.Parse(txt1.Text);// 게임참여 수

                for (int i = 0; i <= frm-1; i++)
                {
                    
                    TextBox[] textBox = new TextBox[8] { txtp1, txtp2, txtp3, txtp4, txtp5, txtp6, txtp7, txtp8 };
                    TextBox[] textBox1 = new TextBox[8] { txtpd1, txtpd2, txtpd3, txtpd4, txtpd5, txtpd6, txtpd7, txtpd8 };
                    comm.Parameters.Clear();
                    if (textBox[i].Text !=null && textBox[i].Text!="")
                    {

                        OracleParameter parameter_mail = new OracleParameter("MAIL", OracleDbType.Varchar2, 100);
                        parameter_mail.Value = textBox1[i].Text.Trim();
                        comm.Parameters.Add(parameter_mail);

                        OracleParameter parameter_name = new OracleParameter("NAME", OracleDbType.Varchar2, 20);
                        parameter_name.Value = textBox[i].Text.Trim();
                        comm.Parameters.Add(parameter_name);

                        grid_1.Rows.Add(textBox[i].Text + " 핀수");
                        grid_1.Rows.Add(textBox[i].Text + " 점수");

                        comm.ExecuteNonQuery();

                    }
                    else
                    {
                        MessageBox.Show("참가자 Name과 Mail을 입력 해주세요.");
                    }
                    
                    
                }
                clear++;

            }
            if (clear >= 2)
            {

                grid_1.Rows.Clear();
                grid_1.Refresh();
                clear = 0;
            }
            
            connClse(con);
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

                            a = 10;

                            //a = 5;
                            c = 10 - a;
                            strike_bonus = 10 - a;

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
                                strike_log[gcnt] += 1;
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
                            b = 10;
                            bonus = c - b;
                            game_Roll[gcnt]++;
                            grid_1.Rows[gcnt].Cells[fcnt + 1].Value = b;

                            if (strike[gcnt] >= 1)
                            {
                                if (game_Roll[gcnt] == 20 && strike_bonus == 0)
                                {
                                    grid_1.Rows[gcnt].Cells[fcnt + 1].Value = b;
                                    strike_j[gcnt] -= 10;
                                    //strike_log[gcnt] += 1;
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
                          
                            }
                            if (bonus == 0 && strike_bonus != 0)
                            {
                                spare[gcnt] += 2;
                                spare_j[gcnt] += 10;
                                spare_log[gcnt] += 1;
                                
                            }
                            if (game_Roll[gcnt] == 20 && (strike_bonus==0 ||bonus == 0))
                            {
                                game_Boll = 3;
                            }
                            else
                            {
                                
                                if (grid_1.Rows[gcnt + 1].Cells[fcnt - 1].Value != null && bonus != 0)
                                {
                                    result[gcnt] += a + b;
                                    grid_1.Rows[gcnt + 1].Cells[fcnt + 1].Value = result[gcnt]; //스트라이크 후 +1 할필요가 없어서 확인중
                                    open_log[gcnt] += 1;
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
                            //if(hap ==0)
                            //{
                            //    strike_log[gcnt] += 1;
                            //}
                            //else
                            //{
                            //    open_log[gcnt] += 1;
                            //}
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

                            MessageBox.Show("게임이 완료되었습니다.!");

                        }
                        else
                        {
                            Reset_Game();
                        }
                    }
                    else if (bonus == 0 || bonus != 0)
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
                
                }

            }
        }
        private void Reset_Game()
        {

            btn_save.Visible = true;

            if (save_t== true)
            {
                if (MessageBox.Show("새 게임을 하시겠습니까?? 혹시 저장을 안하셨다면 아니요(N) 눌러주세요", "진행", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    //NO일때
                    MessageBox.Show("취소 하셨습니다.!!", "취소");

                }
                else
                {
                    txt1.Text = "";
                    Restart_Game();  //YES일때
                }

            }
            else
            {
                MessageBox.Show("게임을 저장 하세요.!");
            }
            
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
                strike_log[j] = 0;
                spare_log[j] = 0;
            }
            TextBox[] textBox = new TextBox[8] { txtp1, txtp2, txtp3, txtp4, txtp5, txtp6, txtp7, txtp8 };
            TextBox[] textBox1 = new TextBox[8] { txtpd1, txtpd2, txtpd3, txtpd4, txtpd5, txtpd6, txtpd7, txtpd8 };
            for (int i = 0; i <= textBox.Length - 1; i++)
            {
                textBox[i].Text = "".Trim();
                textBox1[i].Text = "".Trim();

            }
            txt1.Enabled = true;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            btn_save.Visible = false;
            Label[] label = new Label[8] { label3, label4, label5, label6, label7, label8, label9, label10};
            TextBox[] textBox = new TextBox[8] { txtp1, txtp2, txtp3, txtp4, txtp5, txtp6, txtp7, txtp8 };
            TextBox[] textBox1 = new TextBox[8] { txtpd1, txtpd2, txtpd3, txtpd4, txtpd5, txtpd6, txtpd7, txtpd8 };
            for (int i=0;i<=textBox.Length-1;i++)
            {
                textBox[i].Visible = false;
                textBox1[i].Visible = false;
                label[i].Visible = false;
            }

            this.grid_1.Columns.Add(" ", "");
            this.grid_1.Columns.Add("1번프레임", "1-1번");
            this.grid_1.Columns.Add("1번프레임", "1-2번");
            this.grid_1.Columns.Add("2번프레임", "2-1번");
            this.grid_1.Columns.Add("2번프레임", "2-2번");
            this.grid_1.Columns.Add("3번프레임", "3-1번");
            this.grid_1.Columns.Add("3번프레임", "3-2번");
            this.grid_1.Columns.Add("4번프레임", "4-1번");
            this.grid_1.Columns.Add("4번프레임", "4-2번");
            this.grid_1.Columns.Add("5번프레임", "5-1번");
            this.grid_1.Columns.Add("5번프레임", "5-2번");
            this.grid_1.Columns.Add("6번프레임", "6-1번");
            this.grid_1.Columns.Add("6번프레임", "6-2번");
            this.grid_1.Columns.Add("7번프레임", "7-1번");
            this.grid_1.Columns.Add("7번프레임", "7-2번");
            this.grid_1.Columns.Add("8번프레임", "8-1번");
            this.grid_1.Columns.Add("8번프레임", "8-2번");
            this.grid_1.Columns.Add("9번프레임", "9-1번");
            this.grid_1.Columns.Add("9번프레임", "9-2번");
            this.grid_1.Columns.Add("10번프레임", "10-1번");
            this.grid_1.Columns.Add("10번프레임", "10-2번");
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

        private void btn_save_Click(object sender, EventArgs e)
        {

          
           
            DataTable dt = new DataTable();
            
            //dt = (DataTable)grid_1.DataSource;
      
            for (int c=0;c<=grid_1.Columns.Count-1;c++)
            {
                
                dt.Columns.Add(grid_1.Columns[c].HeaderText);
            }
            dt.Columns.Add("strike_log");
            dt.Columns.Add("spare_log");
            for (int d = 0; d <= grid_1.Rows.Count-1; d++)
            {
                DataRow dr = dt.NewRow();
                for (int c1=0;c1<grid_1.Columns.Count;c1++)
                {
                    
                    dr[c1] = grid_1.Rows[d].Cells[c1].Value;
                   
                }
                
                dt.Rows.Add(dr);
            }
            
            OracleConnection con = conn();
            con.Open();

            try
            {
                save_t = true;
                OracleCommand comm = new OracleCommand(@"INSERT INTO DEV_USER1.GAME_SAVE(NAME,MAIL,HAP, ROLL_1,ROLL_2, ROLL_3,ROLL_4,ROLL_5,ROLL_6, ROLL_7,ROLL_8,ROLL_9,ROLL_10, ROLL_11,ROLL_12,ROLL_13,ROLL_14, ROLL_15,ROLL_16,ROLL_17,ROLL_18, ROLL_19,ROLL_20,ROLL_BONUS,GAMEDATE,strike_log,spare_log,open_log)
                VALUES
                ( 
                :NAME,
                :MAIL,
                :HAP,
                :ROLL_1,
                :ROLL_2,
                :ROLL_3,
                :ROLL_4,
                :ROLL_5,
                :ROLL_6,
                :ROLL_7,
                :ROLL_8,
                :ROLL_9,
                :ROLL_10,
                :ROLL_11,
                :ROLL_12,
                :ROLL_13,
                :ROLL_14,
                :ROLL_15,
                :ROLL_16,
                :ROLL_17,
                :ROLL_18,
                :ROLL_19,
                :ROLL_20,
                :ROLL_BONUS,
                to_char(sysdate,'YYYY.MM.DD HH24:mm'),
                :strike_log,
                :spare_log,
                :open_log
                )", con);
                for (int i = 0; i <= frm - 1; i++)
                {
                    for (int k = i; k <= i; k++)
                    {
                        if (k > 0)
                        {
                            k = i + k;
                        }

                        for (int f = 1; f <= 22; f++)
                        {

                            //if(dt.Rows[k].ItemArray[f].ToString()==null)
                            //{
                            //   //dt.Rows[k].
                            //}
                            //save[i,f] = grid_1.Rows[k].Cells[f].Value;
                            save[i, f] = dt.Rows[k].ItemArray[f].ToString();
                     
                        }
                    }     

                }
                
                for (int i = 0; i <= frm - 1; i++)
                {
                    TextBox[] textBox = new TextBox[8] { txtp1, txtp2, txtp3, txtp4, txtp5, txtp6, txtp7, txtp8 };
                    TextBox[] textBox1 = new TextBox[8] { txtpd1, txtpd2, txtpd3, txtpd4, txtpd5, txtpd6, txtpd7, txtpd8 };
                    comm.Parameters.Clear();

                    OracleParameter parameter_name = new OracleParameter("NAME", OracleDbType.Varchar2, 20);
                    parameter_name.Value = textBox[i].Text.Trim();
                    comm.Parameters.Add(parameter_name);

                    OracleParameter parameter_mail = new OracleParameter("MAIL", OracleDbType.Varchar2, 100);
                    parameter_mail.Value = textBox1[i].Text.Trim();
                    comm.Parameters.Add(parameter_mail);

                    OracleParameter parameter_hap = new OracleParameter("HAP", OracleDbType.Varchar2, 100);
                    for (int h = i; h <= i; h++)
                    {
                        if (h > 0)
                        {
                            h = i + h;
                        }
                        parameter_hap.Value = result[h].ToString().Trim();
                    }

                    comm.Parameters.Add(parameter_hap);

                    OracleParameter parameter_ROLL_1 = new OracleParameter("ROLL_1", OracleDbType.Varchar2, 100);
                    parameter_ROLL_1.Value = save[i, 1].ToString().Trim();
                    comm.Parameters.Add(parameter_ROLL_1);

                    OracleParameter parameter_ROLL_2 = new OracleParameter("ROLL_2", OracleDbType.Varchar2, 100);
                    parameter_ROLL_2.Value = save[i, 2].ToString().Trim();
                    comm.Parameters.Add(parameter_ROLL_2);

                    OracleParameter parameter_ROLL_3 = new OracleParameter("ROLL_3", OracleDbType.Varchar2, 100);
                    parameter_ROLL_3.Value = save[i, 3].ToString().Trim();
                    comm.Parameters.Add(parameter_ROLL_3);

                    OracleParameter parameter_ROLL_4 = new OracleParameter("ROLL_4", OracleDbType.Varchar2, 100);
                    parameter_ROLL_4.Value = save[i, 4].ToString().Trim();
                    comm.Parameters.Add(parameter_ROLL_4);

                    OracleParameter parameter_ROLL_5 = new OracleParameter("ROLL_5", OracleDbType.Varchar2, 100);
                    parameter_ROLL_5.Value = save[i, 5].ToString().Trim();
                    comm.Parameters.Add(parameter_ROLL_5);

                    OracleParameter parameter_ROLL_6 = new OracleParameter("ROLL_6", OracleDbType.Varchar2, 100);
                    parameter_ROLL_6.Value = save[i, 6].ToString().Trim();
                    comm.Parameters.Add(parameter_ROLL_6);

                    OracleParameter parameter_ROLL_7 = new OracleParameter("ROLL_7", OracleDbType.Varchar2, 100);
                    parameter_ROLL_7.Value = save[i, 7].ToString().Trim();
                    comm.Parameters.Add(parameter_ROLL_7);

                    OracleParameter parameter_ROLL_8 = new OracleParameter("ROLL_8", OracleDbType.Varchar2, 100);
                    parameter_ROLL_8.Value = save[i, 8].ToString().Trim();
                    comm.Parameters.Add(parameter_ROLL_8);

                    OracleParameter parameter_ROLL_9 = new OracleParameter("ROLL_9", OracleDbType.Varchar2, 100);
                    parameter_ROLL_9.Value = save[i, 9].ToString().Trim();
                    comm.Parameters.Add(parameter_ROLL_9);

                    OracleParameter parameter_ROLL_10 = new OracleParameter("ROLL_10", OracleDbType.Varchar2, 100);
                    parameter_ROLL_10.Value = save[i, 10].ToString().Trim();
                    comm.Parameters.Add(parameter_ROLL_10);

                    OracleParameter parameter_ROLL_11 = new OracleParameter("ROLL_11", OracleDbType.Varchar2, 100);
                    parameter_ROLL_11.Value = save[i, 11].ToString().Trim();
                    comm.Parameters.Add(parameter_ROLL_11);

                    OracleParameter parameter_ROLL_12 = new OracleParameter("ROLL_12", OracleDbType.Varchar2, 100);
                    parameter_ROLL_12.Value = save[i, 12].ToString().Trim();
                    comm.Parameters.Add(parameter_ROLL_12);

                    OracleParameter parameter_ROLL_13 = new OracleParameter("ROLL_13", OracleDbType.Varchar2, 100);
                    parameter_ROLL_13.Value = save[i, 13].ToString().Trim();
                    comm.Parameters.Add(parameter_ROLL_13);

                    OracleParameter parameter_ROLL_14 = new OracleParameter("ROLL_14", OracleDbType.Varchar2, 100);
                    parameter_ROLL_14.Value = save[i, 14].ToString().Trim();
                    comm.Parameters.Add(parameter_ROLL_14);

                    OracleParameter parameter_ROLL_15 = new OracleParameter("ROLL_15", OracleDbType.Varchar2, 100);
                    parameter_ROLL_15.Value = save[i, 15].ToString().Trim();
                    comm.Parameters.Add(parameter_ROLL_15);

                    OracleParameter parameter_ROLL_16 = new OracleParameter("ROLL_16", OracleDbType.Varchar2, 100);
                    parameter_ROLL_16.Value = save[i, 16].ToString().Trim();
                    comm.Parameters.Add(parameter_ROLL_16);

                    OracleParameter parameter_ROLL_17 = new OracleParameter("ROLL_17", OracleDbType.Varchar2, 100);
                    parameter_ROLL_17.Value = save[i, 17].ToString().Trim();
                    comm.Parameters.Add(parameter_ROLL_17);

                    OracleParameter parameter_ROLL_18 = new OracleParameter("ROLL_18", OracleDbType.Varchar2, 100);
                    parameter_ROLL_18.Value = save[i, 18].ToString().Trim();
                    comm.Parameters.Add(parameter_ROLL_18);

                    OracleParameter parameter_ROLL_19 = new OracleParameter("ROLL_19", OracleDbType.Varchar2, 100);
                    parameter_ROLL_19.Value = save[i, 19].ToString().Trim();
                    comm.Parameters.Add(parameter_ROLL_19);

                    OracleParameter parameter_ROLL_20 = new OracleParameter("ROLL_20", OracleDbType.Varchar2, 100);
                    parameter_ROLL_20.Value = save[i, 20].ToString().Trim();
                    comm.Parameters.Add(parameter_ROLL_20);

                    OracleParameter parameter_ROLL_BONUS = new OracleParameter("ROLL_BONUS", OracleDbType.Varchar2, 100);
                    parameter_ROLL_BONUS.Value = save[i, 21].ToString().Trim();
                    comm.Parameters.Add(parameter_ROLL_BONUS);

                    OracleParameter parameter_STRIKE_LOG = new OracleParameter("STRIKE_LOG", OracleDbType.Varchar2, 100);
                    for (int s = i; s <= i; s++)
                    {
                        if (s > 0)
                        {
                            s = i + s;
                        }
                        double st = 10;
                        parameter_STRIKE_LOG.Value = (strike_log[s]/st).ToString().Trim();
                    }

                    comm.Parameters.Add(parameter_STRIKE_LOG);

                    OracleParameter parameter_SPARE_LOG = new OracleParameter("SPARE_LOG", OracleDbType.Varchar2, 100);
                    for (int s = i; s <= i; s++)
                    {
                        if (s > 0)
                        {
                            s = i + s;
                        }
                        double st = 10;
                        parameter_SPARE_LOG.Value = (spare_log[s]/st).ToString().Trim();
                    }
                    
                    comm.Parameters.Add(parameter_SPARE_LOG);


                    OracleParameter parameter_open_log = new OracleParameter("open_log", OracleDbType.Varchar2, 100);
                    for (int op = i; op <= i; op++)
                    {
                        if (op > 0)
                        {
                            op = i + op;
                        }
                        double st = 10;
                        parameter_open_log.Value = (open_log[op]/st).ToString().Trim();
                    }
                    
                    comm.Parameters.Add(parameter_open_log);
                    comm.ExecuteNonQuery();
                }


                connClse(con);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connClse(con);
            }
            MessageBox.Show("게임이 저장 되었습니다.");
            btn_save.Visible = false;
            txt1.Enabled = true;

            if (MessageBox.Show("새 게임을 하시겠습니까??", "진행", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                //NO일때
                MessageBox.Show("취소 하셨습니다.!!", "취소");

            }
            else
            {
                txt1.Text = "";
                Restart_Game();  //YES일때
            }
        }

        private void bnt_sel_Click(object sender, EventArgs e)
        {
            Form2 popup = new Form2();
            popup.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("시스템을 종료하시겠습니까?", "종료", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                //NO일때
                MessageBox.Show("취소 하셨습니다.!!", "취소");
            }
            else
            {
                Application.Exit();  //YES일때
            }
        }
    }
}
