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
        static DecData data = new DecData(); //Файл хранения выводимых данных
        public static void StartDecoding(BackgroundWorker ProgressBar, DoWorkEventArgs e) //функция декодирования (1 - экземпляр класса BGW, в котором была запущена функция
        {                                                                                 // 2 - аргумент для хранения результата BGW
            CSV_Output.Create_CSV(); // Создание файла CSV
            CSV_Output.InitCsv();    // Инициирование экземпляров записи
            int EvLeng = 0;          // Длина глобального Event в байтах
            int MSLeng = 0;          // Длина блока MStream
            int DataPLLeng = 0;      // Длина блока DataPayload
            ulong NumEv = 0;         // Номер Event
            string Date = "";        // Дата
            long pos = 0;            // Позиция в блоке файла
            long pospl = 0;          // Позиция в блоке DataPayload
            long prog = 0;           // Позициця для Progress Bar
            var FS = new FileStream(String.Format("{0}", TQDC2File.Path), FileMode.Open); // Экземпляр потока чтения
            long prog_st = FS.Length / 999;  // шаг для Progress Bar 
            using (CSV_Output.writer) // поток для записи
            using (CSV_Output.csv)    // поток для записи #csvhelper
            {
                CSV_Output.csv.WriteHeader<DecData>(); // Запись загаловка файла
                CSV_Output.csv.NextRecord();           //
                AddData(TIMESTAMP, "Время(ns)");                //
                AddData(EVENT, "№");                            //
                AddData(TYPE_DATA, "Тип данных");               // Запись размерностей столбцов
                CSV_Output.csv.WriteRecord(data);               //
                Record_Clear();                                 //
                CSV_Output.csv.NextRecord();                    //
                while (pos < FS.Length) // Главный цикл (пока позиция в блоке Event меньше длины файла в байтах)
                {
                    EvLeng = Converters.Byte2Int(TQDC2File.ReadByte(pos + 4, pos + 8, FS));  //Чтение длины Event
                    NumEv = (ulong)Converters.Byte2Int(TQDC2File.ReadByte(pos + 8, pos + 12, FS));  // Номер Event
                    Record_Clear(); // очистка данных
                    AddData(EVENT, NumEv.ToString()); // Добавление номера Event в блок данных
                    MSLeng = Converters.Byte2Int(TQDC2File.ReadByte(pos + 21, pos + 24, FS)) >> 2; // Чтение длины блока MStream
                    Date = Converters.UnixTimeStampToDateTime(Converters.Byte2uInt(TQDC2File.ReadByte(pos + 24, pos + 28, FS))).ToString(); // Чтение даты и времени глобального Event  и конвертация из unix в стандратный вид
                    Date += ":" + (Converters.Byte2Int(TQDC2File.ReadByte(pos + 28, pos + 32, FS)) >> 2).ToString(); // добавление к дате времени в нс
                    AddData(TIMESTAMP, Date); // Добавление временной метки Event в блок данных
                    CSV_Output.csv.WriteRecord(data); //запись блока данных в файл 
                    Record_Clear(); //Очистка блока данных
                    CSV_Output.csv.NextRecord();
                    pospl = pos + 32; // присваивание поцизии в блоке MSPayload начального значения
                    while (pospl != pos + EvLeng + 12) // Цикл на чтение Data Block ( пока позиция в блоке Data block не в конце блока Data block) 
                    {
                        DataPLLeng = Converters.Byte2Int(TQDC2File.ReadByte(pospl + 2, pospl + 4, FS)); //Чтение длины DataPayload
                        switch ((Converters.Byte2Int(TQDC2File.ReadByte(pospl, pospl + 1, FS))) >> 4) // Проверка типа DataPayload ( 0 TDC, 1 ADC)
                        {
                            case 0: //TDC
                                {
                                    pospl += 4; //переход на новую строку
                                    for (int i = 0; i < DataPLLeng / 4; i++) // Чтение данных с Data Block
                                    {
                                        switch (Converters.Byte2Int(TQDC2File.ReadByte(pospl, pospl + 1, FS)) >> 4) // Проверка на тип Header записи TDC
                                        {
                                            case 2: //TDC event header
                                                {
                                                    Record_Clear();
                                                    uint TimeStamp = ((Converters.Byte2uInt(TQDC2File.ReadByte(pospl + 2, pospl + 4, FS)) << 4) >> 4) * 25; // Чтение временной метки TDC
                                                    AddData(TIMESTAMP, TimeStamp.ToString());  // Добавление временной метки в блок данных
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
                                                    uint ch = ((Converters.Byte2uInt(TQDC2File.ReadByte(pospl, pospl + 4, FS))) << 7) >> 28; // Считываемый канал данных
                                                    uint value = ((Converters.Byte2uInt(TQDC2File.ReadByte(pospl, pospl + 4, FS))) << 11) >> 13; // Значение TDC с канала #ch
                                                    AddData(CHANNEL, ch.ToString());                                              //Добавление канала, данных,
                                                    AddData(DATA, (value / 10).ToString() + "," + (value % 10).ToString());       //и типа данных в блок данных
                                                    AddData(TYPE_DATA, TDC);                                                      //
                                                    CSV_Output.csv.WriteRecord(data);                      // Запись блока данных            
                                                    CSV_Output.csv.NextRecord();                           // в файл
                                                    pospl += 4;//переход на новую строку
                                                    break;
                                                }
                                            case 5: //TDC data, trailing edge
                                                {
                                                    uint ch = ((Converters.Byte2uInt(TQDC2File.ReadByte(pospl, pospl + 4, FS))) << 7) >> 28;// Считываемый канал данных
                                                    uint value = ((Converters.Byte2uInt(TQDC2File.ReadByte(pospl, pospl + 4, FS))) << 11) >> 13;// Значение TDC с канала #ch
                                                    AddData(CHANNEL, ch.ToString());                                        //Добавление канала, данных,
                                                    AddData(DATA, (value / 10).ToString() + "," + (value % 10).ToString()); //и типа данных в блок данных
                                                    AddData(TYPE_DATA, TDC);                                                //
                                                    CSV_Output.csv.WriteRecord(data);               // Запись блока данных   
                                                    Record_Clear();                                 // в файл
                                                    CSV_Output.csv.NextRecord();
                                                    //AddSummary(TDC, ch, value);
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
                                    uint ch = ((Converters.Byte2uInt(TQDC2File.ReadByte(pospl, pospl + 1, FS))) << 28) >> 28; // Считываемый канал данных
                                    AddData(CHANNEL, ch.ToString()); //Запись канала в блок данных
                                    long apospl = pospl; // Запись положения Header ADC
                                    pospl += 4; //переход на новую строку
                                    if (pospl == apospl + DataPLLeng) { pospl += 4; break; } // Проверка на отсуствие данных в Data Block
                                    while (pospl != apospl + DataPLLeng + 4) // Цикл на чтение данных ADC (пока позиция в блоке Data Block не в конце блока ADC)
                                    {
                                        string Databuf = ""; // Буфер для данных ADC
                                        uint DataLen = Converters.Byte2uInt(TQDC2File.ReadByte(pospl, pospl + 2, FS)); //Длина в блоке ADC в байтах
                                        bool odd = false; // переменная четности количества данных ( в строках 32 байта)
                                        if (DataLen % 4 !=0) // проверка на нечетность 
                                        {
                                            odd = true;
                                        }
                                        uint Timestamp = Converters.Byte2uInt(TQDC2File.ReadByte(pospl + 2, pospl + 4, FS))*8; // Чтение временной метки ADC
                                        AddData(TIMESTAMP, Timestamp.ToString()); // Добавление временной метки в блок данных
                                        pospl += 4; //переход на новую строку
                                        for (uint i = 0; i < ((DataLen / 4) * 4 == DataLen ? (DataLen / 4) : (DataLen / 4) + 1); i++) //цикл на чтение Sample ADC
                                        {
                                            Databuf += Converters.Byte2uInt(TQDC2File.ReadByte(pospl, pospl + 2, FS)).ToString() + ";"; // Запись первого Sample в строку
                                            if (odd & i == (DataLen / 4)) // если данные нечетные и последняя строка данных, то последнее 16 битный sample не читается
                                            {

                                            }
                                            else
                                            Databuf += Converters.Byte2uInt(TQDC2File.ReadByte(pospl + 2, pospl + 4, FS)).ToString();  // Запись второго Sample в строку
                                            Databuf += i != (DataLen / 4) - 1 ? ";" : ""; // запись разделителя в строку ( при последнем слове разделитель не добавляется)
                                            pospl += 4;//переход на новую строку
                                        }
                                        AddData(DATA, Databuf);     //Запись данных в блок данных 
                                        AddData(TYPE_DATA, ADC);    //
                                        CSV_Output.csv.WriteRecord(data);   // Запись блока данных в файл
                                        Record_Clear();         // Очистка блока данных
                                        CSV_Output.csv.NextRecord();
                                    }
                                    break;
                                }
                        }
                    }
                    Record_Clear(); // Очистка блока данных
                    prog += EvLeng + 12; //Повышение позиции для Progress Bar
                    pos = pos + EvLeng + 12;  // Запись новой позиции в файле
                    if (prog > prog_st)   // Проверка на превышение шага в позиции Progress Bar
                    {
                        prog = 0; // Очистка позиции Progress Bar
                        ProgressBar.ReportProgress(1); // Возращение прогресса в BackgroundWorker ProgressBar ( повышение строки прогресса в окне)
                    }
                }
                
            }
            e.Result = 1; //Возращение переменной для различия процесса
            CSV_Output.CloseCsv(); // закрытие потока записи
            FS.Close(); // закрытие потока чтения
        }

        public static byte EVENT { get; } = 1;          //Константы 
        public static byte TIMESTAMP { get; } = 2;      //
        public static byte CHANNEL { get; } = 3;        //
        public static byte TYPE_DATA { get; } = 4;      //
        public static byte DATA { get; } = 5;           //
        public static string TDC { get; } = "TDC";      //
        public static string ADC { get; } = "ADC";      //

        static void AddData(byte Type, string String) // Функция добавление данных в блок данных
        {
            switch (Type) // проверка на тип данных
            {
                case 1: data.Event = String; break;      // Запись в блок данных
                case 2: data.Timestamp = String; break;  //
                case 3: data.Channel = String; break;    //
                case 4: data.Type_Data = String; break;  //
                case 5: data.Data = String; break;       //
            }
        }

        static void Record_Clear() // очистка блока данных
        {
            data.Event = "";
            data.Timestamp = "";
            data.Channel = "";
            data.Type_Data = "";
            data.Data = "";
    }

        public class DecData            // класс для хранения данных
        {
            public string Event { get; set; } = "";
            public string Timestamp { get; set; } = "";
            public string Channel { get; set; } = "";
            public string Type_Data { get; set; } = "";
            public string Data { get; set; } = "";
        }
    }
}
