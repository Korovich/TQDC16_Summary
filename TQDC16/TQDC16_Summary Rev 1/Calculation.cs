using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static TQDC16_Summary_Rev_1.Converters;
using static TQDC16_Summary_Rev_1.CSV_Output;
using static TQDC16_Summary_Rev_1.TQDC2File;

namespace TQDC16_Summary_Rev_1
{
    class Calculation
    {

        public static void StartCalcBinary(BackgroundWorker ProgressBar, bool[] EnableChannel, DoWorkEventArgs e)
        {
            if (Create_CSV("Вычисление") != DialogResult.OK) // Создание файла CSV
            {
                return;
            }
            InitCsv();    // Инициирование экземпляров записи
            int EvLeng;             // Длина глобального Event в байтах
            int MSLeng;             // Длина блока MStream
            int DataPLLeng;         // Длина блока DataPayload
            ulong NumEv;            // Номер Event
            long pos = 0;           // Позиция в блоке файла
            long pospl;             // Позиция в блоке DataPayload
            long prog = 0;          // Позиция для Progress Bar
            ulong TimeStampnSec;    // Время тригера сек
            ulong TimeStampSec;     // Время ригера нсек
            var FS = new FileStream(string.Format("{0}", ReadFilePath), FileMode.Open); // Экземпляр потока чтения
            long prog_st = FS.Length / 999;  // шаг для Progress Bar 
            using (writer) // поток для записи
            using (csv)    // поток для записи #csvhelper
            {
                AddHeaderCalcData(EnableChannel);// Запись загаловка файл
                //StartTimeStampnSec = Byte2uInt(ReadByte(28, 32, FS)) >> 2;

                while (pos < FS.Length) // Главный цикл (пока позиция в блоке Event меньше длины файла в байтах)
                {
                    BufferData buferfiledata = new BufferData();  // Экземпляр для хранения данных вычислений
                    EvLeng = Byte2Int(ReadBytes(pos + 4, 4, FS));  // Чтение длины Event
                    NumEv = (ulong)Byte2Int(ReadBytes(pos + 8, 4, FS));  // Номер Event

                    MSLeng = Byte2Int(ReadBytes(pos + 21, 3, FS)) >> 2; // Чтение длины блока MStream

                    TimeStampSec = Byte2uInt(ReadBytes(pos + 24, 4, FS));    // Запись время триггера сек
                    TimeStampnSec = Byte2uInt(ReadBytes(pos + 28, 4, FS))>>2;    // Запись время триггера нсек
                    buferfiledata.AddHeaderEvent(NumEv, TimeStampSec, TimeStampnSec);  //Запись данных триггера
                    pospl = pos + 32; // присваивание поцизии в блоке MSPayload начального значения
                    BufferData<Adc_Interface> adcbuffer = new BufferData<Adc_Interface>(); // экземпляр данных adc в блоке event
                    BufferData<Tdc_Interface> tdcbuffer = new BufferData<Tdc_Interface>(); // экземпляр данных tdc в блоке event
                    while (pospl != pos + EvLeng + 12) // Цикл на чтение Data Block ( пока позиция в блоке Data block не в конце блока Data block) 
                    {
                        DataPLLeng = Byte2Int(ReadBytes(pospl + 2, 2, FS)); //Чтение длины DataPayload
                        switch ((Byte2Int(ReadBytes(pospl, 1, FS))) >> 4) // Проверка типа DataPayload ( 0 TDC, 1 ADC)
                        {
                            case 0:
                                {
                                    pospl += 4; //переход на новую строку
                                    for (int i = 0; i < DataPLLeng / 4; i++) // Чтение данных с Data Block
                                    {
                                        switch (Byte2Int(ReadBytes(pospl, 1, FS)) >> 4) // Проверка на тип Header записи TDC
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
                                                    int ch = ((Byte2Int(ReadBytes(pospl, 4, FS))) << 7) >> 28;                   //чтение канала данных
                                                    if (!IsNeedChannel(ch+1)) break;                                            //проверка нужды канала
                                                    uint value = (((Byte2uInt(ReadBytes(pospl, 4, FS))) << 11) >> 11) * 25;      //чтение данных tdc
                                                    tdcbuffer.Newrecord(ch, new Tdc_Interface(LEADING_FRONT, value));           //Запись данных в блок данных tdc
                                                    pospl += 4;
                                                    break;
                                                }
                                            case 5:
                                                {
                                                    int ch = ((Byte2Int(ReadBytes(pospl, 4, FS))) << 7) >> 28;                   //чтение канала данных
                                                    if (!IsNeedChannel(ch+1)) break;                                            //проверка нужды канала
                                                    uint value = (((Byte2uInt(ReadBytes(pospl, 4, FS))) << 11) >> 11) * 25;      //чтение данных tdc
                                                    tdcbuffer.Newrecord(ch, new Tdc_Interface(TRAILING_FRONT, value));          //Запись данных в блок данных tdc
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
                                    int ch = (int)(((Byte2Int(ReadBytes(pospl, 1, FS))) << 28) >> 28); // Считываемый канал данных
                                    if (!IsNeedChannel(ch+1))       //проверка нужды канала
                                    {
                                        pospl = pospl + DataPLLeng + 4;
                                        break;
                                    }

                                    long apospl = pospl; // Запись положения Header ADC
                                    pospl += 4; //переход на новую строку
                                    if (pospl == apospl + DataPLLeng) { pospl += 4; break; } // Проверка на отсуствие данных в Data Block
                                    while (pospl != apospl + DataPLLeng + 4) // Цикл на чтение данных ADC (пока позиция в блоке Data Block не в конце блока ADC)
                                    {
                                        List<int> buf = new List<int>();    //Массив для sample adc
                                        uint DataLen = Byte2uInt(ReadBytes(pospl, 2, FS)); //Длина в блоке ADC в байтах
                                        bool odd = false; // переменная четности количества данных ( в строках 32 байта)
                                        ulong timestamp = Byte2uInt(ReadBytes(pospl + 2, 2, FS)) * 8; //чтение метки времени
                                        pospl += 4;
                                        if (DataLen % 4 != 0) // проверка на нечетность 
                                        {
                                            odd = true;
                                        }
                                        for (uint i = 0; i < ((DataLen / 4) * 4 == DataLen ? (DataLen / 4) : (DataLen / 4) + 1); i++) //цикл на чтение Sample ADC
                                        {
                                            buf.Add(Byte2Sample(ReadBytes(pospl, 2, FS)));
                                            if (odd & i == (DataLen / 4)) // если данные нечетные и последняя строка данных, то последнее 16 битный sample не читается
                                            {

                                            }
                                            else buf.Add(Byte2Sample(ReadBytes(pospl, 2, FS))); // Запись первого Sample в лист
                                            pospl += 4;//переход на новую строку
                                        }
                                        adcbuffer.Newrecord(ch, new Adc_Interface(buf, timestamp));// Чтение временной метки ADC в нС
                                        //pospl += 4; //переход на новую строку
                                    }
                                    break;
                                }
                        }
                    }
                    AddRecord(buferfiledata, tdcbuffer, adcbuffer); // запись данных event в блок вычисленных данных
                    WriteFile(buferfiledata,writer); //запись в файл
                    prog += EvLeng + 12;    //Повышение позиции для Progress Bar
                    pos = pos + EvLeng + 12;    // Запись новой позиции в файле
                    if (prog > prog_st) // Проверка на превышение шага в позиции Progress Bar
                    {
                        prog = 0;
                        ProgressBar.ReportProgress(1);   // Возращение прогресса в BackgroundWorker ProgressBar ( повышение строки прогресса в окне)
                    }
                }
            }
            e.Result = 3; //Возращение переменной для различия процесса
            FS.Close();     //Закрытие чтение
            CloseCsv();     //и записи
        }

        public static void StartCalcText(BackgroundWorker ProgressBar, bool[] EnableChannel, DoWorkEventArgs e)
        {

        }

        static bool IsNeedChannel(int i) //метод проверки в надобности канала
        {
            return Form1.CChannel[i - 1];
        }

        static void AddHeaderCalcData(bool[] EnableChannel) //метод записи header файла
        {
            string result; //переменная хранения строки
            int n = 0;
            string[] Header = new string[5] {"TimeStampADC", "Tdc", "Max", "Min", "Integral" };
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
                        result += Header[j] + string.Format("{0}", i+1) + ";"; // добавления в переменную записываемой строки
                    }                                                          // Header данных
                    n++;
                }
            }
            result = result.Remove(result.Length - 1);  //удаление последнего символа
            writer.WriteLine(result);       //переход на новую строку
            result = "№;Сек:нСек;";
            for (int i = 0; i < n; i++)
            {
                result += "Время;нСек;В;В;В;";  //Запись unit
            }
            result = result.Remove(result.Length - 1);
            writer.WriteLine(result);
        }

        internal class InterfClass //класс хранения данных для записи
        {
            internal ulong timestamp;
            internal ulong tdc;
            internal int max;
            internal int min;
            internal double integral;

            internal InterfClass ( ulong timestamp = 0, ulong tdc = 0, int max = 0, int min = 0, double integral = 0) //конструктор
            {
                this.timestamp = timestamp;
                this.tdc = tdc;
                this.max = max;
                this.min = min;
                this.integral = integral;
            }

            public override string ToString()// Метод в строку
            {
                return string.Format("{0};{1};{2};{3};{4}", this.timestamp, this.tdc, this.max, this.min, this.integral);
            }
        }


        internal static double CalculationIntegral(List<int> samples)   //метод вычисления интеграла из datasamples
        {
            double integral = 0;
            for (int i = 0; i < samples.Count()-1; i++)
            {
                integral += ((double)samples[i] + (double)samples[i + 1]) * 6.25;
            }
            return integral;
        }


        //Метод вычисления статистики из данных(мин макс интеграл)
        internal static void CalculationMMI(BufferData buferfiledata , List<int> data, ulong tdc, ulong timestamp, int channel)
        {
            int[] result = new int[2] { data[0], data[0] }; //max min 
            double integral;
            for (int i = 1; i < data.Count; i++)
            {
                if (data[i] > result[0]) { result[0] = data[i]; }//max
                if (data[i] < result[1]) { result[1] = data[i]; }//min
            }
            integral = CalculationIntegral(data);//integral                                                                                           
            buferfiledata[channel].Add(new InterfClass { timestamp = timestamp, tdc = tdc, max = result[0], min = result[1], integral = integral });
        }


        //Добавление записи в блок данных вычислений
        internal static void AddRecord(BufferData buferfiledata, BufferData<Tdc_Interface> buffertdc, BufferData<Adc_Interface> bufferadc)
        {
            for (int ch = 0; ch < 16; ch++)
            {
                if (!IsNeedChannel(ch+1)) continue;
                if (bufferadc[ch].Count() == buffertdc[ch].Count()) //если количество хитов tdc adc одинаковое
                {
                    for (int i = 0; i < bufferadc[ch].Count(); i++)
                    {
                        CalculationMMI(buferfiledata, bufferadc[ch][i].bufsamples, buffertdc[ch][i].data, bufferadc[ch][i].timestamp, ch);
                    }
                }

                if (bufferadc[ch].Count() > buffertdc[ch].Count()) //если количество хитов tdc<adc
                {
                    for (int i = 0; i < buffertdc[ch].Count(); i++)
                    {
                        CalculationMMI(buferfiledata, bufferadc[ch][i].bufsamples, buffertdc[ch][i].data, bufferadc[ch][i].timestamp, ch);
                    }
                }

                if (bufferadc[ch].Count() < buffertdc[ch].Count())//если количество хитов tdc>adc
                {
                    for (int i = 0; i < bufferadc[ch].Count(); i++)
                    {
                        CalculationMMI(buferfiledata, bufferadc[ch][i].bufsamples, buffertdc[ch][i].data, bufferadc[ch][i].timestamp, ch);
                    }
                }
            }
        }
        
        internal static void WriteFile(BufferData buferfiledata, StreamWriter writer) //Метод записи данных в файл
        {
            int max = buferfiledata.MaxLen();
            buferfiledata.WriteStringHeaderEvent(writer);
            if (max == 0)
            {
                foreach (List<InterfClass> Item in buferfiledata)
                {
                    if (IsNeedChannel(buferfiledata.position + 1))
                    {
                        writer.Write(";;;;");
                    }
                }
                writer.WriteLine();

            }
            for (int i = 0; i < max; i++)
            {
                foreach (List<InterfClass> Item in buferfiledata)
                {
                    if (IsNeedChannel(buferfiledata.position + 1))
                    {
                        writer.Write(";");
                        if (i < Item.Count)
                        {
                            writer.Write(Item[i].ToString());
                        }
                        else writer.Write(";;;;");
                    }
                }
                writer.WriteLine();
                buferfiledata.Reset();
            }
        }

        // переменные для блока данных tdc
        internal static uint LEADING_FRONT = 1;     //Define переднего фронта
        internal static uint TRAILING_FRONT = 2;    //Define заднего фронта

        internal class Adc_Interface //Класс для хранений одной ячейки ADC
        {
            internal List<int> bufsamples;
            internal ulong timestamp;
            internal Adc_Interface(List<int> samples, ulong timestamp )
            {
                this.bufsamples = samples;
                this.timestamp = timestamp;
            }
        }
        internal class Tdc_Interface //Класс для хранений одной ячейки TDC
        {
            internal uint front;
            internal uint data;
            internal Tdc_Interface (uint front , uint data)
            {
                this.front = front;
                this.data = data;
            }
        }

        internal class BufferData<T> // Класс для хранения блока ADC или TDC
        {
            private readonly List<T>[] buffer;

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

            internal int MaxLen() //метод возврата максимальной длины
            {
                int result = 0;
                for (int i=0;i<16;i++)
                {
                    result = this[i].Count > result ? this[i].Count : result;
                }
                return result;
            }

            internal void WriteStringHeaderEvent (StreamWriter writer)
            {
                writer.Write(string.Format("{0};{1}", numEvent, timeStampSec.ToString() + "|" + timeStampnSec.ToString()));
                //for (int i=1;i<17;i++)
                //{
                    //if (IsNeedChannel(i))
                        //writer.Write(";;;;;;");
                //}
                //writer.WriteLine();
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
