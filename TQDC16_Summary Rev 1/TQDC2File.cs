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
        public static bool[] Ch { get; set; } = new bool[16];

        public static OpenResult Open_File()
        {
            OpenResult openresult = new OpenResult();
            OpenFileDialog TQDC_DATA = new OpenFileDialog();
            openresult.Selected = true;
            TQDC_DATA.Filter = "Raw files (*.dat)|*.dat|Decoded files (*.txt)|*.txt|All files (*.*)|*.*";
            while (true)
            {
                DialogResult ResultDil;
                ResultDil = TQDC_DATA.ShowDialog();
                if (ResultDil == DialogResult.Cancel)
                {
                    openresult.Selected = false;
                    openresult.Serial = "/n";
                    openresult.ID = "CancelEr";
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
                            openresult.Selected = false;
                        }
                    }
                    if (openresult.Selected)
                    {
                        Path = TQDC_DATA.FileName;
                        openresult.Serial = Converters.Byte2Str(ReadByte(12, 16, FSD));
                        openresult.ID = Converters.Id2Str(ReadByte(16, 17, FSD)[0]);
                        FSD.Close();
                        break;
                    }
                    FSD.Close();
                    ResultMes = MessageBox.Show("Выберете другой файл", "Неправильный файл", MessageBoxButtons.RetryCancel);
                    if (ResultMes == DialogResult.Retry)
                    {
                        continue;
                    }
                    if (ResultMes == DialogResult.Cancel)
                    {
                        openresult.Selected = false;
                        openresult.Serial = "/n";
                        openresult.ID = "CancelEr";
                        break;
                    }
                }
            }
            return openresult;
        }

        public class OpenResult 
            {
                public string Serial { get; set; } = "";
                public string ID { get; set; } = "";
                public Boolean Selected { get; set; } = false;
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
