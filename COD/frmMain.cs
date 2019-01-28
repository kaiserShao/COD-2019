using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Threading;
using DMSql;
using Modbus.Device;


namespace COD
{
    public partial class frmMain : Form
    {
        string sPortName = "COM1", sBaudRate = "9600", sDataBits = "8", sParity = "0", sStopBits="1";
        public SerialPort sPort = new SerialPort();
        cls_Main clsMain = new cls_Main();
		//cls_Logic clsLogic = new cls_Logic();
        DataTable dtTaskInfo;
		//int  isendLineNumber= 0,  iTX_Len=0;
        private string sConnStr, sRelultMark="0";
        private string sFrm_Loginc_Mark = "0",sMark="0";

        private System.Threading.Timer timerCommdNew;
        private delegate void FlushClient(string sInfo);

        public static ushort[] Data_ZDH_1 = new ushort[100];
        /* 自动化 */
        public static ushort[] Data_JXB_2 = new ushort[100];
        /* 机械臂 */
        public static ushort[] Data_GD_3 = new ushort[100];
        /* 分光度 */
        public static ushort[] Data_XJ_4 = new ushort[100];
        /* 消解 */
        public static string[,] Cod_Record = new string[32, 30];
        /* 过程数据 */
        public static string[,] Cod_Result = new string[32, 30];
        /* 结果数据 */

     
      
        public string sCur_Commd_High = "", sCur_Commd_High_One = "", sCur_Commd_Low = "", sCur_Commd_Low_One="";



        public static SerialPort serialPort1 = new SerialPort(); //声明串口

        public IModbusSerialMaster master ;//连接方式是如RTU
        //串口的初始化如下
        void Modbus_Init()
        {

            master = ModbusSerialMaster.CreateRtu(serialPort1);//连接方式是如RTU
            master.Transport.ReadTimeout = 100;
            master.Transport.WriteTimeout = 100;
            master.Transport.Retries = 0;
            master.Transport.WaitToRetryMilliseconds = 200;
            if (!serialPort1.IsOpen)
            {
                serialPort1.PortName = sPortName;
                serialPort1.BaudRate = int.Parse(sBaudRate); //串口通信波特率 
                serialPort1.DataBits = int.Parse(sDataBits); //数据位 
                if (sParity == "0")
                {
                    serialPort1.Parity = Parity.None; //无校验 
                }
                else if (sParity == "1")
                {
                    serialPort1.Parity = Parity.Odd; //奇 
                }
                else if (sParity == "1")
                {
                    serialPort1.Parity = Parity.Even; // 偶 
                }

                if (sStopBits == "0")
                {
                    serialPort1.StopBits = StopBits.None; //无 
                }
                else if (sStopBits == "1")
                {
                    serialPort1.StopBits = StopBits.One; //1 
                }
                else if (sStopBits == "2")
                {
                    serialPort1.StopBits = StopBits.Two; //2
                }
                 

                serialPort1.Open();//打开串口

               
               

            }




        }

        private void timerCommdStart()
        {
            timerCommdNew = new System.Threading.Timer(new TimerCallback(CommdInfo), null, 0, 50);
        }

        public frmMain()
        {
            InitializeComponent();

            this.DoubleBuffered = true;//设置本窗体
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲


            
        }

        protected override CreateParams CreateParams //使背景加载时不闪烁
        {
            get
            {
                CreateParams paras = base.CreateParams;
                paras.ExStyle |= 0x02000000;
                return paras;
            }
        }

        private void RtStatusInit()
        {
            frmLogic.RTStatus.Exist = new frmLogic.enumLevelStatus[(int)frmLogic.enumPumpType.PumpMax];
            frmLogic.RTStatus.LevelSwitch = new frmLogic.enumLevelStatus[(int)frmLogic.enumPumpType.PumpMax];
            frmLogic.RTStatus.PumpPosition = new int[(int)frmLogic.enumPumpType.PumpMax];
            frmLogic.RTStatus.RbootArmPosition = new int[(int)frmLogic.enumPumpType.PumpMax];
            frmLogic.RTStatus.SampleAmount = new int[(int)frmLogic.enumPumpType.PumpMax];
            frmLogic.RTStatus.TaskIsRuning = new bool[100];
			frmLogic.RTStatus.PumpSpeed = new int[(int)frmLogic.enumPumpType.PumpMax];

	
		
		
		
			

        }
        private void DigestionTubeStateInit()
        {
            int i = 0;

            frmLogic.DigestionTubeState.DigestnID = new int[frmLogic.DigestionTubeMax];
            frmLogic.DigestionTubeState.SampleFrom = new int[frmLogic.DigestionTubeMax];
            frmLogic.DigestionTubeState.NumWithInGroup = new int[frmLogic.DigestionTubeMax];
			frmLogic.DigestionTubeState.Range = new frmLogic.enumRangeState[frmLogic.DigestionTubeMax];
            frmLogic.DigestionTubeState.ConcentrationValue = new int[frmLogic.DigestionTubeMax];
            frmLogic.DigestionTubeState.RelultIsValid = new bool[frmLogic.DigestionTubeMax];
           
            for(i=0;i<frmLogic.DigestionTubeMax;i++)
            frmLogic.DigestionTubeState.RelultIsValid[i] =false;

            frmLogic.DigestionTubeState.DigestnIsExist = new bool[frmLogic.DigestionTubeMax];
            for (i = 0; i < frmLogic.DigestionTubeMax; i++)
                frmLogic.DigestionTubeState.DigestnIsExist[i] = true;
             
        }
        private void TaskSetInit()
        {
            frmLogic.TaskSet.TubeNum = new int[frmLogic.SampleTubeMax];
            frmLogic.TaskSet.ExperimentNum = new int[frmLogic.SampleTubeMax];
			frmLogic.TaskSet.ConcentrationEstimate = new frmLogic.enumRangeState[frmLogic.DigestionTubeMax];
            frmLogic.TaskSet.DissolutionTime = new int[frmLogic.SampleTubeMax];
            frmLogic.TaskSet.ConcentrationMeasure = new int[frmLogic.SampleTubeMax];
 
        }
        private void ProcessCurrentInit()
        {
            frmLogic.ProcessCurrent.Exist = new bool[5];

        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            this.lbl_YX_Info.Text = "";
           sPortName= Properties.Settings.Default.PortName;
           sBaudRate = Properties.Settings.Default.BaudRate;
           sDataBits = Properties.Settings.Default.DataBits;
           sParity = Properties.Settings.Default.Parity;
           sStopBits = Properties.Settings.Default.StopBits;

           this.pic_Close.Left = this.Width - this.pic_Close.Width - 30;
           this.pic_Min.Left = this.pic_Close.Left - this.pic_Min.Width - 20;


           this.pic_Task_Stop.Image = COD.Properties.Resources.zt_01;
           this.TaskSetInit();
           this.RtStatusInit();
           this.DigestionTubeStateInit();
           this.ProcessCurrentInit();
           this.Modbus_Init();

           this.lbl_Water.Top = 10;
           this.lbl_XJQ.Top = this.lbl_Water.Top;


           this.pnl_Test.Top = this.lbl_Water.Top + this.lbl_Water.Height + 10;
           this.pnl_Work.Top = this.pnl_Test.Top;

           this.pnl_Work.Left = 60;
           this.pnl_Work.Height = this.pnl_Main.Height - this.lbl_Water.Height - 20 - 10;
          // this.pic_J_00.Height = this.pnl_Work.Height ;
           this.pic_J_01.Top = 80;
       

           this.pic_J_04.Top = this.pnl_Work.Height - this.pic_J_04.Height - 80;

           this.pic_J_03.Top = this.pic_J_04.Top - this.pic_J_03.Height;

           this.pic_J_02.Top = (this.pic_J_03.Top - 80) / 2  + 80 ;




           this.pnl_Test.Height = this.pnl_Work.Height;
           this.pnl_Test.Left = this.pnl_Work.Left + this.pnl_Work.Width + 40;
           this.pnl_Test.Width = (this.pnl_Main.Width - this.pnl_Test.Left - 60 - 40) / 2;

           this.lbl_Water.Left = this.pnl_Test.Left + this.pnl_Test.Width / 2 - this.lbl_Water.Width / 2;


           this.pnl_XJQ.Top = this.pnl_Test.Top;
           this.pnl_XJQ.Height = this.pnl_Work.Height;
           this.pnl_XJQ.Left = this.pnl_Test.Left + this.pnl_Test.Width + 40;
           this.pnl_XJQ.Width = this.pnl_Test.Width;


           this.lbl_XJQ.Left = this.pnl_XJQ.Left + this.pnl_XJQ.Width / 2 - this.lbl_XJQ.Width / 2;

           this.SetTestLocation();
           this.SetXJQLocation();

        

           try
           {
               sConnStr = Properties.Settings.Default.ConnStr;
               DMSql.DBConnectionString.ConnString = clsMain.Decrypt(sConnStr, "connectStr");
           }
           catch (Exception ee)
           {
               MessageBox.Show("数据库连接异常！请检查数据库。");
           }

           this.Set_uTaskSetArry_init();


           this.pic_Task_Add.Enabled = true;
           this.pic_Task_Stop.Enabled = false;



        }

