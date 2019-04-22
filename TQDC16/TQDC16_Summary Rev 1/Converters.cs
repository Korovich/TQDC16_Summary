using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.BitConverter;

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

        public static int Byte2Sample(byte[] iByte)
        {
            Array.Reverse(iByte);
            return (int)ToInt16(iByte, 0);
        }

        public static int Byte2Int(byte[] iByte)
        {
            if (iByte.Count() != 4)
            {
                switch (iByte.Count())
                {
                    case 3:
                        {
                            iByte = new byte[4] {0,iByte[0], iByte[1], iByte[2]};
                            break;
                        }
                    case 2:
                        {
                            iByte = new byte[4] { 0, 0, iByte[0], iByte[1] };
                            break;
                        }
                    case 1:
                        {
                            iByte = new byte[4] { 0, 0, 0, iByte[0] };
                            break;
                        }
                    case 0:
                        {
                            throw new InvalidOperationException();
                        }

                }
            }
            Array.Reverse(iByte);
            return BitConverter.ToInt32(iByte,0);
        }
        public static int Byte2Int16(byte[] iByte)
        {

            return BitConverter.ToInt16(iByte, 0);
        }
        public static uint Byte2uInt(byte[] iByte)
        {
            if (iByte.Count() != 4)
            {
                switch (iByte.Count())
                {
                    case 3:
                        {
                            iByte = new byte[4] { 0, iByte[0], iByte[1], iByte[2] };
                            break;
                        }
                    case 2:
                        {
                            iByte = new byte[4] { 0, 0, iByte[0], iByte[1] };
                            break;
                        }
                    case 1:
                        {
                            iByte = new byte[4] { 0, 0, 0, iByte[0] };
                            break;
                        }
                    case 0:
                        {
                            throw new InvalidOperationException();
                        }

                }
            }
            Array.Reverse(iByte);
            return BitConverter.ToUInt32(iByte,0);
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
