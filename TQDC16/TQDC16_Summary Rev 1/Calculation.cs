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
using Extreme.Mathematics.Calculus;
using Extreme.Mathematics;

namespace TQDC16_Summary_Rev_1
{
    class Calculation : TQDC16_Summary
    {
        public static void StartCalcBinary(BackgroundWorker ProgressBar, bool[] EnableChannel, DoWorkEventArgs e)
        {
            if (Create_CSV("Summary") != DialogResult.OK) // Создание файла CSV
            {
                return;
            }
            InitCsv();    // Инициирование экземпляров записи
            BackGroundWorkerResult calculationresult;
            int EvLeng;             // Длина глобального Event в байтах
            int MSLeng;             // Длина блока MStream
            int DataPLLeng;         // Длина блока DataPayload
            int NumEv = 0;            // Номер Event
            long pos = 0;           // Позиция в блоке файла
            long pospl;             // Позиция в блоке DataPayload
            long prog = 0;          // Позиция для Progress Bar
            ulong TimeStampnSec;    // Время тригера сек
            ulong TimeStampSec;     // Время тригера нсек
            ulong StartTimeStampnSec;    // Время тригера сек
            int NumCircleNumEvent = 0; // Количество кругов Event
            var fs = new FileStream(string.Format("{0}", ReadFilePath), FileMode.Open); // Экземпляр потока чтения
            using (writer) // поток для записи
            {
                AddHeaderCalc(EnableChannel);// Запись загаловка файл
                StartTimeStampnSec = ((ulong)Byte2uInt(ReadBytes(24, 4, fs)) * 1000000000) + (ulong)(Byte2uInt(ReadBytes(28, 4, fs)) >> 2);    // Запись время триггера нсек
                while (pos < fs.Length) // Главный цикл (пока позиция в блоке Event меньше длины файла в байтах)
                {
                    BufferData<CalcInterf> buferfiledata = new BufferData<CalcInterf>();  // Экземпляр для хранения данных вычислений
                    EvLeng = Byte2Int(ReadBytes(pos + 4, 4, fs));  // Чтение длины Event
                    if (NumEv == 16777215 + 16777215 * NumCircleNumEvent)
                    {
                        NumCircleNumEvent++;
                    }
                    NumEv = Byte2Int(ReadBytes(pos + 8, 4, fs)) + 16777215 * NumCircleNumEvent;  // Номер Event

                    MSLeng = Byte2Int(ReadBytes(pos + 21, 3, fs)) >> 2; // Чтение длины блока MStream

                    TimeStampSec = Byte2uInt(ReadBytes(pos + 24, 4, fs));    // Запись время триггера сек
                    TimeStampnSec = Byte2uInt(ReadBytes(pos + 28, 4, fs)) >> 2;    // Запись время триггера нсек
                    buferfiledata.AddHeaderEvent(NumEv, TimeStampSec, TimeStampnSec);  //Запись данных триггера
                    pospl = pos + 32; // присваивание поцизии в блоке MSPayload начального значения
                    BlockData<Adc_Interface> adcbuffer = new BlockData<Adc_Interface>(); // экземпляр данных adc в блоке event
                    BlockData<Tdc_Interface> tdcbuffer = new BlockData<Tdc_Interface>(); // экземпляр данных tdc в блоке event
                    while (pospl != pos + EvLeng + 12) // Цикл на чтение Data Block ( пока позиция в блоке Data block не в конце блока Data block) 
                    {
                        DataPLLeng = Byte2Int(ReadBytes(pospl + 2, 2, fs)); //Чтение длины DataPayload
                        switch ((Byte2Int(ReadBytes(pospl, 1, fs))) >> 4) // Проверка типа DataPayload ( 0 TDC, 1 ADC)
                        {
                            case 0:
                                {
                                    pospl += 4; //переход на новую строку
                                    for (int i = 0; i < DataPLLeng / 4; i++) // Чтение данных с Data Block
                                    {
                                        switch (Byte2Int(ReadBytes(pospl, 1, fs)) >> 4) // Проверка на тип Header записи TDC
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
                                                    int ch = ((Byte2Int(ReadBytes(pospl, 4, fs))) << 7) >> 28;                   //чтение канала данных
                                                    if (!IsNeedChannel(ch + 1, CChannel)) break;                                            //проверка нужды канала
                                                    uint value = (((Byte2uInt(ReadBytes(pospl, 4, fs))) << 11) >> 11) * 25;      //чтение данных tdc
                                                    if (inl_config)
                                                    {
                                                        //value += TQDC2Configs.Inl[2][12];
                                                    }
                                                    tdcbuffer.Newrecord(ch, new Tdc_Interface(LEADING_FRONT, value));           //Запись данных в блок данных tdc
                                                    pospl += 4;
                                                    break;
                                                }
                                            case 5:
                                                {
                                                    int ch = ((Byte2Int(ReadBytes(pospl, 4, fs))) << 7) >> 28;                   //чтение канала данных
                                                    if (!IsNeedChannel(ch + 1, CChannel)) break;                                            //проверка нужды канала
                                                    uint value = (((Byte2uInt(ReadBytes(pospl, 4, fs))) << 11) >> 11) * 25;      //чтение данных tdc
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
                                    int ch = (int)(((Byte2Int(ReadBytes(pospl, 1, fs))) << 28) >> 28); // Считываемый канал данных
                                    if (!IsNeedChannel(ch + 1, CChannel))       //проверка нужды канала
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
                                        uint DataLen = Byte2uInt(ReadBytes(pospl, 2, fs)); //Длина в блоке ADC в байтах
                                        bool odd = false; // переменная четности количества данных ( в строках 32 байта)
                                        uint timestamp = Byte2uInt(ReadBytes(pospl + 2, 2, fs)) * 8; //чтение метки времени
                                        pospl += 4;
                                        if (DataLen % 4 != 0) // проверка на нечетность 
                                        {
                                            odd = true;
                                        }
                                        for (uint i = 0; i < ((DataLen / 4) * 4 == DataLen ? (DataLen / 4) : (DataLen / 4) + 1); i++) //цикл на чтение Sample ADC
                                        {
                                            buf.Add(TQDC2Configs.ChGain[ch] ? (Byte2Sample(ReadBytes(pospl + 2, 2, fs)) + (int)TQDC2Configs.X4[ch]) : (Byte2Sample(ReadBytes(pospl + 2, 2, fs)) + (int)TQDC2Configs.X1[ch])); // Запись первого Sample в лист
                                            if (odd & i == (DataLen / 4)) // если данные нечетные и последняя строка данных, то последнее 16 битный sample не читается
                                            {

                                            }
                                            else buf.Add(TQDC2Configs.ChGain[ch] ? (Byte2Sample(ReadBytes(pospl, 2, fs)) + (int)TQDC2Configs.X4[ch]) : (Byte2Sample(ReadBytes(pospl + 2, 2, fs)) + (int)TQDC2Configs.X1[ch])); // Запись второго Sample в лист
                                            pospl += 4;//переход на новую строку
                                        }
                                        adcbuffer.Newrecord(ch, new Adc_Interface(buf, timestamp));// Чтение временной метки ADC в нС
                                        //pospl += 4; //переход на новую строку
                                    }
                                    break;
                                }
                        }
                    }
                    AddRecordCalc(buferfiledata, tdcbuffer, adcbuffer); // запись данных event в блок вычисленных данных
                    WriteFileCalc(buferfiledata, writer, StartTimeStampnSec); //запись в файл
                    prog += EvLeng + 12;    //Повышение позиции для Progress Bar
                    pos = pos + EvLeng + 12;    // Запись новой позиции в файле
                    ProgressBar.ReportProgress((int)NumEv, adcbuffer);   // Возращение прогресса в BackgroundWorker ProgressBar ( повышение строки прогресса в окне)
                    if (ProgressBar.CancellationPending == true)
                    {
                        fs.Close();     //Закрытие чтение
                        CloseCsv();     //и записи
                        return;
                    }
                }
            }
            calculationresult = new BackGroundWorkerResult(CALCULATION, 100, OK);
            e.Result = calculationresult; //Возращение переменной для различия процесса
            fs.Close();     //Закрытие чтение
            CloseCsv();     //и записи
        }

