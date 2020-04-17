using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace dtolBowling
{
    public class DataGridViewDisableButtonColumn : DataGridViewButtonColumn
    {
        public DataGridViewDisableButtonColumn()
        {
            this.CellTemplate = new DataGridViewDisableButtonCell();
        }
    }

    // Disable Button Cell
    public class DataGridViewDisableButtonCell : DataGridViewButtonCell
    {
        private bool enabledValue;
        public bool Enabled
        {
            get { return enabledValue; }
            set { enabledValue = value; }
        }

        // Override the Clone method so that the Enabled property is copied.
        public override object Clone()
        {
            DataGridViewDisableButtonCell cell =
                (DataGridViewDisableButtonCell)base.Clone();
            cell.Enabled = this.Enabled;
            return cell;
        }

        // By default, enable the button cell.
        public DataGridViewDisableButtonCell()
        {
            this.enabledValue = true;
        }

        protected override void Paint(Graphics graphics,
            Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
            DataGridViewElementStates elementState, object value,
            object formattedValue, string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            // The button cell is disabled, so paint the border,  
            // background, and disabled button for the cell.
            if (!this.enabledValue)
            {
                // Draw the cell background, if specified.
                if ((paintParts & DataGridViewPaintParts.Background) ==
                    DataGridViewPaintParts.Background)
                {
                    SolidBrush cellBackground =
                        new SolidBrush(cellStyle.BackColor);
                    graphics.FillRectangle(cellBackground, cellBounds);
                    cellBackground.Dispose();
                }

                // Draw the cell borders, if specified.
                if ((paintParts & DataGridViewPaintParts.Border) ==
                    DataGridViewPaintParts.Border)
                {
                    PaintBorder(graphics, clipBounds, cellBounds, cellStyle,
                        advancedBorderStyle);
                }

                // Calculate the area in which to draw the button.
                Rectangle buttonArea = cellBounds;
                Rectangle buttonAdjustment =
                    this.BorderWidths(advancedBorderStyle);
                buttonArea.X += buttonAdjustment.X;
                buttonArea.Y += buttonAdjustment.Y;
                buttonArea.Height -= buttonAdjustment.Height;
                buttonArea.Width -= buttonAdjustment.Width;

                // Draw the disabled button.                
                ButtonRenderer.DrawButton(graphics, buttonArea,
                    PushButtonState.Disabled);

                // Draw the disabled button text. 
                if (this.FormattedValue is String)
                {
                    TextRenderer.DrawText(graphics,
                        (string)this.FormattedValue,
                        this.DataGridView.Font,
                        buttonArea, SystemColors.GrayText);
                }
            }
            else
            {
                // The button cell is enabled, so let the base class 
                // handle the painting.
                base.Paint(graphics, clipBounds, cellBounds, rowIndex,
                    elementState, value, formattedValue, errorText,
                    cellStyle, advancedBorderStyle, paintParts);
            }
        }
    }

    public partial class Form1 : Form
    {
        int frameCnt = 1;
        bool isFirst = true;
        int strike = 0;
        bool spare = false;
        string first = string.Empty;
        string second = string.Empty;
        int jumsu = 0;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e) //키 입력시 숫자만 입력가능
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e) //입력한 숫자가 1~8 인지 확인
        {
            if (txtMember.Text.Length > 0)
            {
                int cnt = Convert.ToInt32(txtMember.Text);

                if (cnt < 1 || cnt > 8)
                {
                    MessageBox.Show("1 ~ 8 사이의 숫자 입력");
                    txtMember.Text = "";
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e) //볼링 게임 시작
        {
            frameCnt = 1;
            if (txtMember.TextLength < 1) //사용자 수 입력없으면 입력안내
            {
                MessageBox.Show("사용자수 입력");
                return;
            }
            DataRow row = null;
            DataTable dt = new DataTable();
            DataColumn[] frame = { new DataColumn("1"), new DataColumn("2"), new DataColumn("3"), new DataColumn("4"), new DataColumn("5"), new DataColumn("6"), new DataColumn("7"), new DataColumn("8"), new DataColumn("9"), new DataColumn("10") };

            dt.Columns.Add(new DataColumn("레일"));
            dt.Columns.AddRange(frame);
            dt.Columns.Add(new DataColumn("게임시작"));

            dataGridView1.DataSource = dt;
            dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


            int member = int.Parse(txtMember.Text);
            int rail = 1;
            for (int i = 1; i < member * 2 + 1; i++)
            {
                if (i % 2 != 0)
                {
                    row = dt.NewRow();
                    row.ItemArray = new object[] { $"{rail}번 레일" };
                    dt.Rows.Add(row);

                    DataGridViewDisableButtonCell btnCell = new DataGridViewDisableButtonCell();
                    dataGridView1.Rows[i - 1].Cells[11] = btnCell;
                    dataGridView1.Rows[i - 1].Cells[11].Value = "Roll";


                    //btnCell.Enabled = false;
                    rail++;
                }
                else
                {
                    row = dt.NewRow();
                    row.ItemArray = new object[] { "점수합계" };
                    dt.Rows.Add(row);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex <= 0)
            {
                return;
            }
            else
            {
                if (frameCnt < 10)
                {
                    if (isFirst)
                    {
                        isFirst = false;
                        rollData(null, e, frameCnt, isFirst);
                    }
                    else
                    {
                        isFirst = true;
                        rollData(null, e, frameCnt, isFirst);
                        frameCnt++;
                    }
                }
                else if (frameCnt == 10)
                {
                    if (strike > 0 || spare)
                    {
                        isFirst = true;
                        rollData(null, e, frameCnt, isFirst);
                        isFirst = false;
                    }
                    else
                    {
                        rollData(null, e, frameCnt, isFirst);
                    }
                }
                else
                {
                    MessageBox.Show($"{e.RowIndex + 1}번 레일 게임 종료");
                    frameCnt = 1;
                    jumsu = 0;
                }
            }
        }

        private void rollData(object sender, DataGridViewCellEventArgs e, int frame, bool firstRoll)
        {
            Random rnd = new Random();
            string roll = Convert.ToString(rnd.Next(0, 11));
            if (!firstRoll)
            {
                if (roll.Equals("10"))
                {
                    dataGridView1.Rows[e.RowIndex].Cells[frame].Value = "X";
                    strike++;
                    frameCnt++;
                    isFirst = true;
                    first = "10";
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].Cells[frame].Value = roll;
                    first = roll;
                }
                if (spare)
                {
                    jumsu = jumsu + 10 + Convert.ToInt32(first);
                    dataGridView1.Rows[e.RowIndex + 1].Cells[frame - 1].Value = jumsu;
                    spare = false;
                }
            }
            else
            {
                if (first == "0")
                {
                    second = Convert.ToString(rnd.Next(0, 11));
                    if (second.Equals("10"))
                    {
                        dataGridView1.Rows[e.RowIndex].Cells[frame].Value = first + "  |  " + "X";
                        strike++;
                        second = "10";
                    }
                }
                second = Convert.ToString(rnd.Next(0, 10));
                int result = Convert.ToInt32(first) + Convert.ToInt32(second);
               
                if (result >= 10)
                {
                    second = "/";
                    spare = true;
                    result = 10;
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex + 1].Cells[frame].Value = jumsu + result;
                    jumsu = Convert.ToInt32(dataGridView1.Rows[e.RowIndex + 1].Cells[frame].Value);
                }
                dataGridView1.Rows[e.RowIndex].Cells[frame].Value = first + "  |  " + second;
                if (strike > 0)
                {
                    dataGridView1.Rows[e.RowIndex + 1].Cells[frame - 1].Value = jumsu + 10 + result;
                    jumsu = jumsu + 10 + result;
                    strike = 0;
                }
            }
        }

        private void bonusGame(object sender, DataGridViewCellEventArgs e, int frame, bool firstRoll)
        {

        }
    }
}
