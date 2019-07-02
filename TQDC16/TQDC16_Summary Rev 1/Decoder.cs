using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using static TQDC16_Summary_Rev_1.Converters;
using static TQDC16_Summary_Rev_1.CSV_Output;
using static TQDC16_Summary_Rev_1.TQDC2File;

namespace TQDC16_Summary_Rev_1
{
    class Decoder:TQDC16_Summary
    {
        public static void StartDecodingBinary(BackgroundWorker ProgressBar, DoWorkEventArgs e) //функция декодирования (1 - экземпляр класса BGW, в котором была запущена функция
        {                                                                                 // 2 - аргумент для хранения результата BGW
            if (Create_CSV("Decoded") != DialogResult.OK) // Создание файла CSV проверка на отмену)
            {
                return;
            }                                      
            InitCsv();    // Инициирование экземпляров записи
            int EvLeng;          // Длина глобального Event в байтах
            int MSLeng;          // Длина блока MStream
            int DataPLLeng;      // Длина блока DataPayload
            int NumEv = 0;         // Номер Event
            string date;        // Дата
            long pos = 0;            // Позиция в блоке файла
            long pospl;          // Позиция в блоке DataPayload
            int NumCircleNumEvent = 0; // Количество кругов Event
            var fs = new FileStream(String.Format("{0}", ReadFilePath), FileMode.Open); // Экземпляр потока чтения
            using (writer) // поток для записи
            {
                writer.WriteLine("Event;Timestamp;Channel;Type Data;Data");
                writer.WriteLine("№;Время(ns);;Тип данных;");
                while (pos < fs.Length) // Главный цикл (пока позиция в блоке Event меньше длины файла в байтах)
                {
                    EvLeng = Byte2Int(ReadBytes(pos + 4, 4, fs));  //Чтение длины Event
                    if (NumEv == 16777215 + 16777215 * NumCircleNumEvent)
                    {
                        NumCircleNumEvent++;
                    }
                    NumEv = Byte2Int(ReadBytes(pos + 8, 4, fs)) + 16777215 * NumCircleNumEvent;  // Номер Event
                    MSLeng = Byte2Int(ReadBytes(pos + 21, 3, fs)) >> 2; // Чтение длины блока MStream
                    date = UnixTimeStampToDateTime(Byte2uInt(ReadBytes(pos + 24, 4, fs))).ToString(); // Чтение даты и времени глобального Event  и конвертация из unix в стандратный вид
                    date += ":" + (Byte2uInt(ReadBytes(pos + 28, 4, fs)) >> 2).ToString(); // добавление к дате времени в нс
                    pospl = pos + 32; // присваивание поцизии в блоке MSPayload начального значения
                    BlockData<Adc_Interface> adcbuffer = new BlockData<Adc_Interface>(); // экземпляр данных adc в блоке event
                    BlockData<Tdc_Interface> tdcbuffer = new BlockData<Tdc_Interface>(); // экземпляр данных tdc в блоке event
                    while (pospl != pos + EvLeng + 12) // Цикл на чтение Data Block ( пока позиция в блоке Data block не в конце блока Data block) 
                    {
                        DataPLLeng = Byte2Int(ReadBytes(pospl + 2, 2, fs)); //Чтение длины DataPayload
                        switch ((Byte2Int(ReadBytes(pospl, 1, fs))) >> 4) // Проверка типа DataPayload ( 0 TDC, 1 ADC)
                        {
                            case 0: //TDC
                                {
                                    pospl += 4; //переход на новую строку
                                    for (int i = 0; i < DataPLLeng / 4; i++) // Чтение данных с Data Block
                                    {
                                        switch (Byte2Int(ReadBytes(pospl, 1, fs)) >> 4) // Проверка на тип Header записи TDC
                                        {
                                            case 2: //TDC event header
                                                {
                                                    //uint TimeStamp = ((Byte2uInt(ReadBytes(pospl + 2, 2, FS)) << 4) >> 4) * 25; // Чтение временной метки TDC
                                                    pospl += 4; //переход на новую строку
                                                    break;
                                                }
                                            case 3: //TDC event trailer
                                                {
                                                    pospl += 4; //переход на новую строку
                                                    break;
                                                }
                                            case 4: //TDC data, leading edge
                                                {
                                                    int ch = (((Byte2Int(ReadBytes(pospl, 4, fs))) << 7) >> 28) + 1; // Считываемый канал данных
                                                    if (!IsNeedChannel(ch,DChannel)) //проверка на нужду записанного канала
                                                    {
                                                        pospl += 4;
                                                        break;
                                                    }
                                                    uint value = (((Byte2uInt(ReadBytes(pospl, 4, fs))) << 11) >> 11) * 25; // Значение TDC с канала #ch
                                                    tdcbuffer.Newrecord(ch - 1, new Tdc_Interface(LEADING_FRONT, value));
                                                    pospl += 4;//переход на новую строку
                                                    break;
                                                }
                                            case 5: //TDC data, trailing edge
                                                {
                                                    int ch = (((Byte2Int(ReadBytes(pospl, 4, fs))) << 7) >> 28) + 1;// Считываемый канал данных
                                                    if (!IsNeedChannel(ch,DChannel)) //проверка на нужду записанного канала
                                                    {
                                                        pospl += 4;
                                                        break;
                                                    }
                                                    uint value = (((Byte2uInt(ReadBytes(pospl, 4, fs))) << 11) >> 11) * 25;// Значение TDC с канала #ch
                                                    tdcbuffer.Newrecord(ch - 1, new Tdc_Interface(TRAILING_FRONT, value));
                                                    pospl += 4;//переход на новую строку
                                                    break;
                                                }
                                            case 7: //TDC Error
                                                {
                                                    pospl += 4; //переход на новую строку
                                                    break;
                                                }
                                        }
                                    }
                                    break;
                                }
                            case 1: //ADC
                                {
                                    int ch = (((Byte2Int(ReadBytes(pospl, 1, fs))) << 28) >> 28) + 1; // Считываемый канал данных
                                    if (!IsNeedChannel(ch,DChannel))
                                    {
                                        pospl = pospl + DataPLLeng + 4;
                                        break;
                                    }
                                    //AddData(CHANNEL, ch.ToString()); //Запись канала в блок данных
                                    long apospl = pospl; // Запись положения Header ADC
                                    pospl += 4; //переход на новую строку
                                    if (pospl == apospl + DataPLLeng) { pospl += 4; break; } // Проверка на отсуствие данных в Data Block
                                    while (pospl != apospl + DataPLLeng + 4) // Цикл на чтение данных ADC (пока позиция в блоке Data Block не в конце блока ADC)
                                    {
                                        List<int> databuf = new List<int>(); // Буфер для данных ADC
                                        uint dataLen = Byte2uInt(ReadBytes(pospl, 2, fs)); //Длина в блоке ADC в байтах
                                        bool odd = false; // переменная четности количества данных ( в строках 32 байта)
                                        if (dataLen % 4 !=0) // проверка на нечетность 
                                        {
                                            odd = true;
                                        }
                                        uint timestamp = Byte2uInt(ReadBytes(pospl + 2, 2, fs))*8; // Чтение временной метки ADC
                                        pospl += 4; //переход на новую строку
                                        for (uint i = 0; i < ((dataLen / 4) * 4 == dataLen ? (dataLen / 4) : (dataLen / 4) + 1); i++) //цикл на чтение Sample ADC
                                        {
                                            databuf.Add(TQDC2Configs.ChGain[ch - 1] ? (Byte2Sample(ReadBytes(pospl + 2, 2, fs)) - (int)TQDC2Configs.X4[ch - 1]) : (Byte2Sample(ReadBytes(pospl + 2, 2, fs)) - (int)TQDC2Configs.X1[ch - 1])); // Запись первого Sample в лист
                                            if (odd & i == (dataLen / 4)) // если данные нечетные и последняя строка данных, то последнее 16 битный sample не читается
                                            {

                                            }
                                            else
                                                databuf.Add(TQDC2Configs.ChGain[ch - 1] ? (Byte2Sample(ReadBytes(pospl, 2, fs)) - (int)TQDC2Configs.X4[ch - 1]) : (Byte2Sample(ReadBytes(pospl + 2, 2, fs)) - (int)TQDC2Configs.X1[ch - 1])); // Запись второго Sample в лист
                                            pospl += 4;//переход на новую строку
                                        }
                                        adcbuffer.Newrecord(ch - 1, new Adc_Interface(databuf, timestamp));// Чтение временной метки ADC в нС
                                    }
                                    break;
                                }
                        }
                    }
                    WriteFileDec(tdcbuffer,adcbuffer, writer,date,NumEv); //запись в файл
                    pos = pos + EvLeng + 12;  // Запись новой позиции в файле
                    ProgressBar.ReportProgress((int)NumEv,adcbuffer); // Возращение прогресса в BackgroundWorker ProgressBar ( повышение строки прогресса в окне)
                    if (ProgressBar.CancellationPending == true)
                    {
                        fs.Close();     //Закрытие чтение
                        CloseCsv();     //и записи
                        return;
                    }
                }
                
            }

            BackGroundWorkerResult calculationresult = new BackGroundWorkerResult(DECODER, 100, OK);
            e.Result = calculationresult; //Возращение переменной для различия процесса
            CloseCsv(); // закрытие потока записи
            fs.Close(); // закрытие потока чтения
        }