        public static void StartCalcText(BackgroundWorker ProgressBar, bool[] EnableChannel, DoWorkEventArgs e)
        {
            if (Create_CSV("Summary") != DialogResult.OK) // Создание файла CSV проверка на отмену)
            {
                return;
            }
            InitCsv();
            //ulong NumEv;         // Номер Event
            var fs = new FileStream(String.Format("{0}", ReadFilePath), FileMode.Open); // Экземпляр потока чтения
            var fsr = new StreamReader(fs);
            string readerLine = "";
            BackGroundWorkerResult calculationresult;
            ulong TimeStampnSec;    // Время тригера сек
            int NumEv = 0;    // Время тригера сек
            ulong TimeStampSec;     // Время ригера нсек
            ulong StartTimeStampnSec;    // Время тригера сек
            int NumCircleNumEvent = 0; // Количество кругов Event
            //long prog_st = (fs.Length / 10000) - 1;  // шаг для Progress Bar 
            using (writer)
            {
                AddHeaderCalc(EnableChannel);// Запись загаловка файл
                readerLine = fsr.ReadLine();
                readerLine = readerLine.Substring(4);
                readerLine = readerLine.Substring(readerLine.IndexOf(' ') + 1);
                StartTimeStampnSec = ulong.Parse(readerLine.Substring(readerLine.IndexOf(" ") + 1, readerLine.IndexOf('.') - 2 - readerLine.IndexOf(" ") + 1)) * 1000000000;    // Запись время триггера нсек*/
                readerLine = readerLine.Substring(readerLine.IndexOf('.') + 1);
                StartTimeStampnSec += ulong.Parse(readerLine.ToString());
                fs.Position = 0;
                fsr.DiscardBufferedData();
                readerLine = fsr.ReadLine();
                while (!fsr.EndOfStream)
                {
                    BufferData<CalcInterf> buferfiledata = new BufferData<CalcInterf>();  // Экземпляр для хранения данных вычислений
                    BlockData<Adc_Interface> adcbuffer = new BlockData<Adc_Interface>(); // экземпляр данных adc в блоке event
                    BlockData<Tdc_Interface> tdcbuffer = new BlockData<Tdc_Interface>(); // экземпляр данных tdc в блоке event
                    if (readerLine.Substring(0, 3) == "Ev:")
                    {
                        readerLine = readerLine.Substring(4);
                        if (NumEv == 16777215 + 16777215 * NumCircleNumEvent)
                        {
                            NumCircleNumEvent++;
                        }
                        NumEv = int.Parse(readerLine.Substring(0, readerLine.IndexOf(" "))) + 16777215 * NumCircleNumEvent;
                        readerLine = readerLine.Substring(readerLine.IndexOf(' ') + 1);
                        TimeStampSec = uint.Parse(readerLine.Substring(readerLine.IndexOf(" ") + 1, readerLine.IndexOf('.') - 2 - readerLine.IndexOf(" ") + 1)); // Чтение даты и времени глобального Event  и конвертация из unix в стандратный вид
                        readerLine = readerLine.Substring(readerLine.IndexOf('.') + 1);
                        TimeStampnSec = uint.Parse(readerLine.ToString()); // добавление к дате времени в нс
                        buferfiledata.AddHeaderEvent(NumEv, TimeStampSec, TimeStampnSec);  //Запись данных триггера
                        bool isenddata = false;
                        while (!isenddata)
                        {
                            readerLine = fsr.ReadLine();
                            if (readerLine is null) break;
                            switch (readerLine.Substring(0, 3))
                            {
                                case "Tdc":
                                    {
                                        readerLine = readerLine.Substring(4);
                                        int ch = int.Parse(readerLine.Substring(0, readerLine.IndexOf(':')));     //Добавление канала, данных,
                                        if (!IsNeedChannel(ch + 1, CChannel)) break;                                            //проверка нужды канала
                                        uint value = uint.Parse(readerLine.Substring(readerLine.IndexOf(": ") + 2)) * 25;      //чтение данных tdc
                                        tdcbuffer.Newrecord(ch, new Tdc_Interface(LEADING_FRONT, value));           //Запись данных в блок данных tdc
                                        break;
                                    }
                                case "Adc":
                                    {
                                        readerLine = readerLine.Substring(4);
                                        int ch = int.Parse(readerLine.Substring(0, readerLine.IndexOf(':')));             //чтение канала данных
                                        if (!IsNeedChannel(ch + 1, CChannel)) break;      //проверка нужды канала
                                        uint timestamp = uint.Parse(readerLine.Substring(readerLine.IndexOf(": ") + 1, readerLine.IndexOf(';') - (readerLine.IndexOf(": ") + 1))) * 8;
                                        readerLine = readerLine.Substring(readerLine.IndexOf(';') + 2);
                                        List<int> databuf = new List<int>();    //Массив для sample adc
                                        while (true)
                                        {
                                            try
                                            {
                                                //databuf.Add(int.Parse(readerLine.Substring(0, readerLine.IndexOf(' '))));
                                                databuf.Add(TQDC2Configs.ChGain[ch] ?
                                                    (int.Parse(readerLine.Substring(0, readerLine.IndexOf(' '))) + (int)TQDC2Configs.X4[ch])
                                                    : (int.Parse(readerLine.Substring(0, readerLine.IndexOf(' '))) + (int)TQDC2Configs.X1[ch]));
                                                readerLine = readerLine.Substring(readerLine.IndexOf(' ') + 1);
                                            }
                                            catch
                                            {
                                                //databuf.Add(int.Parse(readerLine));
                                                databuf.Add(TQDC2Configs.ChGain[ch] ?
                                                    (int.Parse(readerLine) + (int)TQDC2Configs.X4[ch])
                                                    : (int.Parse(readerLine) + (int)TQDC2Configs.X1[ch]));
                                                break;
                                            }
                                        }
                                        adcbuffer.Newrecord(ch, new Adc_Interface(databuf, timestamp));// Чтение временной метки ADC в нС
                                        break;
                                    }
                                case "Ev:":
                                    {
                                        isenddata = true;
                                        break;
                                    }
                            }
                        }
                    }
                    AddRecordCalc(buferfiledata, tdcbuffer, adcbuffer); // запись данных event в блок вычисленных данных
                    WriteFileCalc(buferfiledata, writer, StartTimeStampnSec); //запись в файл
                    ProgressBar.ReportProgress((int)NumEv, adcbuffer);   // Возращение прогресса в BackgroundWorker ProgressBar (повышение строки прогресса в окне)
                    if (ProgressBar.CancellationPending == true)
                    {
                        CloseCsv(); // закрытие потока записи
                        fs.Close(); // закрытие потока чтения
                        return;
                    }
                }
            }
            calculationresult = new BackGroundWorkerResult(CALCULATION, 100, OK);
            e.Result = calculationresult; //Возращение переменной для различия процесса
            CloseCsv(); // закрытие потока записи
            fs.Close(); // закрытие потока чтения
        }