        private void Set_uTaskSetArry_init()
        {
            dtTaskInfo = DbHelper.ExecuteQueryTable("SELECT * FROM D_TestInfo WHERE Mark ='0' ORDER BY TubeNum");

            if (dtTaskInfo.Rows.Count > 0)
            {
                for (int i = 0; i < dtTaskInfo.Rows.Count; i++)
                {
                    frmLogic.TaskSet.TubeNum[int.Parse(dtTaskInfo.Rows[i]["TubeNum"].ToString()) - 1] = int.Parse(dtTaskInfo.Rows[i]["TubeNum"].ToString());

                    frmLogic.TaskSet.ExperimentNum[int.Parse(dtTaskInfo.Rows[i]["TubeNum"].ToString()) - 1] = int.Parse(dtTaskInfo.Rows[i]["ExperimentNum"].ToString());

					frmLogic.TaskSet.ConcentrationEstimate[int.Parse(dtTaskInfo.Rows[i]["TubeNum"].ToString()) - 1] = (frmLogic.enumRangeState)int.Parse(dtTaskInfo.Rows[i]["ConcentrationEstimate"].ToString());

                }
            }

            this.SetTaskArry();
        }


        #region
        private void SetTestLocation()
        {

            int iCol =20;  //列间距
            int iRow=20;  //行间距
            int iWidth= (this.pnl_Test.Width - iCol - iCol -5*20)/6;
            
            int iHeight = (this.pnl_Test.Height - iCol - iCol - 5 * 20) / 6;

            this.pic_W_01.Top = iRow;
            this.pic_W_01.Left = iCol + iCol + iWidth;
            this.pic_W_01.Width = iWidth;
            this.pic_W_01.Height = iHeight;


            this.pic_W_02.Top = iRow;
            this.pic_W_02.Left = this.pic_W_01.Left + this.pic_W_01.Width + iCol;
            this.pic_W_02.Width = iWidth;
            this.pic_W_02.Height = iHeight;

            this.pic_W_03.Top = iRow;
            this.pic_W_03.Left = this.pic_W_02.Left + this.pic_W_02.Width + iCol;
            this.pic_W_03.Width = iWidth;
            this.pic_W_03.Height = iHeight;

            this.pic_W_04.Top = iRow;
            this.pic_W_04.Left = this.pic_W_03.Left + this.pic_W_03.Width + iCol;
            this.pic_W_04.Width = iWidth;
            this.pic_W_04.Height = iHeight;

            //***************   1    ********************

            this.pic_W_05.Top = this.pic_W_01.Top + iHeight + iRow;
            this.pic_W_05.Left = iCol;
            this.pic_W_05.Width = iWidth;
            this.pic_W_05.Height = iHeight;

            this.pic_W_06.Top = this.pic_W_05.Top;
            this.pic_W_06.Left = this.pic_W_05.Left + this.pic_W_05.Width + iCol;
            this.pic_W_06.Width = iWidth;
            this.pic_W_06.Height = iHeight;

            this.pic_W_07.Top = this.pic_W_05.Top;
            this.pic_W_07.Left = this.pic_W_06.Left + this.pic_W_06.Width + iCol;
            this.pic_W_07.Width = iWidth;
            this.pic_W_07.Height = iHeight;

            this.pic_W_08.Top = this.pic_W_05.Top;
            this.pic_W_08.Left = this.pic_W_07.Left + this.pic_W_07.Width + iCol;
            this.pic_W_08.Width = iWidth;
            this.pic_W_08.Height = iHeight;


            this.pic_W_09.Top = this.pic_W_05.Top;
            this.pic_W_09.Left = this.pic_W_08.Left + this.pic_W_08.Width + iCol;
            this.pic_W_09.Width = iWidth;
            this.pic_W_09.Height = iHeight;


            this.pic_W_10.Top = this.pic_W_05.Top;
            this.pic_W_10.Left = this.pic_W_09.Left + this.pic_W_09.Width + iCol;
            this.pic_W_10.Width = iWidth;
            this.pic_W_10.Height = iHeight;


            //***************   2   ********************

            this.pic_W_11.Top = this.pic_W_05.Top + iHeight + iRow;
            this.pic_W_11.Left = iCol;
            this.pic_W_11.Width = iWidth;
            this.pic_W_11.Height = iHeight;

            this.pic_W_12.Top = this.pic_W_11.Top;
            this.pic_W_12.Left = this.pic_W_11.Left + this.pic_W_11.Width + iCol;
            this.pic_W_12.Width = iWidth;
            this.pic_W_12.Height = iHeight;

            this.pic_W_13.Top = this.pic_W_11.Top;
            this.pic_W_13.Left = this.pic_W_12.Left + this.pic_W_12.Width + iCol;
            this.pic_W_13.Width = iWidth;
            this.pic_W_13.Height = iHeight;

            this.pic_W_14.Top = this.pic_W_11.Top;
            this.pic_W_14.Left = this.pic_W_13.Left + this.pic_W_13.Width + iCol;
            this.pic_W_14.Width = iWidth;
            this.pic_W_14.Height = iHeight;


            this.pic_W_15.Top = this.pic_W_11.Top;
            this.pic_W_15.Left = this.pic_W_14.Left + this.pic_W_14.Width + iCol;
            this.pic_W_15.Width = iWidth;
            this.pic_W_15.Height = iHeight;


            this.pic_W_16.Top = this.pic_W_11.Top;
            this.pic_W_16.Left = this.pic_W_15.Left + this.pic_W_15.Width + iCol;
            this.pic_W_16.Width = iWidth;
            this.pic_W_16.Height = iHeight;

          //***************   3  ********************
            this.pic_W_17.Top = this.pic_W_11.Top + iHeight + iRow;
            this.pic_W_17.Left = iCol;
            this.pic_W_17.Width = iWidth;
            this.pic_W_17.Height = iHeight;

            this.pic_W_18.Top = this.pic_W_17.Top;
            this.pic_W_18.Left = this.pic_W_17.Left + this.pic_W_17.Width + iCol;
            this.pic_W_18.Width = iWidth;
            this.pic_W_18.Height = iHeight;

            this.pic_W_19.Top = this.pic_W_17.Top;
            this.pic_W_19.Left = this.pic_W_18.Left + this.pic_W_18.Width + iCol;
            this.pic_W_19.Width = iWidth;
            this.pic_W_19.Height = iHeight;

            this.pic_W_20.Top = this.pic_W_17.Top;
            this.pic_W_20.Left = this.pic_W_19.Left + this.pic_W_19.Width + iCol;
            this.pic_W_20.Width = iWidth;
            this.pic_W_20.Height = iHeight;


            this.pic_W_21.Top = this.pic_W_17.Top;
            this.pic_W_21.Left = this.pic_W_20.Left + this.pic_W_20.Width + iCol;
            this.pic_W_21.Width = iWidth;
            this.pic_W_21.Height = iHeight;


            this.pic_W_22.Top = this.pic_W_17.Top;
            this.pic_W_22.Left = this.pic_W_21.Left + this.pic_W_21.Width + iCol;
            this.pic_W_22.Width = iWidth;
            this.pic_W_22.Height = iHeight;
            //***************   4 ********************

            this.pic_W_23.Top = this.pic_W_17.Top + iHeight + iRow;
            this.pic_W_23.Left = iCol;
            this.pic_W_23.Width = iWidth;
            this.pic_W_23.Height = iHeight;

            this.pic_W_24.Top = this.pic_W_23.Top;
            this.pic_W_24.Left = this.pic_W_23.Left + this.pic_W_23.Width + iCol;
            this.pic_W_24.Width = iWidth;
            this.pic_W_24.Height = iHeight;

            this.pic_W_25.Top = this.pic_W_23.Top;
            this.pic_W_25.Left = this.pic_W_24.Left + this.pic_W_24.Width + iCol;
            this.pic_W_25.Width = iWidth;
            this.pic_W_25.Height = iHeight;

            this.pic_W_26.Top = this.pic_W_23.Top;
            this.pic_W_26.Left = this.pic_W_25.Left + this.pic_W_25.Width + iCol;
            this.pic_W_26.Width = iWidth;
            this.pic_W_26.Height = iHeight;


            this.pic_W_27.Top = this.pic_W_23.Top;
            this.pic_W_27.Left = this.pic_W_26.Left + this.pic_W_26.Width + iCol;
            this.pic_W_27.Width = iWidth;
            this.pic_W_27.Height = iHeight;


            this.pic_W_28.Top = this.pic_W_23.Top;
            this.pic_W_28.Left = this.pic_W_27.Left + this.pic_W_27.Width + iCol;
            this.pic_W_28.Width = iWidth;
            this.pic_W_28.Height = iHeight;
            //***************   4 ********************


            this.pic_W_29.Top = this.pic_W_24.Top + iHeight + iRow;
            this.pic_W_29.Left = this.pic_W_24.Left ;
            this.pic_W_29.Width = iWidth;
            this.pic_W_29.Height = iHeight;


            this.pic_W_30.Top = this.pic_W_29.Top;
            this.pic_W_30.Left = this.pic_W_29.Left + this.pic_W_29.Width + iCol;
            this.pic_W_30.Width = iWidth;
            this.pic_W_30.Height = iHeight;

            this.pic_W_31.Top = this.pic_W_29.Top;
            this.pic_W_31.Left = this.pic_W_30.Left + this.pic_W_30.Width + iCol;
            this.pic_W_31.Width = iWidth;
            this.pic_W_31.Height = iHeight;

            this.pic_W_32.Top = this.pic_W_29.Top;
            this.pic_W_32.Left = this.pic_W_31.Left + this.pic_W_31.Width + iCol;
            this.pic_W_32.Width = iWidth;
            this.pic_W_32.Height = iHeight;
        }
        private void SetXJQLocation()
        {

            int iCol = 20;  //列间距
            int iRow = 20;  //行间距
            int iWidth = (this.pnl_Test.Width - iCol - iCol - 5 * 20) / 6;

            int iHeight = (this.pnl_Test.Height - iCol - iCol - 5 * 20) / 6;

            this.pic_X_01.Top = iRow;
            this.pic_X_01.Left = iCol + iCol + iWidth;
            this.pic_X_01.Width = iWidth;
            this.pic_X_01.Height = iHeight;


            this.pic_X_02.Top = iRow;
            this.pic_X_02.Left = this.pic_X_01.Left + this.pic_X_01.Width + iCol;
            this.pic_X_02.Width = iWidth;
            this.pic_X_02.Height = iHeight;

            this.pic_X_03.Top = iRow;
            this.pic_X_03.Left = this.pic_X_02.Left + this.pic_X_02.Width + iCol;
            this.pic_X_03.Width = iWidth;
            this.pic_X_03.Height = iHeight;

            this.pic_X_04.Top = iRow;
            this.pic_X_04.Left = this.pic_X_03.Left + this.pic_X_03.Width + iCol;
            this.pic_X_04.Width = iWidth;
            this.pic_X_04.Height = iHeight;

            //***************   1    ********************

            this.pic_X_05.Top = this.pic_X_01.Top + iHeight + iRow;
            this.pic_X_05.Left = iCol;
            this.pic_X_05.Width = iWidth;
            this.pic_X_05.Height = iHeight;

            this.pic_X_06.Top = this.pic_X_05.Top;
            this.pic_X_06.Left = this.pic_X_05.Left + this.pic_X_05.Width + iCol;
            this.pic_X_06.Width = iWidth;
            this.pic_X_06.Height = iHeight;

            this.pic_X_07.Top = this.pic_X_05.Top;
            this.pic_X_07.Left = this.pic_X_06.Left + this.pic_X_06.Width + iCol;
            this.pic_X_07.Width = iWidth;
            this.pic_X_07.Height = iHeight;

            this.pic_X_08.Top = this.pic_X_05.Top;
            this.pic_X_08.Left = this.pic_X_07.Left + this.pic_X_07.Width + iCol;
            this.pic_X_08.Width = iWidth;
            this.pic_X_08.Height = iHeight;


            this.pic_X_09.Top = this.pic_X_05.Top;
            this.pic_X_09.Left = this.pic_X_08.Left + this.pic_X_08.Width + iCol;
            this.pic_X_09.Width = iWidth;
            this.pic_X_09.Height = iHeight;


            this.pic_X_10.Top = this.pic_X_05.Top;
            this.pic_X_10.Left = this.pic_X_09.Left + this.pic_X_09.Width + iCol;
            this.pic_X_10.Width = iWidth;
            this.pic_X_10.Height = iHeight;


            //***************   2   ********************

            this.pic_X_11.Top = this.pic_X_05.Top + iHeight + iRow;
            this.pic_X_11.Left = iCol;
            this.pic_X_11.Width = iWidth;
            this.pic_X_11.Height = iHeight;

            this.pic_X_12.Top = this.pic_X_11.Top;
            this.pic_X_12.Left = this.pic_X_11.Left + this.pic_X_11.Width + iCol;
            this.pic_X_12.Width = iWidth;
            this.pic_X_12.Height = iHeight;

            this.pic_X_13.Top = this.pic_X_11.Top;
            this.pic_X_13.Left = this.pic_X_12.Left + this.pic_X_12.Width + iCol;
            this.pic_X_13.Width = iWidth;
            this.pic_X_13.Height = iHeight;

            this.pic_X_14.Top = this.pic_X_11.Top;
            this.pic_X_14.Left = this.pic_X_13.Left + this.pic_X_13.Width + iCol;
            this.pic_X_14.Width = iWidth;
            this.pic_X_14.Height = iHeight;


            this.pic_X_15.Top = this.pic_X_11.Top;
            this.pic_X_15.Left = this.pic_X_14.Left + this.pic_X_14.Width + iCol;
            this.pic_X_15.Width = iWidth;
            this.pic_X_15.Height = iHeight;


            this.pic_X_16.Top = this.pic_X_11.Top;
            this.pic_X_16.Left = this.pic_X_15.Left + this.pic_X_15.Width + iCol;
            this.pic_X_16.Width = iWidth;
            this.pic_X_16.Height = iHeight;

            //***************   3  ********************
            this.pic_X_17.Top = this.pic_X_11.Top + iHeight + iRow;
            this.pic_X_17.Left = iCol;
            this.pic_X_17.Width = iWidth;
            this.pic_X_17.Height = iHeight;

            this.pic_X_18.Top = this.pic_X_17.Top;
            this.pic_X_18.Left = this.pic_X_17.Left + this.pic_X_17.Width + iCol;
            this.pic_X_18.Width = iWidth;
            this.pic_X_18.Height = iHeight;

            this.pic_X_19.Top = this.pic_X_17.Top;
            this.pic_X_19.Left = this.pic_X_18.Left + this.pic_X_18.Width + iCol;
            this.pic_X_19.Width = iWidth;
            this.pic_X_19.Height = iHeight;

            this.pic_X_20.Top = this.pic_X_17.Top;
            this.pic_X_20.Left = this.pic_X_19.Left + this.pic_X_19.Width + iCol;
            this.pic_X_20.Width = iWidth;
            this.pic_X_20.Height = iHeight;


            this.pic_X_21.Top = this.pic_X_17.Top;
            this.pic_X_21.Left = this.pic_X_20.Left + this.pic_X_20.Width + iCol;
            this.pic_X_21.Width = iWidth;
            this.pic_X_21.Height = iHeight;


            this.pic_X_22.Top = this.pic_X_17.Top;
            this.pic_X_22.Left = this.pic_X_21.Left + this.pic_X_21.Width + iCol;
            this.pic_X_22.Width = iWidth;
            this.pic_X_22.Height = iHeight;
            //***************   4 ********************

            this.pic_X_23.Top = this.pic_X_17.Top + iHeight + iRow;
            this.pic_X_23.Left = iCol;
            this.pic_X_23.Width = iWidth;
            this.pic_X_23.Height = iHeight;

            this.pic_X_24.Top = this.pic_X_23.Top;
            this.pic_X_24.Left = this.pic_X_23.Left + this.pic_X_23.Width + iCol;
            this.pic_X_24.Width = iWidth;
            this.pic_X_24.Height = iHeight;

            this.pic_X_25.Top = this.pic_X_23.Top;
            this.pic_X_25.Left = this.pic_X_24.Left + this.pic_X_24.Width + iCol;
            this.pic_X_25.Width = iWidth;
            this.pic_X_25.Height = iHeight;

            this.pic_X_26.Top = this.pic_X_23.Top;
            this.pic_X_26.Left = this.pic_X_25.Left + this.pic_X_25.Width + iCol;
            this.pic_X_26.Width = iWidth;
            this.pic_X_26.Height = iHeight;


            this.pic_X_27.Top = this.pic_X_23.Top;
            this.pic_X_27.Left = this.pic_X_26.Left + this.pic_X_26.Width + iCol;
            this.pic_X_27.Width = iWidth;
            this.pic_X_27.Height = iHeight;


            this.pic_X_28.Top = this.pic_X_23.Top;
            this.pic_X_28.Left = this.pic_X_27.Left + this.pic_X_27.Width + iCol;
            this.pic_X_28.Width = iWidth;
            this.pic_X_28.Height = iHeight;
            //***************   4 ********************


            this.pic_X_29.Top = this.pic_X_24.Top + iHeight + iRow;
            this.pic_X_29.Left = this.pic_X_24.Left;
            this.pic_X_29.Width = iWidth;
            this.pic_X_29.Height = iHeight;


            this.pic_X_30.Top = this.pic_X_29.Top;
            this.pic_X_30.Left = this.pic_X_29.Left + this.pic_X_29.Width + iCol;
            this.pic_X_30.Width = iWidth;
            this.pic_X_30.Height = iHeight;

            this.pic_X_31.Top = this.pic_X_29.Top;
            this.pic_X_31.Left = this.pic_X_30.Left + this.pic_X_30.Width + iCol;
            this.pic_X_31.Width = iWidth;
            this.pic_X_31.Height = iHeight;

            this.pic_X_32.Top = this.pic_X_29.Top;
            this.pic_X_32.Left = this.pic_X_31.Left + this.pic_X_31.Width + iCol;
            this.pic_X_32.Width = iWidth;
            this.pic_X_32.Height = iHeight;
        }
        #endregion







