using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TQDC16_Summary_Rev_1
{
    class TQDC2File
    {
        public static String Path { get; set; } = "";
        public static Boolean Sel { get; set; } = false;
        public static Int32 DeEvPaL { get; set; } = 0;
        public static bool[] Ch { get; set; } = new bool[16];

        public static void Open_File()
        {
            OpenFileDialog TQDC_DATA = new OpenFileDialog();
            Sel = true;
            TQDC_DATA.Filter = "Raw files (*.dat)|*.dat|Decoded files (*.txt)|*.txt|All files (*.*)|*.*";
            while (true)
            {
                DialogResult ResultDil;
                ResultDil = TQDC_DATA.ShowDialog();
                if (ResultDil == DialogResult.Cancel)
                {
                    break;
                }
                if (ResultDil == DialogResult.OK)
                {
                    DialogResult ResultMes;
                    var FSD = new FileStream(String.Format("{0}", TQDC_DATA.FileName), FileMode.Open);
                    byte[] bbyte = new byte[4];
                    byte[] fbyte = new byte[4] { 0x50, 0x2a, 0x50, 0x2a };
                    FSD.Read(bbyte, 0, 4);
                    for (int i = 0; i < 4; i++)
                    {
                        if (bbyte[i] != fbyte[i])
                        {
                            Sel = false;
                        }
                    }
                    FSD.Close();
                    if (Sel)
                    {
                        Path = TQDC_DATA.FileName;
                        break;
                    }
                    ResultMes = MessageBox.Show("Выберете другой файл", "Неправильный файл", MessageBoxButtons.RetryCancel);
                    if (ResultMes == DialogResult.Retry)
                    {
                        continue;
                    }
                    if (ResultMes == DialogResult.Cancel)
                    {
                        Sel = false;
                        break;
                    }
                }
            }
        }
   
        public static void DescFile(TextBox SerialText,TextBox IDText)
        {
            var FSD = new FileStream(String.Format("{0}", Path), FileMode.Open);
            //SerialText.Text = String.Format("{0} - {1}",Converters.ByteAr2Str(serial)), Converters.ByteAr2Str(serial.Skip(2).ToArray));
            SerialText.Text = Converters.Byte2Str(ReadByte(12, 16, FSD));
            IDText.Text = Converters.Id2Str(ReadByte(16, 17, FSD)[0]);
            DeEvPaL =Converters.Byte2Int(ReadByte(17, 20, FSD));
            Event_Block.EventPayLen = Converters.Byte2Int(ReadByte(4, 8, FSD));
            FSD.Close();
        }

        public static void ReadEvBl (int pos)
        {
            object[] TDCB;
            object[] ADCB;
            var FS = new FileStream(String.Format("{0}", Path), FileMode.Open);
            Event_Block.EventPayLen = Converters.Byte2Int(ReadByte(pos + 4, pos + 8, FS));
            Event_Block.NumEvent = Converters.Byte2Int(ReadByte(pos + 8, pos + 12, FS));
            DeviceEventBlock.Serial = Converters.Byte2Str(ReadByte(pos + 12, pos + 16, FS));
            DeviceEventBlock.ID = Converters.Byte2Int(ReadByte(pos + 16, pos + 17, FS));
            DeviceEventBlock.DEventPayLen = Converters.Byte2Int(ReadByte(pos + 17, pos + 20, FS));
            MstreamBlock.STypeBit = Converters.Byte2Int(ReadByte(pos + 20, pos + 21, FS));
            MstreamBlock.MStrPLen = Converters.Byte2Int(ReadByte(pos + 21, pos + 24, FS))>>2;
            MstreamBlock.MstrS = (Converters.Byte2Int(ReadByte(pos + 23, pos + 24, FS)) << 6) >> 6;
            MstreamPL.EvTimSt_Sec = Converters.Byte2Int(ReadByte(pos + 24, pos + 28, FS));
            MstreamPL.EvTimSt_nSec = Converters.Byte2Int(ReadByte(pos + 28, pos + 32, FS)) >>2;
            MstreamPL.EvTim_Flag = (Converters.Byte2Int(ReadByte(pos + 31, pos + 32, FS)) << 6) >> 6;
            /*
            switch (Converters.Byte2Int(ReadByte(pos + 32, pos + 33, FS))>>4) 
            {
                
                case 0:
                    {
                        int postdc = pos+36;
                        TDC.Ch_N = (Converters.Byte2Int(ReadByte(pos + 32, pos + 33, FS)) << 4) >> 4;
                        TDC.LengthPl = Converters.Byte2Int(ReadByte(pos + 34, pos + 36, FS));
                        TDCB = new object[TDC.LengthPl / 4];
                        
                        break;
                    }
                case 1:
                    {

                        break;
                    }
            }
            */
            FS.Close();
        }

        public static byte[] ReadByte (long x,long y,FileStream FS)
        {
            if ((x>y)|(((x/4)*4)+4<y))
            {
                MessageBox.Show(String.Format("x-{0}:y-{1}",x,y), "Ошибка", MessageBoxButtons.RetryCancel);
                return null;
            }
            if (x + 4 == y)
            {
                byte[] Ibuf = new byte[4];
                FS.Position = x;
                FS.Read(Ibuf, 0, (int)(y-x));
                Array.Reverse(Ibuf);
                return Ibuf;
            }
            else
            {
                byte[] Ibuf = new byte[4];
                FS.Position = (x / 4) * 4;
                FS.Read(Ibuf, 0, 4);
                Array.Reverse(Ibuf);
                return cpB2B(Ibuf,(int)(x%4),(int)(x%4+y-x));
            }
        }

        public static int ReadBit(int x, int y, int Byte)
        {
            if ((y-x>32)|(y>32))
            {
                return 0;
            }
            return ((Byte << 32 - x) >> y);
        }

        static byte[] cpB2B (byte[] a, int x, int y)
        {
            byte[] Obuf = new byte[y - x];
            for (int i=x; i<y; i++)
            {
                Obuf[i - x] = a[i];
            }
            return Obuf;
        }
    }
}
