using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
namespace COD
{
	public partial class frmLogic
	{

		public bool[] SampleNumProcessed = new bool[32];
		public int DigestionTubeProcessed = 0;
		private void TubeAnalyze()
		{
			int[] Sum = new int[3];

			for (int i = 0; i < SampleTubeMax; i++)	//	计算所需试管量
			{
				if (!SampleNumProcessed[TaskSet.TubeNum[i]])
				{
					SampleNumProcessed[TaskSet.TubeNum[i]] = true;
					for (int Group = 1; Group <= TaskSet.ExperimentNum[i]; Group++)
					{
						if (TaskSet.ConcentrationEstimate[i] == enumRangeState.StateUnknown)		//	浓度判定
						{
							DigestionTubeState.SampleFrom[DigestionTubeProcessed] = TaskSet.TubeNum[i];
							DigestionTubeState.Range[DigestionTubeProcessed] = enumRangeState.StateHigh;
							DigestionTubeState.DigestnID[DigestionTubeProcessed] = DigestionTubeProcessed + 1;
							DigestionTubeState.NumWithInGroup[DigestionTubeProcessed] = Group;
							DigestionTubeProcessed++;
							DigestionTubeState.SampleFrom[DigestionTubeProcessed] = TaskSet.TubeNum[i];
							DigestionTubeState.Range[DigestionTubeProcessed] = enumRangeState.StateLow;
							DigestionTubeState.DigestnID[DigestionTubeProcessed] = DigestionTubeProcessed + 1;
							DigestionTubeState.NumWithInGroup[DigestionTubeProcessed] = Group;
							DigestionTubeProcessed++;
						}
						else if (TaskSet.ConcentrationEstimate[i] == enumRangeState.StateHigh)	//	高浓度
						{
							DigestionTubeState.SampleFrom[DigestionTubeProcessed] = TaskSet.TubeNum[i];
							DigestionTubeState.Range[DigestionTubeProcessed] = enumRangeState.StateHigh;
							DigestionTubeState.DigestnID[DigestionTubeProcessed] = DigestionTubeProcessed + 1;
							DigestionTubeState.NumWithInGroup[DigestionTubeProcessed] = Group;
							DigestionTubeProcessed++;
						}
						else if (TaskSet.ConcentrationEstimate[i] == enumRangeState.StateLow)	//	低浓度
						{
							DigestionTubeState.SampleFrom[DigestionTubeProcessed] = TaskSet.TubeNum[i];
							DigestionTubeState.Range[DigestionTubeProcessed] = enumRangeState.StateLow;
							DigestionTubeState.DigestnID[DigestionTubeProcessed] = DigestionTubeProcessed + 1;
							DigestionTubeState.NumWithInGroup[DigestionTubeProcessed] = Group;
							DigestionTubeProcessed++;
						}
					}
				}
			}
		}



		
		public bool SamplePumpEmptyIsOK = false;
		public bool PutTubeIsOK = false;
		public bool FingerUnPlugIsOK = false;
		public bool ReagentInjIsOK = false;
		public bool SampleInjIsOK = false;
		public bool FingerReturnIsOK = false;

		public int SampleTubex = 1;
		public int DigestionTubex = 1;
		public int SampleTubeO = 0;

