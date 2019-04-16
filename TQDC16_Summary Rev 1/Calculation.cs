using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TQDC16_Summary_Rev_1.Converters;
using static TQDC16_Summary_Rev_1.CSV_Output;
using static TQDC16_Summary_Rev_1.TQDC2File;

namespace TQDC16_Summary_Rev_1
{
    class Calculation
    {

        public static void StartCalc(BackgroundWorker ProgressBar, bool[] EnableChannel, DoWorkEventArgs e)
        {
            Create_CSV("Вычисление"); // Создание файла CSV
            InitCsv();    // Инициирование экземпляров записи
            int EvLeng = 0;          // Длина глобального Event в байтах
            int MSLeng = 0;          // Длина блока MStream
            int DataPLLeng = 0;      // Длина блока DataPayload
            ulong NumEv = 0;         // Номер Event
            //string Date = "";        // Дата 
            long pos = 0;            // Позиция в блоке файла
            long pospl = 0;          // Позиция в блоке DataPayload
            long prog = 0;           // Позициця для Progress Bar
            //ulong StartTimeStampnSec = 0;
            ulong TimeStampnSec = 0;
            ulong TimeStampSec = 0;
            var FS = new FileStream(string.Format("{0}", ReadFilePath), FileMode.Open); // Экземпляр потока чтения
            long prog_st = FS.Length / 999;  // шаг для Progress Bar 
            using (writer) // поток для записи
            using (csv)    // поток для записи #csvhelper
            {
                AddHeaderCalcData(EnableChannel);// Запись загаловка файл
                //StartTimeStampnSec = Byte2uInt(ReadByte(28, 32, FS)) >> 2;

                while (pos < FS.Length) // Главный цикл (пока позиция в блоке Event меньше длины файла в байтах)
                {
                    BufferData buferfiledata = new BufferData();
                    EvLeng = Byte2Int(ReadByte(pos + 4, pos + 8, FS));  //Чтение длины Event
                    NumEv = (ulong)Byte2Int(ReadByte(pos + 8, pos + 12, FS));  // Номер Event

                    MSLeng = Byte2Int(ReadByte(pos + 21, pos + 24, FS)) >> 2; // Чтение длины блока MStream

                    TimeStampSec = Byte2uInt(ReadByte(pos + 24, pos + 28, FS));
                    TimeStampnSec = Byte2uInt(ReadByte(pos + 28, pos + 32, FS))>>2;
                    buferfiledata.AddHeaderEvent(NumEv, TimeStampSec, TimeStampnSec);
                    //TimeStampnSec = Byte2uInt(ReadByte(pos + 24, pos + 28, FS)) * (ulong)100000000000
                    //+ Byte2uInt(ReadByte(pos + 28, pos + 32, FS)) >> 2;
                    // TimeStampnSec -= StartTimeStampnSec;
                    pospl = pos + 32; // присваивание поцизии в блоке MSPayload начального значения
                    BufferData<Adc_Interface> adcbuffer = new BufferData<Adc_Interface>();
                    BufferData<Tdc_Interface> tdcbuffer = new BufferData<Tdc_Interface>();
                    while (pospl != pos + EvLeng + 12) // Цикл на чтение Data Block ( пока позиция в блоке Data block не в конце блока Data block) 
                    {
                        DataPLLeng = Byte2Int(ReadByte(pospl + 2, pospl + 4, FS)); //Чтение длины DataPayload
                        switch ((Byte2Int(ReadByte(pospl, pospl + 1, FS))) >> 4) // Проверка типа DataPayload ( 0 TDC, 1 ADC)
                        {
                            case 0:
                                {
                                    pospl += 4; //переход на новую строку
                                    for (int i = 0; i < DataPLLeng / 4; i++) // Чтение данных с Data Block
                                    {
                                        switch (Byte2Int(ReadByte(pospl, pospl + 1, FS)) >> 4) // Проверка на тип Header записи TDC
                                        {
                                            case 2:
                                                {
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
                                                    int ch = ((Byte2Int(ReadByte(pospl, pospl + 4, FS))) << 7) >> 28;
                                                    if (!IsNeedChannel(ch+1)) break;
                                                    uint value = (((Byte2uInt(ReadByte(pospl, pospl + 4, FS))) << 11) >> 11) * 25;
                                                    tdcbuffer.Newrecord(ch, new Tdc_Interface(LEADING_FRONT, value));
                                                    //.Add(new BufferTdc { Channel = ch, Front = LEADING_FRONT, Data = value });
                                                    pospl += 4;
                                                    break;
                                                }
                                            case 5:
                                                {
                                                    int ch = ((Byte2Int(ReadByte(pospl, pospl + 4, FS))) << 7) >> 28;
                                                    if (!IsNeedChannel(ch+1)) break;
                                                    uint value = (((Byte2uInt(ReadByte(pospl, pospl + 4, FS))) << 11) >> 11) * 25;
                                                    tdcbuffer.Newrecord(ch, new Tdc_Interface(TRAILING_FRONT, value));
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
                                    int ch = (int)(((Byte2Int(ReadByte(pospl, pospl + 1, FS))) << 28) >> 28); // Считываемый канал данных
                                    if (!IsNeedChannel(ch+1))
                                    {
                                        pospl = pospl + DataPLLeng + 4;
                                        break;
                                    }

                                    long apospl = pospl; // Запись положения Header ADC
                                    pospl += 4; //переход на новую строку
                                    if (pospl == apospl + DataPLLeng) { pospl += 4; break; } // Проверка на отсуствие данных в Data Block
                                    while (pospl != apospl + DataPLLeng + 4) // Цикл на чтение данных ADC (пока позиция в блоке Data Block не в конце блока ADC)
                                    {
                                        List<int> buf = new List<int>();
                                        uint DataLen = Byte2uInt(ReadByte(pospl, pospl + 2, FS)); //Длина в блоке ADC в байтах
                                        bool odd = false; // переменная четности количества данных ( в строках 32 байта)
                                        ulong timestamp = Byte2uInt(ReadByte(pospl + 2, pospl + 4, FS)) * 8;
                                        if (DataLen % 4 != 0) // проверка на нечетность 
                                        {
                                            odd = true;
                                        }
                                        for (uint i = 0; i < ((DataLen / 4) * 4 == DataLen ? (DataLen / 4) : (DataLen / 4) + 1); i++) //цикл на чтение Sample ADC
                                        {
                                            buf.Add(Byte2Int(ReadByte(pospl, pospl + 2, FS)));
                                            if (odd & i == (DataLen / 4)) // если данные нечетные и последняя строка данных, то последнее 16 битный sample не читается
                                            {

                                            }
                                            else buf.Add(Byte2Int(ReadByte(pospl, pospl + 2, FS))); // Запись первого Sample в лист
                                            pospl += 4;//переход на новую строку
                                        }
                                        adcbuffer.Newrecord(ch, new Adc_Interface(buf, timestamp));// Чтение временной метки ADC в нС
                                        pospl += 4; //переход на новую строку
                                    }
                                    break;
                                }
                        }
                    }
                    AddRecord(buferfiledata, tdcbuffer, adcbuffer);
                    WriteFile(buferfiledata,writer);
                    prog += EvLeng + 12;
                    pos = pos + EvLeng + 12;
                    if (prog > prog_st)
                    {
                        prog = 0;
                        ProgressBar.ReportProgress(1);
                    }
                }
                //WriteFile(buferfiledata);
            }
            FS.Close();
            CloseCsv();
        }

        static bool IsNeedChannel(int i)
        {
            return Form1.CChannel[i - 1];
        }

        static void AddHeaderCalcData(bool[] EnableChannel)
        {
            string result = "";
            int n = 0;
            string[] Header = new string[5] {"TimeStampADC", "Tdc", "Max", "Min", "Integral" };
            string[] Comment = new string[5] {"Метка времени ADC", "Время от тригера до Hit", "Максимальное значение АЦП", "Минимальное значение АЦП", "Интеграл от блока АЦП" };
            if (EnableChannel.Length != 16)
            {
                throw new InvalidOperationException();
            }
            result = "Event;TimeStamp;";
            for (int i = 0; i < 16; i++)
            {
                if (EnableChannel[i])
                {
                    for (int j = 0; j < Header.Length; j++)
                    {
                        result += Header[j] + string.Format("{0}", i+1) + ";";
                    }
                    n++;
                }
            }
            result = result.Remove(result.Length - 1);
            writer.WriteLine(result);
            result = "№;Сек:нСек;";
            for (int i = 0; i < n; i++)
            {
                result += "Время;нСек;В;В;В;";
            }
            result = result.Remove(result.Length - 1);
            writer.WriteLine(result);
        }

        internal class InterfClass
        {
            internal ulong timestamp;
            internal ulong tdc;
            internal int max;
            internal int min;
            internal double integral;

            internal InterfClass ( ulong timestamp = 0, ulong tdc = 0, int max = 0, int min = 0, double integral = 0)
            {
                this.timestamp = timestamp;
                this.tdc = tdc;
                this.max = max;
                this.min = min;
                this.integral = integral;
            }

            internal string ReturnStringInterfClass()
            {
                return string.Format("{0};{1};{2};{3};{4}", this.timestamp, this.tdc, this.max, this.min, this.integral);
            }
        }

        static int ReturnMaxLen(BufferData buferfiledata)
        {
            int result = 0;
            foreach (List<InterfClass> list in buferfiledata)
            {
                result = list.Count > result ? list.Count : result;
            }
            buferfiledata.Reset();
            return result;
        }

        internal static double CalculationIntegral(List<int> samples)
        {
            double integral = 0;
            for (int i = 0; i < samples.Count()-1; i++)
            {
                integral += ((double)samples[i] + (double)samples[i + 1]) * 6.25;
            }
            return integral;
        }


        internal static void CalculationMMI(BufferData buferfiledata , List<int> data, ulong tdc, ulong timestamp, int channel)
        {
            int[] result = new int[2] { 0, 2147483647 }; //max min 
            double integral = 0;
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i] > result[0]) { result[0] = data[i]; }//max
                if (data[i] < result[1]) { result[1] = data[i]; }//min
            }
            integral = CalculationIntegral(data);//integral                                                                                           
            buferfiledata[channel].Add(new InterfClass { timestamp = timestamp, tdc = tdc, max = result[0], min = result[1], integral = integral });
        }
        
