using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TQDC16_Summary_Rev_1
{
    class Decoder
    {
        static Data data = new Data();
        public static void StartDecoding(BackgroundWorker ProgressBar)
        {
            CSV_Output.Create_CSV();
            CSV_Output.InitCsv();
            int EvLeng = 0;
            int MSLeng = 0;
            int PLLeng = 0;
            ulong NumEv = 0;
            string Date = "";
            long pos = 0;
            long pospl = 0;
            long prog = 0;
            var FS = new FileStream(String.Format("{0}", TQDC2File.Path), FileMode.Open);
            long prog_st = FS.Length / 999;
            using (CSV_Output.writer)
            using (CSV_Output.csv)
            {
                
                CSV_Output.csv.WriteHeader<Data>();
                CSV_Output.csv.NextRecord();
                while (pos < FS.Length)
                {
                    EvLeng = Converters.Byte2Int(TQDC2File.ReadByte(pos + 4, pos + 8, FS));
                    NumEv = (ulong)Converters.Byte2Int(TQDC2File.ReadByte(pos + 8, pos + 12, FS));
                    Record_Clear();
                    AddData(EVENT, NumEv.ToString());
                    MSLeng = Converters.Byte2Int(TQDC2File.ReadByte(pos + 21, pos + 24, FS)) >> 2;
                    Date = Converters.UnixTimeStampToDateTime(Converters.Byte2uInt(TQDC2File.ReadByte(pos + 24, pos + 28, FS))).ToString();
                    Date += ":" + (Converters.Byte2Int(TQDC2File.ReadByte(pos + 28, pos + 32, FS)) >> 2).ToString();
                    AddData(TIMESTAMP, Date);
                    CSV_Output.csv.WriteRecord(data);
                    Record_Clear();
                    CSV_Output.csv.NextRecord();
                    pospl = pos + 32;
                    while (pospl != pos + EvLeng + 12)
                    {
                        PLLeng = Converters.Byte2Int(TQDC2File.ReadByte(pospl + 2, pospl + 4, FS));
                        switch ((Converters.Byte2Int(TQDC2File.ReadByte(pospl, pospl + 1, FS))) >> 4)
                        {
                            case 0:
                                {
                                    pospl += PLLeng + 4;
                                    break;
                                }
                            case 1:
                                {
                                    uint ch = ((Converters.Byte2uInt(TQDC2File.ReadByte(pospl, pospl + 1, FS))) << 28) >> 28;
                                    long apospl = pospl;
                                    pospl += 4;
                                    if (pospl == apospl + PLLeng) { pospl += 4; break; }
                                    while (pospl != apospl + PLLeng + 4)
                                    {
                                        string Databuf = "";
                                        uint DataLen = Converters.Byte2uInt(TQDC2File.ReadByte(pospl, pospl + 2, FS));
                                        bool odd = false;
                                        if (DataLen % 4 !=0)
                                        {
                                            odd = true;
                                        }
                                        uint Timestamp = Converters.Byte2uInt(TQDC2File.ReadByte(pospl + 2, pospl + 4, FS))*8;
                                        AddData(TIMESTAMP, Timestamp.ToString());
                                        pospl += 4;
                                        for (uint i = 0; i < ((DataLen / 4) * 4 == DataLen ? (DataLen / 4) : (DataLen / 4) + 1); i++) 
                                        {
                                            Databuf += Converters.Byte2uInt(TQDC2File.ReadByte(pospl, pospl + 2, FS)).ToString() + ";";
                                            if (odd & i == (DataLen / 4))
                                            {

                                            }
                                            else
                                            Databuf += Converters.Byte2uInt(TQDC2File.ReadByte(pospl + 2, pospl + 4, FS)).ToString();
                                            Databuf += i != (DataLen / 4) - 1 ? ";" : "";
                                            pospl += 4;
                                        }
                                        AddData(DATA, Databuf, ch);
                                        CSV_Output.csv.WriteRecord(data);
                                        CSV_Output.csv.NextRecord();
                                    }
                                    Record_Clear();
                                    break;
                                }
                        }
                        //pospl += 4;
                    }
                    Record_Clear();
                    prog += EvLeng + 12;
                    pos = pos + EvLeng + 12;
                    if (prog > prog_st)
                    {
                        prog = 0;
                        ProgressBar.ReportProgress(1);
                    }
                }
                
            }
            CSV_Output.CloseCsv();
            FS.Close();
        }

        public static byte EVENT { get; } = 1;
        public static byte TIMESTAMP { get; } = 2;
        public static byte DATA { get; } = 3;

        static void AddData(byte Type, string String, uint Channel = 0)
        {
            switch (Type)
            {
                case 1: data.Event = String; break;
                case 2: data.Timestamp = String; break;
                case 3:
                    {
                        switch (Channel)
                        {
                            case 0: data.Data0 = String; break;
                            case 1: data.Data1 = String; break;
                            case 2: data.Data2 = String; break;
                            case 3: data.Data3 = String; break;
                            case 4: data.Data4 = String; break;
                            case 5: data.Data5 = String; break;
                            case 6: data.Data6 = String; break;
                            case 7: data.Data7 = String; break;
                            case 8: data.Data8 = String; break;
                            case 9: data.Data9 = String; break;
                            case 10: data.Data10 = String; break;
                            case 11: data.Data11 = String; break;
                            case 12: data.Data12 = String; break;
                            case 13: data.Data13 = String; break;
                            case 14: data.Data14 = String; break;
                            case 15: data.Data15 = String; break;
                        }
                    }
                    break;
            }
        }

        static void Record_Clear()
        {
            data.Event = "";
            data.Timestamp = "";
            data.Data0 = "";
            data.Data1 = "";
            data.Data2  = "";
            data.Data3  = "";
            data.Data4  = "";
            data.Data5  = "";
            data.Data6  = "";
            data.Data7  = "";
            data.Data8  = "";
            data.Data9  = "";
            data.Data10  = "";
            data.Data11  = "";
            data.Data12  = "";
            data.Data13  = "";
            data.Data14  = "";
            data.Data15  = "";
    }

        public class Data
        {
            public string Event { get; set; } = "";
            public string Timestamp { get; set; } = "";
            public string Data0 { get; set; } = "";
            public string Data1 { get; set; } = "";
            public string Data2 { get; set; } = "";
            public string Data3 { get; set; } = "";
            public string Data4 { get; set; } = "";
            public string Data5 { get; set; } = "";
            public string Data6 { get; set; } = "";
            public string Data7 { get; set; } = "";
            public string Data8 { get; set; } = "";
            public string Data9 { get; set; } = "";
            public string Data10 { get; set; } = "";
            public string Data11 { get; set; } = "";
            public string Data12 { get; set; } = "";
            public string Data13 { get; set; } = "";
            public string Data14 { get; set; } = "";
            public string Data15 { get; set; } = "";
        }
    }
}