        internal static double CalculationIntegral(List<int> samples, int start, int end)   //метод вычисления интеграла из datasamples
        {
            /*
            double result;
            SimpsonIntegrator simpson = new SimpsonIntegrator();
            simpson.RelativeTolerance = 1e-5;
            simpson.ConvergenceCriterion =
                ConvergenceCriterion.WithinRelativeTolerance;
            double integral = 0;
            result = simpson.Integrate(samples.ToArray(), 0, 2);*/
            double result = 0;
            for (int i = start; i < end - 1; i++)
            {
                result += Math.Abs(((double)samples[i] + (double)samples[i + 1]) * 6.25);
            }
            return result;
        }


        //Метод вычисления статистики из данных(мин макс интеграл)
        internal static bool CalculationMMI(BufferData<CalcInterf> buferfiledata, List<int> data, List<Tdc_Interface> tdcdata, int startTdc, int channel, uint timestampadc) //x long tdcnext = -1
        {
            int indexFindedEnd = 0;
            bool isFindEnd = false;
            bool isnexttdcpass = false;
            int indexStartTdc = 0;
            double result = 0;
            int indexnexttdc = 0;
            int max = data[0];
            int min = data[0];
            bool isinverted = false;
            bool isEndTdcinData = tdcdata.Count - 1 == startTdc;
            for (int j = 1; j < data.Count; j++)
            {
                if (data[j] > max)
                { max = data[j]; }//max
                if (data[j] < min)
                { min = data[j]; }//min
            }
            if (Math.Abs(max) < Math.Abs(min))
            {
                for (int j = 0; j < data.Count; j++)
                {
                    data[j] = -data[j];
                }
                isinverted = true;
            }
            for (int i = 0; i < data.Count; i++) //находим первую точку после tdc
            {
                if ((i * 12.5) + timestampadc > tdcdata[startTdc].data / 1000)
                {
                    indexStartTdc = i;
                    break;
                }
            }
            if (isEndTdcinData)
                for (int i = indexStartTdc; i < data.Count; i++) //находим первую точку после tdc
                {
                    if ((i * 12.5) + timestampadc > tdcdata[startTdc + 1].data / 1000)
                    {
                        indexnexttdc = i;
                        break;
                    }
                }
            for (int i = indexStartTdc + 1; i < data.Count; i++)
            {
                if (data[i] < data[indexStartTdc + 1] || data[i] < 0)
                {
                    isFindEnd = true;
                    indexFindedEnd = i;
                    break;
                }
            }
            if (isFindEnd)
            {
                if (isEndTdcinData)
                {
                    max = data[indexStartTdc];
                    min = data[indexStartTdc];
                    for (int j = indexStartTdc; j < indexFindedEnd; j++)
                    {
                        if (data[j] > max)
                        { max = data[j]; }//max
                        if (data[j] < min)
                        { min = data[j]; }//min
                    }
                    result = CalculationIntegral(data, indexStartTdc, indexFindedEnd);
                }
                else
                {

                }
            }
            else
            {
                if (isEndTdcinData)
                {
                    int indexMinAfterMax = 0;
                    max = data[indexStartTdc];
                    min = data[indexStartTdc];
                    for (int j = indexStartTdc; j < data.Count; j++)
                    {
                        if (data[j] > max)
                        {
                            max = data[j];
                            indexMinAfterMax = j;
                        }
                        if (data[j] < min)
                        {
                            min = data[j];
                        }
                        if (data[j]<data[indexMinAfterMax])
                        {
                            indexMinAfterMax = j;
                        }
                    }
                    result = CalculationIntegral(data, indexStartTdc, indexMinAfterMax);
                }
                else
                {
                    int indexMinAfterMax = 0;
                    max = data[indexStartTdc];
                    min = data[indexStartTdc];
                    for (int j = indexStartTdc; j < indexnexttdc; j++)
                    {
                        if (data[j] > max)
                        {
                            max = data[j];
                            indexMinAfterMax = j;
                        }
                        if (data[j] < min)
                        {
                            min = data[j];
                        }
                        if (data[j] < data[indexMinAfterMax])
                        {
                            indexMinAfterMax = j;
                        }
                    }
                    result = CalculationIntegral(data, indexStartTdc, indexMinAfterMax);
                }
            }   
            buferfiledata[channel].Add(new CalcInterf { tdc = tdcdata[startTdc].data, max = max, min = min, integral = result });
            if (isinverted)
            {
                for (int j = 0; j < data.Count; j++)
                {
                    data[j] = -data[j];
                }
            }
            return isnexttdcpass;
        }
    }
}
