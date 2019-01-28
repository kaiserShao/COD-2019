using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace COD
{
    public partial class frm_Num : Form
    {
        public string sNUM = "0",sType="2";
        private string sNum1 = "",sNum2="";
        private string sState = "0";// 0表示首次进入，1表示已经操作
        //sType  1表示整数，2表示带小数点数据
        
        public frm_Num(string vNum,string vType,string vNum01,string vNum02)
        {
            sNUM = vNum;
            sType = vType;
            sNum1 = vNum01;
            sNum2 = vNum02;

            InitializeComponent();
        }
        cls_Main clsData = new cls_Main();

        private void frm_Num_Load(object sender, EventArgs e)
        {
            
            string sValInfo01 = "", sValInfo02 = "";
            if (sNum1 != "")
            {
                sValInfo01 = "数值下限：【" + sNum1 + "】";

            }
            if (sNum2 != "")
            {
                sValInfo02 =  "数值上限：【" + sNum2 + "】";
            }
            this.lbl_Info_01.Text = sValInfo01;
            this.lbl_Info_02.Text = sValInfo02;

            this.txtShuRi.Text = sNUM;
            if (sType == "1")
            {
                this.btn_A.Enabled = false;
            }
            this.btn_OK.Focus();
        }

        private void btn_1_Click(object sender, EventArgs e)
        {
            if (sState == "0")
            {
                this.txtShuRi.Text = "1";
                sState = "1";
            }
            else
            {
                this.txtShuRi.Text = this.txtShuRi.Text + "1";
            }
        }

        private void btn_2_Click(object sender, EventArgs e)
        {
            if (sState == "0")
            {
                this.txtShuRi.Text = "2";
                sState = "1";
            }
            else
            {
                this.txtShuRi.Text = this.txtShuRi.Text + "2";
            }
        }

        private void btn_3_Click(object sender, EventArgs e)
        {
            if (sState == "0")
            {
                this.txtShuRi.Text = "3";
                sState = "1";
            }
            else
            {
                this.txtShuRi.Text = this.txtShuRi.Text + "3";
            }
        }

        private void btn_4_Click(object sender, EventArgs e)
        {
            if (sState == "0")
            {
                this.txtShuRi.Text = "4";
                sState = "1";
            }
            else
            {
                this.txtShuRi.Text = this.txtShuRi.Text + "4";
            }
        }

        private void btn_5_Click(object sender, EventArgs e)
        {
            if (sState == "0")
            {
                this.txtShuRi.Text = "5";
                sState = "1";
            }
            else
            {
                this.txtShuRi.Text = this.txtShuRi.Text + "5";
            }
        }

        private void btn_6_Click(object sender, EventArgs e)
        {
            if (sState == "0")
            {
                this.txtShuRi.Text = "6";
                sState = "1";
            }
            else
            {
                this.txtShuRi.Text = this.txtShuRi.Text + "6";
            }
        }
    
        private void btn_7_Click(object sender, EventArgs e)
        {
            if (sState == "0")
            {
                this.txtShuRi.Text = "7";
                sState = "1";
            }
            else
            {
                this.txtShuRi.Text = this.txtShuRi.Text + "7";
            }
        }

        private void btn_8_Click(object sender, EventArgs e)
        {
            if (sState == "0")
            {
                this.txtShuRi.Text = "8";
                sState = "1";
            }
            else
            {
                this.txtShuRi.Text = this.txtShuRi.Text + "8";
            }
        }
        private void btn_9_Click(object sender, EventArgs e)
        {
            if (sState == "0")
            {
                this.txtShuRi.Text = "9";
                sState = "1";
            }
            else
            {
                this.txtShuRi.Text = this.txtShuRi.Text + "9";
            }
        }

        private void btn_D1_Click(object sender, EventArgs e)
        {
            if (this.txtShuRi.Text.Length > 0)
            {
                this.txtShuRi.Text = this.txtShuRi.Text.Substring(0, this.txtShuRi.Text.Length - 1);
            }
        }

        private void btn_Del_Click(object sender, EventArgs e)
        {
            this.txtShuRi.Text = "";
            //this.Close();
        }
        private bool BoolNum(string sVal)
        {
            try
            {
                double var1 = Convert.ToDouble(sVal);
                return true;
            }
            catch
            {
                return false;
            }

        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            string sMark = "0";
         
            if (this.txtShuRi.Text != "")
            {
                if (sType != "3")
                {

                    if (clsData.IsNumber(this.txtShuRi.Text))
                    {
                        if (BoolNum(this.txtShuRi.Text))
                        {
                            if (sNum1 !="")
                            {
                                if (double.Parse(this.txtShuRi.Text) < double.Parse(sNum1))
                                {
                                   frm_Info frm = new frm_Info("必须大于或等于数值下限！");
                                   frm.ShowDialog();

                                    sMark = "1";

                                    
                                }
                               
                            }

                            if (sMark == "0")
                            {
                                if (sNum2 != "")
                                {
                                    if (double.Parse(this.txtShuRi.Text) > double.Parse(sNum2))
                                    {
                                        frm_Info frm = new frm_Info("必须小于或等于数值上限！");
                                        frm.ShowDialog();

                                        sMark = "1";


                                    }
                                }
 
                            }

                            if (sMark == "0")
                            {

                                  sNUM = this.txtShuRi.Text;
                                this.Close(); 
                            }
                      
                        }
                        else
                        {
                            frm_Info frm = new frm_Info("必须为数字！");
                            frm.ShowDialog();
                        }

                    }
                    else
                    {

                       frm_Info frm = new frm_Info("必须为数字！");
                       frm.ShowDialog();
                        this.txtShuRi.Focus();
                    }
                }
                else  //IP地址
                {
                    sNUM = this.txtShuRi.Text;
                    this.Close();

                }
            }
            else
            {
               
                frm_Info frm = new frm_Info("数字不能为空！");
                frm.ShowDialog();
                this.txtShuRi.Focus();
            }
        }

        private void btn_A_Click(object sender, EventArgs e)
        {
            if (sState == "0")
            {
                this.txtShuRi.Text = ".";
                sState = "1";
            }
            else
            {
                this.txtShuRi.Text = this.txtShuRi.Text + ".";
            }
        }

        private void btn_0_Click(object sender, EventArgs e)
        {
            if (sState == "0")
            {
                this.txtShuRi.Text = "0";
                sState = "1";
            }
            else
            {
                this.txtShuRi.Text = this.txtShuRi.Text + "0";
            }
        }

        private void btn_B_Click(object sender, EventArgs e)
        {
            if (sState == "0")
            {
                this.txtShuRi.Text = "-";
                sState = "1";
            }
            else
            {
                this.txtShuRi.Text = this.txtShuRi.Text + "-";
            }
        }

        private void pic_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
