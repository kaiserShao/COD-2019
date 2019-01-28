namespace COD
{
    partial class frmHistroy
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();

            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();

            this.pnl_Top = new System.Windows.Forms.Panel();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.pnl_Main = new System.Windows.Forms.Panel();
            this.dtGridView = new System.Windows.Forms.DataGridView();
            this.pnl_Right = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.pnl_Left = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnl_bot = new System.Windows.Forms.Panel();
            this.pic_Back = new System.Windows.Forms.PictureBox();
            this.pnl_Top.SuspendLayout();
            this.pnl_Main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtGridView)).BeginInit();
            this.pnl_Right.SuspendLayout();
            this.pnl_Left.SuspendLayout();
            this.pnl_bot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Back)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_Top
            // 
            this.pnl_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.pnl_Top.Controls.Add(this.lbl_Title);
            this.pnl_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Top.Location = new System.Drawing.Point(0, 0);
            this.pnl_Top.Name = "pnl_Top";
            this.pnl_Top.Size = new System.Drawing.Size(1375, 100);
            this.pnl_Top.TabIndex = 3;
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("楷体", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(223)))), ((int)(((byte)(92)))), ((int)(((byte)(12)))));
            this.lbl_Title.Location = new System.Drawing.Point(429, 19);
            this.lbl_Title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(546, 60);
            this.lbl_Title.TabIndex = 134;
            this.lbl_Title.Text = "历 史 实 验 数 据";
            // 
            // pnl_Main
            // 
            this.pnl_Main.BackColor = System.Drawing.SystemColors.Info;
            this.pnl_Main.Controls.Add(this.dtGridView);
            this.pnl_Main.Controls.Add(this.pnl_Right);
            this.pnl_Main.Controls.Add(this.pnl_Left);
            this.pnl_Main.Controls.Add(this.pnl_bot);
            this.pnl_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Main.Location = new System.Drawing.Point(0, 100);
            this.pnl_Main.Name = "pnl_Main";
            this.pnl_Main.Size = new System.Drawing.Size(1375, 412);
            this.pnl_Main.TabIndex = 4;
            // 
            // dtGridView
            // 
            this.dtGridView.AllowUserToAddRows = false;
            this.dtGridView.AllowUserToDeleteRows = false;
            this.dtGridView.AllowUserToResizeColumns = false;
            this.dtGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 15F);
            this.dtGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dtGridView.BackgroundColor = System.Drawing.SystemColors.Info;
            this.dtGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dtGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dtGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dtGridView.DefaultCellStyle = dataGridViewCellStyle3;
            this.dtGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtGridView.EnableHeadersVisualStyles = false;
            this.dtGridView.Location = new System.Drawing.Point(13, 0);
            this.dtGridView.Name = "dtGridView";
            this.dtGridView.ReadOnly = true;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 15F);
            this.dtGridView.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dtGridView.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.Empty;
            this.dtGridView.RowTemplate.Height = 27;
            this.dtGridView.Size = new System.Drawing.Size(1098, 394);
            this.dtGridView.TabIndex = 0;
            // 
            // pnl_Right
            // 
            this.pnl_Right.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.pnl_Right.Controls.Add(this.label2);
            this.pnl_Right.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnl_Right.Location = new System.Drawing.Point(1337, 0);
            this.pnl_Right.Name = "pnl_Right";
            this.pnl_Right.Size = new System.Drawing.Size(38, 312);
            this.pnl_Right.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(1338, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "label2";
            // 
            // pnl_Left
            // 
            this.pnl_Left.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.pnl_Left.Controls.Add(this.label1);
            this.pnl_Left.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_Left.Location = new System.Drawing.Point(0, 0);
            this.pnl_Left.Name = "pnl_Left";
            this.pnl_Left.Size = new System.Drawing.Size(30, 312);
            this.pnl_Left.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(1338, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // pnl_bot
            // 
            this.pnl_bot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(13)))), ((int)(((byte)(210)))), ((int)(((byte)(210)))));
            this.pnl_bot.Controls.Add(this.pic_Back);
            this.pnl_bot.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_bot.Location = new System.Drawing.Point(0, 312);
            this.pnl_bot.Name = "pnl_bot";
            this.pnl_bot.Size = new System.Drawing.Size(1375, 100);
            this.pnl_bot.TabIndex = 5;
            // 
            // pic_Back
            // 
            this.pic_Back.Image = global::COD.Properties.Resources.back00;
            this.pic_Back.Location = new System.Drawing.Point(30, 15);
            this.pic_Back.Name = "pic_Back";
            this.pic_Back.Size = new System.Drawing.Size(198, 73);
            this.pic_Back.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_Back.TabIndex = 85;
            this.pic_Back.TabStop = false;
            this.pic_Back.Click += new System.EventHandler(this.pic_Back_Click);
            // 
            // frmHistroy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1375, 512);
            this.Controls.Add(this.pnl_Main);
            this.Controls.Add(this.pnl_Top);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmHistroy";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmHistroy";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmHistroy_Load);
            this.pnl_Top.ResumeLayout(false);
            this.pnl_Top.PerformLayout();
            this.pnl_Main.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtGridView)).EndInit();
            this.pnl_Right.ResumeLayout(false);
            this.pnl_Right.PerformLayout();
            this.pnl_Left.ResumeLayout(false);
            this.pnl_Left.PerformLayout();
            this.pnl_bot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Back)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Top;
        private System.Windows.Forms.Panel pnl_Main;
        private System.Windows.Forms.Panel pnl_bot;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.DataGridView dtGridView;
        private System.Windows.Forms.Panel pnl_Right;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnl_Left;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pic_Back;
    }
}