		private void RegentProcess()
		{
			ProcessCurrent.RangePumpIn = true;
			qushiji();
			ProcessCurrent.RangePumpIn = false;
			while (!PutTubeIsOK)
				osDelay(200);
			while (!FingerUnPlugIsOK)
				osDelay(200);
			while (!SampleInjIsOK)
				osDelay(200);
			ProcessCurrent.RangePumpOut = true;
			zhushiji();
			ProcessCurrent.RangePumpOut = false;
			ReagentInjIsOK = true;
			while (!FingerReturnIsOK)
				osDelay(200);

		}
		private void SampleProcess()
		{
			if (SampleTubeO != SampleTubex)
			{
				ProcessCurrent.SamplePumpClean = true;
				qingxi();
				//qingxi();
				ProcessCurrent.SamplePumpClean = false;
			}
			SamplePumpEmptyIsOK = true;

			while (!FingerUnPlugIsOK)
				osDelay(200);
			if (SampleTubeO != SampleTubex)
			{
				ProcessCurrent.SamplePumpRinse = true;
				runxi(SampleTubex);
				//runxi(SampleTubex);
				ProcessCurrent.SamplePumpRinse = false;
			}
			ProcessCurrent.SamplePumpIn = true;
			qushuiyang(SampleTubex);
			ProcessCurrent.SamplePumpIn = false;
			ProcessCurrent.SamplePumpOut = true;
			zhushuiyang();
			ProcessCurrent.SamplePumpOut = false;
			SampleInjIsOK = true;
			ProcessCurrent.SampleEffluents = true;
			paikong();
			ProcessCurrent.SampleEffluents = false;
			while (!FingerReturnIsOK)
				osDelay(200);

		}
		private void FingerProcess()
		{
			while (!SamplePumpEmptyIsOK)
				osDelay(200);
			ProcessCurrent.GrabToReagentAdd = true;
			quxiaojieguan(DigestionTubex);
			ProcessCurrent.GrabToReagentAdd = false;
			PutTubeIsOK = true;

			basaizi();
			FingerUnPlugIsOK = true;
			while ((!ReagentInjIsOK) || (!SampleInjIsOK))
				osDelay(200);
			ProcessCurrent.GrabToDigestion = true;
			yasaiziguiwei(DigestionTubex);
			ProcessCurrent.GrabToDigestion = false;

			FingerReturnIsOK = true;
		}

		private ProErr SingelTubeProcess()
		{
			ProErr Err = ProErr.ProNOErr;
			
			Thread oThreadRegentProcess = new Thread(new ThreadStart(this.RegentProcess));
			oThreadRegentProcess.Start();
			Thread oThreadSampleProcess = new Thread(new ThreadStart(this.SampleProcess));
			oThreadSampleProcess.Start();
			Thread oThreadFingerProcess = new Thread(new ThreadStart(this.FingerProcess));
			oThreadFingerProcess.Start();

			while (!FingerReturnIsOK)
				osDelay(1000);
			HoldingSingelWrite(Heat_05, 50000);
			while (HoldingSingelRead(Heat_06) != 50000)
				osDelay(500);

			HoldingSingelWrite(Heat_10, 8300);
			while (HoldingSingelRead(Heat_11) != 8300)
				osDelay(500);
			osDelay(2000);
			HoldingSingelWrite(Heat_10, 400);
			while (HoldingSingelRead(Heat_11) != 400)
				osDelay(500);

			HoldingSingelWrite(Heat_05, 0);
			while (HoldingSingelRead(Heat_06) != 0)
				osDelay(500);
			return Err;
		}

		public void ProcessRun()
		{
			SampleTubeO = 0;
			SampleTubex = 0;
			//for (; ; )
			//{
			//	RTStatus.HeatRealTemp = HoldingSingelRead(Heat_18);
			//	osDelay(1000);
			//}
			ServoCTRL(ServoUp);

			for( int i = 0; i < SampleTubeMax; i++ )
			{

				if ((DigestionTubeState.DigestnID[i] != 0) && (DigestionTubeState.SampleFrom[i] != 0))
				{
					RBootARMMoveZ(Z_High0);
					ServoCTRL(ServoUp);
					RBootARMMoveXYZ(400, 400, Z_High0);
					RBootArmZFindZero();
					osDelay(500);
					RBootArmXFindZero();
					osDelay(500);
					RBootArmYFindZero();
					osDelay(500);

					SampleTubex = DigestionTubeState.SampleFrom[i];
					DigestionTubex = DigestionTubeState.DigestnID[i];
					SingelTubeProcess();
					SamplePumpEmptyIsOK = false;
					FingerUnPlugIsOK = false;
					ReagentInjIsOK = false;
					SampleInjIsOK = false;
					PutTubeIsOK = false;
					FingerReturnIsOK = false;
					DigestionTubeState.RelultIsValid[i] = true;
					SampleTubeO = SampleTubex;

				}
			}
			sCod_Mark = "正常结束";
		}

