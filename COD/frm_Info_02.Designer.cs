namespace COD
{
    partial class frm_Info_02
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
            this.txt_Info = new System.Windows.Forms.TextBox();
            this.pnl_Top = new System.Windows.Forms.Panel();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.pic_Close = new System.Windows.Forms.PictureBox();
            this.pic_Quit = new System.Windows.Forms.PictureBox();
            this.pic_Ok = new System.Windows.Forms.PictureBox();
            this.pnl_Top.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Quit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Ok)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_Info
            // 
            this.txt_Info.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(246)))), ((int)(((byte)(252)))));
            this.txt_Info.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Info.Enabled = false;
            this.txt_Info.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Info.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.txt_Info.Location = new System.Drawing.Point(-2, 101);
            this.txt_Info.Margin = new System.Windows.Forms.Padding(4);
            this.txt_Info.Multiline = true;
            this.txt_Info.Name = "txt_Info";
            this.txt_Info.ReadOnly = true;
            this.txt_Info.Size = new System.Drawing.Size(511, 91);
            this.txt_Info.TabIndex = 4;
            this.txt_Info.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pnl_Top
            // 
            this.pnl_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(207)))), ((int)(((byte)(167)))));
            this.pnl_Top.Controls.Add(this.lbl_Title);
            this.pnl_Top.Controls.Add(this.pic_Close);
            this.pnl_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Top.Location = new System.Drawing.Point(0, 0);
            this.pnl_Top.Margin = new System.Windows.Forms.Padding(4);
            this.pnl_Top.Name = "pnl_Top";
            this.pnl_Top.Size = new System.Drawing.Size(511, 77);
            this.pnl_Top.TabIndex = 3;
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Title.Location = new System.Drawing.Point(6, 19);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(62, 25);
            this.lbl_Title.TabIndex = 102;
            this.lbl_Title.Text = "标题";
            this.lbl_Title.Visible = false;
            // 
            // pic_Close
            // 
            this.pic_Close.Image = global::COD.Properties.Resources.gb02;
            this.pic_Close.Location = new System.Drawing.Point(432, 2);
            this.pic_Close.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pic_Close.Name = "pic_Close";
            this.pic_Close.Size = new System.Drawing.Size(77, 75);
            this.pic_Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_Close.TabIndex = 101;
            this.pic_Close.TabStop = false;
            this.pic_Close.Click += new System.EventHandler(this.pic_Close_Click);
            // 
            // pic_Quit
            // 
            this.pic_Quit.Image = global::COD.Properties.Resources.quxiao_04;
            this.pic_Quit.Location = new System.Drawing.Point(285, 197);
            this.pic_Quit.Name = "pic_Quit";
            this.pic_Quit.Size = new System.Drawing.Size(169, 69);
            this.pic_Quit.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_Quit.TabIndex = 6;
            this.pic_Quit.TabStop = false;
            this.pic_Quit.Click += new System.EventHandler(this.pic_Quit_Click);
            // 
            // pic_Ok
            // 
            this.pic_Ok.Image = global::COD.Properties.Resources.QD011;
            this.pic_Ok.Location = new System.Drawing.Point(50, 197);
            this.pic_Ok.Name = "pic_Ok";
            this.pic_Ok.Size = new System.Drawing.Size(169, 69);
            this.pic_Ok.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_Ok.TabIndex = 5;
            this.pic_Ok.TabStop = false;
            this.pic_Ok.Click += new System.EventHandler(this.pic_Ok_Click);
            // 
            // frm_Info_02
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(246)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(511, 280);
            this.Controls.Add(this.pic_Quit);
            this.Controls.Add(this.pic_Ok);
            this.Controls.Add(this.txt_Info);
            this.Controls.Add(this.pnl_Top);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frm_Info_02";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_Info_02";
            this.Load += new System.EventHandler(this.frm_Info_02_Load);
            this.pnl_Top.ResumeLayout(false);
            this.pnl_Top.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Quit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Ok)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_Info;
        private System.Windows.Forms.PictureBox pic_Close;
        private System.Windows.Forms.Panel pnl_Top;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.PictureBox pic_Ok;
        private System.Windows.Forms.PictureBox pic_Quit;
    }
}