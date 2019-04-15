using CsvHelper;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TQDC16_Summary_Rev_1
{
    public class CSV_Output
    {
        public static string Path { get; set; } = null;
        public static StreamWriter writer;
        public static CsvWriter csv;
        public static List<dynamic> records = new List<dynamic>();
        public static DialogResult Create_CSV ()
        {
            DialogResult dialogResult = new DialogResult();
            var t = new Thread((ThreadStart)(() =>
            {
                SaveFileDialog OutFile = new SaveFileDialog();
                OutFile.Filter = "Comma Separated Value(*.csv) | *.csv";
                OutFile.FileName = String.Format("TQDC2-Summary {0}", DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss"));
                OutFile.InitialDirectory = TQDC2File.ReadFilePath;
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
            csv = new CsvWriter(writer);
        }

        public static void CloseCsv()
        {
            writer.Close();
        }
        
    }
}
