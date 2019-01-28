using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using DMSql;


namespace COD
{
    public class cls_Main
    {
        private static string sCommd_Mark = "COM";
        public static string Commd_Mark
        {
            get { return sCommd_Mark; }
            set { sCommd_Mark = value; }
        }
        public static bool sCommdWrite_Mark = false;
        public static bool sCommdRead_Mark = false;
        public static bool CommdWrite_Mark
        {
            get { return sCommdWrite_Mark; }
            set { sCommdWrite_Mark = value; }
        }
        public static bool CommdRead_Mark
        {
            get { return sCommdRead_Mark; }
            set { sCommdRead_Mark = value; }
        }
        private static string sCommdWrite = "";
        private static string sCommdRead = "";
        public static string CommdWrite
        {
            get { return sCommdWrite; }
            set { sCommdWrite = value; }
        }
        public static string CommdRead
        {
            get { return sCommdRead; }
            set { sCommdRead = value; }
        }
        public static int iRegVal = 0;
        public static int CommdReg_Val
        {
            get { return iRegVal; }
            set { iRegVal = value; }
        }

        public static string  sRead_Address = "";
        public static string  Read_Address
        {
            get { return sRead_Address; }
            set { sRead_Address = value; }
        }

        public static string sRead_Reg = "";
        public static string Read_Reg
        {
            get { return sRead_Reg; }
            set { sRead_Reg = value; }
        }

        public static int  iRead_timeout = 2;
        public static int Read_Timeout
        {
            get { return iRead_timeout; }
            set { iRead_timeout = value; }
        }

        private static string sCommd_Com = "";

        public static string Commd_Com
        {
            get { return sCommd_Com; }
            set { sCommd_Com = value; }
        }

        public static string sCom_Address = "";
              public static string Com_Address
        {
            get { return sCom_Address; }
            set { sCom_Address = value; }
        }
              public static string sCom_Reg = "";
              public static string Com_Reg
              {
                  get { return sCom_Reg; }
                  set { sCom_Reg = value; }
              }
              public static int iCom_RegNum = 0;
              public static int Com_RegNum
              {
                  get { return iCom_RegNum; }
                  set { iCom_RegNum = value; }
              }

        //二进制转十六进制
        public string ConvertString(string value, int fromBase, int toBase)
        {
            string sHex = "";
            int intValue = Convert.ToInt32(value, fromBase);
            sHex = Convert.ToString(intValue, toBase);
            if (sHex.Length == 1)
                sHex = "0" + sHex;

            return sHex.ToUpper();
        }
        public string DoCRC(byte[] data)
        {
            int register = 0xFFFF;

            int len = data.Length;

            for (int i = 0; i < len; i++)
            {

                //register      = HighCalculate( register,data[i] );
                register = data[i] ^ register;


                for (int j = 0; j < 8; j++)
                {

                    bool isRight1 = IsRight1(register);

                    register = register >> 1;

                    if (isRight1)
                    {

                        register = register ^ 0xA001;

                    }

                }
            }

            string regStr = register.ToString("x");
            if (regStr.Length > 4)
            {
                throw new ArgumentException("CRC校验的长度不对");
            }
            else
            {
                regStr = regStr.PadLeft(4, '0');
            }
            string result = null;
            result += regStr.Substring(2, 2);
            result += regStr.Substring(0, 2);
            return result.ToUpper();


        }
        private static bool IsRight1(int number)
        {
            if ((number | 0xFFFE) == 0xFFFF)
                return true;
            else
                return false;

        }

