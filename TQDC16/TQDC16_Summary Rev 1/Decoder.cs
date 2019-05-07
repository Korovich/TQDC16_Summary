using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using static TQDC16_Summary_Rev_1.Converters;
using static TQDC16_Summary_Rev_1.CSV_Output;
using static TQDC16_Summary_Rev_1.TQDC2File;

namespace TQDC16_Summary_Rev_1
{
    class Decoder
    {
        public static void StartDecodingBinary(BackgroundWorker ProgressBar, DoWorkEventArgs e) //функция декодирования (1 - экземпляр класса BGW, в котором была запущена функция
        {                                                                                 // 2 - аргумент для хранения результата BGW
            if (Create_CSV("Декодер") != DialogResult.OK) // Создание файла CSV проверка на отмену)
            {
                return;
            }                                      
            InitCsv();    // Инициирование экземпляров записи
            int EvLeng;          // Длина глобального Event в байтах
            int MSLeng;          // Длина блока MStream
            int DataPLLeng;      // Длина блока DataPayload
            uint NumEv;         // Номер Event
            string Date;        // Дата
            long pos = 0;            // Позиция в блоке файла
            long pospl;          // Позиция в блоке DataPayload
            var FS = new FileStream(String.Format("{0}", ReadFilePath), FileMode.Open); // Экземпляр потока чтения
            using (writer) // поток для записи
            {
                writer.WriteLine("Event;Timestamp;Channel;Type Data;Data");
                writer.WriteLine("№;Время(ns);;Тип данных;");
                while (pos < FS.Length) // Главный цикл (пока позиция в блоке Event меньше длины файла в байтах)
                {
                    EvLeng = Byte2Int(ReadBytes(pos + 4, 4, FS));  //Чтение длины Event
                    NumEv = Byte2uInt(ReadBytes(pos + 8, 4, FS));  // Номер Event
                    MSLeng = Byte2Int(ReadBytes(pos + 21, 3, FS)) >> 2; // Чтение длины блока MStream
                    Date = UnixTimeStampToDateTime(Byte2uInt(ReadBytes(pos + 24, 4, FS))).ToString(); // Чтение даты и времени глобального Event  и конвертация из unix в стандратный вид
                    Date += ":" + (Byte2uInt(ReadBytes(pos + 28, 4, FS)) >> 2).ToString(); // добавление к дате времени в нс
                    writer.WriteLine(string.Format("{0};{1};;",NumEv,Date));
                    pospl = pos + 32; // присваивание поцизии в блоке MSPayload начального значения
                    while (pospl != pos + EvLeng + 12) // Цикл на чтение Data Block ( пока позиция в блоке Data block не в конце блока Data block) 
                    {
                        DataPLLeng = Byte2Int(ReadBytes(pospl + 2, 2, FS)); //Чтение длины DataPayload
                        switch ((Byte2Int(ReadBytes(pospl, 1, FS))) >> 4) // Проверка типа DataPayload ( 0 TDC, 1 ADC)
                        {
                            case 0: //TDC
                                {
                                    pospl += 4; //переход на новую строку
                                    for (int i = 0; i < DataPLLeng / 4; i++) // Чтение данных с Data Block
                                    {
                                        switch (Byte2Int(ReadBytes(pospl, 1, FS)) >> 4) // Проверка на тип Header записи TDC
                                        {
                                            case 2: //TDC event header
                                                {
                                                    uint TimeStamp = ((Byte2uInt(ReadBytes(pospl + 2, 2, FS)) << 4) >> 4) * 25; // Чтение временной метки TDC
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
                                                    uint ch = (((Byte2uInt(ReadBytes(pospl, 4, FS))) << 7) >> 28) + 1; // Считываемый канал данных
                                                    if (!IsNeedChannel(ch)) //проверка на нужду записанного канала
                                                    {
                                                        pospl += 4;
                                                        break;
                                                    }
                                                    uint value = (((Byte2uInt(ReadBytes(pospl, 4, FS))) << 11) >> 11) * 25; // Значение TDC с канала #ch
                                                    writer.WriteLine(string.Format(";;{0};TDC;{1}", ch + 1,value));
                                                    pospl += 4;//переход на новую строку
                                                    break;
                                                }
                                            case 5: //TDC data, trailing edge
                                                {
                                                    uint ch = (((Byte2uInt(ReadBytes(pospl, 4, FS))) << 7) >> 28) + 1;// Считываемый канал данных
                                                    if (!IsNeedChannel(ch)) //проверка на нужду записанного канала
                                                    {
                                                        pospl += 4;
                                                        break;
                                                    }
                                                    uint value = (((Byte2uInt(ReadBytes(pospl, 4, FS))) << 11) >> 11) * 25;// Значение TDC с канала #ch
                                                    writer.WriteLine(string.Format("{0};TDC;{1}", ch, value));
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
                                    uint ch = (((Byte2uInt(ReadBytes(pospl, 1, FS))) << 28) >> 28) + 1; // Считываемый канал данных
                                    if (!IsNeedChannel(ch))
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
                                        string Databuf = ""; // Буфер для данных ADC
                                        uint DataLen = Byte2uInt(ReadBytes(pospl, 2, FS)); //Длина в блоке ADC в байтах
                                        bool odd = false; // переменная четности количества данных ( в строках 32 байта)
                                        if (DataLen % 4 !=0) // проверка на нечетность 
                                        {
                                            odd = true;
                                        }
                                        uint Timestamp = Byte2uInt(ReadBytes(pospl + 2, 2, FS))*8; // Чтение временной метки ADC
                                        writer.Write(string.Format(";{0};{1};ADC;",Timestamp,ch));
                                        pospl += 4; //переход на новую строку
                                        for (uint i = 0; i < ((DataLen / 4) * 4 == DataLen ? (DataLen / 4) : (DataLen / 4) + 1); i++) //цикл на чтение Sample ADC
                                        {
                                            Databuf += Byte2Sample(ReadBytes(pospl + 2, 2, FS)).ToString() + ";"; // Запись первого Sample в строку
                                            if (odd & i == (DataLen / 4)) // если данные нечетные и последняя строка данных, то последнее 16 битный sample не читается
                                            {

                                            }
                                            else
                                            Databuf += Byte2Sample(ReadBytes(pospl, 2, FS)).ToString();  // Запись второго Sample в строку
                                            Databuf += i != (DataLen / 4) - 1 ? ";" : ""; // запись разделителя в строку ( при последнем слове разделитель не добавляется)
                                            pospl += 4;//переход на новую строку
                                        }
                                        writer.WriteLine(Databuf);
                                    }
                                    break;
                                }
                        }
                    }
                    pos = pos + EvLeng + 12;  // Запись новой позиции в файле
                    ProgressBar.ReportProgress((int)NumEv); // Возращение прогресса в BackgroundWorker ProgressBar ( повышение строки прогресса в окне)
                }
                
            }
            e.Result = 1; //Возращение переменной для различия процесса
            CSV_Output.CloseCsv(); // закрытие потока записи
            FS.Close(); // закрытие потока чтения
        }

        public static void StartDecodingText(BackgroundWorker ProgressBar, DoWorkEventArgs e)
        {
            if (Create_CSV("Декодер") != DialogResult.OK) // Создание файла CSV проверка на отмену)
            {
                return;
            }
            InitCsv();
            //ulong NumEv;         // Номер Event
            string Date;        // Дата
            int NumEv = 0;           // Позициця для Progress Bar
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
                    
                    if (readerLine.Substring(0, 3) == "Ev:")
                    {
                        readerLine = readerLine.Substring(4);
                        NumEv = int.Parse( readerLine.Substring(0, readerLine.IndexOf(" ")));
                        readerLine = readerLine.Substring(readerLine.IndexOf(' ')+1);
                        Date = UnixTimeStampToDateTime(uint.Parse(readerLine.Substring(readerLine.IndexOf(" ")+1, readerLine.IndexOf('.')-2 - readerLine.IndexOf(" ") + 1))).ToString(); // Чтение даты и времени глобального Event  и конвертация из unix в стандратный вид
                        readerLine = readerLine.Substring(readerLine.IndexOf('.')+1);
                        Date += ":" + readerLine.ToString(); // добавление к дате времени в нс
                        writer.WriteLine(string.Format("{0};{1};;", NumEv, Date));
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
                                        uint ch = uint.Parse(readerLine.Substring(0, readerLine.IndexOf(':'))) + 1;            //Добавление канала, данных
                                        uint value = uint.Parse(readerLine.Substring(readerLine.IndexOf(": ") + 2)) * 25;  //и типа данных в блок данных
                                        writer.WriteLine(string.Format(";;{0};TDC;{1}", ch, value));
                                        break;
                                    }
                                case "Adc":
                                    {
                                        readerLine = readerLine.Substring(4);
                                        uint ch = uint.Parse(readerLine.Substring(0, readerLine.IndexOf(':'))) + 1;            //Добавление канала, данных
                                        uint timestamp = uint.Parse(readerLine.Substring(readerLine.IndexOf(": ") + 1, readerLine.IndexOf(';') - (readerLine.IndexOf(": ") + 1))) * 8;
                                        writer.Write(string.Format(";{0};{1};ADC;", timestamp, ch));
                                        readerLine = readerLine.Substring(readerLine.IndexOf(';') + 2);
                                        string databuf = ""; // Буфер для данных ADC
                                        while (true)
                                        {
                                            try
                                            {
                                                databuf += readerLine.Substring(0, readerLine.IndexOf(' ')) + ";";
                                                readerLine = readerLine.Substring(readerLine.IndexOf(' ') + 1);
                                            }
                                            catch
                                            {
                                                databuf += readerLine;
                                                break;
                                            }
                                        }
                                        writer.WriteLine(databuf);
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
                    ProgressBar.ReportProgress(NumEv);   // Возращение прогресса в BackgroundWorker ProgressBar ( повышение строки прогресса в окне)
                }
            }
            e.Result = 1; //Возращение переменной для различия процесса
            CSV_Output.CloseCsv(); // закрытие потока записи
            fs.Close(); // закрытие потока чтения
        }

        static bool IsNeedChannel(uint i)
        {
            return Form1.DChannel[i-1];
        }
    }
}
