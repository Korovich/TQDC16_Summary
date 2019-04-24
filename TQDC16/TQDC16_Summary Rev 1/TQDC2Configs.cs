using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TQDC16_Summary_Rev_1
{
    class TQDC2Configs
    {

        public static void ReadConfigFile()
        {
            OpenFileDialog TQDC_DATA = new OpenFileDialog();
            TQDC_DATA.Filter = "Config files (*.conf)|*.conf";
            while (true)
            {
                DialogResult ResultDil;
                ResultDil = TQDC_DATA.ShowDialog();
                if (ResultDil == DialogResult.Cancel)
                {
                    return;
                }
                if (ResultDil == DialogResult.OK)
                {
                    var fs = new FileStream(String.Format("{0}", TQDC_DATA.FileName), FileMode.Open);
                    var fsr = new StreamReader(fs);
                    using (fsr)
                    {
                        if (fsr.ReadLine() != "[DevConf_0]") return;
                        string file = fsr.ReadToEnd();
                        for (int i=1; i<17;i++)
                        {
                            adcEn[i-1] = FindS(file, "adcEn", i) == "true" ? true:false;
                            chGain[i-1] = FindS(file, "chGain", i) == "true" ? true : false;
                            chTrigEn[i-1] = FindS(file, "chTrigEn", i) == "true" ? true : false;
                            inv[i-1] = FindS(file, "inv", i) == "true" ? true : false;
                            tdcEn[i-1] = FindS(file, "tdcEn", i) == "true" ? true : false;
                            thr[i-1] = int.Parse(FindS(file, "thr", i));
                            x1[i-1] = double.Parse(FindD(file, "x1", i), CultureInfo.InvariantCulture); 
                            x4[i-1] = double.Parse(FindD(file, "x4", i), CultureInfo.InvariantCulture);
                        }
                        invertInput = FindS(file, "invertInput") == "true" ? true:false;
                        invert_thr_trig = FindS(file, "invert_thr_trig") == "true" ? true : false;
                        invert_zs_thr = FindS(file, "invert_zs_thr") == "true" ? true : false;
                        selfTrigDelay = int.Parse(FindS(file, "selfTrigDelay"));
                        trigDelay = int.Parse(FindS(file, "trigDelay"));
                        trigPeriod = int.Parse(FindS(file, "trigPeriod"));
                        trigsetup = int.Parse(FindS(file, "trigsetup"));
                        zsEn = FindS(file, "zsEn") == "true" ? true : false;
                    }
                    fsr.Close();
                    fs.Close();
                    break;
                }
            }
        }

        private static string FindS(string file, string s)
        {
            file = file.Substring(file.IndexOf(string.Format("{0}=", s)) + string.Format("{0}=", s).Length);
            file = file.Substring(0, file.IndexOf('\n'));
            return file;
        }

        private static string FindS (string file ,string s, int ch)
        {
            file = file.Substring(file.IndexOf(string.Format("ch\\{0}\\{1}=", ch, s)) + string.Format("ch\\{0}\\{1}=", ch, s).Length);
            file = file.Substring(0, file.IndexOf('\n'));
            return file;
        }

        private static string FindD(string file ,string s,int ch)
        {
            ch = ch - 1;
            file = file.Substring(file.IndexOf(string.Format("{1}_{0}=", ch, s)) + string.Format("{1}_{0}=", ch, s).Length);
            file = file.Substring(0, file.IndexOf('\n'));
            return file;
        }

        //Кофигурационные переменные//
        public static bool[] adcEn { get; internal set; } = new bool[16];
        public static bool[] chGain { get; internal set; } = new bool[16];
        public static bool[] chTrigEn { get; internal set; } = new bool[16];
        public static bool[] inv { get; internal set; } = new bool[16];
        public static bool[] tdcEn { get; internal set; } = new bool[16];
        public static int[] thr { get; internal set; } = new int[16];
        public static bool invertInput { get; internal set; }
        public static bool invert_thr_trig { get; internal set; }
        public static bool invert_zs_thr { get; internal set; }
        public static int selfTrigDelay { get; internal set; }
        public static int trigDelay { get; internal set; }
        public static int trigPeriod { get; internal set; }
        public static int trigsetup { get; internal set; }
        public static bool zsEn { get; internal set; }
        public static double[] x1 { get; internal set; } = new double[16];
        public static double[] x4 { get; internal set; } = new double[16];
    }
}
