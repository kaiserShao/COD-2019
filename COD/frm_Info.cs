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
    public partial class frm_Info : Form
    {
        string sInfo = "";
        public frm_Info(string vInfo)
        {
            sInfo = vInfo;
            InitializeComponent();
        }

        private void pic_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_Info_Load(object sender, EventArgs e)
        {
            this.txt_Info.Text = sInfo;
            this.pic_Close.Focus();
        }
    }
}
