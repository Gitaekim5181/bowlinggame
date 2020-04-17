using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dtolBowling
{
    public partial class Form1 : Form
    {

        int clickCnt = 1;
        bool cFlag = false;
        string first = string.Empty;
        string second = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (txtMember.TextLength < 1)
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

                    dataGridView1.Rows[i - 1].Cells[11] = new DataGridViewButtonCell();
                    dataGridView1.Rows[i - 1].Cells[11].Value = "Roll";
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
                Random rnd = new Random();
                string roll = Convert.ToString(rnd.Next(0, 11));
                
                if (clickCnt <= 2)
                {
                    if(!cFlag)
                    {
                        cFlag = true;
                        dataGridView1.Rows[e.RowIndex].Cells[1].Value = roll;
                        first = roll;
                    }
                    else
                    {
                        second = roll;
                        int result = Convert.ToInt32(first) + Convert.ToInt32(second);
                        if (result >= 10)
                        {
                            second = "/";
                        }
                        dataGridView1.Rows[e.RowIndex].Cells[1].Value = first + " | " + second;
                        dataGridView1.Rows[e.RowIndex + 1].Cells[1].Value = result.ToString();
                    }
                }
                clickCnt++;
            }
        }
    }
}