        private void frmLogic_Info()
        {
            frmLogic frm = new frmLogic(this);
            frm.Show();
           
        }
       


        public int Port_WriteiAddRess;
        public int Port_WriteiReg;
        public int Port_WriteiRegVal;
        public bool Port_Write(int iAddRess, int iReg, int iRegVal)
        {
            lock (this)
            {
                while (cls_Main.sCommdWrite_Mark) ;

                Port_WriteiAddRess = iAddRess;
                Port_WriteiReg = iReg;
                Port_WriteiRegVal = iRegVal;
                cls_Main.Commd_Mark = "";
                while (cls_Main.sCommdRead_Mark) ;
                cls_Main.sCommdWrite_Mark = true;
                while (cls_Main.sCommdWrite_Mark) ;

                return WriteOK;
            }
        }
        public int Port_Read(int iAddRess, int iReg, int iRegVal, int iTimeOut)
        {
            int iPortRead = 65536;   //  65536超时   
            int iTime = 0, iCount = 0;


            while (iTime <= iTimeOut * 1000)
            {

                if (frmLogic.getRegData(iAddRess, iReg) == iRegVal)
                {
                    iPortRead = iRegVal;
                    break;
                }
                Thread.Sleep(100);
                iCount++;

                iTime = iCount * 100;
            }

            return iPortRead;
        }


