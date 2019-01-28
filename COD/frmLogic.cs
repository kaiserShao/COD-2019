using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace COD
{
	public partial class frmLogic : Form
	{
		frmMain fFrmMain;
		cls_Main clsMain = new cls_Main();

		void osDelay(int ms)
		{
			Thread.Sleep(ms);
		}
		public frmLogic(frmMain vFrmMain)
		{
			fFrmMain = vFrmMain;
			//TaskSetArry = vTaskSetArry;
			//DigestionTubeStateArry = vDigestionTubeStateArry;

			InitializeComponent();

		}

		public int Port_Read_Val(int iAddRess, int iReg)
		{
			int iPortRead = getRegData(iAddRess, iReg);

			return iPortRead;
		}

		private void frmLogic_Load(object sender, EventArgs e)
		{
            this.Height = 0;
            this.Width = 0;
			// fFrmMain.sPort.Write();
			Thread ThreadTest = new Thread(new ThreadStart(Test));
			ThreadTest.Start();



		}



		private void btn_Write_Click(object sender, EventArgs e)
		{

		}

		private void TaskWriteInfo()
		{
			//  bool bl = fFrmMain.Port_Write(1,5,10)

			//参数顺序为   从机地址，寄存器地址，寄存器数值
			bool bl = fFrmMain.Port_Write(int.Parse(this.textBox1.Text), int.Parse(this.textBox2.Text), int.Parse(this.textBox3.Text));
			MessageBox.Show(bl.ToString());
		}
		private void TaskReadInfo()
		{
			//参数顺序为   从机地址，寄存器地址，期望值，超时时间

			int iInfo = fFrmMain.Port_Read(int.Parse(this.textBox4.Text), int.Parse(this.textBox5.Text), int.Parse(this.textBox6.Text), int.Parse(this.textBox7.Text));
			MessageBox.Show(iInfo.ToString());

		}
		private void btn_Red_Click(object sender, EventArgs e)
		{
			Thread oThreadRead = new Thread(new ThreadStart(this.TaskReadInfo));
			oThreadRead.Start();

		}
		private void button1_Click(object sender, EventArgs e)
		{

		}

		private void button2_Click(object sender, EventArgs e)
		{

		}

		private void buttonstop_Click(object sender, EventArgs e)
		{
			if (Cod_Mark == "正常结束")
			{
				Cod_Mark = "运行";
				buttonstop.Text = "运行";
			}
			else if (Cod_Mark == "运行")
			{
				Cod_Mark = "暂停";
				buttonstop.Text = "暂停";
			}
			else if (Cod_Mark == "暂停")
			{
				Cod_Mark = "运行";
				buttonstop.Text = "运行";
			}
		}


	}
}
