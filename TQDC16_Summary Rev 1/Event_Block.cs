using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TQDC16_Summary_Rev_1
{

    class Event_Block
    {
        public static int NumEvent { get; set; } = 0;
        public static int EventPayLen { get; set; } = 0;
        public static int PosLineByte { get; set; } = 0;
    }
    public static class DeviceEventBlock
    {
        public static string Serial { get; set; } = "";
        public static int ID { get; set; } = 0;
        public static int DEventPayLen { get; set; } = 0;
        public static int DPosLineByte { get; set; } = 0;
    }
    public class MstreamBlock
    {
        public static int STypeBit { get; set; } = 0;
        public static int MStrPLen { get; set; } = 0;
        public static int MstrS { get; set; } = 0;
    }

    public static class MstreamPL
    {
        public static Int32 EvTimSt_Sec { get; set; } = 0;
        public static Int32 EvTimSt_nSec { get; set; } = 0;
        public static Int32 EvTim_Flag { get; set; } = 0;
    }
    /*
    public class TDC
    {
        public static int Ch_N { get; set; } = 0;
        public static int LengthPl = 0;
        public static int[] Data;
        
        public void Config ()
        {
            Data = new int[Ch_N];
        }
        public class Header
        {
            public int ID { get; set; } = 0;
            public int EventNum { get; set; } = 0;
            public int TimeStamp { get; set; } = 0;
        }
        public class Trailer
        {
            public int ID { get; set; } = 0;
            public int EventNum { get; set; } = 0;
            public int Wordcount { get; set; } = 0;
        }
        public class Leading
        {
            public int Ch { get; set; } = 0;
            public int Data { get; set; } = 0;
        }
        public class Trailing
        {
            public int Ch { get; set; } = 0;
            public int Data { get; set; } = 0;
        }
        public class Error
        {
            public int ID { get; set; } = 0;
            public int Flag { get; set; } = 0;
        }
    }

    public class ADC
    {
        public static int Ch_N { get; set; } = 0;
        public static int N { get; set; } = 0;
        public static int Specific { get; set; } = 0;
        public static int LengthPl = 0;
        public int[,] Data;
        public void Config ()
        {
            Data = new int[Ch_N,N];
        }
    }
    */
}