        public static bool WriteOK = true;
        public static bool ReadOK = true;
        public static bool WriteFinish = true;
        public static bool ReadFinish = true;
        #region
        private void CommdInfo(object sender)
        {
            Application.DoEvents();
            //if (WriteOK && ReadOK)
            {
                if (cls_Main.Commd_Mark == "COM")
                {
                    if (ReadFinish)
                    {
                        ReadFinish = false;
                        if (sCur_Commd_Low != "")
                        {

                            while (cls_Main.sCommdWrite_Mark) ;
                            cls_Main.sCommdRead_Mark = true;
                            if (sCur_Commd_Low.IndexOf(",") >= 0)
                            {
                                sCur_Commd_Low_One = sCur_Commd_Low.Substring(0, sCur_Commd_Low.IndexOf(","));

                                try
                                {
                                    if (sCur_Commd_Low_One == "ALL_ZDH")
                                    {

                                        ReadOK = true;
                                        Data_ZDH_1 = master.ReadHoldingRegisters(1, 0, 40);


                                    }
                                    else if (sCur_Commd_Low_One == "ALL_JXB")
                                    {

                                        ReadOK = true;
                                        Data_JXB_2 = master.ReadHoldingRegisters(2, 0, 20);

                                    }
                                    else if (sCur_Commd_Low_One == "ALL_GD")
                                    {

                                        ReadOK = true;
                                        Data_GD_3 = master.ReadHoldingRegisters(3, 0, 80);

                                    }
                                    else if (sCur_Commd_Low_One == "ALL_XJ")
                                    {
                                        ReadOK = true;
                                        Data_XJ_4 = master.ReadHoldingRegisters(4, 0, 40);

                                    }
                                }
                                catch (Exception ex)
                                {
                                    WriteOK = false;
                                    if (ex.Source.Equals("System"))
                                    {

                                        if (txtDataShow.InvokeRequired)
                                        {
                                            FlushClient fc = new FlushClient(ThreadDataShowFunction);
                                          //  this.txtDataShow.Invoke(fc, sCur_Commd_Low_One + "***" + ex.Message);
                                        }
                                        else
                                        {
                                           // ThreadDataShowFunction(sCur_Commd_Low_One + "***" + ex.Message);
                                        }


                                    }
                                    if (ex.Source.Equals("nModbusPC"))
                                    {
                                        string str = ex.Message;
                                        int FunctionCode;
                                        string ExceptionCode;
                                        str = str.Remove(0, str.IndexOf("\r\n") + 17);
                                        FunctionCode = Convert.ToInt16(str.Remove(str.IndexOf("\r\n")));
                                        ExceptionCode = str.Remove(str.IndexOf("-"));
                                        switch (ExceptionCode.Trim())
                                        {
                                            case "1":
                                                {
                                                    if (txtDataShow.InvokeRequired)
                                                    {
                                                        FlushClient fc = new FlushClient(ThreadDataShowFunction);
                                                      //  this.txtDataShow.Invoke(fc, "Exception Code: " + sCur_Commd_Low_One + "***" + ExceptionCode.Trim() + "----> 不被允许的指令!");
                                                    }
                                                    else
                                                    {
                                                      //  ThreadDataShowFunction("Exception Code: " + sCur_Commd_Low_One + "***" + ExceptionCode.Trim() + "----> 不被允许的指令!");
                                                    }
                                                }
                                                break;
                                            case "2":
                                                {
                                                    if (txtDataShow.InvokeRequired)
                                                    {
                                                        FlushClient fc = new FlushClient(ThreadDataShowFunction);
                                                       // this.txtDataShow.Invoke(fc, "Exception Code: " + sCur_Commd_Low_One + "***" + ExceptionCode.Trim() + "----> 不正当的地址!");
                                                    }
                                                    else
                                                    {
                                                      //  ThreadDataShowFunction("Exception Code: " + ExceptionCode.Trim() + "---->不正当的地址!");
                                                    }
                                                }
                                                break;
                                            case "3":
                                                {
                                                    if (txtDataShow.InvokeRequired)
                                                    {
                                                        FlushClient fc = new FlushClient(ThreadDataShowFunction);
                                                      //  this.txtDataShow.Invoke(fc, "Exception Code: " + sCur_Commd_Low_One + "***" + ExceptionCode.Trim() + "---->不合法的数值!");
                                                    }
                                                    else
                                                    {
                                                      //  ThreadDataShowFunction("Exception Code: " + sCur_Commd_Low_One + "***" + ExceptionCode.Trim() + "----> 不合法的数值!");
                                                    }
                                                }
                                                break;
                                            case "4":
                                                {
                                                    if (txtDataShow.InvokeRequired)
                                                    {
                                                        FlushClient fc = new FlushClient(ThreadDataShowFunction);
                                                     //   this.txtDataShow.Invoke(fc, "Exception Code: " + sCur_Commd_Low_One + "***" + ExceptionCode.Trim() + "----> Slave 裝置失效!");
                                                    }
                                                    else
                                                    {
                                                      //  ThreadDataShowFunction("Exception Code: " + sCur_Commd_Low_One + "***" + ExceptionCode.Trim() + "----> Slave 裝置失效!");
                                                    }
                                                }
                                                break;
                                            case "B":
                                                {
                                                    if (txtDataShow.InvokeRequired)
                                                    {
                                                        FlushClient fc = new FlushClient(ThreadDataShowFunction);
                                                     //   this.txtDataShow.Invoke(fc, "Exception Code: " + sCur_Commd_Low_One + "***" + ExceptionCode.Trim() + "----> 目标设备没有回应!");
                                                    }
                                                    else
                                                    {
                                                     //   ThreadDataShowFunction("Exception Code: " + sCur_Commd_Low_One + "***" + ExceptionCode.Trim() + "----> 目标设备没有回应!");
                                                    }
                                                }
                                                break;
                                            default:
                                                {
                                                    if (txtDataShow.InvokeRequired)
                                                    {
                                                        FlushClient fc = new FlushClient(ThreadDataShowFunction);
                                                      //  this.txtDataShow.Invoke(fc, "Exception Code: " + sCur_Commd_Low_One + "***" + ExceptionCode.Trim() + "----> 未定义的错误!");
                                                    }
                                                    else
                                                    {
                                                      //  ThreadDataShowFunction("Exception Code: " + sCur_Commd_Low_One + "***" + ExceptionCode.Trim() + "----> 未定义的错误!");
                                                    }
                                                }
                                                break;
                                        }
                                    }
                                }
                                sCur_Commd_Low = sCur_Commd_Low.Substring(sCur_Commd_Low.IndexOf(",") + 1, sCur_Commd_Low.Length - sCur_Commd_Low.IndexOf(",") - 1);
                            }
                            cls_Main.sCommdRead_Mark = false;
                        }
                        else
                        {
                            sCur_Commd_Low = "ALL_ZDH,ALL_JXB,ALL_GD,ALL_XJ,";
                            // sCur_Commd_Low = "ALL_GD,";
                        }
                        ReadFinish = true;
                    }
                }
                else if (cls_Main.sCommdWrite_Mark)
                {
                    if (WriteFinish)
                    {
                        WriteFinish = false;
                        try
                        {
                            WriteOK = true;
                            master.WriteSingleRegister((byte)Port_WriteiAddRess, (ushort)Port_WriteiReg, (ushort)Port_WriteiRegVal);
                        }
                        catch (Exception ex)
                        {
                            WriteOK = false;
                            if (ex.Source.Equals("System"))
                            {

                                if (txtDataShow.InvokeRequired)
                                {
                                    FlushClient fc = new FlushClient(ThreadDataShowFunction);
                                  //  this.txtDataShow.Invoke(fc, "写指令：【" + Port_WriteiAddRess.ToString() + "****" + Port_WriteiReg.ToString() + "****" + Port_WriteiRegVal.ToString() + "】" + ex.Message);
                                }
                                else
                                {
                                  //  ThreadDataShowFunction("写指令：【" + Port_WriteiAddRess.ToString() + "****" + Port_WriteiReg.ToString() + "****" + Port_WriteiRegVal.ToString() + "】" + ex.Message);
                                }


                            }
                            if (ex.Source.Equals("nModbusPC"))
                            {
                                string str = ex.Message;
                                int FunctionCode;
                                string ExceptionCode;
                                str = str.Remove(0, str.IndexOf("\r\n") + 17);
                                FunctionCode = Convert.ToInt16(str.Remove(str.IndexOf("\r\n")));
                                ExceptionCode = str.Remove(str.IndexOf("-"));
                                switch (ExceptionCode.Trim())
                                {
                                    case "1":
                                        {
                                            if (txtDataShow.InvokeRequired)
                                            {
                                                FlushClient fc = new FlushClient(ThreadDataShowFunction);
                                           //     this.txtDataShow.Invoke(fc, "Exception Code: 写指令：【" + Port_WriteiAddRess.ToString() + "****" + Port_WriteiReg.ToString() + "****" + Port_WriteiRegVal.ToString() + "】" + ExceptionCode.Trim() + "----> 不被允许的指令!");
                                            }
                                            else
                                            {
                                              //  ThreadDataShowFunction("Exception Code: 写指令：【" + Port_WriteiAddRess.ToString() + "****" + Port_WriteiReg.ToString() + "****" + Port_WriteiRegVal.ToString() + "】" + ExceptionCode.Trim() + "----> 不被允许的指令!");
                                            }
                                        }
                                        break;
                                    case "2":
                                        {
                                            if (txtDataShow.InvokeRequired)
                                            {
                                                FlushClient fc = new FlushClient(ThreadDataShowFunction);
                                              //  this.txtDataShow.Invoke(fc, "Exception Code:写指令：【" + Port_WriteiAddRess.ToString() + "****" + Port_WriteiReg.ToString() + "****" + Port_WriteiRegVal.ToString() + "】" + ExceptionCode.Trim() + "----> 不正当的地址!");
                                            }
                                            else
                                            {
                                              //  ThreadDataShowFunction("Exception Code: 写指令：【" + Port_WriteiAddRess.ToString() + "****" + Port_WriteiReg.ToString() + "****" + Port_WriteiRegVal.ToString() + "】" + ExceptionCode.Trim() + "---->不正当的地址!");
                                            }
                                        }
                                        break;
                                    case "3":
                                        {
                                            if (txtDataShow.InvokeRequired)
                                            {
                                                FlushClient fc = new FlushClient(ThreadDataShowFunction);
                                              //  this.txtDataShow.Invoke(fc, "Exception Code: 写指令：【" + Port_WriteiAddRess.ToString() + "****" + Port_WriteiReg.ToString() + "****" + Port_WriteiRegVal.ToString() + "】" + ExceptionCode.Trim() + "---->不合法的数值!");
                                            }
                                            else
                                            {
                                             //   ThreadDataShowFunction("Exception Code: 写指令：【" + Port_WriteiAddRess.ToString() + "****" + Port_WriteiReg.ToString() + "****" + Port_WriteiRegVal.ToString() + "】" + ExceptionCode.Trim() + "----> 不合法的数值!");
                                            }
                                        }
                                        break;
                                    case "4":
                                        {
                                            if (txtDataShow.InvokeRequired)
                                            {
                                                FlushClient fc = new FlushClient(ThreadDataShowFunction);
                                               // this.txtDataShow.Invoke(fc, "Exception Code: 写指令：【" + Port_WriteiAddRess.ToString() + "****" + Port_WriteiReg.ToString() + "****" + Port_WriteiRegVal.ToString() + "】" + ExceptionCode.Trim() + "----> Slave 裝置失效!");
                                            }
                                            else
                                            {
                                              //  ThreadDataShowFunction("Exception Code: 写指令：【" + Port_WriteiAddRess.ToString() + "****" + Port_WriteiReg.ToString() + "****" + Port_WriteiRegVal.ToString() + "】" + ExceptionCode.Trim() + "----> Slave 裝置失效!");
                                            }
                                        }
                                        break;
                                    case "B":
                                        {
                                            if (txtDataShow.InvokeRequired)
                                            {
                                                FlushClient fc = new FlushClient(ThreadDataShowFunction);
                                              //  this.txtDataShow.Invoke(fc, "Exception Code: 写指令：【" + Port_WriteiAddRess.ToString() + "****" + Port_WriteiReg.ToString() + "****" + Port_WriteiRegVal.ToString() + "】" + ExceptionCode.Trim() + "----> 目标设备没有回应!");
                                            }
                                            else
                                            {
                                              //  ThreadDataShowFunction("Exception Code: 写指令：【" + Port_WriteiAddRess.ToString() + "****" + Port_WriteiReg.ToString() + "****" + Port_WriteiRegVal.ToString() + "】" + ExceptionCode.Trim() + "----> 目标设备没有回应!");
                                            }
                                        }
                                        break;
                                    default:
                                        {
                                            if (txtDataShow.InvokeRequired)
                                            {
                                                FlushClient fc = new FlushClient(ThreadDataShowFunction);
                                             //   this.txtDataShow.Invoke(fc, "Exception Code: 写指令：【" + Port_WriteiAddRess.ToString() + "****" + Port_WriteiReg.ToString() + "****" + Port_WriteiRegVal.ToString() + "】" + ExceptionCode.Trim() + "----> 未定义的错误!");
                                            }
                                            else
                                            {
                                              //  ThreadDataShowFunction("Exception Code: 写指令：【" + Port_WriteiAddRess.ToString() + "****" + Port_WriteiReg.ToString() + "****" + Port_WriteiRegVal.ToString() + "】" + ExceptionCode.Trim() + "----> 未定义的错误!");
                                            }

                                        }
                                        break;
                                }
                            }
                        }
                        cls_Main.sCommdWrite_Mark = false;
                        cls_Main.Commd_Mark = "COM";
                        WriteFinish = true;
                    }
                }
            }
        }
        #endregion
        private void timerCommd_Tick(object sender, EventArgs e)
        {
            



        }
        public void ThreadDataShowFunction(string sInfo)
        {
            //this.txtDataShow.AppendText("\r\n");
            //this.txtDataShow.AppendText(DateTime.Now.ToString());
            //this.txtDataShow.AppendText("信息：" + sInfo);
            //this.textSendData.AppendText("\r\n");
        }

