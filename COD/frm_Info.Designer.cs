namespace COD
{
    partial class frm_Info
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
            this.pnl_Top = new System.Windows.Forms.Panel();
            this.pic_Close = new System.Windows.Forms.PictureBox();
            this.txt_Info = new System.Windows.Forms.TextBox();
            this.lbl_Hide = new System.Windows.Forms.Label();
            this.pnl_Top.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Close)).BeginInit();
            this.SuspendLayout();
            // 
            // pnl_Top
            // 
            this.pnl_Top.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(207)))), ((int)(((byte)(167)))));
            this.pnl_Top.Controls.Add(this.pic_Close);
            this.pnl_Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_Top.Location = new System.Drawing.Point(0, 0);
            this.pnl_Top.Margin = new System.Windows.Forms.Padding(4);
            this.pnl_Top.Name = "pnl_Top";
            this.pnl_Top.Size = new System.Drawing.Size(529, 85);
            this.pnl_Top.TabIndex = 0;
            // 
            // pic_Close
            // 
            this.pic_Close.Image = global::COD.Properties.Resources.gb02;
            this.pic_Close.Location = new System.Drawing.Point(439, 4);
            this.pic_Close.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pic_Close.Name = "pic_Close";
            this.pic_Close.Size = new System.Drawing.Size(77, 75);
            this.pic_Close.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_Close.TabIndex = 101;
            this.pic_Close.TabStop = false;
            this.pic_Close.Click += new System.EventHandler(this.pic_Close_Click);
            // 
            // txt_Info
            // 
            this.txt_Info.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(246)))), ((int)(((byte)(252)))));
            this.txt_Info.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txt_Info.Enabled = false;
            this.txt_Info.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Info.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.txt_Info.Location = new System.Drawing.Point(31, 115);
            this.txt_Info.Margin = new System.Windows.Forms.Padding(4);
            this.txt_Info.Multiline = true;
            this.txt_Info.Name = "txt_Info";
            this.txt_Info.ReadOnly = true;
            this.txt_Info.Size = new System.Drawing.Size(443, 151);
            this.txt_Info.TabIndex = 2;
            this.txt_Info.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl_Hide
            // 
            this.lbl_Hide.AutoSize = true;
            this.lbl_Hide.Location = new System.Drawing.Point(355, 93);
            this.lbl_Hide.Name = "lbl_Hide";
            this.lbl_Hide.Size = new System.Drawing.Size(0, 15);
            this.lbl_Hide.TabIndex = 3;
            // 
            // frm_Info
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(159)))), ((int)(((byte)(246)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(529, 288);
            this.Controls.Add(this.lbl_Hide);
            this.Controls.Add(this.txt_Info);
            this.Controls.Add(this.pnl_Top);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frm_Info";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frm_Info";
            this.Load += new System.EventHandler(this.frm_Info_Load);
            this.pnl_Top.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Close)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnl_Top;
        private System.Windows.Forms.PictureBox pic_Close;
        private System.Windows.Forms.TextBox txt_Info;
        private System.Windows.Forms.Label lbl_Hide;
    }
}