        //   02         10        00 0F       00 01         02   00 00   B2 5F
        //从机地址  + 功能码  +   寄存器地址 + 寄存器个数+  字节 + 数据   +CRC
        public byte[] Byte_Write(string sAddRess, string sReg, string sRegVal)
        {
            string sComm = "";
            
            byte[] OutBytes = new byte[11];
            OutBytes[0] = Convert.ToByte(sAddRess, 16);  //从机地址
            OutBytes[1] = Convert.ToByte("10", 16);   //功能码

            string sR = "0000" + ConvertString(sReg, 10, 16);

            sR = sR.Substring(sR.Length - 4, 4);

            OutBytes[2] = Convert.ToByte(sR.Substring(0, 2), 16);   //寄存器地址
            OutBytes[3] = Convert.ToByte(sR.Substring(2, 2), 16);   //寄存器地址

            OutBytes[4] = Convert.ToByte("00", 16);  //寄存器个数
            OutBytes[5] = Convert.ToByte("01", 16);   //寄存器个数

            OutBytes[6] = Convert.ToByte("02", 16);   //字节


            string sRVal = "0000" + ConvertString(sRegVal, 10, 16);

            sRVal = sRVal.Substring(sRVal.Length - 4, 4);

            OutBytes[7] = Convert.ToByte(sRVal.Substring(0, 2), 16);   //寄存器数值
            OutBytes[8] = Convert.ToByte(sRVal.Substring(2, 2), 16);   //寄存器数值


            byte[] CrcBytes = new byte[9];
            string sTemp ="";
            for (int i = 0; i < 9; i++)
            {
                CrcBytes[i] = OutBytes[i];
                if (i < 5)
                {
                    sTemp = Convert.ToString(long.Parse(OutBytes[i].ToString()), 16).ToUpper();
                    if (sTemp.Length == 1)
                        sTemp = "0" + sTemp;

                    sComm = sComm + sTemp; 
                }

            }

            string sCRC = DoCRC(CrcBytes);

            OutBytes[9] = Convert.ToByte(sCRC.Substring(0, 2), 16);   //CRC
            OutBytes[10] = Convert.ToByte(sCRC.Substring(2, 2), 16);   //CRC

            cls_Main.CommdWrite_Mark = false;
            cls_Main.CommdWrite = sComm;

            cls_Main.Commd_Mark = "Write";
            return OutBytes;


            
        }

        //   02         03            00 10      00 01           85 FC
        //从机地址  + 功能码  +   寄存器地址 + 寄存器个数+    +CRC
        public byte[] Byte_Read(string sAddRess, string sReg, string sRegVal,int iTime)
        {
            string sComm = "";

            byte[] OutBytes = new byte[8];
            OutBytes[0] = Convert.ToByte(sAddRess, 16);  //从机地址
            OutBytes[1] = Convert.ToByte("03", 16);   //功能码

            string sR = "0000" + ConvertString(sReg, 10, 16);

            sR = sR.Substring(sR.Length - 4, 4);

            OutBytes[2] = Convert.ToByte(sR.Substring(0, 2), 16);   //寄存器地址
            OutBytes[3] = Convert.ToByte(sR.Substring(2, 2), 16);   //寄存器地址

            OutBytes[4] = Convert.ToByte("00", 16);  //寄存器个数
            OutBytes[5] = Convert.ToByte("01", 16);   //寄存器个数

          


            byte[] CrcBytes = new byte[6];
            string sTemp = "";
            for (int i = 0; i < 6; i++)
            {
                CrcBytes[i] = OutBytes[i];
                if (i < 2)
                {
                    sTemp = Convert.ToString(long.Parse(OutBytes[i].ToString()), 16).ToUpper();
                    if (sTemp.Length == 1)
                        sTemp = "0" + sTemp;

                    sComm = sComm + sTemp;
                }

            }

            string sCRC = DoCRC(CrcBytes);

            OutBytes[6] = Convert.ToByte(sCRC.Substring(0, 2), 16);   //CRC
            OutBytes[7] = Convert.ToByte(sCRC.Substring(2, 2), 16);   //CRC

            cls_Main.CommdRead_Mark = false;
            cls_Main.CommdRead = sComm;
            cls_Main.CommdReg_Val = int.Parse(sRegVal);
            cls_Main.Read_Address = sAddRess;

            cls_Main.Read_Reg = sReg;
            cls_Main.Read_Timeout = iTime;

            cls_Main.Commd_Mark = "Read";
            return OutBytes;



        }