        public void ThreadFunction(string sInfo)
        {
            //this.textSendData.AppendText("\r\n");
            //this.textSendData.AppendText(DateTime.Now.ToString());
            //this.textSendData.AppendText("信息：" + sInfo);
           
        }

      

        #region 
        private void pic_W_01_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_02_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_03_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_04_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_05_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_06_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_07_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_08_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_09_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_10_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_11_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_12_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_13_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_14_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_15_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_16_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_17_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_18_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_19_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_20_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_21_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_22_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_23_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_24_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_25_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_26_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_27_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_28_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_29_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_30_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_31_Click(object sender, EventArgs e)
        {

        }

        private void pic_W_32_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_01_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_02_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_03_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_04_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_05_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_06_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_07_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_08_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_09_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_10_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_11_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_12_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_13_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_14_Click(object sender, EventArgs e)
        {

        }
        private void pic_X_15_Click(object sender, EventArgs e)
        {

        }
        private void pic_X_16_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_17_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_18_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_19_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_20_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_21_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_22_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_23_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_24_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_25_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_26_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_27_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_28_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_29_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_30_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_31_Click(object sender, EventArgs e)
        {

        }

        private void pic_X_32_Click(object sender, EventArgs e)
        {

        }

        private void pic_J_01_Click(object sender, EventArgs e)
        {

        }

        private void pic_J_02_Click(object sender, EventArgs e)
        {

        }

#endregion 

        private void pic_Task_Add_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
          
            frmTask frm = new frmTask(this);
            frm.ShowDialog();

            if (frm.sChange == "1")
            {
                if (sFrm_Loginc_Mark == "0")
                {
                    this.SetTaskArry();


                    Thread.Sleep(500);
                    timerCommdStart();

                    frmLogic.Cod_Mark = "运行";
                 
                    Thread oThreadRead = new Thread(new ThreadStart(this.frmLogic_Info));
                    oThreadRead.Start();

                    sFrm_Loginc_Mark = "1";

                    this.pic_Task_Stop.Enabled = true;
                    this.pic_Task_Stop.Image = COD.Properties.Resources.zt;

                    sRelultMark = "0";
                }
            }
       
         
            timerXJQ.Start();
            timerProcess.Start();

            
        }
        public string getNumInfo(double d, int i)
        {
            string s = d.ToString();

            int j = 0, m = 0;
            int iWZ = s.IndexOf(".");
            if (iWZ < 0)
            {
                s = s + ".";
                for (j = 0; j < i; j++)
                {
                    s = s + "0";
                }
            }
            else if (iWZ == 0)
            {
                s = "0" + s;
                for (j = 0; j < i; j++)
                {
                    s = s + "0";
                }

                m = s.IndexOf(".");

                s = s.Substring(0, m+1) + s.Substring(m + 1, i);

            }
            else
            {
                for (j = 0; j < i; j++)
                {
                    s = s + "0";
                }
                m = s.IndexOf(".");
                s = s.Substring(0, m+1) + s.Substring(m + 1, i);

            }


            return s;
        }

        private void pic_Task_Stop_Click(object sender, EventArgs e)
        {
            if (sMark=="0")
            {

                this.pic_Task_Stop.Image = COD.Properties.Resources.jx;
                sMark = "1";
                frmLogic.Cod_Mark = "运行";

                timerCommdNew.Dispose();
            
            }
            else
            {
                this.pic_Task_Stop.Image = COD.Properties.Resources.zt;
                sMark = "0";
                frmLogic.Cod_Mark = "暂停";
                timerCommdStart();  

            }

           // cls_Main.writeLogFile("时间：任务：实验员：");
      
        }
     

