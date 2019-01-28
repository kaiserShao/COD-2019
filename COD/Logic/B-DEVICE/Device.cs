using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COD
{
	public partial class frmLogic
	{
		public bool ProcessReagents = true;
		public bool ProcessFilling = true;
		public bool ProcessFinger = true;

		void qingxi()
		{
			RBootARMMoveZ(Z_High0);
			WaterValveCTRL(0);
			SamplePumpSpeed(60);
			SamplePumpCTRL(4000);
			SamplePumpSpeed(120);
			WaterValveCTRL(1);
			SamplePumpCTRL(3000);

			RBootARMMoveXY(enumCoordinateType.CoordSampleHead, enumAreaType.AreaReagentAdd, 2);
			RBootARMMoveZ(Z_High1);
			SamplePumpCTRL(-3500);
			SamplePumpSpeed(40);
			SamplePumpCTRL(-10000);
			MotorCTRL(enumM_Type.M_Type3);
			MotorCTRL(enumM_Type.M_Type11);
			MotorCTRL(enumM_Type.M_Type6);
			RBootARMMoveZ(Z_High0);
		}

		void runxi(int x)
		{
			RBootARMMoveXY(enumCoordinateType.CoordSampleHead, enumAreaType.AreaSampler, x);
			RBootARMMoveZ(Z_High2);
			ProcessCurrent.SamplerUsing = x;
			WaterValveCTRL(1);
			SamplePumpSpeed(40);
			SamplePumpCTRL(2500);
			RBootARMMoveZ(Z_High0);
			ProcessCurrent.SamplerUsing = 0;
			SamplePumpSpeed(120);
			SamplePumpCTRL(4500);

			RBootARMMoveXY(enumCoordinateType.CoordSampleHead, enumAreaType.AreaReagentAdd, 2);
			RBootARMMoveZ(Z_High1);
			ProcessCurrent.SampleEffluents = true;
			SamplePumpCTRL(-4500);
			SamplePumpSpeed(40);
			SamplePumpCTRL(-10000);
			MotorCTRL(enumM_Type.M_Type3);
			MotorCTRL(enumM_Type.M_Type11);
			MotorCTRL(enumM_Type.M_Type6);
			ProcessCurrent.SampleEffluents = false;
		}
		void qushuiyang(int x)
		{

			ServoCTRL(ServoUp);

			RBootARMMoveXY(enumCoordinateType.CoordSampleHead, enumAreaType.AreaSampler, x);
			RBootARMMoveZ(Z_High2);

			WaterValveCTRL(1);
			SamplePumpSpeed(40);
			SamplePumpCTRL(4000);

			RBootARMMoveZ(Z_High0);
			SamplePumpCTRL(1000);

			RBootARMMoveXY(enumCoordinateType.CoordSampleHead, enumAreaType.AreaReagentAdd, 2);
			RBootARMMoveZ(Z_High1);

			SamplePumpSpeed(30);
			SamplePumpCTRL(-1700);
			MotorCTRL(enumM_Type.M_Type2);
			MotorCTRL(enumM_Type.M_Type9);

		}
		void zhushuiyang()
		{

			RBootARMMoveXY(enumCoordinateType.CoordSampleHead, enumAreaType.AreaReagentAdd, 1);
			RBootARMMoveZ(Z_High1);
			ProcessCurrent.SampleAdd = true;
			SamplePumpSpeed(30);
			SamplePumpCTRL(-2000);
			MotorCTRL(enumM_Type.M_Type2);
			MotorCTRL(enumM_Type.M_Type9);
			ProcessCurrent.SampleAdd = false;

			RBootARMMoveXY(enumCoordinateType.CoordSampleHead, enumAreaType.AreaSampler, 1);

		}
		void paikong()
		{
			RBootARMMoveXY(enumCoordinateType.CoordSampleHead, enumAreaType.AreaReagentAdd, 2);
			RBootARMMoveZ(Z_High1);
			SamplePumpSpeed(30);
			SamplePumpCTRL(-10000);
			MotorCTRL(enumM_Type.M_Type3);
			MotorCTRL(enumM_Type.M_Type9);
		}

		void quxiaojieguan(int x)
		{
			RBootARMMoveXY(enumCoordinateType.CoordFingers, enumAreaType.AreaDigestion, x);
			ProcessCurrent.DigestionUsing = x;
			FingersCTRL(FingerClose1);
			ServoCTRL(ServoZero);
			RBootARMMoveZ(Z_High3);
			FingersCTRL(FingerClose2);
			ProcessCurrent.DigestionUsing = 0;
			MeterARMMove(SignelArm_2);
			RBootARMMoveXY(enumCoordinateType.CoordFingers, enumAreaType.AreaReagentAdd, 1);
			RBootARMMoveZ(Z_High6);
			FingersCTRL(FingerZero);
			RBootARMMoveZ(Z_High4);
		}
		void basaizi()
		{
			MeterARMMove(SignelArm_3);
			FingersCTRL(FingerClose3);
			RBootARMMoveZ(Z_High4 - 1500);
			RBootARMMoveZ(Z_High0);
			ServoCTRL(ServoUp);
		}

		void yasaiziguiwei(int x)
		{
			RBootARMMoveZ(Z_High0);
			ServoCTRL(ServoZero);
			MeterARMMove(SignelArm_2);

			RBootARMMoveXY(enumCoordinateType.CoordFingers, enumAreaType.AreaReagentAdd, 1);
			RBootARMMoveZ(Z_High5 - 1200);
			RBootARMMoveZ(Z_High5);
			osDelay(1500);
			RBootARMMoveZ(Z_High4);
			FingersCTRL(FingerZero);
			RBootARMMoveZ(Z_High6-100);
			FingersCTRL(FingerClose2);
			RBootARMMoveXY(enumCoordinateType.CoordFingers, enumAreaType.AreaDigestion, x);
			RBootARMMoveZ(Z_High3);
			FingersCTRL(FingerZero);
			RBootARMMoveZ(Z_High0);
			RBootArmZFindZero();
			RBootARMMoveXYZ(400, 400, Z_High0);

		}
		void zhushijiInit()
		{
			int[] PumpRange = new int[(int)enumPumpType.PumpMax];

			CHxPumpPositionQuery();

			CHxPumpSpeed(30, enumPumpType.CH1Pump);
			CHxPumpSpeed(30, enumPumpType.CH2Pump);
			CHxPumpSpeed(30, enumPumpType.CH3Pump);
			CHxPumpSpeed(30, enumPumpType.CH4Pump);

			PumpRange[(int)enumPumpType.SamplePump] = 0;
			PumpRange[(int)enumPumpType.CH1Pump] = 0;
			PumpRange[(int)enumPumpType.CH2Pump] = 0;
			PumpRange[(int)enumPumpType.CH3Pump] = 0;
			PumpRange[(int)enumPumpType.CH4Pump] = 0;

			if (RTStatus.PumpPosition[(int)enumPumpType.CH1Pump] < 800)
			{
				HoldingSingelWrite(Meter_10, 0);
				PumpRange[(int)enumPumpType.CH1Pump] = 800;
				CHxPumpSpeed(15, enumPumpType.CH1Pump);
			}
			if (RTStatus.PumpPosition[(int)enumPumpType.CH2Pump] < 400)
			{
				HoldingSingelWrite(Meter_12, 0);
				PumpRange[(int)enumPumpType.CH2Pump] = 400;
				CHxPumpSpeed(30, enumPumpType.CH2Pump);
			}
			if (RTStatus.PumpPosition[(int)enumPumpType.CH3Pump] < 400)
			{
				HoldingSingelWrite(Meter_14, 0);
				PumpRange[(int)enumPumpType.CH3Pump] = 400;
				CHxPumpSpeed(30, enumPumpType.CH3Pump);
			}
			if (RTStatus.PumpPosition[(int)enumPumpType.CH4Pump] < 200)
			{
				HoldingSingelWrite(Meter_16, 0);
				PumpRange[(int)enumPumpType.CH4Pump] = 200;
				CHxPumpSpeed(60, enumPumpType.CH4Pump);
			}

			CHxPumpCTRL(PumpRange);

		}


		void qushiji()
		{
			
			int[] PumpRange = new int[(int)enumPumpType.PumpMax];

			MeterARMMove(SignelArm_2);

			PumpRange[(int)enumPumpType.SamplePump] = 0;
			PumpRange[(int)enumPumpType.CH1Pump] = 0;
			PumpRange[(int)enumPumpType.CH2Pump] = 0;
			PumpRange[(int)enumPumpType.CH3Pump] = 0;
			PumpRange[(int)enumPumpType.CH4Pump] = 0;

			HoldingSingelWrite(Meter_10, 0);
			HoldingSingelWrite(Meter_12, 0);
			HoldingSingelWrite(Meter_14, 0);
			HoldingSingelWrite(Meter_16, 0);

			PumpRange[(int)enumPumpType.SamplePump] = 0;
			PumpRange[(int)enumPumpType.CH1Pump] = 4000 + 100;
			PumpRange[(int)enumPumpType.CH2Pump] = 667 + 100;
			PumpRange[(int)enumPumpType.CH3Pump] = 0;//667 + 150;
			PumpRange[(int)enumPumpType.CH4Pump] = 333 + 100;
			CHxPumpSpeed(15, enumPumpType.CH1Pump);
			CHxPumpSpeed(30, enumPumpType.CH2Pump);
			CHxPumpSpeed(30, enumPumpType.CH3Pump);
			CHxPumpSpeed(60, enumPumpType.CH4Pump);
			CHxPumpCTRL(PumpRange);

			HoldingSingelWrite(Meter_10, 1);
			HoldingSingelWrite(Meter_12, 1);
			HoldingSingelWrite(Meter_14, 1);
			HoldingSingelWrite(Meter_16, 1);
			PumpRange[(int)enumPumpType.SamplePump] = 0;
			PumpRange[(int)enumPumpType.CH1Pump] = -100;
			PumpRange[(int)enumPumpType.CH2Pump] = -100;
			PumpRange[(int)enumPumpType.CH3Pump] = -100;
			PumpRange[(int)enumPumpType.CH4Pump] = -100;
			CHxPumpSpeed(15, enumPumpType.CH1Pump);
			CHxPumpSpeed(30, enumPumpType.CH2Pump);
			CHxPumpSpeed(30, enumPumpType.CH3Pump);
			CHxPumpSpeed(60, enumPumpType.CH4Pump);
			CHxPumpCTRL(PumpRange);


			HoldingSingelWrite(Meter_75, 1);
			HoldingSingelWrite(Meter_22, M_TypeSet[(int)enumM_Type.M_Type1].M_Time);
			HoldingSingelWrite(Meter_21, M_TypeSet[(int)enumM_Type.M_Type1].M_Speed);
			osDelay(6300);
			HoldingSingelWrite(Meter_75, 0);


			HoldingSingelWrite(Meter_22, M_TypeSet[(int)enumM_Type.M_Type3].M_Time);
			HoldingSingelWrite(Meter_21, M_TypeSet[(int)enumM_Type.M_Type3].M_Speed);
			osDelay(1000);
			HoldingSingelWrite(Meter_75, 1);
			osDelay(4500);
			HoldingSingelWrite(Meter_75, 0);



			HoldingSingelWrite(Meter_76, 1);
			HoldingSingelWrite(Meter_22, M_TypeSet[(int)enumM_Type.M_Type10].M_Time);
			HoldingSingelWrite(Meter_21, M_TypeSet[(int)enumM_Type.M_Type10].M_Speed);
			osDelay(4300);
			HoldingSingelWrite(Meter_76, 0);

			HoldingSingelWrite(Meter_22, M_TypeSet[(int)enumM_Type.M_Type3].M_Time);
			HoldingSingelWrite(Meter_21, M_TypeSet[(int)enumM_Type.M_Type3].M_Speed);
			osDelay(1000);
			HoldingSingelWrite(Meter_76, 1);
			osDelay(4500);
			HoldingSingelWrite(Meter_76, 0);



			HoldingSingelWrite(Meter_78, 1);
			HoldingSingelWrite(Meter_22, M_TypeSet[(int)enumM_Type.M_Type10].M_Time);
			HoldingSingelWrite(Meter_21, M_TypeSet[(int)enumM_Type.M_Type10].M_Speed);
			osDelay(4300);
			HoldingSingelWrite(Meter_78, 0);

			HoldingSingelWrite(Meter_22, M_TypeSet[(int)enumM_Type.M_Type3].M_Time);
			HoldingSingelWrite(Meter_21, M_TypeSet[(int)enumM_Type.M_Type3].M_Speed);
			osDelay(1000);
			HoldingSingelWrite(Meter_78, 1);
			osDelay(4500);
			HoldingSingelWrite(Meter_78, 0);


		}
		void zhushiji()
		{

			int[] PumpRange = new int[(int)enumPumpType.PumpMax];
			PumpRange[(int)enumPumpType.SamplePump] = 0;
			PumpRange[(int)enumPumpType.CH1Pump] = 0;
			PumpRange[(int)enumPumpType.CH2Pump] = 0;
			PumpRange[(int)enumPumpType.CH3Pump] = 0;
			PumpRange[(int)enumPumpType.CH4Pump] = 0;

			MeterARMMove(SignelArm_1);
			StirrerCtrl(true);
			HoldingSingelWrite(Meter_75, 0);
			HoldingSingelWrite(Meter_76, 0);
			HoldingSingelWrite(Meter_78, 0);

			HoldingSingelWrite(Meter_10, 1);
			HoldingSingelWrite(Meter_12, 1);
			HoldingSingelWrite(Meter_14, 1);
			HoldingSingelWrite(Meter_16, 1);

			PumpRange[(int)enumPumpType.SamplePump] = 0;
			CHxPumpSpeed(30, enumPumpType.CH3Pump);

			PumpRange[(int)enumPumpType.CH4Pump] = -333;
			CHxPumpSpeed(60, enumPumpType.CH4Pump);
			CHxPumpCTRL(PumpRange);
			PumpRange[(int)enumPumpType.CH4Pump] = 0;


			HoldingSingelWrite(Meter_78, 1);
			HoldingSingelWrite(Meter_22, M_TypeSet[(int)enumM_Type.M_Type10].M_Time);
			HoldingSingelWrite(Meter_21, M_TypeSet[(int)enumM_Type.M_Type10].M_Speed);
			osDelay(4300);
			HoldingSingelWrite(Meter_78, 0);

			HoldingSingelWrite(Meter_22, M_TypeSet[(int)enumM_Type.M_Type3].M_Time);
			HoldingSingelWrite(Meter_21, M_TypeSet[(int)enumM_Type.M_Type3].M_Speed);
			osDelay(1000);
			HoldingSingelWrite(Meter_78, 1);
			osDelay(4500);
			HoldingSingelWrite(Meter_78, 0);


			PumpRange[(int)enumPumpType.CH2Pump] = -667;
			CHxPumpSpeed(30, enumPumpType.CH2Pump);
			CHxPumpCTRL(PumpRange);
			PumpRange[(int)enumPumpType.CH2Pump] = 0;

			HoldingSingelWrite(Meter_76, 1);
			HoldingSingelWrite(Meter_22, M_TypeSet[(int)enumM_Type.M_Type10].M_Time);
			HoldingSingelWrite(Meter_21, M_TypeSet[(int)enumM_Type.M_Type10].M_Speed);
			osDelay(4300);
			HoldingSingelWrite(Meter_76, 0);

			HoldingSingelWrite(Meter_22, M_TypeSet[(int)enumM_Type.M_Type3].M_Time);
			HoldingSingelWrite(Meter_21, M_TypeSet[(int)enumM_Type.M_Type3].M_Speed);
			osDelay(1000);
			HoldingSingelWrite(Meter_76, 1);
			osDelay(4500);
			HoldingSingelWrite(Meter_76, 0);



			PumpRange[(int)enumPumpType.CH1Pump] = -4000;
			CHxPumpSpeed(10, enumPumpType.CH1Pump);
			CHxPumpCTRL(PumpRange);
			PumpRange[(int)enumPumpType.CH1Pump] = 0;


			HoldingSingelWrite(Meter_75, 1);
			HoldingSingelWrite(Meter_22, M_TypeSet[(int)enumM_Type.M_Type1].M_Time);
			HoldingSingelWrite(Meter_21, M_TypeSet[(int)enumM_Type.M_Type1].M_Speed);
			osDelay(6300);
			HoldingSingelWrite(Meter_75, 0);

			HoldingSingelWrite(Meter_22, M_TypeSet[(int)enumM_Type.M_Type3].M_Time);
			HoldingSingelWrite(Meter_21, M_TypeSet[(int)enumM_Type.M_Type3].M_Speed);
			osDelay(1000);
			HoldingSingelWrite(Meter_75, 1);
			osDelay(4500);
			HoldingSingelWrite(Meter_75, 0);

			HoldingSingelWrite(Meter_10, 0);
			HoldingSingelWrite(Meter_12, 0);
			HoldingSingelWrite(Meter_14, 0);
			HoldingSingelWrite(Meter_16, 0);
			StirrerCtrl(false);
			MeterARMMove(SignelArm_2);


		}
 

	}
}
