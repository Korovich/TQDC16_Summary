using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TQDC16_Summary_Rev_1.TQDC16_Summary;
using static TQDC16_Summary_Rev_1.Calculation;
using System.Collections;

namespace TQDC16_Summary_Rev_1
{
    public class CSV_Output
    {
        public static string Path { get; set; } = null;

        public static StreamWriter writer;
        public static List<dynamic> records = new List<dynamic>();
        public static DialogResult Create_CSV (string infixName = "")
        {
            DialogResult dialogResult = new DialogResult();
            var t = new Thread((ThreadStart)(() =>
            {
                SaveFileDialog OutFile = new SaveFileDialog
                {
                    Filter = "Comma Separated Value(*.csv) | *.csv",
                    FileName = String.Format("{0} TQDC2-Summary {1}-{2}", TQDC2File.FileName, DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss"), infixName),
                    InitialDirectory = TQDC2File.ReadFilePath
                };
                dialogResult = OutFile.ShowDialog();
                Path = OutFile.FileName;
            }));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join();
            return dialogResult;
        }

        public static void InitCsv ()
        {
            writer = new StreamWriter(Path);
        }

        public static void CloseCsv()
        {
            writer.Close();
        }

        internal static void AddHeaderCalc(bool[] EnableChannel) //метод записи header файла
        {
            string result; //переменная хранения строки
            int n = 0;
            string[] Header = new string[4] { "Tdc", "Max", "Min", "Integral" };
            //string[] Comment = new string[5] {"Метка времени ADC", "Время от тригера до Hit", "Максимальное значение АЦП", "Минимальное значение АЦП", "Интеграл от блока АЦП" };
            if (EnableChannel.Length != 16) //Проверка на количество каналов
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
                        result += Header[j] + string.Format("{0}", i + 1) + ";"; // добавления в переменную записываемой строки
                    }                                                          // Header данных
                    n++;
                }
            }
            result = result.Remove(result.Length - 1);  //удаление последнего символа
            writer.WriteLine(result);       //переход на новую строку
            result = "№;Сек:нСек;";
            for (int i = 0; i < n; i++)
            {
                result += "пСек;В;В;В;";  //Запись unit
            }
            result = result.Remove(result.Length - 1);
            writer.WriteLine(result);
        }

        //Добавление записи в блок данных вычислений
        internal static void AddRecordCalc(BufferData<CalcInterf> buferfiledata, BlockData<Tdc_Interface> buffertdc, BlockData<Adc_Interface> bufferadc)
        {
            for (int ch = 0; ch < 16; ch++)
            {
                if (!IsNeedChannel(ch + 1, CChannel)||bufferadc[ch].Count==0||buffertdc[ch].Count==0) continue; 
                if(bufferadc[ch].Count() > 1)
                {
                    List<int> result = new List<int>();
                    uint timestamp = bufferadc[ch][0].timestamp;
                    int lasttdcindex = 0;
                    int numtdcenteres = 0;
                    result.InsertRange(0, bufferadc[ch][0].bufsamples);
                    for (int i=1;i<bufferadc[ch].Count;i++)
                    {
                        if (result.Count() == 0)
                        {
                            result.InsertRange(result.Count, bufferadc[ch][i].bufsamples);
                            timestamp = bufferadc[ch][i].timestamp;
                        }
                        if (timestamp + ((result.Count + 1) * 12.5) >=                  //x переделать на сравнение с временной точкой 
                            bufferadc[ch][i].timestamp)                               //x в массиве result а не с предыдущего sample из буффера
                        {
                            double pluscorr = (bufferadc[ch][i - 1].timestamp + (bufferadc[ch][i - 1].bufsamples.Count + 1) * 12.5 - bufferadc[ch][i].timestamp);
                            for (int t = 0; t < buffertdc[ch].Count; t++)
                            {
                                if (buffertdc[ch][t].data / 1000 > bufferadc[ch][i].timestamp)
                                {
                                    buffertdc[ch][t].data += (uint)pluscorr*1000;
                                }
                            }
                            result.InsertRange(result.Count, bufferadc[ch][i].bufsamples);
                        }
                        else
                        {
                            for (int k=0;k<buffertdc[ch].Count;k++)
                            {
                                if (result.Count() * 12.5 + timestamp > (buffertdc[ch][k].data / 1000))
                                {
                                    numtdcenteres = k;
                                    lasttdcindex = k - 1;
                                    break;
                                }
                            }
                            for (int j = 0; j < numtdcenteres; j++)
                            {
                                if (i != numtdcenteres - 1)
                                {
                                    if (CalculationMMI(buferfiledata, result, buffertdc[ch][j].data, ch, timestamp, buffertdc[ch][j + 1].data))
                                        j++;
                                }
                                else CalculationMMI(buferfiledata, result, buffertdc[ch].Last().data, ch, timestamp);
                            }
                            result = new List<int>();
                        }
                    }
                    
                    for (int i = lasttdcindex; i < buffertdc[ch].Count; i++)
                    {
                        if (i != buffertdc[ch].Count - 1)
                        {
                            if (CalculationMMI(buferfiledata, result, buffertdc[ch][i].data, ch, timestamp, buffertdc[ch][i + 1].data))
                                i++;
                        }
                        else CalculationMMI(buferfiledata, result, buffertdc[ch].Last().data, ch, timestamp);

                    }
                    //CalculationMMI(buferfiledata, result, buffertdc[ch].Last().data, ch, timestamp);
                }
                else
                {
                    for (int i = 0;i<buffertdc[ch].Count;i++)
                    {
                        if (i != buffertdc[ch].Count - 1)
                        {
                            if (CalculationMMI(buferfiledata, bufferadc[ch][0].bufsamples, buffertdc[ch][i].data, ch, bufferadc[ch][0].timestamp, buffertdc[ch][i + 1].data))
                                i++;
                        }
                        else CalculationMMI(buferfiledata, bufferadc[ch][0].bufsamples, buffertdc[ch].Last().data, ch, bufferadc[ch].Last().timestamp);
                    }
                    //CalculationMMI(buferfiledata, bufferadc[ch][0].bufsamples, buffertdc[ch].Last().data, ch, bufferadc[ch].Last().timestamp);
                }
            }
        }

        internal static void WriteFileCalc(BufferData<CalcInterf> buferfiledata, StreamWriter writer, ulong starttimestamp) //Метод записи данных в файл
        {
            int max = buferfiledata.MaxLen();
            if (max == 0)
            {
                buferfiledata.WriteStringHeaderEvent(writer, starttimestamp);
                foreach (List<CalcInterf> Item in buferfiledata)
                {
                    if (IsNeedChannel(buferfiledata.position + 1,CChannel))
                    {
                        writer.Write(";;;");
                    }
                }
                writer.WriteLine();
                return;
            }
            for (int i = 0; i < max; i++)
            {
                buferfiledata.WriteStringHeaderEvent(writer, starttimestamp);
                foreach (List<CalcInterf> Item in buferfiledata)
                {
                    if (IsNeedChannel(buferfiledata.position + 1,CChannel))
                    {
                        writer.Write(";");
                        if (i < Item.Count)
                        {
                            writer.Write(Item[i].ToString());
                        }
                        else writer.Write(";;;");
                    }
                }
                writer.WriteLine();
                buferfiledata.Reset();
            }
        }

        internal static void WriteFileDec(BlockData<Tdc_Interface> buffertdc, BlockData<Adc_Interface> bufferadc, StreamWriter writer, string date, int numEvent) //Метод записи данных в файл
        {
            writer.WriteLine(string.Format("{0};{1};;;",numEvent,date));
            for (int i = 0; i < 16; i++)
            {
                if (!IsNeedChannel(i + 1,DChannel)) continue;
                foreach (Tdc_Interface item in buffertdc[i])
                {
                    writer.WriteLine(string.Format(";;{0};{1};{2}", i + 1, "TDC", item.data));
                }
            }
            for (int i = 0; i < 16; i++)
            {
                if (!IsNeedChannel(i + 1,DChannel)) continue;
                foreach (Adc_Interface item in bufferadc[i])
                {
                    writer.WriteLine(string.Format(";;{0};{1}{2}", i, "ADC", item.ToString()));
                }
            }
        }

        // переменные для блока данных tdc
        internal static uint LEADING_FRONT = 1;     //Define переднего фронта
        internal static uint TRAILING_FRONT = 2;    //Define заднего фронта
        internal static uint UNKNOWN_FRONT = 2;     //Define заднего фронта

        public class CalcInterf //класс хранения данных для записи
        {
            internal ulong tdc;
            internal int max;
            internal int min;
            internal double integral;

            internal CalcInterf( ulong tdc = 0, int max = 0, int min = 0, double integral = 0) //конструктор
            {
                this.tdc = tdc;
                this.max = max;
                this.min = min;
                this.integral = integral;
            }

            public override string ToString()// Метод в строку
            {
                return string.Format("{0};{1};{2};{3}", this.tdc, this.max, this.min, this.integral);
            }
        }

        internal class Adc_Interface //Класс для хранений одной ячейки ADC
        {
            internal List<int> bufsamples;
            internal uint timestamp;
            internal Adc_Interface(List<int> samples, uint timestamp)
            {
                this.bufsamples = samples;
                this.timestamp = timestamp;
            }

            public override string ToString()
            {
                string result = "";
                foreach(int item in bufsamples)
                {
                    result += ";" + item.ToString();
                }
                return result;
            }
        }

        internal class Tdc_Interface //Класс для хранений одной ячейки TDC
        {
            internal uint front;
            internal uint data;
            internal Tdc_Interface(uint front, uint data)
            {
                this.front = front;
                this.data = data;
            }
        }

        internal class BlockData<T> : IEnumerator // Класс для хранения блока ADC или TDC
        {
            private readonly List<T>[] buffer;
            public int position;

            object IEnumerator.Current => Current;

            internal BlockData()
            {
                buffer = new List<T>[16];

                for (uint i = 0; i < 16; i++)
                {

                    buffer[i] = new List<T>();
                }
                position = -1;
            }

            internal void Newrecord(int channel, T item)
            {

                buffer[channel].Add(item);
            }

            internal T LastValue(int channel)
            {
                return buffer[channel].Last();
            }

            public IEnumerator GetEnumerator()
            {
                return (IEnumerator)this;
            }

            public bool MoveNext()
            {
                position++;
                return (position < 16);
            }

            public void Reset()
            {
                position = -1;
            }

            public List<T> Current
            {
                get
                {
                    try
                    {
                        return this[position];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
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

        public class BufferData<T> : IEnumerator
        {
            public int numEvent;
            public ulong timeStampSec;
            public ulong timeStampnSec;
            public int position;

            public List<T> Channel1 = new List<T>();
            public List<T> Channel2 = new List<T>();
            public List<T> Channel3 = new List<T>();
            public List<T> Channel4 = new List<T>();
            public List<T> Channel5 = new List<T>();
            public List<T> Channel6 = new List<T>();
            public List<T> Channel7 = new List<T>();
            public List<T> Channel8 = new List<T>();
            public List<T> Channel9 = new List<T>();
            public List<T> Channel10 = new List<T>();
            public List<T> Channel11 = new List<T>();
            public List<T> Channel12 = new List<T>();
            public List<T> Channel13 = new List<T>();
            public List<T> Channel14 = new List<T>();
            public List<T> Channel15 = new List<T>();
            public List<T> Channel16 = new List<T>();

            public void AddHeaderEvent(int numEvent,
                                    ulong timeStampSec,
                                    ulong timeStampnSec)
            {
                this.numEvent = numEvent;
                this.timeStampSec = timeStampSec;
                this.timeStampnSec = timeStampnSec;
            }

            public int MaxLen() //метод возврата максимальной длины
            {
                int result = 0;
                for (int i = 0; i < 16; i++)
                {
                    result = this[i].Count > result ? this[i].Count : result;
                }
                return result;
            }

            public void WriteStringHeaderEvent(StreamWriter writer, ulong starttimestamp)
            {
                ulong timestamp = ((ulong)timeStampSec * 1000000000) + (ulong)timeStampnSec;
                timestamp -= starttimestamp;
                writer.Write(string.Format("{0};{1}", numEvent, timestamp));
            }

            public BufferData(int numEvent = 0, ulong timeStampSec = 0, ulong timeStampnSec = 0, int position = -1)
            {
                this.numEvent = numEvent;
                this.timeStampSec = timeStampSec;
                this.timeStampnSec = timeStampnSec;
                this.position = position;

                Channel1 = new List<T>();
                Channel2 = new List<T>();
                Channel3 = new List<T>();
                Channel4 = new List<T>();
                Channel5 = new List<T>();
                Channel6 = new List<T>();
                Channel7 = new List<T>();
                Channel8 = new List<T>();
                Channel9 = new List<T>();
                Channel10 = new List<T>();
                Channel11 = new List<T>();
                Channel12 = new List<T>();
                Channel13 = new List<T>();
                Channel14 = new List<T>();
                Channel15 = new List<T>();
                Channel16 = new List<T>();
            }


            public List<T> this[int index]
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
            {
                position = -1;
            }

            //IEnumerable
            object IEnumerator.Current
            {
                get
                {
                    return Current;
                }
            }

            public List<T> Current
            {
                get
                {
                    try
                    {
                        switch (position)
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
                    catch (IndexOutOfRangeException)
                    {
                        throw new InvalidOperationException();
                    }
                }
            }
        }
    }
}
