using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TQDC16_Summary_Rev_1.Converters;
using static TQDC16_Summary_Rev_1.TQDC2File;

namespace TQDC16_Summary_Rev_1
{
    public class AnalysisFile
    {

        public static bool[] Channel = new bool[16] ;
        public static int NumEv = 0;
        public static int NumCh = 0;
        public static void AnChannelBinary(BackgroundWorker ProgressBar, DoWorkEventArgs e)
        {
            int EvLeng;
            int PLLeng;
            long pos = 0;
            long pospl;
            long prog = 0;
            var FS = new FileStream(String.Format("{0}", TQDC2File.ReadFilePath), FileMode.Open);
            long prog_st = FS.Length / 1000;
            while (pos < FS.Length)
            {
                EvLeng = Byte2Int(ReadBytes(pos + 4, 4, FS));
                NumEv = Byte2Int(ReadBytes(pos + 8, 4, FS));
                pospl = pos+32;
                while (pospl != pos+ EvLeng + 12)
                {
                    PLLeng = Byte2Int(ReadBytes(pospl + 2, 2, FS));
                    switch ((Byte2Int(ReadBytes(pospl, 1, FS)))>>4)
                    {
                        case 0:
                            {
                                pospl += 4;
                                for (int i = 0; i < PLLeng / 4; i++)
                                {
                                    if ((Byte2Int(ReadBytes(pospl , 1, FS)) >> 4 == 4) |
                                        (Byte2Int(ReadBytes(pospl , 1, FS)) >> 4 == 5))
                                    {
                                        uint ch = (Byte2uInt(ReadBytes(pospl, 4, FS)) << 7) >> 28;
                                        if (Channel[ch] == false)
                                        {
                                            Channel[ch] = true;
                                            NumCh++;
                                        }
                                    }
                                    pospl += 4;
                                }
                                break;
                            }
                        case 1:
                            {
                                uint ch = ((Byte2uInt(ReadBytes(pospl , 1, FS))) << 28) >> 28;
                                if (Channel[ch] == false)
                                {
                                    Channel[ch] = true;
                                    NumCh++;
                                }
                                pospl += Byte2Int(ReadBytes(pospl + 2, 2, FS)) + 4;
                                break;
                            }
                    }
                }
                prog += EvLeng + 12;
                pos = pos + EvLeng + 12;
                if (prog > prog_st)
                {
                    prog = 0;
                    ProgressBar.ReportProgress(1);
                }
                NumEv++;
            }
            e.Result = 2; //Возращение переменной для различия процесса
            FS.Close();
        }
        public static void AnChannelText(BackgroundWorker ProgressBar, DoWorkEventArgs e)
        {

        }
    }
}
