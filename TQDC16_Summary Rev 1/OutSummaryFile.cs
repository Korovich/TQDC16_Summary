

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TQDC16_Summary_Rev_1
{
    class OutSummaryFile
    {
        static Summary SummFile = new Summary();
        public static void StartSummary(BackgroundWorker ProgressBar)
        {
            CSV_Output.Create_CSV();
            CSV_Output.InitCsv();
            int EvLeng = 0;
            int MSLeng = 0;
            int PLLeng = 0;
            ulong NumEv = 0;
            long pos = 0;
            long pospl = 0;
            long prog = 0;
            var FS = new FileStream(String.Format("{0}", TQDC2File.Path), FileMode.Open);
            long prog_st = FS.Length / 999;
            using (CSV_Output.writer)
            using (CSV_Output.csv)
            {
                CSV_Output.csv.WriteHeader<Summary>();
                CSV_Output.csv.NextRecord();
                while (pos < FS.Length)
                {
                    EvLeng = Converters.Byte2Int(TQDC2File.ReadByte(pos + 4, pos + 8, FS));
                    NumEv = (ulong)Converters.Byte2Int(TQDC2File.ReadByte(pos + 8, pos + 12, FS));
                    MSLeng = Converters.Byte2Int(TQDC2File.ReadByte(pos + 21, pos + 24, FS)) >> 2;
                    pospl = pos + 32;
                    while (pospl != pos + EvLeng + 12)
                    {
                        PLLeng = Converters.Byte2Int(TQDC2File.ReadByte(pospl + 2, pospl + 4, FS));
                        switch ((Converters.Byte2Int(TQDC2File.ReadByte(pospl, pospl + 1, FS))) >> 4)
                        {
                            case 0:
                                {
                                    for (int i = 0; i < PLLeng / 4; i++)
                                    {
                                        switch (Converters.Byte2Int(TQDC2File.ReadByte(pospl + 4, pospl + 5, FS)) >> 4)
                                        {
                                            case 2:
                                                {
                                                    SummFile.TDCEvent = Converters.Byte2uInt(TQDC2File.ReadByte(pospl + 5, pospl + 8, FS)) >> 12;
                                                    pospl += 4;
                                                    break;
                                                }
                                            case 3:
                                                {
                                                    pospl += 4;
                                                    break;
                                                }
                                            case 4:
                                                {
                                                    uint ch = ((Converters.Byte2uInt(TQDC2File.ReadByte(pospl + 4, pospl + 8, FS))) << 7) >> 28;
                                                    uint value = ((Converters.Byte2uInt(TQDC2File.ReadByte(pospl + 4, pospl + 8, FS))) << 11) >> 11;
                                                    AddSummary(TDC, ch, value);
                                                    pospl += 4;
                                                    break;
                                                }
                                            case 5:
                                                {
                                                    uint ch = ((Converters.Byte2uInt(TQDC2File.ReadByte(pospl + 4, pospl + 8, FS))) << 7) >> 28;
                                                    uint value = ((Converters.Byte2uInt(TQDC2File.ReadByte(pospl + 4, pospl + 8, FS))) << 11) >> 11;
                                                    AddSummary(TDC, ch, value);
                                                    pospl += 4;
                                                    break;
                                                }
                                            case 7:
                                                {
                                                    pospl += 4;
                                                    break;
                                                }
                                        }
                                    }
                                    break;
                                }
                            case 1:
                                {
                                    uint ch = ((Converters.Byte2uInt(TQDC2File.ReadByte(pospl, pospl + 1, FS))) << 28) >> 28;
                                    AddSummary(ATS, ch, Converters.Byte2uInt(TQDC2File.ReadByte(pospl + 6, pospl + 8, FS)));
                                    pospl += Converters.Byte2uInt(TQDC2File.ReadByte(pospl + 2, pospl + 4, FS));
                                    break;
                                }
                        }
                        pospl += 4;
                    }
                    SummFile.Event = NumEv;
                    prog += EvLeng + 12;
                    pos = pos + EvLeng + 12;
                    if (prog > prog_st)
                    {
                        prog = 0;
                        ProgressBar.ReportProgress(1);
                    }
                    CSV_Output.csv.WriteRecord(SummFile);
                    Record_Clear(SummFile);
                    CSV_Output.csv.NextRecord();
                }
            }
            CSV_Output.CloseCsv();
            FS.Close();
        }

        public static byte TDC { get; } = 1;
        public static byte MIN { get; } = 2;
        public static byte MAX { get; } = 3;
        public static byte ATS { get; } = 4;

        static void AddSummary(byte Type, uint ch, uint value)
        {
            switch (Type)
            {
                case 1:
                    {
                        switch (ch)
                        {
                            case 0: { SummFile.TDC0 = (value); break; }
                            case 1: { SummFile.TDC1 = (value); break; }
                            case 2: { SummFile.TDC2 = (value); break; }
                            case 3: { SummFile.TDC3 = (value); break; }
                            case 4: { SummFile.TDC4 = (value); break; }
                            case 5: { SummFile.TDC5 = (value); break; }
                            case 6: { SummFile.TDC6 = (value); break; }
                            case 7: { SummFile.TDC7 = (value); break; }
                            case 8: { SummFile.TDC8 = (value); break; }
                            case 9: { SummFile.TDC9 = (value); break; }
                            case 10: { SummFile.TDC10 = (value); break; }
                            case 11: { SummFile.TDC11 = (value); break; }
                            case 12: { SummFile.TDC12 = (value); break; }
                            case 13: { SummFile.TDC13 = (value); break; }
                            case 14: { SummFile.TDC14 = (value); break; }
                            case 15: { SummFile.TDC15 = (value); break; }
                        }
                        break;
                    }
                case 2:
                    {
                        switch (ch)
                        {
                            case 0: { SummFile.Min0 = (value); break; }
                            case 1: { SummFile.Min1 = (value); break; }
                            case 2: { SummFile.Min2 = (value); break; }
                            case 3: { SummFile.Min3 = (value); break; }
                            case 4: { SummFile.Min4 = (value); break; }
                            case 5: { SummFile.Min5 = (value); break; }
                            case 6: { SummFile.Min6 = (value); break; }
                            case 7: { SummFile.Min7 = (value); break; }
                            case 8: { SummFile.Min8 = (value); break; }
                            case 9: { SummFile.Min9 = (value); break; }
                            case 10: { SummFile.Min10 = (value); break; }
                            case 11: { SummFile.Min11 = (value); break; }
                            case 12: { SummFile.Min12 = (value); break; }
                            case 13: { SummFile.Min13 = (value); break; }
                            case 14: { SummFile.Min14 = (value); break; }
                            case 15: { SummFile.Min15 = (value); break; }
                        }
                        break;
                    }
                case 3:
                    {
                        switch (ch)
                        {
                            case 0: { SummFile.Max0 = (value); break; }
                            case 1: { SummFile.Max1 = (value); break; }
                            case 2: { SummFile.Max2 = (value); break; }
                            case 3: { SummFile.Max3 = (value); break; }
                            case 4: { SummFile.Max4 = (value); break; }
                            case 5: { SummFile.Max5 = (value); break; }
                            case 6: { SummFile.Max6 = (value); break; }
                            case 7: { SummFile.Max7 = (value); break; }
                            case 8: { SummFile.Max8 = (value); break; }
                            case 9: { SummFile.Max9 = (value); break; }
                            case 10: { SummFile.Max10 = (value); break; }
                            case 11: { SummFile.Max11 = (value); break; }
                            case 12: { SummFile.Max12 = (value); break; }
                            case 13: { SummFile.Max13 = (value); break; }
                            case 14: { SummFile.Max14 = (value); break; }
                            case 15: { SummFile.Max15 = (value); break; }
                        }
                        break;
                    }
                case 4:
                    {
                        switch (ch)
                        {
                            case 0: { SummFile.ATS0 = (value); break; }
                            case 1: { SummFile.ATS1 = (value); break; }
                            case 2: { SummFile.ATS2 = (value); break; }
                            case 3: { SummFile.ATS3 = (value); break; }
                            case 4: { SummFile.ATS4 = (value); break; }
                            case 5: { SummFile.ATS5 = (value); break; }
                            case 6: { SummFile.ATS6 = (value); break; }
                            case 7: { SummFile.ATS7 = (value); break; }
                            case 8: { SummFile.ATS8 = (value); break; }
                            case 9: { SummFile.ATS9 = (value); break; }
                            case 10: { SummFile.ATS10 = (value); break; }
                            case 11: { SummFile.ATS11 = (value); break; }
                            case 12: { SummFile.ATS12 = (value); break; }
                            case 13: { SummFile.ATS13 = (value); break; }
                            case 14: { SummFile.ATS14 = (value); break; }
                            case 15: { SummFile.ATS15 = (value); break; }
                        }
                        break;
                    }
            }

        }


        public static void Record_Clear(Summary record)
        {
            record.TDCEvent = 0;
            record.ATS0 = 0;
            record.ATS1 = 0;
            record.ATS2 = 0;
            record.ATS3 = 0;
            record.ATS4 = 0;
            record.ATS5 = 0;
            record.ATS6 = 0;
            record.ATS7 = 0;
            record.ATS8 = 0;
            record.ATS9 = 0;
            record.ATS10 = 0;
            record.ATS11 = 0;
            record.ATS12 = 0;
            record.ATS13 = 0;
            record.ATS14 = 0;
            record.ATS15 = 0;
            record.TDC0 = 0;
            record.TDC1 = 0;
            record.TDC2 = 0;
            record.TDC3 = 0;
            record.TDC4 = 0;
            record.TDC5 = 0;
            record.TDC6 = 0;
            record.TDC7 = 0;
            record.TDC8 = 0;
            record.TDC9 = 0;
            record.TDC10 = 0;
            record.TDC11 = 0;
            record.TDC12 = 0;
            record.TDC13 = 0;
            record.TDC14 = 0;
            record.TDC15 = 0;
            record.Max0 = 0;
            record.Max1 = 0;
            record.Max2 = 0;
            record.Max3 = 0;
            record.Max4 = 0;
            record.Max5 = 0;
            record.Max6 = 0;
            record.Max7 = 0;
            record.Max8 = 0;
            record.Max9 = 0;
            record.Max10 = 0;
            record.Max11 = 0;
            record.Max12 = 0;
            record.Max13 = 0;
            record.Max14 = 0;
            record.Max15 = 0;
            record.Min0 = 0;
            record.Min1 = 0;
            record.Min2 = 0;
            record.Min3 = 0;
            record.Min4 = 0;
            record.Min5 = 0;
            record.Min6 = 0;
            record.Min7 = 0;
            record.Min8 = 0;
            record.Min9 = 0;
            record.Min10 = 0;
            record.Min11 = 0;
            record.Min12 = 0;
            record.Min13 = 0;
            record.Min14 = 0;
            record.Min15 = 0;
        }

        public class Summary
        {
            public ulong Event { get; set; }
            public uint TDCEvent { get; set; }
            public ulong TimeStamp { get; set; }
            public uint TDC0 { get; set; }
            public uint TDC1 { get; set; }
            public uint TDC2 { get; set; }
            public uint TDC3 { get; set; }
            public uint TDC4 { get; set; }
            public uint TDC5 { get; set; }
            public uint TDC6 { get; set; }
            public uint TDC7 { get; set; }
            public uint TDC8 { get; set; }
            public uint TDC9 { get; set; }
            public uint TDC10 { get; set; }
            public uint TDC11 { get; set; }
            public uint TDC12 { get; set; }
            public uint TDC13 { get; set; }
            public uint TDC14 { get; set; }
            public uint TDC15 { get; set; }
            public uint ATS0 { get; set; }
            public uint ATS1 { get; set; }
            public uint ATS2 { get; set; }
            public uint ATS3 { get; set; }
            public uint ATS4 { get; set; }
            public uint ATS5 { get; set; }
            public uint ATS6 { get; set; }
            public uint ATS7 { get; set; }
            public uint ATS8 { get; set; }
            public uint ATS9 { get; set; }
            public uint ATS10 { get; set; }
            public uint ATS11 { get; set; }
            public uint ATS12 { get; set; }
            public uint ATS13 { get; set; }
            public uint ATS14 { get; set; }
            public uint ATS15 { get; set; }
            public uint Max0 { get; set; }
            public uint Max1 { get; set; }
            public uint Max2 { get; set; }
            public uint Max3 { get; set; }
            public uint Max4 { get; set; }
            public uint Max5 { get; set; }
            public uint Max6 { get; set; }
            public uint Max7 { get; set; }
            public uint Max8 { get; set; }
            public uint Max9 { get; set; }
            public uint Max10 { get; set; }
            public uint Max11 { get; set; }
            public uint Max12 { get; set; }
            public uint Max13 { get; set; }
            public uint Max14 { get; set; }
            public uint Max15 { get; set; }
            public uint Min0 { get; set; }
            public uint Min1 { get; set; }
            public uint Min2 { get; set; }
            public uint Min3 { get; set; }
            public uint Min4 { get; set; }
            public uint Min5 { get; set; }
            public uint Min6 { get; set; }
            public uint Min7 { get; set; }
            public uint Min8 { get; set; }
            public uint Min9 { get; set; }
            public uint Min10 { get; set; }
            public uint Min11 { get; set; }
            public uint Min12 { get; set; }
            public uint Min13 { get; set; }
            public uint Min14 { get; set; }
            public uint Min15 { get; set; }
        }
    }
}
