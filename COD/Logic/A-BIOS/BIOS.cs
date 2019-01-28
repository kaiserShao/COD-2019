using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace COD
{
	public partial class frmLogic
	{

		private void Init1()
		{
			osDelay(20);

			HoldingSingelWrite(ARM_04, 1);
			while ((HoldingSingelRead(ARM_00) != 1))
				osDelay(20);

			HoldingSingelWrite(Meter_04, 1);
			while ((HoldingSingelRead(Meter_00) != 1))
				osDelay(20);

			HoldingSingelWrite(Auto_04, 1);
			while ((HoldingSingelRead(Auto_00) != 1))
				osDelay(20);

			HoldingSingelWrite(ARM_04, 2);
			while ((HoldingSingelRead(ARM_00) != 2))
				osDelay(20);

			HoldingSingelWrite(Auto_04, 2);
			while ((HoldingSingelRead(Auto_00) != 2))
				osDelay(20);

			HoldingSingelWrite(ARM_04, 3);
			while ((HoldingSingelRead(ARM_00) != 3))
				osDelay(20);

			HoldingSingelWrite(Meter_04, 2);
			while ((HoldingSingelRead(Meter_00) != 2))
				osDelay(20);

		}
		private void Init2()
		{
			osDelay(20);

			HoldingSingelWrite(Heat_04, 1);
			while ((HoldingSingelRead(Heat_00) != 1))
				osDelay(20);

		}

		//private delegate void btn_writeClient(bool State);
























		ProErr WaterValveCTRL(int State)
		{
			ProErr
				Err = ProErr.ProNOErr;
			int i = 0;
			if (HoldingSingelRead(Auto_15) != State)
				HoldingSingelWrite(Auto_14, State);
			do
			{
				osDelay(50);
				i++;
			} while ((HoldingSingelRead(Auto_15) != State) && i < 20);
			if (i >= 20)
			{
				return ProErr.ProValveErr;
			}
			return Err;
		}


		private int SamplePumpSpeedOldSet = 0;
		void SamplePumpSpeed(int Speed)
		{

			if (SamplePumpSpeedOldSet != Speed)
			{
				HoldingSingelWrite(Auto_07, Speed);
				SamplePumpSpeedOldSet = Speed;
				RTStatus.PumpSpeed[(int)(enumPumpType.SamplePump)] = Speed;
			}
		}

		static int PumpZero = 3;
		static int SamplePumpPositionMax = 9800;
		int SamplePumpPositionConvert(int ulPumpPosition)
		{
			float JYJD = 1.0381f;	//	进样精度
			int PumpPosition;
			if (ulPumpPosition == 0)
			{
				PumpPosition = 0;
			}
			else if (ulPumpPosition > 0)
			{
				PumpPosition = (int)((float)(ulPumpPosition + JYJD / 2) / JYJD);
			}
			else
			{
				PumpPosition = (int)((float)(ulPumpPosition - JYJD / 2) / JYJD);
			}

			if ((int)RTStatus.PumpPosition[(int)(enumPumpType.SamplePump)] + PumpPosition > (SamplePumpPositionMax + JYJD / 2) / JYJD)
			{
				PumpPosition = (int)((SamplePumpPositionMax + JYJD / 2) / JYJD);
			}
			else if ((int)RTStatus.PumpPosition[(int)(enumPumpType.SamplePump)] + PumpPosition <= PumpZero)
			{
				PumpPosition = 0;
			}
			else
			{
				PumpPosition += RTStatus.PumpPosition[(int)(enumPumpType.SamplePump)];
			}
			return PumpPosition;
		}
		ProErr SamplePumpCTRL(int ulPumpPosition)
		{
			ProErr
				Err = ProErr.ProNOErr;

			//int ValueStatus = 0;
			int i = 0;
			int ii = 0;
			int Common = 0;
			int j = 0;
			int t = 0;
			bool COMCheck = false;
			int PumpPosition;
			int RTPumpPosition;
			osDelay(1500);
			//RTPumpPosition = HoldingSingelRead(Auto_06);
			//if (RTPumpPosition != 0xFFFF)
			//{
			//	RTStatus.PumpPosition[(int)(enumPumpType.SamplePump)] = RTPumpPosition;
			//}
			//PumpPosition = SamplePumpPositionConvert(ulPumpPosition);
			//if (Math.Abs(PumpPosition - RTStatus.PumpPosition[(int)(enumPumpType.SamplePump)]) <= PumpZero)
			//	return ProErr.ProNOErr;
			//if (RTStatus.PumpSpeed[(int)(enumPumpType.SamplePump)] == 0)
			//	return ProErr.ProPumpSpeedErr;
			//COMCheck = false;
			//i = 0;
			//ii = (Math.Abs(PumpPosition - RTStatus.PumpPosition[(int)(enumPumpType.SamplePump)])/*要走的步数*/ / (RTStatus.PumpSpeed[(int)(enumPumpType.SamplePump)]/*每分钟转速*/ * 400/*单圈步数*/ / 60/*秒*/ * 1000/*毫秒*/)) / 300/*计数周期*/ + 100;
			//Common = 0;
			//j = 0;
			//t = 0;
			//HoldingSingelWrite(Auto_05, PumpPosition);
			//osDelay(500);
			//if (PumpPosition > PumpZero)
			//{
			//	COMCheck = true;
			//	ii *= 6;
			//}
			//do
			//{
			//	if (COMCheck)
			//	{
			//		t++;
			//		Common = HoldingSingelRead(Auto_09);
			//		if (Common == 0)
			//		{
			//			j++;
			//		}
			//		else
			//		{
			//			j = 0;
			//		}

			//		osDelay(50);
			//	}
			//	else
			//	{
			//		osDelay(300);
			//	}
			//	if (t % 10 == 0)
			//	{
			//		RTStatus.PumpPosition[(int)(enumPumpType.SamplePump)] = HoldingSingelRead(Auto_06);
			//	}
			//} while ((Math.Abs(PumpPosition - RTStatus.PumpPosition[(int)(enumPumpType.SamplePump)]) > PumpZero) && (i++ < ii));

			//if (i > ii)
			//{
			//	Err = ProErr.ProPumpBlockingErr;
			//}
			//if (COMCheck == true)
			//{
			//	if (j > 60)
			//	{
			//		RTStatus.Exist[(int)(enumPumpType.SamplePump)] = RTStatus.LevelSwitch[(int)(enumPumpType.SamplePump)] = enumLevelStatus.LevelStatusBlank;
			//		Err = ProErr.ProCommonErr;
			//	}
			//	else
			//	{
			//		RTStatus.Exist[(int)(enumPumpType.SamplePump)] = RTStatus.LevelSwitch[(int)(enumPumpType.SamplePump)] = enumLevelStatus.LevelStatusNonblank;
			//	}
			//	COMCheck = false;
			//}


			return Err;
		}

		public enum enumM_Type
		{
			M_Type1 = 0,			//	超低速	中时
			M_Type2,	 			//	超低速	长时
			M_Type3,	 			//	低速		中时
			M_Type4,	 			//	低速		长时
			M_Type5,	 			//	中速		中时
			M_Type6,	 			//	高速		短时
			M_Type7,	 			//	高速		中时
			M_Type8,	 			//	高速		长时
			M_Type9,
			M_Type10,
			M_Type11,				//冲	高速		中时	
			M_Type12,				//冲	高速		长时	
			M_TypeMax,
		}						//	空气泵运行时间 速度

		//	水样空气泵操作 
		static int M_Time1 = 1000;
		static int M_Time2 = 2000;
		static int M_Time3 = 3000;
		static int M_Time4 = 4000;
		static int M_Time5 = 5000;
		static int M_Time6 = 6000;
		static int M_Time7 = 7000;
		static int M_Time8 = 8000;
		static int M_Time9 = 9000;
		static int M_Time10 = 16000;		//	空气泵运行时间选项

		static int M_Speed1 = 600;
		static int M_Speed2 = 90;
		static int M_Speed3 = 70;
		static int M_Speed4 = 58;
		static int M_Speed5 = 46;
		static int M_Speed6 = 36;
		static int M_Speed7 = 28;
		static int M_Speed8 = 20;			// 空气泵运行速度选项
		struct uM_TypeSet
		{
			public int M_Time;
			public int M_Speed;
			public uM_TypeSet(int Time, int Speed)
			{
				M_Time = Time;
				M_Speed = Speed;
			}

		};

		uM_TypeSet[] M_TypeSet = 
		{
			new uM_TypeSet(M_Time6,M_Speed1),
			new uM_TypeSet(M_Time10, M_Speed1),
			new uM_TypeSet(M_Time4, M_Speed3),
			new uM_TypeSet(M_Time10, M_Speed3),
			new uM_TypeSet(M_Time6, M_Speed5),
			new uM_TypeSet(M_Time3, M_Speed8),
			new uM_TypeSet(M_Time6, M_Speed8),
			new uM_TypeSet(M_Time10, M_Speed8),
			new uM_TypeSet(M_Time2, M_Speed6),
			new uM_TypeSet(M_Time4, M_Speed1),
			new uM_TypeSet(M_Time6, M_Speed6),
			new uM_TypeSet(M_Time10, M_Speed6),
		};

		ProErr MotorCTRL(enumM_Type M_Type)
		{
			ProErr
				Err = ProErr.ProNOErr;
			int i = 0;
			switch (M_Type)
			{
				case enumM_Type.M_Type1:
				case enumM_Type.M_Type2:
				case enumM_Type.M_Type3:
				case enumM_Type.M_Type4:
				case enumM_Type.M_Type5:
				case enumM_Type.M_Type7:
				case enumM_Type.M_Type8:
					{
						WaterValveCTRL(1);
						HoldingSingelWrite(Auto_22, M_TypeSet[(int)(enumM_Type)M_Type].M_Time);
						HoldingSingelWrite(Auto_10, 1);
						HoldingSingelWrite(Auto_21, M_TypeSet[(int)(enumM_Type)M_Type].M_Speed);
						osDelay(300);
						do
						{
							osDelay(300);
						} while ((HoldingSingelRead(Auto_11) != 0) &&/* HoldingSingelRead(Auto_23) && */ (i++ < (M_TypeSet[(int)(enumM_Type)M_Type].M_Time + 3000000) / 300));


						if (i >= (M_TypeSet[(int)(enumM_Type)M_Type].M_Time + 3000000) / 300)
						{
							//g_pBaseInterface->ptrFuncLogger("ProCommonError %s", i);
							return ProErr.ProCommonErr;
						}
						break;
					}
				case enumM_Type.M_Type6:
				case enumM_Type.M_Type9:
					{
						WaterValveCTRL(1);
						HoldingSingelWrite(Auto_22, M_TypeSet[(int)(enumM_Type)M_Type].M_Time);
						HoldingSingelWrite(Auto_21, M_TypeSet[(int)(enumM_Type)M_Type].M_Speed);
						osDelay(1500);
						HoldingSingelWrite(Auto_10, 1);
						do
						{
							osDelay(300);
						} while ((HoldingSingelRead(Auto_11) != 0) && (i++ < (M_TypeSet[(int)(enumM_Type)M_Type].M_Time + 3000000) / 300));


						if (i >= (M_TypeSet[(int)(enumM_Type)M_Type].M_Time + 3000000) / 300)
						{
							//g_pBaseInterface->ptrFuncLogger("ProCommonErrorM_Type6 %s", i);
							return ProErr.ProCommonErr;
						}
						break;
					}

				case enumM_Type.M_Type10:
					break;
				case enumM_Type.M_Type11:
					{
						WaterValveCTRL(1);
						HoldingSingelWrite(Auto_22, M_TypeSet[(int)(enumM_Type)M_Type].M_Time);
						HoldingSingelWrite(Auto_27, M_TypeSet[(int)(enumM_Type)M_Type].M_Time);
						osDelay(200);
						HoldingSingelWrite(Auto_21, M_TypeSet[(int)(enumM_Type)M_Type].M_Speed);
						HoldingSingelWrite(Auto_26, M_TypeSet[(int)(enumM_Type)M_Type].M_Speed);
						osDelay(1000);
						HoldingSingelWrite(Auto_12, 1);
						osDelay(500);
						HoldingSingelWrite(Auto_10, 1);
						do
						{
							osDelay(300);
						} while ((HoldingSingelRead(Auto_11) != 0) && (i++ < (M_TypeSet[(int)(enumM_Type)M_Type].M_Time + 3000000) / 300 * 2));

						HoldingSingelWrite(Auto_10, 0);
						HoldingSingelWrite(Auto_12, 0);
						osDelay(200);
						HoldingSingelWrite(Auto_21, 0);
						HoldingSingelWrite(Auto_26, 0);

						if (i >= (M_TypeSet[(int)(enumM_Type)M_Type].M_Time + 3000000) / 300 * 2u)
						{
							//g_pBaseInterface->ptrFuncLogger("ProCommonErrorM_Type11 %s", i);
							return ProErr.ProCommonErr;
						}
						break;
					}
				case enumM_Type.M_Type12:
					break;
				default:
					break;
			}

			return Err;
		}

		private int FingersSpeedOldSet;
		ProErr FingersSpeed(int Speed)
		{
			ProErr
				Err = ProErr.ProNOErr;
			if (FingersSpeedOldSet != Speed)
			{
				HoldingSingelWrite(Auto_32, Speed);
				FingersSpeedOldSet = Speed;
				RTStatus.FingersSpeed = Speed;
			}

			return Err;
		}

		static int FingerZero = 0;
		static int FingerClose1 = 40;
		static int FingerClose2 = 175;
		static int FingerClose3 = 190;

		ProErr FingersCTRL(int Setsition)
		{
			ProErr
				Err = ProErr.ProNOErr;

			int i = 0;
			int FingersPosition = HoldingSingelRead(Auto_31);
			if (FingersPosition != Setsition)
				HoldingSingelWrite(Auto_30, Setsition);
			do
			{
				osDelay(100);
				FingersPosition = HoldingSingelRead(Auto_31);
			} while (Setsition != FingersPosition && (i++ < 40));

			if (i >= 40)
				Err = ProErr.ProFingersErr;
			return Err;
		}

		static int ServoZero = 1630;
		static int ServoUp = 100;
		void ServoCTRL(int Setsition)
		{
			int i = 0;
			RTStatus.ServoPosition = HoldingSingelRead(Auto_36);
			if (Setsition != RTStatus.ServoPosition)
			{
				HoldingSingelWrite(Auto_35, Setsition);
				do
				{
					osDelay(300);
				} while ((HoldingSingelRead(Auto_36) != Setsition) && (i++ < 20));

				RTStatus.ServoPosition = Setsition;
			}


		}











		static int CoordinateTransX = 0;
		static int CoordinateTransY = 0;
		static int[,] XY_HeadSample = new int[2, 2]
		{
			{ 10300, 1200 },	//	注液区
			{ 1600, 7356 },	//	水样区
		};
		static int[,] XY_HeadFingers = new int[2, 2]
		{
			{ 10270, 6280 },	//	注液区
			{ 1000, 33131 },	//	消解区
		};
		public enum enumCoordinateType
		{
			CoordSampleHead,
			CoordFingers,
		};

		enum enumAreaType
		{
			AreaReagentAdd,
			AreaSampler,
			AreaDigestion,
		};
		static int NoX = 0;
		static int NoY = 0;
		ProErr NoXYTrans(int SerialNumbler)
		{
			ProErr
				Err = ProErr.ProNOErr;
			if (SerialNumbler == 0)
				Err = ProErr.ProCoordinateErr;
			if (Err != ProErr.ProNOErr)
				return Err;
			if (SerialNumbler <= 4u)
			{
				NoX = 0;
				NoY = SerialNumbler;
			}
			else
				if (SerialNumbler <= 10)
				{
					NoX = 1;
					NoY = SerialNumbler - 5;
				}
				else
					if (SerialNumbler <= 16u)
					{
						NoX = 2;
						NoY = SerialNumbler - 11;
					}
					else
						if (SerialNumbler <= 22u)
						{
							NoX = 3;
							NoY = SerialNumbler - 17;
						}
						else
							if (SerialNumbler <= 28u)
							{
								NoX = 4;
								NoY = SerialNumbler - 23;
							}
							else
								if (SerialNumbler <= 32)
								{
									NoX = 5;
									NoY = SerialNumbler - 28;
								}
								else
								{
									Err = ProErr.ProCoordinateErr;
								}
			return Err;
		}

		ProErr CoordinateTransformation(enumCoordinateType CoordinateType, enumAreaType AreaType, int SerialNumbler)
		{
			ProErr
				Err = ProErr.ProNOErr;
			switch (CoordinateType)
			{
				case enumCoordinateType.CoordSampleHead:
					{
						switch (AreaType)
						{
							case enumAreaType.AreaReagentAdd:
								CoordinateTransX = XY_HeadSample[0, 0] - 8450 * (SerialNumbler - 1);
								CoordinateTransY = XY_HeadSample[0, 1] + 0;
								break;
							case enumAreaType.AreaSampler:
								Err = NoXYTrans(SerialNumbler);
								if (Err != ProErr.ProNOErr)
									return Err;
								CoordinateTransX = XY_HeadSample[1, 0] + (int)(/*6400.0f / 72 * 32*/2844.44f * NoX + 0.5f);
								CoordinateTransY = XY_HeadSample[1, 1] + (int)(/*6400.0f / 72 * 32*/2844.44f * NoY + 0.5f);
								break;
							case enumAreaType.AreaDigestion:
								Err = ProErr.ProCoordinateErr;
								break;
							default:
								break;
						}
					}
					break;
				case enumCoordinateType.CoordFingers:
					{
						switch (AreaType)
						{
							case enumAreaType.AreaReagentAdd:
								CoordinateTransX = XY_HeadFingers[0, 0] + 6150 * (SerialNumbler - 1);
								CoordinateTransY = XY_HeadFingers[0, 1] + 0;
								break;
							case enumAreaType.AreaSampler:
								Err = ProErr.ProCoordinateErr;
								break;
							case enumAreaType.AreaDigestion:
								Err = NoXYTrans(SerialNumbler);
								if (Err != ProErr.ProNOErr)
									return Err;
								CoordinateTransX = XY_HeadFingers[1, 0] + (int)(/*6400.0f / 72 * 32*/2844.44f * NoX + 0.5f);
								CoordinateTransY = XY_HeadFingers[1, 1] + (int)(/*6400.0f / 72 * 32*/2844.44f * NoY + 0.5f);
								break;
							default:
								break;
						}

					}
					break;
				default:
					break;
			}
			return Err;
		}


		ProErr RBootARMMoveXYZ(int X, int Y, int Z)
		{
			int i = 0;
			int LocationX = HoldingSingelRead(ARM_06);
			int LocationY = HoldingSingelRead(ARM_11);
			int LocationZ = HoldingSingelRead(ARM_16);
			ProErr
				Err = ProErr.ProNOErr;
			i = 0;
			if ((LocationX != X) || (LocationY != Y))
			{
				if ((LocationZ > 400))
				{
					HoldingSingelWrite(ARM_15, 400);
					do
					{
						osDelay(300);
					} while ((HoldingSingelRead(ARM_16) != 400) && (i++ < Math.Abs(400 - LocationZ) / 10));
					if (i >= Math.Abs(0 - LocationZ) / 10)
					{
						Err = ProErr.ProArmZErr;
						return Err;
					}
					LocationZ = RTStatus.RbootArmPosition[2u] = HoldingSingelRead(ARM_16);
				}
				HoldingSingelWrite(ARM_05, X);
				HoldingSingelWrite(ARM_10, Y);

				i = 0;
				int RT_X;
				int RT_Y;
				do
				{
					osDelay(300);
					RT_X = HoldingSingelRead(ARM_06);
					RT_Y = HoldingSingelRead(ARM_11);
				} while (
					((X != RT_X) || (Y != RT_Y)) &&
					(i++ <
					(
					Math.Abs(X - LocationX) > Math.Abs(Y - LocationY) ?
					Math.Abs(X - LocationX) : Math.Abs(Y - LocationY)
					) / 10
					)
					);
				if (i >=
					(
					Math.Abs(X - LocationX) > Math.Abs(Y - LocationY) ?
					Math.Abs(X - LocationX) : Math.Abs(Y - LocationY)
					) / 10
					)
				{
					Err = ProErr.ProArmXErr;
					return Err;
				}
				LocationX = RTStatus.RbootArmPosition[0] = HoldingSingelRead(ARM_06);
				LocationY = RTStatus.RbootArmPosition[1u] = HoldingSingelRead(ARM_11);
			}
			return Err;
		}

		ProErr RBootARMMoveXY(enumCoordinateType CoordinateType, enumAreaType AreaType, int SerialNumbler)
		{
			int i = 0;
			int LocationX = HoldingSingelRead(ARM_06);
			int LocationY = HoldingSingelRead(ARM_11);
			int LocationZ = HoldingSingelRead(ARM_16);
			ProErr
				Err = ProErr.ProNOErr;
			i = 0;
			Err = CoordinateTransformation(CoordinateType, AreaType, SerialNumbler);
			if (Err != ProErr.ProNOErr)
				return Err;

			if ((LocationX != CoordinateTransX) || (LocationY != CoordinateTransY))
			{

				if ((LocationZ > Z_High0))
				{
					HoldingSingelWrite(ARM_15, Z_High0);
					do
					{
						osDelay(10);
					} while ((HoldingSingelRead(ARM_16) != Z_High0) && (i++ < Math.Abs(Z_High0 - LocationZ) / 3u));
					if (i >= Math.Abs(Z_High0 - LocationZ) / 3u)
					{
						Err = ProErr.ProArmZErr;
						return Err;
					}
					LocationZ = RTStatus.RbootArmPosition[2u] = HoldingSingelRead(ARM_16);
				}
				HoldingSingelWrite(ARM_05, CoordinateTransX);
				HoldingSingelWrite(ARM_10, CoordinateTransY);

				i = 0;
				int X;
				int Y;
				do
				{
					osDelay(10);
					X = HoldingSingelRead(ARM_06);
					Y = HoldingSingelRead(ARM_11);
				} while (
					((X != CoordinateTransX) || (Y != CoordinateTransY))
					&& (i++ <
					(
					Math.Abs(CoordinateTransX - LocationX) > Math.Abs(CoordinateTransY - LocationY) ?
					Math.Abs(CoordinateTransX - LocationX) : Math.Abs(CoordinateTransY - LocationY)
					) / 3u
					)
					);
				if (i >=
					(
					Math.Abs(CoordinateTransX - LocationX) > Math.Abs(CoordinateTransY - LocationY) ?
					Math.Abs(CoordinateTransX - LocationX) : Math.Abs(CoordinateTransY - LocationY)
					) / 3u
					)
				{
					if (X != CoordinateTransX)
						Err = ProErr.ProArmXErr;
					if (Y != CoordinateTransY)
						Err = ProErr.ProArmYErr;
					return Err;
				}
				LocationX = RTStatus.RbootArmPosition[0] = HoldingSingelRead(ARM_06);
				LocationY = RTStatus.RbootArmPosition[1u] = HoldingSingelRead(ARM_11);
			}
			return Err;
		}
		//	Z轴机械臂位置
		static int Z_High0 = 400;		//	复位
		static int Z_High1 = 12700;	//	排废液/注水样
		static int Z_High2 = 12000;	//	吸取水样
		static int Z_High3 = 11700;	//	抓取消解管
		static int Z_High6 = 11550;	//	抓取消解管
		static int Z_High4 = 10400;//	拔塞子
		static int Z_High5 = 10550;	//	压塞子

		ProErr RBootARMMoveZ(int High)
		{
			int i = 0;
			int LocationZ = HoldingSingelRead(ARM_16);
			ProErr
				Err = ProErr.ProNOErr;
			i = 0;
			if (High != 0)
			{
				if (RTStatus.RbootArmPosition[0] == XY_HeadSample[0, 0])
					if (RTStatus.RbootArmPosition[1u] == XY_HeadSample[0, 1])
					{
						if (RTStatus.FingersPosition >= 1500)
							if (RTStatus.RbootArmSignelPosition == SignelArm_1)
							{
								Err = ProErr.ProArmZErr;
							}
					}

				if (RTStatus.RbootArmPosition[0] == XY_HeadSample[0, 0])
					if (RTStatus.RbootArmPosition[1u] == XY_HeadSample[0, 1] + 6150)
					{
						if (RTStatus.FingersPosition >= 1500)
							if (RTStatus.RbootArmSignelPosition == SignelArm_3)
							{
								Err = ProErr.ProArmZErr;
							}
					}

				if (RTStatus.RbootArmPosition[0] == XY_HeadFingers[0, 0])
					if (RTStatus.RbootArmPosition[1u] == XY_HeadFingers[0, 1])
					{
						if (RTStatus.FingersPosition >= 1500)
							if (RTStatus.RbootArmSignelPosition == SignelArm_1)
							{
								Err = ProErr.ProArmZErr;
							}
					}
				if (Err != ProErr.ProNOErr)
					while (true)
					{
						osDelay(10);
					};

			}

			if (HoldingSingelRead(ARM_16) != High)
			{
				HoldingSingelWrite(ARM_15, High);
				do
				{
					osDelay(10);
				} while ((HoldingSingelRead(ARM_16) != High) && (i++ < Math.Abs(High - LocationZ) / 3u));
				if (i >= Math.Abs(High - LocationZ) / 3u)
				{
					Err = ProErr.ProArmZErr;
					return Err;
				}
				LocationZ = RTStatus.RbootArmPosition[2u] = HoldingSingelRead(ARM_16);
			}
			return Err;
		}

		ProErr RBootArmXFindZero()
		{
			ProErr
				Err = ProErr.ProNOErr;
			int i = 0;
			HoldingSingelWrite(ARM_09, 1);
			do
			{
				osDelay(100);
			} while ((i++ < 30) && (HoldingSingelRead(ARM_09) != 0));
			if (i >= 30)
			{
				Err = ProErr.ProArmZFindZeroErr;
			}
			return Err;
		}

		ProErr RBootArmYFindZero()
		{
			ProErr
				Err = ProErr.ProNOErr;
			int i = 0;
			HoldingSingelWrite(ARM_14, 1);
			do
			{
				osDelay(100);
			} while ((i++ < 30) && (HoldingSingelRead(ARM_14) != 0));
			if (i >= 30)
			{
				Err = ProErr.ProArmZFindZeroErr;
			}
			return Err;
		}

		ProErr RBootArmZFindZero()
		{
			ProErr
				Err = ProErr.ProNOErr;
			int i = 0;
			HoldingSingelWrite(ARM_19, 1);
			do
			{
				osDelay(100);
			} while ((i++ < 30) && (HoldingSingelRead(ARM_19) != 0));
			if (i >= 30)
			{
				Err = ProErr.ProArmZFindZeroErr;
			}
			return Err;
		}
















		static int SignelArm_1 = 1250;
		static int SignelArm_2 = 3900;
		static int SignelArm_3 = 5600;

		ProErr MeterARMMove(int ARMX)
		{
			int i = 0;
			int LocationZ = HoldingSingelRead(Meter_06);
			ProErr
				Err = ProErr.ProNOErr;
			i = 0;

			if (HoldingSingelRead(Meter_06) != ARMX)
			{
				HoldingSingelWrite(Meter_05, ARMX);
				do
				{
					osDelay(10);
				} while ((HoldingSingelRead(Meter_06) != ARMX) && (i++ < Math.Abs(ARMX - LocationZ) / 3u));
				if (i >= Math.Abs(ARMX - LocationZ) / 3u)
				{
					Err = ProErr.ProArmZErr;
					return Err;
				}
				LocationZ = RTStatus.RbootArmSignelPosition = HoldingSingelRead(Meter_06);
				if(LocationZ == SignelArm_1)
				{
					ProcessCurrent.RengentAdd = 1;
				}
				else if (LocationZ == SignelArm_2)
				{
					ProcessCurrent.RengentAdd = 2;
				}
				else if (LocationZ == SignelArm_3)
				{
					ProcessCurrent.RengentAdd = 3;
				}
			}
			return Err;
		}

		static int[] CHxPumpSpeedOldSet = new int[(int)enumPumpType.PumpMax];
		void CHxPumpSpeed(int Speed, enumPumpType Type)
		{
			if (CHxPumpSpeedOldSet[(int)Type] != Speed)
			{
				switch (Type)
				{
					case enumPumpType.SamplePump:
						HoldingSingelWrite(Auto_08, Speed);
						break;
					case enumPumpType.CH1Pump:
						HoldingSingelWrite(Meter_27, Speed);
						break;
					case enumPumpType.CH2Pump:
						HoldingSingelWrite(Meter_32, Speed);
						break;
					case enumPumpType.CH3Pump:
						HoldingSingelWrite(Meter_37, Speed);
						break;
					case enumPumpType.CH4Pump:
						HoldingSingelWrite(Meter_42, Speed);
						break;
					case enumPumpType.PumpMax:
					default:
						return;
				}
				CHxPumpSpeedOldSet[(int)Type] = Speed;
				RTStatus.PumpSpeed[(int)Type] = Speed;
			}
		}




		static float JYJDCH1 = 2.0833f;
		static float JYJDCH2 = 1.0381f;
		static float JYJDCH3 = 1.0381f;
		static float JYJDCH4 = 0.4154f;

		static int PumpPositionMaxCH1 = 19800;
		static int PumpPositionMaxCH2 = 9800;
		static int PumpPositionMaxCH3 = 9800;
		static int PumpPositionMaxCH4 = 4900;
		void CHxPumpPositionQuery()
		{
			for (int Typei = 0; Typei < (int)enumPumpType.PumpMax; Typei++)
			{
				switch ((enumPumpType)Typei)
				{
					case enumPumpType.SamplePump:
						RTStatus.PumpPosition[Typei] = HoldingSingelRead(Auto_06);
						break;
					case enumPumpType.CH1Pump:
						RTStatus.PumpPosition[Typei] = HoldingSingelRead(Meter_26);
						break;
					case enumPumpType.CH2Pump:
						RTStatus.PumpPosition[Typei] = HoldingSingelRead(Meter_31);
						break;
					case enumPumpType.CH3Pump:
						RTStatus.PumpPosition[Typei] = HoldingSingelRead(Meter_36);
						break;
					case enumPumpType.CH4Pump:
						RTStatus.PumpPosition[Typei] = HoldingSingelRead(Meter_41);
						break;
					case enumPumpType.PumpMax:
						break;
					default:
						break;
				}
			}
		}
		int CHxPumpPositionConvert(int ulPumpPosition, enumPumpType Type)
		{
			//	进样精度
			int PumpPosition;
			int PumpPositionMaxCHx;
			float JYJDCHx = JYJDCH2;
			switch (Type)
			{
				case enumPumpType.SamplePump:
					JYJDCHx = JYJDCH2;
					PumpPositionMaxCHx = SamplePumpPositionMax;
					RTStatus.PumpPosition[(int)Type] = HoldingSingelRead(Auto_06);
					break;
				case enumPumpType.CH1Pump:
					JYJDCHx = JYJDCH1;
					PumpPositionMaxCHx = PumpPositionMaxCH1;
					RTStatus.PumpPosition[(int)Type] = HoldingSingelRead(Meter_26);
					break;
				case enumPumpType.CH2Pump:
					JYJDCHx = JYJDCH2;
					PumpPositionMaxCHx = PumpPositionMaxCH2;
					RTStatus.PumpPosition[(int)Type] = HoldingSingelRead(Meter_31);
					break;
				case enumPumpType.CH3Pump:
					JYJDCHx = JYJDCH3;
					PumpPositionMaxCHx = PumpPositionMaxCH3;
					RTStatus.PumpPosition[(int)Type] = HoldingSingelRead(Meter_36);
					break;
				case enumPumpType.CH4Pump:
					JYJDCHx = JYJDCH4;
					PumpPositionMaxCHx = PumpPositionMaxCH4;
					RTStatus.PumpPosition[(int)Type] = HoldingSingelRead(Meter_41);
					break;
				case enumPumpType.PumpMax:
				default:
					return -1;
			}

			if (ulPumpPosition == 0)
			{
				PumpPosition = 0;
			}
			else if (ulPumpPosition > 0)
			{
				PumpPosition = (int)((float)(ulPumpPosition + JYJDCHx / 2) / JYJDCHx);
			}
			else
			{
				PumpPosition = (int)((float)(ulPumpPosition - JYJDCHx / 2) / JYJDCHx);
			}

			if ((int)RTStatus.PumpPosition[(int)Type] + PumpPosition > (PumpPositionMaxCHx + JYJDCHx / 2) / JYJDCHx)
			{
				PumpPosition = (int)((PumpPositionMaxCHx + JYJDCHx / 2) / JYJDCHx);
			}
			else if ((int)RTStatus.PumpPosition[(int)Type] + PumpPosition <= PumpZero)
			{
				PumpPosition = 0;
			}
			else
			{
				PumpPosition += RTStatus.PumpPosition[(int)Type];
			}
			return PumpPosition;
		}
		ProErr CHxPumpCTRL(int[] ulPumpPosition)
		{
			ProErr
				Err = ProErr.ProNOErr;

			int Typei = 0;
			int i = 0;
			int ii = 0;

			int[] Common = new int[(int)enumPumpType.PumpMax] { 0, 0, 0, 0, 0 };
			int[] j = new int[(int)enumPumpType.PumpMax] { 0, 0, 0, 0, 0 };
			int[] t = new int[(int)enumPumpType.PumpMax] { 0, 0, 0, 0, 0 };
			bool[] COMCheck = new bool[(int)enumPumpType.PumpMax] { false, false, false, false, false };
			bool[] IsOK = new bool[(int)enumPumpType.PumpMax] { false, false, false, false, false };
			int[] PumpPosition = new int[(int)enumPumpType.PumpMax] { 0, 0, 0, 0, 0 };
			int[] RTPumpPosition = new int[(int)enumPumpType.PumpMax] { 0, 0, 0, 0, 0 };
			osDelay(1500);
			//for (Typei = 0; Typei < (int)enumPumpType.PumpMax; Typei++)
			//{
			//	switch ((enumPumpType)Typei)
			//	{
			//		case enumPumpType.SamplePump:
			//			//RTPumpPosition[Typei] = HoldingSingelRead(Auto_06);
			//			break;
			//		case enumPumpType.CH1Pump:
			//			RTPumpPosition[Typei] = HoldingSingelRead(Meter_26);
			//			break;
			//		case enumPumpType.CH2Pump:
			//			RTPumpPosition[Typei] = HoldingSingelRead(Meter_31);
			//			break;
			//		case enumPumpType.CH3Pump:
			//			RTPumpPosition[Typei] = HoldingSingelRead(Meter_36);
			//			break;
			//		case enumPumpType.CH4Pump:
			//			RTPumpPosition[Typei] = HoldingSingelRead(Meter_41);
			//			break;
			//		case enumPumpType.PumpMax:
			//			break;
			//		default:
			//			break;
			//	}
			//	if (RTPumpPosition[Typei] != 0xFFFF)
			//	{
			//		RTStatus.PumpPosition[Typei] = RTPumpPosition[Typei];
			//	}
			//	PumpPosition[Typei] = CHxPumpPositionConvert(ulPumpPosition[Typei], (enumPumpType)Typei);
			//	COMCheck[Typei] = false;
			//	Common[Typei] = 0;
			//	j[Typei] = 0;
			//	t[Typei] = 0;
			//	if (Math.Abs(PumpPosition[Typei] - RTStatus.PumpPosition[Typei]) > PumpZero)
			//	{
			//		switch ((enumPumpType)Typei)
			//		{
			//			case enumPumpType.SamplePump:
			//				//HoldingSingelWrite(Auto_05, PumpPosition[(int)(enumPumpType.SamplePump)]);
			//				break;
			//			case enumPumpType.CH1Pump:
			//				HoldingSingelWrite(Meter_25, PumpPosition[(int)enumPumpType.CH1Pump]);
			//				break;
			//			case enumPumpType.CH2Pump:
			//				HoldingSingelWrite(Meter_30, PumpPosition[(int)enumPumpType.CH2Pump]);
			//				break;
			//			case enumPumpType.CH3Pump:
			//				HoldingSingelWrite(Meter_35, PumpPosition[(int)enumPumpType.CH3Pump]);
			//				break;
			//			case enumPumpType.CH4Pump:
			//				HoldingSingelWrite(Meter_40, PumpPosition[(int)enumPumpType.CH4Pump]);
			//				break;
			//			case enumPumpType.PumpMax:
			//				break;
			//			default:
			//				break;
			//		}
			//	}
			//	int CountOut = 0;
			//	if (RTStatus.PumpSpeed[Typei] > 0)
			//		CountOut = (Math.Abs(PumpPosition[Typei] - RTStatus.PumpPosition[Typei])/*要走的步数*/ / (RTStatus.PumpSpeed[Typei]/*每分钟转速*/ * 400/*单圈步数*/ / 60/*秒*/ * 1000/*毫秒*/)) / 300/*计数周期*/ + 100;
			//	if (CountOut > ii)
			//		ii = CountOut;
			//	if (PumpPosition[Typei] > PumpZero)
			//	{
			//		COMCheck[Typei] = true;
			//		ii *= 6;
			//	}
			//}

			//i = 0;
			//do
			//{
			//	for (Typei = 0; Typei < (int)enumPumpType.PumpMax; Typei++)
			//	{
			//		if (COMCheck[Typei])
			//		{
			//			t[Typei]++;
			//			switch ((enumPumpType)Typei)
			//			{
			//				case enumPumpType.SamplePump:
			//					//Common[Typei] = HoldingSingelRead(Auto_09);
			//					break;
			//				case enumPumpType.CH1Pump:
			//					Common[Typei] = HoldingSingelRead(Meter_29);
			//					break;
			//				case enumPumpType.CH2Pump:
			//					Common[Typei] = HoldingSingelRead(Meter_34);
			//					break;
			//				case enumPumpType.CH3Pump:
			//					Common[Typei] = HoldingSingelRead(Meter_39);
			//					break;
			//				case enumPumpType.CH4Pump:
			//					Common[Typei] = HoldingSingelRead(Meter_44);
			//					break;
			//				case enumPumpType.PumpMax:
			//					break;
			//				default:
			//					break;
			//			}
			//			if (Common[Typei] == 0)
			//			{
			//				j[Typei]++;
			//			}
			//			else
			//			{
			//				j[Typei] = 0;
			//			}

			//			osDelay(50);
			//		}
			//		else
			//		{
			//			osDelay(300);
			//		}

			//		if (t[Typei] % 6 == 0)
			//		{
			//			switch ((enumPumpType)Typei)
			//			{
			//				case enumPumpType.SamplePump:
			//					//RTStatus.PumpPosition[Typei] = HoldingSingelRead(Auto_06);
			//					break;
			//				case enumPumpType.CH1Pump:
			//					RTStatus.PumpPosition[Typei] = HoldingSingelRead(Meter_26);
			//					break;
			//				case enumPumpType.CH2Pump:
			//					RTStatus.PumpPosition[Typei] = HoldingSingelRead(Meter_31);
			//					break;
			//				case enumPumpType.CH3Pump:
			//					RTStatus.PumpPosition[Typei] = HoldingSingelRead(Meter_36);
			//					break;
			//				case enumPumpType.CH4Pump:
			//					RTStatus.PumpPosition[Typei] = HoldingSingelRead(Meter_41);
			//					break;
			//				case enumPumpType.PumpMax:
			//				default:
			//					break;
			//			}
			//			if ((Math.Abs(PumpPosition[Typei] - RTStatus.PumpPosition[Typei]) > PumpZero))
			//				IsOK[Typei] = false;
			//			else
			//				IsOK[Typei] = true;
			//		}
			//	}
			//} while (
			//	(!IsOK[(int)enumPumpType.CH1Pump]
			//	|| !IsOK[(int)enumPumpType.CH2Pump]
			//	|| !IsOK[(int)enumPumpType.CH3Pump]
			//	|| !IsOK[(int)enumPumpType.CH4Pump]
			//	)
			//	&& (i++ < ii)
			//		);

			//if (i > ii)
			//{
			//	Err = ProErr.ProPumpBlockingErr;
			//}
			//for (Typei = 0; Typei < (int)enumPumpType.PumpMax; Typei++)
			//{
			//	if (COMCheck[Typei] == true)
			//	{
			//		if (j[Typei] > 60)
			//		{
			//			RTStatus.Exist[Typei] = RTStatus.LevelSwitch[Typei] = enumLevelStatus.LevelStatusBlank;
			//			Err = ProErr.ProCommonErr;
			//		}
			//		else
			//		{
			//			RTStatus.Exist[Typei] = RTStatus.LevelSwitch[Typei] = enumLevelStatus.LevelStatusNonblank;
			//		}
			//		COMCheck[Typei] = false;
			//	}
			//}
			return Err;
		}

		ProErr StirrerCtrl(bool CMD)
		{
			ProErr Err = ProErr.ProNOErr;
			if (CMD)
				HoldingSingelWrite(Meter_18, 1);
			else
				HoldingSingelWrite(Meter_18, 0);
			return Err;
		}

		ProErr HeatColdCtrl(bool CMD)
		{
			ProErr Err = ProErr.ProNOErr;
			if (CMD)
			{
				HoldingSingelWrite(Heat_15, 1);
				HoldingSingelWrite(Heat_17, 165);
			}
			else
				HoldingSingelWrite(Heat_15, 0);
			return Err;
		}






	}
}