        internal static void WriteHeaderEvent(BufferData buferfiledata)
        {

        }

        internal static void AddRecord(BufferData buferfiledata, BufferData<Tdc_Interface> buffertdc, BufferData<Adc_Interface> bufferadc)
        {
            for (int ch = 0; ch < 16; ch++)
            {
                if (!IsNeedChannel(ch+1)) continue;
                if (bufferadc[ch].Count() == buffertdc[ch].Count())
                {
                    for (int i = 0; i < bufferadc[ch].Count(); i++)
                    {
                        CalculationMMI(buferfiledata, bufferadc[ch][i].bufsamples, buffertdc[ch][i].data, bufferadc[ch][i].timestamp, ch);
                    }
                }

                if (bufferadc[ch].Count() > buffertdc[ch].Count())
                {
                    for (int i = 0; i < buffertdc[ch].Count(); i++)
                    {
                        CalculationMMI(buferfiledata, bufferadc[ch][i].bufsamples, buffertdc[ch][i].data, bufferadc[ch][i].timestamp, ch);
                    }
                }

                if (bufferadc[ch].Count() < buffertdc[ch].Count())
                {
                    for (int i = 0; i < bufferadc[ch].Count(); i++)
                    {
                        CalculationMMI(buferfiledata, bufferadc[ch][i].bufsamples, buffertdc[ch][i].data, bufferadc[ch][i].timestamp, ch);
                    }
                }
            }
        }
        
