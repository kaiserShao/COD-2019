using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace COD
{
	public partial class frmLogic
	{
		//public static uProcessCurrent	ProcessCurrent;
		private static string		sCod_Mark = "正常结束";
		/* Record 或 Result */ /*--->*/ /* 运行状态 暂停/停止/运行/正常结束*/
		public static string Cod_Mark	
		{
			get
			{
				return(sCod_Mark);
			}
			set
			{
				sCod_Mark = value;
			}
		}
		public static int getRegData(int iAddRess, int iRegID)
		{
			int iData = 0;
			switch ( iAddRess )
			{
				case 1:
				{
					iData = frmMain.Data_ZDH_1[iRegID];
					break;
				}
				case 2:
				{
					iData = frmMain.Data_JXB_2[iRegID];
					break;
				}
				case 3:
				{
					iData = frmMain.Data_GD_3[iRegID];
					break;
				}
				case 4:
				{
					iData = frmMain.Data_XJ_4[iRegID];
					break;
				}
				default:
				{
					iData = 0;
					break;
				}
			}
			return(iData);
		}

		public const int SampleTubeMax = 32;
		public const int DigestionTubeMax = 32;
		public enum enumRangeState      /*	试剂量程 未知/高/低 */
		{
			StateUnknown,
			StateHigh,
			StateLow,
		}
		public enum enumLevelStatus     /*	管路状态 空/非空 */
		{
			LevelStatusBlank,
			LevelStatusNonblank,
			LevelStatusMax,
		}
		public  enum enumPumpType        /*	泵类型 */
		{
			SamplePump,                /*	水样泵 */
			CH1Pump,                /*	试剂泵1 */
			CH2Pump,                /*	试剂泵2 */
			CH3Pump,                /*	试剂泵3 */
			CH4Pump,                /*	试剂泵4 */
			PumpMax,                /*	泵总数 */
		}
		public enum ProErr              /*	流程错误信息 */
		{
			ProNOErr = 0,
			ProPumpBlockingErr,
			ProPumpRangeErr,
			ProPumpPositionErr,
			ProPumpSpeedErr,
			ProValveErr,
			ProCommonErr,
			ProFingersErr,
			ProNoFluid,
			ProArmXErr,
			ProArmYErr,
			ProArmZErr,
			ProArmZFindZeroErr,
			ProReagentErr,
			ProMBReadErr,
			ProMBWriteErr,
			ProBusy,
			ProCoordinateErr,
			ProErrNoDef = 65536,
		};


		public struct uTaskSet
		{
			public int[] TubeNum;				/*水样试管号 */

			public int[] ExperimentNum;			/*平行试验组数 */

			public enumRangeState[] ConcentrationEstimate;	/*	0 未知浓度		1 高浓度			2 低浓度 */

/*去掉*/			public int[] DissolutionTime;			/*	0 不消解			1 消解时长15min	2 消解时长5min */

/*去掉*/			public int[] ConcentrationMeasure;	/*	0 不比色			1 比色分析浓度		2 */

		}
		public static uTaskSet TaskSet = new uTaskSet();

		public struct uDigestionTubeState		/*消解区试管状态 */
		{
			public int[] DigestnID;				/*	消解管编号 */

			public int[] SampleFrom;			/*	水样源 */

			public int[] NumWithInGroup;		/*	组内编号 */

			public enumRangeState[] Range;		/*	判别浓度值量程高低 */

			public int[] ConcentrationValue;	/*	浓度值 */

			public bool[] RelultIsValid;		/*浓度值有效（默认为false）*/
			public bool[] DigestnIsExist;		/*消解管存在（默认为true）*/
		}
		public static uDigestionTubeState DigestionTubeState = new uDigestionTubeState();

		public struct uProcessCurrent /*	这个结构体是为了界面显示和实时记录显示而设立 */
		{
/*
 * 界面走到什么步骤了 我会设置这里的步骤标志位，
 * 包括
 * 界面三个区域的闪烁://	用于监视界面动画
 */
			public int RengentAdd	/*注液头位置*//*1注液位置 2废液位置 3锁紧位置*/
			{
				get;
				set;
			}
			//public int RgtAdd_Effluents	/*废液位置*/
			//{
			//	get;
			//	set;
			//}
			//public int RgtAdd_Filling	/*注液位置*/
			//{
			//	get;
			//	set;
			//}
			//public int RgtAdd_Vacancy	/*空置位置*/
			//{
			//	get;
			//	set;
			//}
			/*	注液区[3];*/
			public bool SampleAdd;  /*添加水样*/
			public bool SampleEffluents;  /*排废水样*/
			public int SamplerUsing
			{
				get;
				set;
			}
/*	水样区[32];	//	正在操作的水样源ID */
			public int DigestionUsing
			{
				get;
				set;
			}
/*消解区[32];	//	正在操作的消解管ID*/

/* 正在运行的流程：//	用于实时记录显示*/

			public bool GrabToReagentAdd
			{
				get;
				set;
			}
/*	抓取消解管到注液区*/
			public bool SamplePumpClean
			{
				get;
				set;
			}
/*	水样泵清洗 */
			public bool SamplePumpRinse
			{
				get;
				set;
			}
/*	水样泵润洗 */
			public bool SamplePumpIn
			{
				get;
				set;
			}
/*	水样泵取水样 */
			public bool SamplePumpOut
			{
				get;
				set;
			}
/*	水样泵注水样 */
			public bool RangePumpEx
			{
				get;
				set;
			}
/*	试剂泵排废液 */
			public bool RangePumpIn
			{
				get;
				set;
			}
/*	试剂泵取试剂 */
			public bool RangePumpOut
			{
				get;
				set;
			}
/*	试剂泵注试剂 */
			public bool stirring
			{
				get;
				set;
			}
/*	搅拌子搅拌 */
			public bool GrabToDigestion
			{
				get;
				set;
			}
/*	抓取消解管到消解区 */
			public bool DigestionHeating
			{
				get;
				set;
			}
/*	消解区加热 */
			public bool DigestionDigesting
			{
				get;
				set;
			}
/*	消解区消解 */
			public bool DigestionCold
			{
				get;
				set;
			}
/*	消解区冷却 */
			public bool MixSolutions
			{
				get;
				set;
			}
/*	旋转消解管混合溶液 */
			public bool Spectral
			{
				get;
				set;
			}
/*	分光比色 */
			public  int HeatRealTemp
			{
				get;
				set;
			}
/*	消解器实时温度 */
			public  bool[] Exist;
/*	试剂是否存在 */
			public bool UpDated
			{
				get;
				set;
			}
/*
 * 更新标志位	//	不管什么步骤更新都伴随着此标志的置位 需要上位机清零
 * 这里的所有步骤位可能有同时进行的，就是说不是互斥唯一的
 */
		}
		public static uProcessCurrent ProcessCurrent = new uProcessCurrent();

		public struct uRTStatus /*	实时运行数据 */
		{
			public enumLevelStatus[] Exist;		//	试剂是否存在
			/*	水样试管的任务是否正在运行中 */
			public bool[] TaskIsRuning;
			/*	注射泵实时位置 */
			public int[] PumpPosition;
			/*	注射泵实时速度 */
			public int[] PumpSpeed;
			/*	机械手指实时位置 */
			public  int FingersPosition;
			/*	机械手指实时速度 */
			public  int FingersSpeed;
			/*	机械手指实时位置 */
			public  int ServoPosition;
			/*	机械臂实时位置 */
			public  int[] RbootArmPosition;
			/*	单轴机械臂实时位置 */
			public  int RbootArmSignelPosition;
			/*	液体存在状态 */
			public enumLevelStatus[] LevelSwitch;
			/*	水样区水样余量 */
			public int[] SampleAmount;
			/*	消解器实时温度 */
			public  int HeatRealTemp;
		}
		public static uRTStatus RTStatus = new uRTStatus();
		
		
	}
}