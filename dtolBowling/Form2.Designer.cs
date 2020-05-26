namespace dtolBowling
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtFrame1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.txtFrame8 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.txtFrame7 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.txtFrame6 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.txtFrame5 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtFrame4 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtFrame2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.조회하기ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtFrame3 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1169, 72);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 35);
            this.button1.TabIndex = 11;
            this.button1.Text = "볼링 시작";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1057, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "플레이어수 입력";
            // 
            // txtUser
            // 
            this.txtUser.Font = new System.Drawing.Font("맑은 고딕", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtUser.Location = new System.Drawing.Point(1063, 69);
            this.txtUser.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(100, 38);
            this.txtUser.TabIndex = 9;
            this.txtUser.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.txtUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(22, 50);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(1029, 545);
            this.dataGridView1.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(1103, 121);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 35);
            this.button2.TabIndex = 16;
            this.button2.Text = "Roll";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(1103, 570);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(112, 31);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "게임기록저장";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtFrame1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(13, 32);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(184, 38);
            this.panel1.TabIndex = 17;
            // 
            // txtFrame1
            // 
            this.txtFrame1.Location = new System.Drawing.Point(83, 6);
            this.txtFrame1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFrame1.Name = "txtFrame1";
            this.txtFrame1.Size = new System.Drawing.Size(85, 25);
            this.txtFrame1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "Frame 1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button3);
            this.groupBox1.Controls.Add(this.panel8);
            this.groupBox1.Controls.Add(this.panel7);
            this.groupBox1.Controls.Add(this.panel6);
            this.groupBox1.Controls.Add(this.panel5);
            this.groupBox1.Controls.Add(this.panel4);
            this.groupBox1.Controls.Add(this.panel3);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Location = new System.Drawing.Point(1059, 169);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(206, 391);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "플레이어 입력";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(46, 350);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 31);
            this.button3.TabIndex = 8;
            this.button3.Text = "플레이어 등록";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.txtFrame8);
            this.panel8.Controls.Add(this.label9);
            this.panel8.Location = new System.Drawing.Point(13, 296);
            this.panel8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(184, 36);
            this.panel8.TabIndex = 103;
            // 
            // txtFrame8
            // 
            this.txtFrame8.Location = new System.Drawing.Point(83, 6);
            this.txtFrame8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFrame8.Name = "txtFrame8";
            this.txtFrame8.Size = new System.Drawing.Size(85, 25);
            this.txtFrame8.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(14, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(59, 15);
            this.label9.TabIndex = 7;
            this.label9.Text = "Frame 8";
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.txtFrame7);
            this.panel7.Controls.Add(this.label8);
            this.panel7.Location = new System.Drawing.Point(13, 258);
            this.panel7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(184, 36);
            this.panel7.TabIndex = 94;
            // 
            // txtFrame7
            // 
            this.txtFrame7.Location = new System.Drawing.Point(83, 6);
            this.txtFrame7.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFrame7.Name = "txtFrame7";
            this.txtFrame7.Size = new System.Drawing.Size(85, 25);
            this.txtFrame7.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(14, 12);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 15);
            this.label8.TabIndex = 7;
            this.label8.Text = "Frame 7";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.txtFrame6);
            this.panel6.Controls.Add(this.label7);
            this.panel6.Location = new System.Drawing.Point(13, 220);
            this.panel6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(184, 36);
            this.panel6.TabIndex = 85;
            // 
            // txtFrame6
            // 
            this.txtFrame6.Location = new System.Drawing.Point(83, 6);
            this.txtFrame6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFrame6.Name = "txtFrame6";
            this.txtFrame6.Size = new System.Drawing.Size(85, 25);
            this.txtFrame6.TabIndex = 5;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 15);
            this.label7.TabIndex = 7;
            this.label7.Text = "Frame 6";
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.txtFrame5);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Location = new System.Drawing.Point(13, 184);
            this.panel5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(184, 36);
            this.panel5.TabIndex = 88;
            // 
            // txtFrame5
            // 
            this.txtFrame5.Location = new System.Drawing.Point(83, 6);
            this.txtFrame5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFrame5.Name = "txtFrame5";
            this.txtFrame5.Size = new System.Drawing.Size(85, 25);
            this.txtFrame5.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 15);
            this.label6.TabIndex = 7;
            this.label6.Text = "Frame 5";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.txtFrame4);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Location = new System.Drawing.Point(13, 147);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(184, 36);
            this.panel4.TabIndex = 27;
            // 
            // txtFrame4
            // 
            this.txtFrame4.Location = new System.Drawing.Point(83, 6);
            this.txtFrame4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFrame4.Name = "txtFrame4";
            this.txtFrame4.Size = new System.Drawing.Size(85, 25);
            this.txtFrame4.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "Frame 4";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtFrame3);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Location = new System.Drawing.Point(13, 110);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(184, 36);
            this.panel3.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Frame 3";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.txtFrame2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(13, 73);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(184, 36);
            this.panel2.TabIndex = 24;
            // 
            // txtFrame2
            // 
            this.txtFrame2.Location = new System.Drawing.Point(83, 6);
            this.txtFrame2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFrame2.Name = "txtFrame2";
            this.txtFrame2.Size = new System.Drawing.Size(85, 25);
            this.txtFrame2.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "Frame 2";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.조회하기ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1287, 28);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 조회하기ToolStripMenuItem
            // 
            this.조회하기ToolStripMenuItem.Name = "조회하기ToolStripMenuItem";
            this.조회하기ToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.조회하기ToolStripMenuItem.Text = "조회하기";
            this.조회하기ToolStripMenuItem.Click += new System.EventHandler(this.조회하기ToolStripMenuItem_Click);
            // 
            // txtFrame3
            // 
            this.txtFrame3.Location = new System.Drawing.Point(83, 6);
            this.txtFrame3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFrame3.Name = "txtFrame3";
            this.txtFrame3.Size = new System.Drawing.Size(85, 25);
            this.txtFrame3.TabIndex = 2;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1287, 612);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSave);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtFrame1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.TextBox txtFrame8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TextBox txtFrame7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.TextBox txtFrame6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox txtFrame5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox txtFrame4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtFrame2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 조회하기ToolStripMenuItem;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox txtFrame3;
    }
}