        public static void StartDecodingText(BackgroundWorker ProgressBar, DoWorkEventArgs e)
        {
            if (Create_CSV("Summary") != DialogResult.OK) // Создание файла CSV проверка на отмену)
            {
                return;
            }
            InitCsv();
            //ulong NumEv;         // Номер Event
            string date = "";        // Дата
            int NumEv = 0;           // Позициця для Progress Bar
            int NumCircleNumEvent = 0; // Количество кругов Event
            var fs = new FileStream(String.Format("{0}", ReadFilePath), FileMode.Open); // Экземпляр потока чтения
            var fsr = new StreamReader(fs);
            string readerLine = "";
            using (writer)
            {
                writer.WriteLine("Event;Timestamp;Channel;Type Data;Data");
                writer.WriteLine("№;Время(ns);;Тип данных;");                    
                readerLine = fsr.ReadLine();
                while (!fsr.EndOfStream)
                {
                    BlockData<Adc_Interface> adcbuffer = new BlockData<Adc_Interface>(); // экземпляр данных adc в блоке event
                    BlockData<Tdc_Interface> tdcbuffer = new BlockData<Tdc_Interface>(); // экземпляр данных tdc в блоке event
                    if (readerLine.Substring(0, 3) == "Ev:")
                    {
                        readerLine = readerLine.Substring(4);
                        if (NumEv == 16777215 + 16777215 * NumCircleNumEvent)
                        {
                            NumCircleNumEvent++;
                        }
                        NumEv = int.Parse( readerLine.Substring(0, readerLine.IndexOf(" "))) + 16777215 * NumCircleNumEvent;
                        readerLine = readerLine.Substring(readerLine.IndexOf(' ')+1);
                        date = UnixTimeStampToDateTime(uint.Parse(readerLine.Substring(readerLine.IndexOf(" ")+1, readerLine.IndexOf('.')-2 - readerLine.IndexOf(" ") + 1))).ToString(); // Чтение даты и времени глобального Event  и конвертация из unix в стандратный вид
                        readerLine = readerLine.Substring(readerLine.IndexOf('.')+1);
                        date += ":" + readerLine.ToString(); // добавление к дате времени в нс
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
                                        int ch = int.Parse(readerLine.Substring(0, readerLine.IndexOf(':'))) + 1;            //Добавление канала, данных
                                        uint value = uint.Parse(readerLine.Substring(readerLine.IndexOf(": ") + 2)) * 25;  //и типа данных в блок данных
                                        tdcbuffer.Newrecord(ch - 1, new Tdc_Interface(LEADING_FRONT, value));
                                        //writer.WriteLine(string.Format(";;{0};TDC;{1}", ch, value));
                                        break;
                                    }
                                case "Adc":
                                    {
                                        readerLine = readerLine.Substring(4);
                                        int ch = int.Parse(readerLine.Substring(0, readerLine.IndexOf(':'))) + 1;            //Добавление канала, данных
                                        uint timestamp = uint.Parse(readerLine.Substring(readerLine.IndexOf(": ") + 1, readerLine.IndexOf(';') - (readerLine.IndexOf(": ") + 1))) * 8;
                                        writer.Write(string.Format(";{0};{1};ADC;", timestamp, ch));
                                        readerLine = readerLine.Substring(readerLine.IndexOf(';') + 2);
                                        List<int> databuf = new List<int>(); // Буфер для данных ADC
                                        while (true)
                                        {
                                            try
                                            {
                                                databuf.Add(int.Parse(readerLine.Substring(0, readerLine.IndexOf(' '))));
                                                readerLine = readerLine.Substring(readerLine.IndexOf(' ') + 1);
                                            }
                                            catch
                                            {
                                                databuf.Add(int.Parse(readerLine));
                                                break;
                                            }
                                        }
                                        adcbuffer.Newrecord(ch - 1, new Adc_Interface(databuf, timestamp));// Чтение временной метки ADC в нС
                                        //writer.WriteLine(databuf);
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
                    WriteFileDec(tdcbuffer, adcbuffer, writer, date, NumEv); //запись в файл
                    ProgressBar.ReportProgress((int)NumEv, adcbuffer);   // Возращение прогресса в BackgroundWorker ProgressBar ( повышение строки прогресса в окне)
                    if (ProgressBar.CancellationPending == true)
                    {
                        fs.Close();     //Закрытие чтение
                        CloseCsv();     //и записи
                        return;
                    }
                }
            }
            BackGroundWorkerResult calculationresult = new BackGroundWorkerResult(DECODER, 100, OK);
            e.Result = calculationresult; //Возращение переменной для различия процесса
            CloseCsv(); // закрытие потока записи
            fs.Close(); // закрытие потока чтения
        }
    }
}
