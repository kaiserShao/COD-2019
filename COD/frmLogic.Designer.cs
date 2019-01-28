namespace COD
{
    partial class frmLogic
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.btn_Write = new System.Windows.Forms.Button();
			this.btn_Red = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.textBox4 = new System.Windows.Forms.TextBox();
			this.textBox5 = new System.Windows.Forms.TextBox();
			this.textBox6 = new System.Windows.Forms.TextBox();
			this.textBox7 = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.txtDataShow = new System.Windows.Forms.TextBox();
			this.buttonstop = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(97, 24);
			this.textBox1.Margin = new System.Windows.Forms.Padding(2);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(126, 21);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "2";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(97, 64);
			this.textBox2.Margin = new System.Windows.Forms.Padding(2);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(126, 21);
			this.textBox2.TabIndex = 1;
			this.textBox2.Text = "15";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(97, 108);
			this.textBox3.Margin = new System.Windows.Forms.Padding(2);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(126, 21);
			this.textBox3.TabIndex = 2;
			this.textBox3.Text = "0";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(24, 26);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 3;
			this.label1.Text = "从机地址";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(24, 66);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 12);
			this.label2.TabIndex = 4;
			this.label2.Text = "寄存器地址";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(24, 116);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(29, 12);
			this.label3.TabIndex = 5;
			this.label3.Text = "数值";
			// 
			// btn_Write
			// 
			this.btn_Write.Location = new System.Drawing.Point(26, 176);
			this.btn_Write.Margin = new System.Windows.Forms.Padding(2);
			this.btn_Write.Name = "btn_Write";
			this.btn_Write.Size = new System.Drawing.Size(140, 37);
			this.btn_Write.TabIndex = 6;
			this.btn_Write.Text = "写寄存器";
			this.btn_Write.UseVisualStyleBackColor = true;
			this.btn_Write.Click += new System.EventHandler(this.btn_Write_Click);
			// 
			// btn_Red
			// 
			this.btn_Red.Location = new System.Drawing.Point(221, 197);
			this.btn_Red.Margin = new System.Windows.Forms.Padding(2);
			this.btn_Red.Name = "btn_Red";
			this.btn_Red.Size = new System.Drawing.Size(140, 37);
			this.btn_Red.TabIndex = 13;
			this.btn_Red.Text = "读寄存器";
			this.btn_Red.UseVisualStyleBackColor = true;
			this.btn_Red.Click += new System.EventHandler(this.btn_Red_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(251, 115);
			this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(41, 12);
			this.label4.TabIndex = 12;
			this.label4.Text = "期望值";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(251, 66);
			this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(65, 12);
			this.label5.TabIndex = 11;
			this.label5.Text = "寄存器地址";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(251, 26);
			this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(53, 12);
			this.label6.TabIndex = 10;
			this.label6.Text = "从机地址";
			// 
			// textBox4
			// 
			this.textBox4.Location = new System.Drawing.Point(325, 23);
			this.textBox4.Margin = new System.Windows.Forms.Padding(2);
			this.textBox4.Name = "textBox4";
			this.textBox4.Size = new System.Drawing.Size(126, 21);
			this.textBox4.TabIndex = 9;
			this.textBox4.Text = "2";
			// 
			// textBox5
			// 
			this.textBox5.Location = new System.Drawing.Point(325, 63);
			this.textBox5.Margin = new System.Windows.Forms.Padding(2);
			this.textBox5.Name = "textBox5";
			this.textBox5.Size = new System.Drawing.Size(126, 21);
			this.textBox5.TabIndex = 8;
			this.textBox5.Text = "3";
			// 
			// textBox6
			// 
			this.textBox6.Location = new System.Drawing.Point(325, 113);
			this.textBox6.Margin = new System.Windows.Forms.Padding(2);
			this.textBox6.Name = "textBox6";
			this.textBox6.Size = new System.Drawing.Size(126, 21);
			this.textBox6.TabIndex = 7;
			this.textBox6.Text = "0";
			// 
			// textBox7
			// 
			this.textBox7.Location = new System.Drawing.Point(325, 156);
			this.textBox7.Margin = new System.Windows.Forms.Padding(2);
			this.textBox7.Name = "textBox7";
			this.textBox7.Size = new System.Drawing.Size(126, 21);
			this.textBox7.TabIndex = 14;
			this.textBox7.Text = "5";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(251, 164);
			this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(41, 12);
			this.label7.TabIndex = 15;
			this.label7.Text = "超时秒";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(26, 261);
			this.button1.Margin = new System.Windows.Forms.Padding(2);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(140, 37);
			this.button1.TabIndex = 16;
			this.button1.Text = "设置实验任务信息";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(221, 261);
			this.button2.Margin = new System.Windows.Forms.Padding(2);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(140, 37);
			this.button2.TabIndex = 17;
			this.button2.Text = "读实验任务信息";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// txtDataShow
			// 
			this.txtDataShow.Location = new System.Drawing.Point(468, 24);
			this.txtDataShow.Margin = new System.Windows.Forms.Padding(2);
			this.txtDataShow.Multiline = true;
			this.txtDataShow.Name = "txtDataShow";
			this.txtDataShow.Size = new System.Drawing.Size(430, 380);
			this.txtDataShow.TabIndex = 18;
			// 
			// buttonstop
			// 
			this.buttonstop.Location = new System.Drawing.Point(26, 330);
			this.buttonstop.Margin = new System.Windows.Forms.Padding(2);
			this.buttonstop.Name = "buttonstop";
			this.buttonstop.Size = new System.Drawing.Size(119, 45);
			this.buttonstop.TabIndex = 19;
			this.buttonstop.Text = "暂停/运行";
			this.buttonstop.UseVisualStyleBackColor = true;
			this.buttonstop.Click += new System.EventHandler(this.buttonstop_Click);
			// 
			// frmLogic
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(928, 455);
			this.Controls.Add(this.buttonstop);
			this.Controls.Add(this.txtDataShow);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.textBox7);
			this.Controls.Add(this.btn_Red);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textBox4);
			this.Controls.Add(this.textBox5);
			this.Controls.Add(this.textBox6);
			this.Controls.Add(this.btn_Write);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox3);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.textBox1);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "frmLogic";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Load += new System.EventHandler(this.frmLogic_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Write;
        private System.Windows.Forms.Button btn_Red;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox txtDataShow;
		private System.Windows.Forms.Button buttonstop;
    }
}