        private void SetTaskArry()
        {
            for (int i =0;i<32;i++)
            {
                if (frmLogic.TaskSet.TubeNum[i] != 0)
                {
                    this.SetTest_Pic(frmLogic.TaskSet.TubeNum[i], 1);

                }
                else
                {
                    this.SetTest_Pic(frmLogic.TaskSet.TubeNum[i], 0);
                }
            }
        }
        private void SetXJQArry()
        {
            for (int i = 0; i < 32; i++)
            {
                if (frmLogic.DigestionTubeState.DigestnIsExist[i] ==true)
                {
                    this.SetXJQ_Pic(frmLogic.DigestionTubeState.DigestnID[i], frmLogic.DigestionTubeState.SampleFrom[i], 1);

                }
                else
                {
                    this.SetXJQ_Pic(frmLogic.DigestionTubeState.DigestnID[i], frmLogic.DigestionTubeState.SampleFrom[i], 0);
                }

                if (frmLogic.Cod_Mark == "正常结束")  //存储结构数据
                {
                    if (frmLogic.DigestionTubeState.RelultIsValid[i] == true && sRelultMark =="0")
                    {
                        clsMain.Insert_D_TestResult(frmLogic.DigestionTubeState.DigestnID[i].ToString(), frmLogic.DigestionTubeState.SampleFrom[i].ToString(), frmLogic.DigestionTubeState.NumWithInGroup[i].ToString(), frmLogic.DigestionTubeState.Range[i].ToString(), frmLogic.DigestionTubeState.ConcentrationValue[i].ToString());

                        clsMain.Insert_D_TestInfo_His();

                        clsMain.Del_D_TestInfo();


                        int j = DbHelper.ExecuteNonQuery("UPDATE  D_Sys_Info SET ItemVal ='0' WHERE ItemID ='1'");




                    }

                    
 
                }
            }
             if (frmLogic.Cod_Mark == "正常结束")
             {
                 sRelultMark = "1";
             }
        }
        #region
        private void SetTest_Pic(int i,int j)
        {
         
                switch (i)
                {
                    case 1:
                        {
                            if (j == 0)
                            {
                                this.pic_W_01.Image = COD.Properties.Resources.L01;
                            }
                            else
                            {
                                this.pic_W_01.Image = COD.Properties.Resources.G01;
                            }
                            break;
                        }
                    case 2:
                        {
                            if (j == 0)
                            {
                                this.pic_W_02.Image = COD.Properties.Resources.L02;
                            }
                            else
                            {
                                this.pic_W_02.Image = COD.Properties.Resources.G02;
                            }
                            
                            break;
                        }
                    case 3:
                        {
                            if (j == 0)
                            {
                                this.pic_W_03.Image = COD.Properties.Resources.L03;
                            }
                            else
                            {
                                this.pic_W_03.Image = COD.Properties.Resources.G03;
                            }
                            break;
                        }
                    case 4:
                        {
                            if (j == 0)
                            {
                                this.pic_W_04.Image = COD.Properties.Resources.L04;
                            }
                            else
                            {
                                this.pic_W_04.Image = COD.Properties.Resources.G04;
                            }
                            break;
                        }
                    case 5:
                        {
                            if (j == 0)
                            {
                                this.pic_W_05.Image = COD.Properties.Resources.L05;
                            }
                            else
                            {
                                this.pic_W_05.Image = COD.Properties.Resources.G05;
                            }
                            break;
                        }
                    case 6:
                        {
                            if (j == 0)
                            {
                                this.pic_W_06.Image = COD.Properties.Resources.L06;
                            }
                            else
                            {
                                this.pic_W_06.Image = COD.Properties.Resources.G06;
                            }
                            break;
                        }
                    case 7:
                        {
                            if (j == 0)
                            {
                                this.pic_W_07.Image = COD.Properties.Resources.L07;
                            }
                            else
                            {
                                this.pic_W_07.Image = COD.Properties.Resources.G07;
                            }
                            break;
                        }
                    case 8:
                        {
                            if (j == 0)
                            {
                                this.pic_W_08.Image = COD.Properties.Resources.L08;
                            }
                            else
                            {
                                this.pic_W_08.Image = COD.Properties.Resources.G08;
                            }
                            break;
                        }
                    case 9:
                        {
                            if (j == 0)
                            {
                                this.pic_W_09.Image = COD.Properties.Resources.L09;
                            }
                            else
                            {
                                this.pic_W_09.Image = COD.Properties.Resources.G09;
                            }
                            break;
                        }
                    case 10:
                        {
                            if (j == 0)
                            {
                                this.pic_W_10.Image = COD.Properties.Resources.L10;
                            }
                            else
                            {
                                this.pic_W_10.Image = COD.Properties.Resources.G10;
                            }
                            break;
                        }
                    case 11:
                        {
                            if (j == 0)
                            {
                                this.pic_W_11.Image = COD.Properties.Resources.L11;
                            }
                            else
                            {
                                this.pic_W_11.Image = COD.Properties.Resources.G11;
                            }
                            break;
                        }
                    case 12:
                        {
                            if (j == 0)
                            {
                                this.pic_W_12.Image = COD.Properties.Resources.L12;
                            }
                            else
                            {
                                this.pic_W_12.Image = COD.Properties.Resources.G12;
                            }
                            break;
                        }
                    case 13:
                        {
                            if (j == 0)
                            {
                                this.pic_W_13.Image = COD.Properties.Resources.L13;
                            }
                            else
                            {
                                this.pic_W_13.Image = COD.Properties.Resources.G13;
                            }
                            break;
                        }
                    case 14:
                        {
                            if (j == 0)
                            {
                                this.pic_W_14.Image = COD.Properties.Resources.L14;
                            }
                            else
                            {
                                this.pic_W_14.Image = COD.Properties.Resources.G14;
                            }
                            break;
                        }
                    case 15:
                        {
                            if (j == 0)
                            {
                                this.pic_W_15.Image = COD.Properties.Resources.L15;
                            }
                            else
                            {
                                this.pic_W_15.Image = COD.Properties.Resources.G15;
                            }
                            break;
                        }
                    case 16:
                        {
                            if (j == 0)
                            {
                                this.pic_W_16.Image = COD.Properties.Resources.L16;
                            }
                            else
                            {
                                this.pic_W_16.Image = COD.Properties.Resources.G16;
                            }
                            break;
                        }
                    case 17:
                        {
                            if (j == 0)
                            {
                                this.pic_W_17.Image = COD.Properties.Resources.L17;
                            }
                            else
                            {
                                this.pic_W_17.Image = COD.Properties.Resources.G17;
                            }
                            break;
                        }
                    case 18:
                        {
                            if (j == 0)
                            {
                                this.pic_W_18.Image = COD.Properties.Resources.L18;
                            }
                            else
                            {
                                this.pic_W_18.Image = COD.Properties.Resources.G18;
                            }
                            break;
                        }
                    case 19:
                        {
                            if (j == 0)
                            {
                                this.pic_W_19.Image = COD.Properties.Resources.L19;
                            }
                            else
                            {
                                this.pic_W_19.Image = COD.Properties.Resources.G19;
                            }
                            break;
                        }
                    case 20:
                        {
                            if (j == 0)
                            {
                                this.pic_W_20.Image = COD.Properties.Resources.L20;
                            }
                            else
                            {
                                this.pic_W_20.Image = COD.Properties.Resources.G20;
                            }
                            break;
                        }
                    case 21:
                        {
                            if (j == 0)
                            {
                                this.pic_W_21.Image = COD.Properties.Resources.L21;
                            }
                            else
                            {
                                this.pic_W_21.Image = COD.Properties.Resources.G21;
                            }
                            break;
                        }
                    case 22:
                        {
                            if (j == 0)
                            {
                                this.pic_W_22.Image = COD.Properties.Resources.L22;
                            }
                            else
                            {
                                this.pic_W_22.Image = COD.Properties.Resources.G22;
                            }
                            break;
                        }
                    case 23:
                        {
                            if (j == 0)
                            {
                                this.pic_W_23.Image = COD.Properties.Resources.L23;
                            }
                            else
                            {
                                this.pic_W_23.Image = COD.Properties.Resources.G23;
                            }
                            break;
                        }
                    case 24:
                        {
                            if (j == 0)
                            {
                                this.pic_W_24.Image = COD.Properties.Resources.L24;
                            }
                            else
                            {
                                this.pic_W_24.Image = COD.Properties.Resources.G24;
                            }
                            break;
                        }
                    case 25:
                        {
                            if (j == 0)
                            {
                                this.pic_W_25.Image = COD.Properties.Resources.L25;
                            }
                            else
                            {
                                this.pic_W_25.Image = COD.Properties.Resources.G25;
                            }
                            break;
                        }
                    case 26:
                        {
                            if (j == 0)
                            {
                                this.pic_W_26.Image = COD.Properties.Resources.L26;
                            }
                            else
                            {
                                this.pic_W_26.Image = COD.Properties.Resources.G26;
                            }
                            break;
                        }
                    case 27:
                        {
                            if (j == 0)
                            {
                                this.pic_W_27.Image = COD.Properties.Resources.L27;
                            }
                            else
                            {
                                this.pic_W_27.Image = COD.Properties.Resources.G27;
                            }
                            break;
                        }
                    case 28:
                        {
                            if (j == 0)
                            {
                                this.pic_W_28.Image = COD.Properties.Resources.L28;
                            }
                            else
                            {
                                this.pic_W_28.Image = COD.Properties.Resources.G28;
                            }
                            break;
                        }
                    case 29:
                        {
                            if (j == 0)
                            {
                                this.pic_W_29.Image = COD.Properties.Resources.L29;
                            }
                            else
                            {
                                this.pic_W_29.Image = COD.Properties.Resources.G29;
                            }
                            break;
                        }
                    case 30:
                        {
                            if (j == 0)
                            {
                                this.pic_W_30.Image = COD.Properties.Resources.L30;
                            }
                            else
                            {
                                this.pic_W_30.Image = COD.Properties.Resources.G30;
                            }
                            break;
                        }
                    case 31:
                        {
                            if (j == 0)
                            {
                                this.pic_W_31.Image = COD.Properties.Resources.L31;
                            }
                            else
                            {
                                this.pic_W_31.Image = COD.Properties.Resources.G31;
                            }
                            break;
                        }
                    case 32:
                        {
                            if (j == 0)
                            {
                                this.pic_W_32.Image = COD.Properties.Resources.L32;
                            }
                            else
                            {
                                this.pic_W_32.Image = COD.Properties.Resources.G32;
                            }
                            break;
                        }
                    default:
                        {
                            
                            break;
                        }

                }




        }
        #endregion
        #region

