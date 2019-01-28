using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COD
{
    public class cls_Logic
    {
        public uTaskSet[] uTaskSetArry = new uTaskSet[32];
        public  uDigestionTubeState[] uDigestionTubeStateArry = new uDigestionTubeState[32];

        public static uProcessCurrent uProcessCurrentInfo;
        public static uRTStatus uRTStatusInfo;
        private static string sCod_Mark = "Record";    // Record   或  Result
        public static string Cod_Mark
        {
            get { return sCod_Mark; }
            set { sCod_Mark = value; }
        }

        public static int[] Data_ZDH_1 = new int[100];  //自动化
        public static int[] Data_JXB_2 = new int[100];  //机械臂
        public static int[] Data_GD_3 = new int[100];  //分光度
        public static int[] Data_XJ_4 = new int[100];  //消解

        public static string[,] Cod_Record = new string[32, 30];  //过程数据
        public static string[,] Cod_Result = new string[32, 30];  //结果数据


        public int getRegData(int iAddRess, int iRegID)
        {
            int iData = 0;
            switch(iAddRess)
            {
                case 1 :
                    {
                        iData = Data_ZDH_1[iRegID];
                        break;
                    }
                case 2 :
                    {
                        iData = Data_JXB_2[iRegID];
                        break;
                    }
                case 3:
                    {
                        iData = Data_GD_3[iRegID];
                        break;
                    }
                case 4:
                    {
                        iData = Data_XJ_4[iRegID];
                        break;
                    }
                default :
                    {
                        iData = 0;
                        break;
                    }           
            
            }

            return iData;
 

        }

        public struct uTaskSet
        {

           public int TubeNum { get; set; }//	水样试管号          			
           public int ExperimentNum{ get; set; }				//	平行试验组数
           public int ConcentrationEstimate{ get; set; }		//	0 未知浓度		1 高浓度			2 低浓度
           public int DissolutionTime{ get; set; }		//	0 不消解			1 消解时长15min	2 消解时长5min
           public int ConcentrationMeasure{ get; set; }		//	0 不比色			1 比色分析浓度		2

        }
        public struct uDigestionTubeState	//	消解区试管状态
        {
            public int DigestnID { get; set; }			//	消解管编号

            public int SampleFrom { get; set; }			//	水样源
            public int NumWithInGroup { get; set; }		//	组内编号
            public enumRangeState Range;//	判别浓度值量程高低 
            public int ConcentrationValue { get; set; }				//	浓度值
        }
        public enum enumRangeState	//	试剂量程 未知/高/低
        {         
            StateUnknown,
            StateHigh,           
            StateLow
        }
        public enum enumLevelStatus	//	管路状态 空/非空
        {
            LevelStatusBlank ,
            LevelStatusNonblank,
            LevelStatusMax,
        }
        public enum enumPumpType	//	泵类型
        {
            InjPump,	//	水样泵
            CH1Pump,	//	试剂泵1
            CH2Pump,	//	试剂泵2
            CH3Pump,	//	试剂泵3
            CH4Pump,	//	试剂泵4
            PumpMax,	//	泵总数
        }
        public struct uProcessCurrent	//	这个结构体是为了界面显示和实时记录显示而设立
        {
            //	界面走到什么步骤了 我会设置这里的步骤标志位，
            //	包括 
            //界面三个区域的闪烁://	用于监视界面动画

           public int  ReagentAddCurrent{ get; set; }	//	注液区[3];	//	废液位置	注液位置	空置位置
           public bool SamplerUsing{ get; set; }		//	水样区[32];	//	正在操作的水样源ID
           public bool DigestionUsing{ get; set; }	//	消解区[32];	//	正在操作的消解管ID


            //正在运行的流程：//	用于实时记录显示

           public bool GrabToReagentAdd { get; set; }	//	抓取消解管到注液区
           public bool SamplePumpClean { get; set; }	//	水样泵清洗
           public bool SamplePumpRinse { get; set; }	//	水样泵润洗
           public bool SamplePumpIn { get; set; }	//	水样泵取水样
           public bool SamplePumpOut { get; set; }	//	水样泵注水样
           public bool RangePumpEx { get; set; }		//	试剂泵排废液
           public bool RangePumpIn { get; set; }	//	试剂泵取试剂
           public bool RangePumpOut { get; set; }	//	试剂泵注试剂
           public bool stirring { get; set; }		//	搅拌子搅拌
           public bool GrabToDigestion { get; set; }	//	抓取消解管到消解区
           public bool DigestionHeating { get; set; }	//	消解区加热
           public bool DigestionDigesting { get; set; }	//	消解区消解
           public bool DigestionCold { get; set; }	//	消解区冷却
           public bool MixSolutions { get; set; }	//	旋转消解管混合溶液
           public bool Spectral { get; set; }		//	分光比色


           public bool UpDated { get; set; }	//	更新标志位	//	不管什么步骤更新都伴随着此标志的置位 需要上位机清零

            //	这里的所有步骤位可能有同时进行的，就是说不是互斥唯一的
        }

        public struct uRTStatus			//	实时运行数据
        {
            //public enum	enumLevelStatus	Exist[PumpMax];			//	试剂是否存在

            //uint16_t	SampleAmount[SampleTubeMax];	//	水样区水样余量
            public int HeatRealTemp { get; set; }					//	消解器实时温度
            public uDigestionTubeState[] DigestionTubeState;


        }
    

    }
}
