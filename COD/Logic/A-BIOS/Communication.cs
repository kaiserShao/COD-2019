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
	public partial class frmLogic
	{

		//  寄存器地址定义 RegName
		private static int Auto_00 = 100;
		private static int Auto_01 = 101;
		private static int Auto_02 = 102;
		private static int Auto_03 = 103;
		private static int Auto_04 = 104;
		private static int Auto_05 = 105;
		private static int Auto_06 = 106;
		private static int Auto_07 = 107;
		private static int Auto_08 = 108;
		private static int Auto_09 = 109;
		private static int Auto_10 = 110;
		private static int Auto_11 = 111;
		private static int Auto_12 = 112;
		private static int Auto_13 = 113;
		private static int Auto_14 = 114;
		private static int Auto_15 = 115;
		private static int Auto_16 = 116;
		private static int Auto_17 = 117;
		private static int Auto_18 = 118;
		private static int Auto_19 = 119;
		private static int Auto_20 = 120;
		private static int Auto_21 = 121;
		private static int Auto_22 = 122;
		private static int Auto_23 = 123;
		private static int Auto_24 = 124;
		private static int Auto_25 = 125;
		private static int Auto_26 = 126;
		private static int Auto_27 = 127;
		private static int Auto_28 = 128;
		private static int Auto_29 = 129;
		private static int Auto_30 = 130;
		private static int Auto_31 = 131;
		private static int Auto_32 = 132;
		private static int Auto_33 = 133;
		private static int Auto_34 = 134;
		private static int Auto_35 = 135;
		private static int Auto_36 = 136;
		private static int Auto_37 = 137;
		private static int Auto_38 = 138;
		private static int Auto_39 = 139;		//	自动化部分

		private static int ARM_00 = 200;
		private static int ARM_01 = 201;
		private static int ARM_02 = 202;
		private static int ARM_03 = 203;
		private static int ARM_04 = 204;
		private static int ARM_05 = 205;
		private static int ARM_06 = 206;
		private static int ARM_07 = 207;
		private static int ARM_08 = 208;
		private static int ARM_09 = 209;
		private static int ARM_10 = 210;
		private static int ARM_11 = 211;
		private static int ARM_12 = 212;
		private static int ARM_13 = 213;
		private static int ARM_14 = 214;
		private static int ARM_15 = 215;
		private static int ARM_16 = 216;
		private static int ARM_17 = 217;
		private static int ARM_18 = 218;
		private static int ARM_19 = 219;		//	机械臂部分

		private static int Meter_00 = 300;
		private static int Meter_01 = 301;
		private static int Meter_02 = 302;
		private static int Meter_03 = 303;
		private static int Meter_04 = 304;
		private static int Meter_05 = 305;
		private static int Meter_06 = 306;
		private static int Meter_07 = 307;
		private static int Meter_08 = 308;
		private static int Meter_09 = 309;
		private static int Meter_10 = 310;
		private static int Meter_11 = 311;
		private static int Meter_12 = 312;
		private static int Meter_13 = 313;
		private static int Meter_14 = 314;
		private static int Meter_15 = 315;
		private static int Meter_16 = 316;
		private static int Meter_17 = 317;
		private static int Meter_18 = 318;
		private static int Meter_19 = 319;
		private static int Meter_20 = 320;
		private static int Meter_21 = 321;
		private static int Meter_22 = 322;
		private static int Meter_23 = 323;
		private static int Meter_24 = 324;
		private static int Meter_25 = 325;
		private static int Meter_26 = 326;
		private static int Meter_27 = 327;
		private static int Meter_28 = 328;
		private static int Meter_29 = 329;
		private static int Meter_30 = 330;
		private static int Meter_31 = 331;
		private static int Meter_32 = 332;
		private static int Meter_33 = 333;
		private static int Meter_34 = 334;
		private static int Meter_35 = 335;
		private static int Meter_36 = 336;
		private static int Meter_37 = 337;
		private static int Meter_38 = 338;
		private static int Meter_39 = 339;
		private static int Meter_40 = 340;
		private static int Meter_41 = 341;
		private static int Meter_42 = 342;
		private static int Meter_43 = 343;
		private static int Meter_44 = 344;
		private static int Meter_45 = 345;
		private static int Meter_46 = 346;
		private static int Meter_47 = 347;
		private static int Meter_48 = 348;
		private static int Meter_49 = 359;
		private static int Meter_50 = 350;
		private static int Meter_51 = 351;
		private static int Meter_52 = 352;
		private static int Meter_53 = 353;
		private static int Meter_54 = 354;
		private static int Meter_55 = 355;
		private static int Meter_56 = 356;
		private static int Meter_57 = 357;
		private static int Meter_58 = 358;
		private static int Meter_59 = 359;
		private static int Meter_60 = 360;
		private static int Meter_61 = 361;
		private static int Meter_62 = 362;
		private static int Meter_63 = 363;
		private static int Meter_64 = 364;
		private static int Meter_65 = 365;
		private static int Meter_66 = 366;
		private static int Meter_67 = 367;
		private static int Meter_68 = 368;
		private static int Meter_69 = 369;
		private static int Meter_70 = 370;
		private static int Meter_71 = 371;
		private static int Meter_72 = 372;
		private static int Meter_73 = 373;
		private static int Meter_74 = 374;
		private static int Meter_75 = 375;
		private static int Meter_76 = 376;
		private static int Meter_77 = 377;
		private static int Meter_78 = 378;
		private static int Meter_79 = 379;		//	注液部分

		private static int Heat_00 = 400;
		private static int Heat_01 = 401;
		private static int Heat_02 = 402;
		private static int Heat_03 = 403;
		private static int Heat_04 = 404;
		private static int Heat_05 = 405;
		private static int Heat_06 = 406;
		private static int Heat_07 = 407;
		private static int Heat_08 = 408;
		private static int Heat_09 = 409;
		private static int Heat_10 = 410;
		private static int Heat_11 = 411;
		private static int Heat_12 = 412;
		private static int Heat_13 = 413;
		private static int Heat_14 = 414;
		private static int Heat_15 = 415;
		private static int Heat_16 = 416;
		private static int Heat_17 = 417;
		private static int Heat_18 = 418;
		private static int Heat_19 = 419;
		private static int Heat_20 = 420;
		private static int Heat_21 = 421;
		private static int Heat_22 = 422;
		private static int Heat_23 = 423;
		private static int Heat_24 = 424;
		private static int Heat_25 = 425;
		private static int Heat_26 = 426;
		private static int Heat_27 = 427;
		private static int Heat_28 = 428;
		private static int Heat_29 = 429;		//	消解部分

		private static int MBRetry = 4;		//	重试次数

		private bool bl;

		private delegate void CommunicationClient(string sInfo);
		public void OutThreadFunction(string sInfo)
		{
			this.txtDataShow.AppendText("\r\n");
			this.txtDataShow.AppendText(DateTime.Now.ToString());
			this.txtDataShow.AppendText(sInfo);
			this.txtDataShow.AppendText("\r\n");
		}

		private void HoldingSingelWrite(int RegName, int nValue)
		{

			lock (this)
			{
				int WritingAddress;
				int WritingRegNo;
				int WritingValue;
				WritingAddress = RegName / 100;
				WritingRegNo = RegName % 100;
				WritingValue = nValue;

				bl = fFrmMain.Port_Write(WritingAddress, WritingRegNo, WritingValue);
				string Out = "命令数据:" + WritingAddress.ToString() + "  ";

				string Out2 = WritingRegNo.ToString();

				if (Out2.Length == 1)
					Out2 = "0" + Out2;

				Out = Out + Out2 + "  " + "【" + bl.ToString() + "】" + "  " + WritingValue.ToString() + "\r\n";

				if (txtDataShow.InvokeRequired)
				{
					CommunicationClient Com = new CommunicationClient(OutThreadFunction);

					this.txtDataShow.Invoke(Com, Out);
				}
				else
				{
					OutThreadFunction(Out);
				}
				osDelay(30);
				//if (!bl)
				//{
				//	for (int i = 0; i < MBRetry; i++)
				//	{
				//		bl = fFrmMain.Port_Write(WritingAddress, WritingRegNo, WritingValue);
				//		if (bl)
				//			break;
				//	}
				//}
			}
		}
		private int Err;
		private int HoldingSingelReadKnown(int RegName, int nValue, int TimeoutMs)
		{
			int ReadingAddress = RegName / 100;
			int ReadingRegNo = RegName % 100;
			int Value;
			//参数顺序为   从机地址，寄存器地址，期望值，超时时间
			Err = 0;
			Value = fFrmMain.Port_Read(ReadingAddress, ReadingRegNo, nValue, TimeoutMs);
			if (Value == nValue)
			{
				Err = 0;
			}
			else
			{
				for (int i = 0; i < MBRetry; i++)
				{
					osDelay(2000);
					Value = fFrmMain.Port_Read(ReadingAddress, ReadingRegNo, nValue, TimeoutMs);
					if (Value == nValue)
					{
						osDelay(100);
						Err = 0;
					}
					else
					{
						Err = -1;
					}
				}
			}

			if (Err == -1)
				return 65536;
			else
				return Value;
		}
		private int HoldingSingelRead(int RegName)
		{
			lock(this)
			{
				int ReadingValAddress = RegName / 100;
				int ReadingValRegNo = RegName % 100;
				int ReadingValue;


				ReadingValue = Port_Read_Val(ReadingValAddress, ReadingValRegNo);

				return ReadingValue;

			}
		}


	}
}