        private Bitmap getBitMap_XJQ(int iSampleFrom,int j)
        {
            Bitmap bitMapInfo = COD.Properties.Resources.L01;
            switch (iSampleFrom)
            {
                case 1:
                       if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L01;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G01;
                        }
                        break;
                case 2:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L02;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G02;
                        }
                        break;
                case 3:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L03;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G03;
                        }
                        break;
                case 4:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L04;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G04;
                        }
                        break;
                case 5:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L05;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G05;
                        }
                        break;
                case 6:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L06;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G06;
                        }
                        break;
                case 7:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L07;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G07;
                        }
                        break;
                case 8:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L08;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G08;
                        }
                        break;
                case 9:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L09;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G09;
                        }
                        break;
                case 10:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L10;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G10;
                        }
                        break;
                case 11:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L11;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G11;
                        }
                        break;
                case 12:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L12;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G12;
                        }
                        break;
                case 13:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L13;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G13;
                        }
                        break;
                case 14:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L14;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G14;
                        }
                        break;
                case 15:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L15;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G15;
                        }
                        break;
                case 16:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L16;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G16;
                        }
                        break;
                case 17:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L17;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G17;
                        }
                        break;
                case 18:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L18;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G18;
                        }
                        break;
                case 19:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L19;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G19;
                        }
                        break;
                case 20:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L20;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G20;
                        }
                        break;
                case 21:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L21;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G21;
                        }
                        break;
                case 22:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L22;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G22;
                        }
                        break;
                case 23:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L23;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G23;
                        }
                        break;
                case 24:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L24;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G24;
                        }
                        break;
                case 25:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L25;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G25;
                        }
                        break;
                case 26:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L26;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G26;
                        }
                        break;
                case 27:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L27;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G27;
                        }
                        break;
                case 28:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L28;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G28;
                        }
                        break;
                case 29:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L29;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G29;
                        }
                        break;
                case 30:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L30;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G30;
                        }
                        break;
                case 31:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L31;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G31;
                        }
                        break;
                case 32:
                        if (j == 0)
                        {
                            bitMapInfo = COD.Properties.Resources.L32;
                        }
                        else
                        {
                            bitMapInfo = COD.Properties.Resources.G32;
                        }
                        break;
            }
            return bitMapInfo;
 
        }

        private void SetXJQ_Pic(int i,int iSampleFrom ,int j)
        {

            switch (i)
            {
                case 1:
                    {
                        if (j == 0)
                        {
                            this.pic_X_01.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_01.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 2:
                    {
                        if (j == 0)
                        {
                            this.pic_X_02.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_02.Image = getBitMap_XJQ(iSampleFrom, j);
                        }

                        break;
                    }
                case 3:
                    {
                        if (j == 0)
                        {
                            this.pic_X_03.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_03.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 4:
                    {
                        if (j == 0)
                        {
                            this.pic_X_04.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_04.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 5:
                    {
                        if (j == 0)
                        {
                            this.pic_X_05.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_05.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 6:
                    {
                        if (j == 0)
                        {
                            this.pic_X_06.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_06.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 7:
                    {
                        if (j == 0)
                        {
                            this.pic_X_07.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_07.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 8:
                    {
                        if (j == 0)
                        {
                            this.pic_X_08.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_08.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 9:
                    {
                        if (j == 0)
                        {
                            this.pic_X_09.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_09.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 10:
                    {
                        if (j == 0)
                        {
                            this.pic_X_10.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_10.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 11:
                    {
                        if (j == 0)
                        {
                            this.pic_X_11.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_11.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 12:
                    {
                        if (j == 0)
                        {
                            this.pic_X_12.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_12.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 13:
                    {
                        if (j == 0)
                        {
                            this.pic_X_13.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_13.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 14:
                    {
                        if (j == 0)
                        {
                            this.pic_X_14.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_14.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 15:
                    {
                        if (j == 0)
                        {
                            this.pic_X_15.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_15.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 16:
                    {
                        if (j == 0)
                        {
                            this.pic_X_16.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_16.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 17:
                    {
                        if (j == 0)
                        {
                            this.pic_X_17.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_17.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 18:
                    {
                        if (j == 0)
                        {
                            this.pic_X_18.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_18.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 19:
                    {
                        if (j == 0)
                        {
                            this.pic_X_19.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_19.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 20:
                    {
                        if (j == 0)
                        {
                            this.pic_X_20.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_20.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 21:
                    {
                        if (j == 0)
                        {
                            this.pic_X_21.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_21.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 22:
                    {
                        if (j == 0)
                        {
                            this.pic_X_22.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_22.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 23:
                    {
                        if (j == 0)
                        {
                            this.pic_X_23.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_23.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 24:
                    {
                        if (j == 0)
                        {
                            this.pic_X_24.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_24.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 25:
                    {
                        if (j == 0)
                        {
                            this.pic_X_25.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_25.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 26:
                    {
                        if (j == 0)
                        {
                            this.pic_X_26.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_26.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 27:
                    {
                        if (j == 0)
                        {
                            this.pic_X_27.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_27.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 28:
                    {
                        if (j == 0)
                        {
                            this.pic_X_28.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_28.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 29:
                    {
                        if (j == 0)
                        {
                            this.pic_X_29.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_29.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 30:
                    {
                        if (j == 0)
                        {
                            this.pic_X_30.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_30.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 31:
                    {
                        if (j == 0)
                        {
                            this.pic_X_31.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_31.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                case 32:
                    {
                        if (j == 0)
                        {
                            this.pic_X_32.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        else
                        {
                            this.pic_X_32.Image = getBitMap_XJQ(iSampleFrom, j);
                        }
                        break;
                    }
                default:
                    {

                        break;
                    }

            }




        }
        #endregion

        private void timerXJQ_Tick(object sender, EventArgs e)
        {
            Application.DoEvents();
            this.SetXJQArry();

        }

        string sRgtAdd_Effluents_01 = "0", sRgtAdd_Effluents_02 = "0";
        int iSamplerUsing_01 = 1, iDigestionUsing_01 = 1;

        private void Set_DigestionUsing()
        {
            if (frmLogic.ProcessCurrent.DigestionUsing <= 32)
            {
                if (iDigestionUsing_01 == 0)
                {
                    this.SetXJQ_Pic(frmLogic.ProcessCurrent.DigestionUsing, getSampleFromByDigestionUsing(frmLogic.ProcessCurrent.DigestionUsing), iDigestionUsing_01);

                    iDigestionUsing_01 = 1;
                }
                else
                {
                    this.SetXJQ_Pic(frmLogic.ProcessCurrent.DigestionUsing,getSampleFromByDigestionUsing(frmLogic.ProcessCurrent.DigestionUsing), iDigestionUsing_01);
                    iDigestionUsing_01 = 0;
                }
            }
        }

        private int getSampleFromByDigestionUsing(int iD)
        {
            int j = 0;

            for (int i = 0; i < frmLogic.DigestionTubeState.DigestnID.Length; i++)
            {
                if (frmLogic.DigestionTubeState.DigestnID[i] == iD)
                {
                    j = frmLogic.DigestionTubeState.SampleFrom[i];
                    break;
                }
            }
            return j;
        }


        private void Set_SamplerUsing()
        {
            if (frmLogic.ProcessCurrent.SamplerUsing <= 32)
            {
                if (iSamplerUsing_01 == 0)
                {
                    this.SetTest_Pic(frmLogic.ProcessCurrent.SamplerUsing, iSamplerUsing_01);
                    iSamplerUsing_01 = 1;
                }
                else
                {
                    this.SetTest_Pic(frmLogic.ProcessCurrent.SamplerUsing, iSamplerUsing_01);
                    iSamplerUsing_01 = 0;
                }
            }
 
        }

        private void Set_RgtAdd_Effluents()
        {
            if (frmLogic.ProcessCurrent.SampleEffluents == true)
            {

                if (sRgtAdd_Effluents_01 == "0")
                {
                    sRgtAdd_Effluents_01 = "1";
                    this.pic_J_01.Image = COD.Properties.Resources.J02;
                }
                else
                {
                    sRgtAdd_Effluents_01 = "0";
                    this.pic_J_01.Image = COD.Properties.Resources.J00;
                }
                this.pic_J_02.Image = COD.Properties.Resources.J00;
                this.pic_J_03.Image = COD.Properties.Resources.J03;
                this.pic_J_04.Image = COD.Properties.Resources.J03;

       
            }
            else if (frmLogic.ProcessCurrent.SampleAdd == true)
            {
                if (frmLogic.ProcessCurrent.RengentAdd == 1)
                {

                    if (sRgtAdd_Effluents_02 == "0")
                    {
                        sRgtAdd_Effluents_02 = "1";
                        this.pic_J_02.Image = COD.Properties.Resources.J02;
                    }
                    else
                    {
                        sRgtAdd_Effluents_02 = "0";
                        this.pic_J_02.Image = COD.Properties.Resources.J00;
                    }
                    this.pic_J_01.Image = COD.Properties.Resources.J00;
                    this.pic_J_03.Image = COD.Properties.Resources.J03;
                    this.pic_J_04.Image = COD.Properties.Resources.J03;
                }
                else if (frmLogic.ProcessCurrent.RengentAdd == 2)
                {
                    if (sRgtAdd_Effluents_02 == "0")
                    {
                        sRgtAdd_Effluents_02 = "1";
                        this.pic_J_03.Image = COD.Properties.Resources.J04;
                    }
                    else
                    {
                        sRgtAdd_Effluents_02 = "0";
                        this.pic_J_03.Image = COD.Properties.Resources.J03;
                    }
                    this.pic_J_01.Image = COD.Properties.Resources.J00;
                    this.pic_J_02.Image = COD.Properties.Resources.J00;
                    this.pic_J_04.Image = COD.Properties.Resources.J03;
 
                }
                else if (frmLogic.ProcessCurrent.RengentAdd == 3)
                {
                    if (sRgtAdd_Effluents_02 == "0")
                    {
                        sRgtAdd_Effluents_02 = "1";
                        this.pic_J_04.Image = COD.Properties.Resources.J04;
                    }
                    else
                    {
                        sRgtAdd_Effluents_02 = "0";
                        this.pic_J_04.Image = COD.Properties.Resources.J03;
                    }
                    this.pic_J_01.Image = COD.Properties.Resources.J00;
                    this.pic_J_02.Image = COD.Properties.Resources.J00;
                    this.pic_J_03.Image = COD.Properties.Resources.J03;
                }

         

            }
            else
            {
                this.pic_J_01.Image = COD.Properties.Resources.J00;
                this.pic_J_02.Image = COD.Properties.Resources.J00;

                this.pic_J_03.Image = COD.Properties.Resources.J03;
                this.pic_J_04.Image = COD.Properties.Resources.J03;


            }
    
        }
        int iCru_Info = 0;
        private void timerProcess_Tick(object sender, EventArgs e)
        {
            if (sFrm_Loginc_Mark == "1")  //任务已经启动
            {

               // this.Set_RgtAdd_Effluents();
               // SamplerUsing  水样区
                this.Set_SamplerUsing();

                // DigestionUsing  消解区
                this.Set_DigestionUsing();

                //  SampleAdd  第2个孔     SampleEffluents   第1个孔
                this.Set_RgtAdd_Effluents();

               // this.Set_RgtAdd_Filling();
              //  this.Set_RgtAdd_Vacancy();
				this.SetTaskArry();


              

				if (frmLogic.ProcessCurrent.GrabToReagentAdd == true)
				{
					this.lbl_YX_Info.Text = "【抓取消解管到注液区】";

                    if (iCru_Info != 1)
                    {
                        cls_Main.writeLogFile("抓取消解管到注液区");
                    }

                    iCru_Info = 1;					

				}
				else if (frmLogic.ProcessCurrent.SamplePumpClean == true)
				{
					this.lbl_YX_Info.Text = "【水样泵清洗】";
                    if (iCru_Info != 2)
                    {
                        cls_Main.writeLogFile("水样泵清洗");
                    }

                    iCru_Info = 2;		

				}
				else if (frmLogic.ProcessCurrent.SamplePumpRinse == true)
				{
					this.lbl_YX_Info.Text = "【水样泵润洗】";
                    if (iCru_Info != 3)
                    {
                        cls_Main.writeLogFile("水样泵润洗");
                    }

                    iCru_Info = 3;	

				}
                else if (frmLogic.ProcessCurrent.SamplePumpOut == true)
                {
                    this.lbl_YX_Info.Text = "【水样泵注水样】";
                    if (iCru_Info != 4)
                    {
                        cls_Main.writeLogFile("水样泵注水样");
                    }

                    iCru_Info = 4;

                }
                else if (frmLogic.ProcessCurrent.RangePumpEx == true)
                {
                    this.lbl_YX_Info.Text = "【试剂泵排废液】";
                    if (iCru_Info != 5)
                    {
                        cls_Main.writeLogFile("试剂泵排废液");
                    }

                    iCru_Info = 5;

                }
                else if (frmLogic.ProcessCurrent.RangePumpIn == true)
                {
                    this.lbl_YX_Info.Text = "【试剂泵取试剂】";
                    if (iCru_Info != 6)
                    {
                        cls_Main.writeLogFile("试剂泵取试剂");
                    }

                    iCru_Info = 6;

                }
                else if (frmLogic.ProcessCurrent.RangePumpOut == true)
                {
                    this.lbl_YX_Info.Text = "【试剂泵注试剂】";
                    if (iCru_Info != 7)
                    {
                        cls_Main.writeLogFile("试剂泵注试剂");
                    }

                    iCru_Info = 7;

                }
                else if (frmLogic.ProcessCurrent.stirring == true)
                {
                    this.lbl_YX_Info.Text = "【搅拌子搅拌】";
                    if (iCru_Info != 8)
                    {
                        cls_Main.writeLogFile("搅拌子搅拌");
                    }

                    iCru_Info = 8;

                }
                else if (frmLogic.ProcessCurrent.GrabToDigestion == true)
                {
                    this.lbl_YX_Info.Text = "【抓取消解管到消解区】";
                    if (iCru_Info != 9)
                    {
                        cls_Main.writeLogFile("抓取消解管到消解区");
                    }

                    iCru_Info = 9;

                }
                else if (frmLogic.ProcessCurrent.DigestionHeating == true)
                {
                    this.lbl_YX_Info.Text = "【消解区加热】";
                    if (iCru_Info != 10)
                    {
                        cls_Main.writeLogFile("消解区加热");
                    }

                    iCru_Info = 10;

                }
                else if (frmLogic.ProcessCurrent.DigestionDigesting == true)
                {
                    this.lbl_YX_Info.Text = "【消解区消解】";
                    if (iCru_Info != 11)
                    {
                        cls_Main.writeLogFile("消解区消解");
                    }

                    iCru_Info = 11;

                }
                else if (frmLogic.ProcessCurrent.DigestionCold == true)
                {
                    this.lbl_YX_Info.Text = "【消解区冷却】";
                    if (iCru_Info != 12)
                    {
                        cls_Main.writeLogFile("消解区冷却");
                    }

                    iCru_Info = 12;

                }
                else if (frmLogic.ProcessCurrent.MixSolutions == true)
                {
                    this.lbl_YX_Info.Text = "【旋转消解管混合溶液】";
                    if (iCru_Info != 13)
                    {
                        cls_Main.writeLogFile("旋转消解管混合溶液");
                    }

                    iCru_Info = 13;

                }
                else if (frmLogic.ProcessCurrent.Spectral == true)
                {
                    this.lbl_YX_Info.Text = "【分光比色】";
                    if (iCru_Info != 14)
                    {
                        cls_Main.writeLogFile("分光比色");
                    }

                    iCru_Info = 14;

                }
                else
                {
                    this.lbl_YX_Info.Text = "";
                    iCru_Info = 0;
                }

 
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
           // frmLogic.ProcessCurrent.RgtAdd_Effluents = int.Parse(txtReadData.Text);
           // frmLogic.ProcessCurrent.RgtAdd_Filling = int.Parse(txtReadData.Text);
          //  frmLogic.ProcessCurrent.RgtAdd_Vacancy = int.Parse(txtReadData.Text);

            frmLogic.DigestionTubeState.DigestnID[0] = 1;
            frmLogic.DigestionTubeState.SampleFrom[0] = 1;


            frmLogic.DigestionTubeState.DigestnID[1] = 2;
            frmLogic.DigestionTubeState.SampleFrom[1] = 2;

            frmLogic.DigestionTubeState.DigestnID[2] = 3;
            frmLogic.DigestionTubeState.SampleFrom[2] = 2;

            frmLogic.DigestionTubeState.DigestnID[3] = 4;
            frmLogic.DigestionTubeState.SampleFrom[3] = 2;


        }

        private void pic_Close_Click(object sender, EventArgs e)
        {
            frm_Info_02 frm = new frm_Info_02("关闭程序", "您确定要关闭程序吗？");
            frm.ShowDialog();

            if (frm.sChageMark == "1")
            {

                //this.CreateWlanCole();
                this.Close();
            }
        }

        private void pic_Min_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void pic_His_Click(object sender, EventArgs e)
        {
            frmHistroy frm = new frmHistroy();
            frm.ShowDialog();
        }
       

    }
}
