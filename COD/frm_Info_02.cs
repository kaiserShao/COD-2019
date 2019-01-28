using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace COD
{
    public partial class frm_Info_02 : Form
    {
        public string sChageMark = "0";
        string sTitle = "", sText = "";
        public frm_Info_02(string vTitle,string vText)
        {
            sTitle = vTitle;
            sText = vText;
            InitializeComponent();
        }

        private void frm_Info_02_Load(object sender, EventArgs e)
        {
            if (sTitle == "关闭程序")
            {
                this.BackColor = Color.FromArgb(192, 192, 255);
                txt_Info.BackColor = Color.FromArgb(192, 192, 255);
            }
           
            if (sText != "")
            {
                this.lbl_Title.Text = sTitle;
                this.lbl_Title.Visible = true;

            }
            this.txt_Info.Text = sText;
            this.pic_Quit.Focus();

            

        }

        private void pic_Ok_Click(object sender, EventArgs e)
        {
            sChageMark = "1";
            this.Close();
        }

        private void pic_Quit_Click(object sender, EventArgs e)
        {
            sChageMark = "0";
            this.Close();
        }

        private void pic_Close_Click(object sender, EventArgs e)
        {
            sChageMark = "0";
            this.Close();
        }
    }
}
