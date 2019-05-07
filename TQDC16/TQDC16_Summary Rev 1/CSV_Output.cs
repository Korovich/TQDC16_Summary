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
        
    }
}
