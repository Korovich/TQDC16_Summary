using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TQDC16_Summary_Rev_1
{
    class Converters
    {
        public static string Byte2Str(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static string Id2Str(byte id)
        {
            switch (id)
            {
                case 0xd6:
                    {
                        return "TQDC16VS";
                    }
            }
            return null;
        }
        public static int Byte2Int(byte[] iByte)
        {
            int OutI = 0;
            for (int i=0;i<iByte.Length;i++)
            {
                OutI += iByte[i] << ((iByte.Length-i-1) * 8);
            }
            return OutI;
        }
        public static uint Byte2uInt(byte[] iByte)
        {
            uint OutI = 0;
            for (uint i = 0; i < iByte.Length; i++)
            {
                OutI += (uint)iByte[i] << (int)(((uint)iByte.Length - i - 1) * 8);
            }
            return OutI;
        }

        public static DateTime UnixTimeStampToDateTime(uint unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