        //   02         03            00 10      00 01           85 FC
        //从机地址  + 功能码  +   寄存器地址 + 寄存器个数+    +CRC
        public byte[] Byte_Read_Com(string sAddRess, string sReg, int iRegNum)
        {
            string sComm = "";

            byte[] OutBytes = new byte[8];
            OutBytes[0] = Convert.ToByte(sAddRess, 16);  //从机地址
            OutBytes[1] = Convert.ToByte("03", 16);   //功能码

            string sR = "0000" + ConvertString(sReg, 10, 16);

            sR = sR.Substring(sR.Length - 4, 4);

            OutBytes[2] = Convert.ToByte(sR.Substring(0, 2), 16);   //寄存器地址
            OutBytes[3] = Convert.ToByte(sR.Substring(2, 2), 16);   //寄存器地址

            string sNum = "0000" + ConvertString(iRegNum.ToString(), 10, 16);
            sNum = sNum.Substring(sNum.Length - 4, 4);

            OutBytes[4] = Convert.ToByte(sNum.Substring(0, 2), 16);  //寄存器个数
            OutBytes[5] = Convert.ToByte(sNum.Substring(2, 2), 16);  //寄存器个数

            
            byte[] CrcBytes = new byte[6];
            string sTemp = "";
            for (int i = 0; i < 6; i++)
            {
                CrcBytes[i] = OutBytes[i];
                if (i < 2)
                {
                    sTemp = Convert.ToString(long.Parse(OutBytes[i].ToString()), 16).ToUpper();
                    if (sTemp.Length == 1)
                        sTemp = "0" + sTemp;

                    sComm = sComm + sTemp;
                }

            }

            string sCRC = DoCRC(CrcBytes);

            OutBytes[6] = Convert.ToByte(sCRC.Substring(0, 2), 16);   //CRC
            OutBytes[7] = Convert.ToByte(sCRC.Substring(2, 2), 16);   //CRC

            
            cls_Main.Commd_Com = sComm;
            cls_Main.Com_Address = sAddRess;
            cls_Main.Com_Reg = sReg;
            cls_Main.Commd_Mark = "COM";
            cls_Main.Com_RegNum = iRegNum;

            return OutBytes;



        }
        //生成校验码
        public string CrcInfo(string str, int iLen)
        {
            byte[] ByteOut = new byte[iLen];

            for (int i = 0; i < iLen; i++)
            {
                ByteOut[i] = Convert.ToByte(ConvertString(str.Substring(i * 2, 2), 16, 10));
            }

            return DoCRC(ByteOut);

        }
        //生成校验码
        public byte[] ByteInfo(string str, int iLen)
        {
            byte[] ByteOut = new byte[iLen];

            for (int i = 0; i < iLen; i++)
            {
                ByteOut[i] = Convert.ToByte(ConvertString(str.Substring(i * 2, 2), 16, 10));
            }

            return ByteOut; 

        }
        public string Decrypt(string stringToDecrypt, string sEncryptionKey)
        {
            byte[] rgbKey = new byte[0];
            byte[] rgbIV = new byte[] { 10, 20, 30, 40, 50, 60, 70, 80 };
            byte[] buffer = new byte[stringToDecrypt.Length];
            try
            {
                rgbKey = Encoding.Unicode.GetBytes(sEncryptionKey.Substring(0, 4));
                DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
                buffer = Convert.FromBase64String(stringToDecrypt);
                MemoryStream stream = new MemoryStream();
                CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                stream2.Write(buffer, 0, buffer.Length);
                stream2.FlushFinalBlock();
                return Encoding.Unicode.GetString(stream.ToArray());
            }
            catch
            {
                return string.Empty;
            }
        }
       
        public bool IsNumber(string Input)
        {
            Regex regex = new Regex("^[0-9.-]");
            return regex.IsMatch(Input);

        }
        
        public bool IsInt_Number(string Input)
        {
            Regex regex = new Regex("^[0-9-]");
            return regex.IsMatch(Input);

        }
        public static void writeLogFile(string LogStr)
        {
            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "//Log//") == false)//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "//Log//");
            }

            System.IO.StreamWriter sw = null;
            string sPath = AppDomain.CurrentDomain.BaseDirectory + "//Log//" + DateTime.Today.ToString("yyyyMMdd") + "Log.txt";
            try
            {
                LogStr = "\n" + "【"+DateTime.Now.ToString()+"】"+LogStr;
                sw = new System.IO.StreamWriter(sPath, true);
                sw.WriteLine(LogStr);

            }
            catch
            {
            }
            finally
            {
                if (sw != null)
                {
                    sw.Close();
                }
            }
        }

        public int Insert_D_TestResult(string sDigestnID, string sSampleFrom, string sNumWithInGroup, string senumRangeState, string sConcentrationValue)
        {
            return DbHelper.ExecuteNonQuery("Insert_D_TestResult", sDigestnID, sSampleFrom, sNumWithInGroup,senumRangeState, sConcentrationValue);
        }
        public int Insert_D_TestInfo_His()
        {
            return DbHelper.ExecuteNonQuery("INSERT INTO D_TestInfo_His SELECT * FROM  D_TestInfo");
        }
        public int Del_D_TestInfo()
        {
            return DbHelper.ExecuteNonQuery("DELETE D_TestInfo ");
        }
    }

    }