		public void SlaveInit()
		{
			if ((HoldingSingelRead(Auto_00) == 2) && (HoldingSingelRead(ARM_00) == 3) && (HoldingSingelRead(Meter_00) == 2) && (HoldingSingelRead(Heat_00) == 1))
			{
				osDelay(10);
			}
			else
			{
				Thread oThreadInit1 = new Thread(new ThreadStart(this.Init1));
				oThreadInit1.Start();
				osDelay(100);
				Thread oThreadInit2 = new Thread(new ThreadStart(this.Init2));
				oThreadInit2.Start();
			}

			while ((HoldingSingelRead(Auto_00) != 2) || (HoldingSingelRead(ARM_00) != 3) || (HoldingSingelRead(Meter_00) != 2) || (HoldingSingelRead(Heat_00) != 1))
				osDelay(10);

			MeterARMMove(SignelArm_2);
			RBootARMMoveXY(enumCoordinateType.CoordSampleHead, enumAreaType.AreaSampler, 1);		
		}
		private void Test()
		{
			Thread oThreadProcessRun = new Thread(new ThreadStart(this.ProcessRun));

			TubeAnalyze();

			SlaveInit();
			ChangeIN();
			zhushijiInit();
			//HeatColdCtrl(true);
			for (; ; )
			{
				if( sCod_Mark == "运行")
				{
					if( oThreadProcessRun.ThreadState == ThreadState.Unstarted)
						oThreadProcessRun.Start();
					while (sCod_Mark == "运行")
						osDelay(300);
					if(sCod_Mark == "暂停")
					{
						while (sCod_Mark == "暂停")
							osDelay(300);
						if(sCod_Mark == "运行")
						{				
							TubeAnalyze();
						}
					}
				}

				if ((sCod_Mark == "停止") || (sCod_Mark == "正常结束"))
				{
					if (oThreadProcessRun.ThreadState != ThreadState.Unstarted)
						oThreadProcessRun.Abort();

					SamplePumpEmptyIsOK = false;
					FingerUnPlugIsOK = false;
					ReagentInjIsOK = false;
					SampleInjIsOK = false;
					PutTubeIsOK = false;
					FingerReturnIsOK = false;

					while((sCod_Mark == "停止") || (sCod_Mark == "正常结束"))
						osDelay(300);
				}
			}
		}