        internal static void WriteFile(BufferData buferfiledata, StreamWriter writer)
        {
            int max = ReturnMaxLen(buferfiledata);
            buferfiledata.WriteStringHeaderEvent(writer);
            for (int i = 0; i < max; i++)
            {
                writer.Write(";;");
                foreach (List<InterfClass> Item in buferfiledata)
                {
                    if (IsNeedChannel(buferfiledata.position + 1))
                    {
                       if (buferfiledata.position !=0)  writer.Write(";");
                        if (i < Item.Count)
                        {
                            writer.Write(Item[i].ReturnStringInterfClass());
                        }
                        else writer.Write(";;;;");
                    }
                }
                writer.WriteLine();
                buferfiledata.Reset();
            }
        }


        internal static uint LEADING_FRONT = 1;
        internal static uint TRAILING_FRONT = 2;

        internal class Adc_Interface 
        {
            internal List<int> bufsamples;
            internal ulong timestamp;
            internal Adc_Interface(List<int> samples, ulong timestamp )
            {
                this.bufsamples = samples;
                this.timestamp = timestamp;
            }
        }
        internal class Tdc_Interface 
        {
            internal uint front;
            internal uint data;
            internal Tdc_Interface (uint front , uint data)
            {
                this.front = front;
                this.data = data;
            }
        }

        internal class BufferData<T>
        {
            private List<T>[] buffer;

            internal BufferData ()
            {
                buffer = new List<T>[16]; 
                
                for (uint i = 0; i<16;i++)
                {
                    
                    buffer[i] = new List<T>();
                }
                
            }

            internal void Newrecord (int channel, T item)
            {
                
                buffer[channel].Add(item);
            }

            internal T LastValue (int channel)
            {
                return buffer[channel].Last();
            }

            internal List<T> this[int channel]
            {
                get
                {
                    return buffer[channel];
                }
                set
                {
                    buffer[channel] = value;
                }
            }

        }
        
