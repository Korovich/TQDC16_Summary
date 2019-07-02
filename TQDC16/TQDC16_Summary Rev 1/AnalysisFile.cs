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
    public class AnalysisFile:TQDC16_Summary
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
            long prog;
            int NumCircleNumEvent = 0; // Количество кругов Event
            var fs = new FileStream(String.Format("{0}", TQDC2File.ReadFilePath), FileMode.Open);
            BackGroundWorkerResult analysisresult;
            //long prog_st = 0; 
            while (pos < fs.Length)
            {
                EvLeng = Byte2Int(ReadBytes(pos + 4, 4, fs));
                if (NumEv == 16777215 + 16777215 * NumCircleNumEvent)
                {
                    NumCircleNumEvent++;
                }
                NumEv = Byte2Int(ReadBytes(pos + 8, 4, fs)) + 16777215 * NumCircleNumEvent;
                pospl = pos+32;
                while (pospl != pos+ EvLeng + 12)
                {
                    PLLeng = Byte2Int(ReadBytes(pospl + 2, 2, fs));
                    switch ((Byte2Int(ReadBytes(pospl, 1, fs)))>>4)
                    {
                        case 0:
                            {
                                pospl += 4;
                                for (int i = 0; i < PLLeng / 4; i++)
                                {
                                    if ((Byte2Int(ReadBytes(pospl , 1, fs)) >> 4 == 4) |
                                        (Byte2Int(ReadBytes(pospl , 1, fs)) >> 4 == 5))
                                    {
                                        uint ch = (Byte2uInt(ReadBytes(pospl, 4, fs)) << 7) >> 28;
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
                                uint ch = ((Byte2uInt(ReadBytes(pospl , 1, fs))) << 28) >> 28;
                                if (Channel[ch] == false)
                                {
                                    Channel[ch] = true;
                                    NumCh++;
                                }
                                pospl += Byte2Int(ReadBytes(pospl + 2, 2, fs)) + 4;
                                break;
                            }
                    }
                }
                //prog += EvLeng + 12;
                pos = pos + EvLeng + 12;
                prog = ((fs.Position * 10000) / fs.Length);
                ProgressBar.ReportProgress((int)prog);
                if (ProgressBar.CancellationPending == true)
                {
                    fs.Close();
                    return;
                }
            }
            analysisresult = new BackGroundWorkerResult(ANALYS,100,OK);
            e.Result = analysisresult; //Возращение переменной для различия процесса
            fs.Close();
        }
        public static void AnChannelText(BackgroundWorker ProgressBar, DoWorkEventArgs e)
        {
            int NumCircleNumEvent = 0; // Количество кругов Event
            var fs = new FileStream(string.Format("{0}", ReadFilePath), FileMode.Open); // Экземпляр потока чтения
            var fsr = new StreamReader(fs);
            BackGroundWorkerResult analysisresult;
            while (!fsr.EndOfStream)
            {
                string readerLine = fsr.ReadLine();
                switch (readerLine.Substring(0, readerLine.IndexOf(" ")))
                {
                    case "Ev:":
                        {
                            readerLine = readerLine.Substring(readerLine.IndexOf(" "));
                            readerLine = readerLine.Remove(0,1);
                            if (NumEv == 16777215 + 16777215 * NumCircleNumEvent)
                            {
                                NumCircleNumEvent++;
                            }
                            NumEv = int.Parse(readerLine.Substring(0, readerLine.IndexOf(" "))) + 16777215 * NumCircleNumEvent;
                            break;
                        }
                    case "Tdc":
                        {
                            readerLine = readerLine.Substring(readerLine.IndexOf(" "));
                            readerLine = readerLine.Remove(0, 1);
                            uint ch = uint.Parse(readerLine.Substring(0, readerLine.IndexOf(":")));
                            if (Channel[ch] == false)
                            {
                                Channel[ch] = true;
                                NumCh++;
                            }
                            break;
                        }
                    case "Adc":
                        {
                            readerLine = readerLine.Substring(readerLine.IndexOf(" "));
                            readerLine = readerLine.Remove(0, 1);
                            uint ch = uint.Parse(readerLine.Substring(0, readerLine.IndexOf(":")));
                            if (Channel[ch] == false)
                            {
                                Channel[ch] = true;
                                NumCh++;
                            }
                            break;
                        }
                }
                ProgressBar.ReportProgress((int)((fs.Position * 10000) / fs.Length));
                if (ProgressBar.CancellationPending == true)
                {
                    fsr.Close();
                    fs.Close();
                    return;
                }
            }
            analysisresult = new BackGroundWorkerResult(ANALYS, 100, OK);
            e.Result = analysisresult; //Возращение переменной для различия процесса
            fsr.Close();
            fs.Close();
        }
    }
}