		void ChangeIN()
		{
			//CHxPumpPositionQuery();

			//CHxPumpSpeed(30, enumPumpType.CH1Pump);
			//CHxPumpSpeed(30, enumPumpType.CH2Pump);
			//CHxPumpSpeed(30, enumPumpType.CH3Pump);
			//CHxPumpSpeed(100, enumPumpType.CH4Pump);
			//MeterARMMove(SignelArm_1);
			//HoldingSingelWrite(Meter_10, 1);
			//HoldingSingelWrite(Meter_12, 1);
			//HoldingSingelWrite(Meter_14, 1);
			//HoldingSingelWrite(Meter_16, 1);


			//int[] PumpRange = new int[(int)enumPumpType.PumpMax];


			//HoldingSingelWrite(Meter_10, 1);
			//PumpRange[(int)enumPumpType.CH1Pump] = -3000;
			//CHxPumpCTRL(PumpRange);

			//HoldingSingelWrite(Meter_16, 1);

			//HoldingSingelWrite(Meter_75, 1);
			//HoldingSingelWrite(Meter_22, M_TypeSet[(int)enumM_Type.M_Type2].M_Time);
			//HoldingSingelWrite(Meter_21, M_TypeSet[(int)enumM_Type.M_Type2].M_Speed);
			//osDelay(16500);
			//HoldingSingelWrite(Meter_75, 0);


			//HoldingSingelWrite(Meter_22, M_TypeSet[(int)enumM_Type.M_Type3].M_Time);
			//HoldingSingelWrite(Meter_21, M_TypeSet[(int)enumM_Type.M_Type3].M_Speed);
			//osDelay(1500);
			//HoldingSingelWrite(Meter_75, 1);
			//osDelay(4500);
			//HoldingSingelWrite(Meter_75, 0);



			//HoldingSingelWrite(Meter_76, 1);
			//HoldingSingelWrite(Meter_22, M_TypeSet[(int)enumM_Type.M_Type1].M_Time);
			//HoldingSingelWrite(Meter_21, M_TypeSet[(int)enumM_Type.M_Type1].M_Speed);
			//osDelay(6300);
			//HoldingSingelWrite(Meter_76, 0);

			//HoldingSingelWrite(Meter_22, M_TypeSet[(int)enumM_Type.M_Type3].M_Time);
			//HoldingSingelWrite(Meter_21, M_TypeSet[(int)enumM_Type.M_Type3].M_Speed);
			//osDelay(1500);
			//HoldingSingelWrite(Meter_76, 1);
			//osDelay(4500);
			//HoldingSingelWrite(Meter_76, 0);



			//HoldingSingelWrite(Meter_78, 1);
			//HoldingSingelWrite(Meter_22, M_TypeSet[(int)enumM_Type.M_Type1].M_Time);
			//HoldingSingelWrite(Meter_21, M_TypeSet[(int)enumM_Type.M_Type1].M_Speed);
			//osDelay(6300);
			//HoldingSingelWrite(Meter_78, 0);

			//HoldingSingelWrite(Meter_22, M_TypeSet[(int)enumM_Type.M_Type3].M_Time);
			//HoldingSingelWrite(Meter_21, M_TypeSet[(int)enumM_Type.M_Type3].M_Speed);
			//osDelay(1500);
			//HoldingSingelWrite(Meter_78, 1);
			//osDelay(4500);
			//HoldingSingelWrite(Meter_78, 0);

			//MeterARMMove(SignelArm_2);

			//RBootARMMoveXY(enumCoordinateType.CoordSampleHead, enumAreaType.AreaSampler, 7);
			//RBootARMMoveZ(Z_High2);
			//WaterValveCTRL(1);
			//SamplePumpSpeed(40);
			//SamplePumpCTRL(1500);
			//RBootARMMoveXY(enumCoordinateType.CoordSampleHead, enumAreaType.AreaReagentAdd, 2);
			//RBootARMMoveZ(Z_High1);
			//SamplePumpSpeed(30);
			//SamplePumpCTRL(-1700);
			//MotorCTRL(enumM_Type.M_Type2);
			//MotorCTRL(enumM_Type.M_Type9);

			//RBootARMMoveXY(enumCoordinateType.CoordSampleHead, enumAreaType.AreaSampler, 7);

			//zhushuiyang();



			//RBootARMMoveXY(enumCoordinateType.CoordSampleHead, enumAreaType.AreaSampler, 15);
			//RBootARMMoveZ(Z_High2);
			//RBootARMMoveXY(enumCoordinateType.CoordSampleHead, enumAreaType.AreaSampler, 2);
			//RBootARMMoveZ(Z_High2);
			//RBootARMMoveXY(enumCoordinateType.CoordSampleHead, enumAreaType.AreaSampler, 3);
			//RBootARMMoveZ(Z_High2);
			//RBootARMMoveXY(enumCoordinateType.CoordSampleHead, enumAreaType.AreaSampler, 4);
			//RBootARMMoveZ(Z_High2);
			//RBootARMMoveXY(enumCoordinateType.CoordSampleHead, enumAreaType.AreaSampler, 5);
			//RBootARMMoveZ(Z_High2);
			//RBootARMMoveXY(enumCoordinateType.CoordSampleHead, enumAreaType.AreaSampler, 6);
			//RBootARMMoveZ(Z_High2);
			//RBootARMMoveXY(enumCoordinateType.CoordSampleHead, enumAreaType.AreaSampler, 7);
			//RBootARMMoveZ(Z_High2);


			//RBootARMMoveZ(Z_High0);
			//WaterValveCTRL(0);
			//SamplePumpSpeed(60);
			//SamplePumpCTRL(10000);

			//WaterValveCTRL(1);

			//RBootARMMoveXY(enumCoordinateType.CoordSampleHead, enumAreaType.AreaReagentAdd, 2);
			//RBootARMMoveZ(Z_High1);
			//SamplePumpSpeed(60);
			//SamplePumpCTRL(-10000);
			//MotorCTRL(enumM_Type.M_Type3);
			//MotorCTRL(enumM_Type.M_Type11);
			//MotorCTRL(enumM_Type.M_Type6);
			//RBootARMMoveZ(Z_High0);
			//WaterValveCTRL(0);

			////int[] PumpRange = new int[(int)enumPumpType.PumpMax];
			////MeterARMMove(SignelArm_3);

			////HoldingSingelWrite(Meter_10, 1);
			////HoldingSingelWrite(Meter_12, 1);
			////HoldingSingelWrite(Meter_14, 1);
			////HoldingSingelWrite(Meter_16, 1);
			////PumpRange[(int)enumPumpType.SamplePump] = 0;
			////PumpRange[(int)enumPumpType.CH1Pump] = -100;
			////PumpRange[(int)enumPumpType.CH2Pump] = -100;
			////PumpRange[(int)enumPumpType.CH3Pump] = -100;
			////PumpRange[(int)enumPumpType.CH4Pump] = -100;
			////CHxPumpSpeed(15, enumPumpType.CH1Pump);
			////CHxPumpSpeed(30, enumPumpType.CH2Pump);
			////CHxPumpSpeed(30, enumPumpType.CH3Pump);
			////CHxPumpSpeed(60, enumPumpType.CH4Pump);
			////CHxPumpCTRL(PumpRange);


			////HoldingSingelWrite(Meter_75, 1);
			////HoldingSingelWrite(Meter_22, M_TypeSet[(int)enumM_Type.M_Type2].M_Time);
			////HoldingSingelWrite(Meter_21, M_TypeSet[(int)enumM_Type.M_Type2].M_Speed);
			////osDelay(16500);
			////HoldingSingelWrite(Meter_75, 0);


			////HoldingSingelWrite(Meter_22, M_TypeSet[(int)enumM_Type.M_Type3].M_Time);
			////HoldingSingelWrite(Meter_21, M_TypeSet[(int)enumM_Type.M_Type3].M_Speed);
			////osDelay(1500);
			////HoldingSingelWrite(Meter_75, 1);
			////osDelay(4500);
			////HoldingSingelWrite(Meter_75, 0);



			////HoldingSingelWrite(Meter_76, 1);
			////HoldingSingelWrite(Meter_22, M_TypeSet[(int)enumM_Type.M_Type1].M_Time);
			////HoldingSingelWrite(Meter_21, M_TypeSet[(int)enumM_Type.M_Type1].M_Speed);
			////osDelay(6300);
			////HoldingSingelWrite(Meter_76, 0);

			////HoldingSingelWrite(Meter_22, M_TypeSet[(int)enumM_Type.M_Type3].M_Time);
			////HoldingSingelWrite(Meter_21, M_TypeSet[(int)enumM_Type.M_Type3].M_Speed);
			////osDelay(1500);
			////HoldingSingelWrite(Meter_76, 1);
			////osDelay(4500);
			////HoldingSingelWrite(Meter_76, 0);



			////HoldingSingelWrite(Meter_78, 1);
			////HoldingSingelWrite(Meter_22, M_TypeSet[(int)enumM_Type.M_Type1].M_Time);
			////HoldingSingelWrite(Meter_21, M_TypeSet[(int)enumM_Type.M_Type1].M_Speed);
			////osDelay(6300);
			////HoldingSingelWrite(Meter_78, 0);

			////HoldingSingelWrite(Meter_22, M_TypeSet[(int)enumM_Type.M_Type3].M_Time);
			////HoldingSingelWrite(Meter_21, M_TypeSet[(int)enumM_Type.M_Type3].M_Speed);
			////osDelay(1500);
			////HoldingSingelWrite(Meter_78, 1);
			////osDelay(4500);
			////HoldingSingelWrite(Meter_78, 0);

			//zhushiji();


		}





	}
}