        internal class BufferData : IEnumerator
        {
            internal ulong numEvent;
            internal ulong timeStampSec;
            internal ulong timeStampnSec;
            internal int position;

            internal List<InterfClass> Channel1 = new List<InterfClass>();
            internal List<InterfClass> Channel2 = new List<InterfClass>();
            internal List<InterfClass> Channel3 = new List<InterfClass>();
            internal List<InterfClass> Channel4 = new List<InterfClass>();
            internal List<InterfClass> Channel5 = new List<InterfClass>();
            internal List<InterfClass> Channel6 = new List<InterfClass>();
            internal List<InterfClass> Channel7 = new List<InterfClass>();
            internal List<InterfClass> Channel8 = new List<InterfClass>();
            internal List<InterfClass> Channel9 = new List<InterfClass>();
            internal List<InterfClass> Channel10 = new List<InterfClass>();
            internal List<InterfClass> Channel11 = new List<InterfClass>();
            internal List<InterfClass> Channel12 = new List<InterfClass>();
            internal List<InterfClass> Channel13 = new List<InterfClass>();
            internal List<InterfClass> Channel14 = new List<InterfClass>();
            internal List<InterfClass> Channel15 = new List<InterfClass>();
            internal List<InterfClass> Channel16 = new List<InterfClass>();

            internal void AddHeaderEvent(ulong numEvent,
                                    ulong timeStampSec,
                                    ulong timeStampnSec)
            {
                this.numEvent = numEvent;
                this.timeStampSec = timeStampSec;
                this.timeStampnSec = timeStampnSec;
            }

            internal void WriteStringHeaderEvent (StreamWriter writer)
            {
                writer.WriteLine(string.Format("{0};{1}", numEvent, timeStampSec.ToString() + "|" + timeStampnSec.ToString()));
            }

            internal BufferData(ulong numEvent = 0, ulong timeStampSec = 0, ulong timeStampnSec = 0, int position = -1)
            {
                this.numEvent = numEvent;
                this.timeStampSec = timeStampSec;
                this.timeStampnSec = timeStampnSec;
                this.position = position;

                Channel1 = new List<InterfClass>();
                Channel2 = new List<InterfClass>();
                Channel3 = new List<InterfClass>();
                Channel4 = new List<InterfClass>();
                Channel5 = new List<InterfClass>();
                Channel6 = new List<InterfClass>();
                Channel7 = new List<InterfClass>();
                Channel8 = new List<InterfClass>();
                Channel9 = new List<InterfClass>();
                Channel10 = new List<InterfClass>();
                Channel11 = new List<InterfClass>();
                Channel12 = new List<InterfClass>();
                Channel13 = new List<InterfClass>();
                Channel14 = new List<InterfClass>();
                Channel15 = new List<InterfClass>();
                Channel16 = new List<InterfClass>();
            }


            internal List<InterfClass> this[int index]
            {
                get
                {
                    switch (index)
                    {
                        case 0: return Channel1;
                        case 1: return Channel2;
                        case 2: return Channel3;
                        case 3: return Channel4;
                        case 4: return Channel5;
                        case 5: return Channel6;
                        case 6: return Channel7;
                        case 7: return Channel8;
                        case 8: return Channel9;
                        case 9: return Channel10;
                        case 10: return Channel11;
                        case 11: return Channel12;
                        case 12: return Channel13;
                        case 13: return Channel14;
                        case 14: return Channel15;
                        case 15: return Channel16;
                        default: throw new InvalidOperationException();
                    }
                }
            }

            public IEnumerator GetEnumerator()
            {
                return (IEnumerator)this;
            }


            //IEnumerator
            public bool MoveNext()
            {
                position++;
                return (position < 16);
            }

            //IEnumerable
            public void Reset()
            { position = -1; }

            //IEnumerable
            object IEnumerator.Current
            {
                get {
                    return Current;
                }
            }

            public List<InterfClass> Current
            {
                get
                {
                    try
                    {
                        switch(position){
                            case 0: return Channel1;
                            case 1: return Channel2;
                            case 2: return Channel3;
                            case 3: return Channel4;
                            case 4: return Channel5;
                            case 5: return Channel6;
                            case 6: return Channel7;
                            case 7: return Channel8;
                            case 8: return Channel9;
                            case 9: return Channel10;
                            case 10: return Channel11;
                            case 11: return Channel12;
                            case 12: return Channel13;
                            case 13: return Channel14;
                            case 14: return Channel15;
                            case 15: return Channel16;
                            default: throw new InvalidOperationException();
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }
    }
}
