using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Media;
using System.Threading;
using static TQDC16_Summary_Rev_1.CSV_Output; 

namespace TQDC16_Summary_Rev_1
{

    public partial class TQDC16_Summary : Form
    {
        public TQDC16_Summary()
        {
            InitializeComponent();
            DChannel = new bool[16];
            CChannel = new bool[16];
            isAnalysis = false;
            inl_config = false;
            ChartSample.Series.Add("Null");
            ChartSample.Series[0].Points.AddXY(0, 0);
            ChartSample.ChartAreas[0].AxisY.Maximum = 32768;
            ChartSample.ChartAreas[0].AxisY.Minimum = -32768;
        }

        public const int OK = 0x01;
        public const int CANCEL = 0x02;
        public const int ERROR = 0x03;
        public const int ANALYS = 0x04;
        public const int DECODER = 0x05;
        public const int CALCULATION = 0x06;
        public const int MANUALCONFIGBASELINE = 0x07;
        public const int GRAPHIC = 0x10;

        public static bool[] DChannel;
        public static bool[] CChannel;
        public static bool isAnalysis;
        public static double[] manualBaseline = new double[16];
        readonly Color CheckBoxChColor = SystemColors.ButtonShadow;
        TQDC2File.OpenResult result;
        public static bool inl_config;
        private int MinY = 0;
        private int MaxY = 1;
        private int stepchart = 0;
        private int stepchartdivmax = 100;
        private int stepchartdivmin = 800;
        private bool iscanclose = false;
        private int modeBGW = 0;
        private int[] manualEventNum = new int[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //private int manualEventBlockHitNum = 0;
        private int manualChoosenChannel = 0;


        public static int ModeBaseline { get; private set; } = 0; //Переменная режима калибровки BaseLine 0-без калибровки 1-стандартная 2-из файла 3-в ручную

        private void label1_Click(object sender, EventArgs e)
        {
           
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void SaveFile_Click(object sender, EventArgs e)
        {
          
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void OpenFileBtn_Click(object sender, EventArgs e)
        {
            result = TQDC2File.Open_File();
            if (result.Selected)
            {
                if (BackGrWorkProgressBar.IsBusy != true)
                {
                    OpenFileBtn.Enabled = false;
                    StartDecoder.Enabled = false;
                    StartWrite.Enabled = false;
                    modeBGW = ANALYS;
                    Progress.Maximum = 10000;
                    BackGrWorkProgressBar.RunWorkerAsync(new BackGroundWorkerTask(ANALYS, result.Format));
                    StopButton.Enabled = true;
                }
                IDText.Text = result.ID;
                SerialText.Text = result.Serial;
            }

        }

        private void StartWrite_Click(object sender, EventArgs e)
        {
            if (BackGrWorkProgressBar.IsBusy != true)
            {
                PanelRadioButtonChart.Enabled = true;
                PanelConfigChart.Enabled = true;
                //StepChart.Maximum = AnalysisFile.NumEv/stepchartdivmax;
                StepChart.Maximum = 1000;
                //StepChart.Minimum = AnalysisFile.NumEv/stepchartdivmin;
                StepChart.Minimum = 300;
                BackGrWorkProgressBar.RunWorkerAsync(new BackGroundWorkerTask(CALCULATION, result.Format));
                ChangedStateRadioButtonChannel(CChannel);
                modeBGW = CALCULATION;
                OpenFileBtn.Enabled = false;
                StartDecoder.Enabled = false;
                StartWrite.Enabled = false;
                Progress.Maximum = AnalysisFile.NumEv;
                StopButton.Enabled = true;
            }
        }

        private void StartDecoder_Click(object sender, EventArgs e)
        {
            if (BackGrWorkProgressBar.IsBusy != true)
            {
                PanelRadioButtonChart.Enabled = true;
                PanelConfigChart.Enabled = true;
                //StepChart.Maximum = AnalysisFile.NumEv / stepchartdivmax;
                StepChart.Maximum = 1000;
                //StepChart.Minimum = AnalysisFile.NumEv / stepchartdivmin;
                StepChart.Minimum = 300;
                ChangedStateRadioButtonChannel(DChannel);
                modeBGW = DECODER;
                BackGrWorkProgressBar.RunWorkerAsync(new BackGroundWorkerTask(DECODER, result.Format));
                OpenFileBtn.Enabled = false;
                StartDecoder.Enabled = false;
                StartWrite.Enabled = false;
                Progress.Maximum = AnalysisFile.NumEv;
                StopButton.Enabled = true;
            }
        }

        private void BackGrWorkProgressBar_DoWork(object sender, DoWorkEventArgs e)
        {
            BackGrWorkProgressBar.WorkerSupportsCancellation = true;
            BackGroundWorkerTask Task;
            if (e.Argument is BackGroundWorkerTask)
            {
                Task = e.Argument as BackGroundWorkerTask;
                
            }
            else { throw new InvalidOperationException(); }
            switch (Task.Type)
            {
                case DECODER:
                    {
                        switch (Task.Format)
                        {
                            case "txt":
                                {
                                    Decoder.StartDecodingText(BackGrWorkProgressBar, e);
                                    break;
                                }
                            case "dat":
                                {
                                    Decoder.StartDecodingBinary(BackGrWorkProgressBar, e);
                                    break;
                                }
                        }
                        
                        break;
                    }
                case ANALYS:
                    {
                        switch (Task.Format)
                        {
                            case "txt":
                                {
                                    AnalysisFile.AnChannelText(BackGrWorkProgressBar, e);
                                    break;
                                }
                            case "dat":
                                {
                                    AnalysisFile.AnChannelBinary(BackGrWorkProgressBar, e);
                                    break;
                                }
                        }
                        break;
                    }
                case CALCULATION:
                    {
                        switch (Task.Format)
                        {
                            case "txt":
                                {
                                    Calculation.StartCalcText(BackGrWorkProgressBar, CChannel, e);
                                    break;
                                }
                            case "dat":
                                {
                                    Calculation.StartCalcBinary(BackGrWorkProgressBar, CChannel, e);
                                    break;
                                }
                        }
                        break;
                    }
                case MANUALCONFIGBASELINE:
                    {

                        break;
                    }
                default:
                    {
                        MessageBox.Show(Task.Type.ToString(), "Ошибка", MessageBoxButtons.RetryCancel);
                        break;
                    }
            }
            if (BackGrWorkProgressBar.CancellationPending) iscanclose = true;
        }

        private void BackGrWorkProgressBar_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (BackGrWorkProgressBar.CancellationPending) return;
            BlockData<Adc_Interface> adcdata;
            Progress.Value = (e.ProgressPercentage);
            if (e.UserState is BlockData<Adc_Interface> && e.UserState != null)
            {
                adcdata = (BlockData<Adc_Interface>)e.UserState;
                if (stepchart == StepChart.Value)
                {
                    UpdateChartSample(adcdata);
                    stepchart = 0;
                }
                else stepchart++;
            }

        }

        private void BackGrWorkProgressBar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackGroundWorkerResult result;
            if (e.Result is BackGroundWorkerResult)
            {
                result = (BackGroundWorkerResult)e.Result;

                switch (result.Type)
                {
                    case DECODER:
                        {
                            break;
                        }
                    case ANALYS:
                        {
                            isAnalysis = true;
                            NumEvText.Text = AnalysisFile.NumEv.ToString();
                            NumUsCh.Text = AnalysisFile.NumCh.ToString();
                            ConReadFile.BackColor = System.Drawing.Color.White;
                            ConSaveFile.BackColor = System.Drawing.Color.White;
                            ConfiguireWindow.Enabled = true;

                            //ConfiguireWindow.TabPages.Add(OutInfoFile);
                            for (int i = 0; i < 16; i++)
                            {
                                switch (i)
                                {
                                    case 0:
                                        {
                                            if (AnalysisFile.Channel[i]) { DChannel1.Checked = true; CChannel1.Checked = true; }
                                            else
                                            {
                                                DChannel1.BackColor = CheckBoxChColor; DChannel1.Enabled = false;
                                                CChannel1.BackColor = CheckBoxChColor; CChannel1.Enabled = false;
                                            }
                                            break;
                                        }
                                    case 1:
                                        {
                                            if (AnalysisFile.Channel[i]) { DChannel2.Checked = true; CChannel2.Checked = true; }
                                            else
                                            {
                                                DChannel2.BackColor = CheckBoxChColor; DChannel2.Enabled = false;
                                                CChannel2.BackColor = CheckBoxChColor; CChannel2.Enabled = false;
                                            }
                                            break;
                                        }
                                    case 2:
                                        {
                                            if (AnalysisFile.Channel[i]) { DChannel3.Checked = true; CChannel3.Checked = true; }
                                            else
                                            {
                                                DChannel3.BackColor = CheckBoxChColor; DChannel3.Enabled = false;
                                                CChannel3.BackColor = CheckBoxChColor; CChannel3.Enabled = false;
                                            }
                                            break;
                                        }
                                    case 3:
                                        {
                                            if (AnalysisFile.Channel[i]) { DChannel4.Checked = true; CChannel4.Checked = true; }
                                            else
                                            {
                                                CChannel4.BackColor = CheckBoxChColor; CChannel4.Enabled = false;
                                                DChannel4.BackColor = CheckBoxChColor; DChannel4.Enabled = false;
                                            }
                                            break;
                                        }
                                    case 4:
                                        {
                                            if (AnalysisFile.Channel[i]) { DChannel5.Checked = true; CChannel5.Checked = true; }
                                            else
                                            {
                                                DChannel5.BackColor = CheckBoxChColor; DChannel5.Enabled = false;
                                                CChannel5.BackColor = CheckBoxChColor; CChannel5.Enabled = false;
                                            }
                                            break;
                                        }
                                    case 5:
                                        {
                                            if (AnalysisFile.Channel[i]) { DChannel6.Checked = true; CChannel6.Checked = true; }
                                            else
                                            {
                                                DChannel6.BackColor = CheckBoxChColor; DChannel6.Enabled = false;
                                                CChannel6.BackColor = CheckBoxChColor; CChannel6.Enabled = false;
                                            }
                                            break;
                                        }
                                    case 6:
                                        {
                                            if (AnalysisFile.Channel[i]) { DChannel7.Checked = true; CChannel7.Checked = true; }
                                            else
                                            {
                                                DChannel7.BackColor = CheckBoxChColor; DChannel7.Enabled = false;
                                                CChannel7.BackColor = CheckBoxChColor; CChannel7.Enabled = false;
                                            }
                                            break;
                                        }
                                    case 7:
                                        {
                                            if (AnalysisFile.Channel[i]) { DChannel8.Checked = true; CChannel8.Checked = true; }
                                            else
                                            {
                                                DChannel8.BackColor = CheckBoxChColor; DChannel8.Enabled = false;
                                                CChannel8.BackColor = CheckBoxChColor; CChannel8.Enabled = false;
                                            }
                                            break;
                                        }
                                    case 8:
                                        {
                                            if (AnalysisFile.Channel[i]) { DChannel9.Checked = true; CChannel9.Checked = true; }
                                            else
                                            {
                                                DChannel9.BackColor = CheckBoxChColor; DChannel9.Enabled = false;
                                                CChannel9.BackColor = CheckBoxChColor; CChannel9.Enabled = false;
                                            }
                                            break;
                                        }
                                    case 9:
                                        {
                                            if (AnalysisFile.Channel[i]) { DChannel10.Checked = true; CChannel10.Checked = true; }
                                            else
                                            {
                                                DChannel10.BackColor = CheckBoxChColor; DChannel10.Enabled = false;
                                                CChannel10.BackColor = CheckBoxChColor; CChannel10.Enabled = false;
                                            }
                                            break;
                                        }
                                    case 10:
                                        {
                                            if (AnalysisFile.Channel[i]) { DChannel11.Checked = true; CChannel11.Checked = true; }
                                            else
                                            {
                                                DChannel11.BackColor = CheckBoxChColor; DChannel11.Enabled = false;
                                                CChannel11.BackColor = CheckBoxChColor; CChannel11.Enabled = false;
                                            }
                                            break;
                                        }
                                    case 11:
                                        {
                                            if (AnalysisFile.Channel[i]) { DChannel12.Checked = true; CChannel12.Checked = true; }
                                            else
                                            {
                                                DChannel12.BackColor = CheckBoxChColor; DChannel12.Enabled = false;
                                                CChannel12.BackColor = CheckBoxChColor; CChannel12.Enabled = false;
                                            }
                                            break;
                                        }
                                    case 12:
                                        {
                                            if (AnalysisFile.Channel[i]) { DChannel13.Checked = true; CChannel13.Checked = true; }
                                            else
                                            {
                                                DChannel13.BackColor = CheckBoxChColor; DChannel13.Enabled = false;
                                                CChannel13.BackColor = CheckBoxChColor; CChannel13.Enabled = false;
                                            }
                                            break;
                                        }
                                    case 13:
                                        {
                                            if (AnalysisFile.Channel[i]) { DChannel14.Checked = true; CChannel14.Checked = true; }
                                            else
                                            {
                                                DChannel14.BackColor = CheckBoxChColor; DChannel14.Enabled = false;
                                                CChannel14.BackColor = CheckBoxChColor; CChannel14.Enabled = false;
                                            }
                                            break;
                                        }
                                    case 14:
                                        {
                                            if (AnalysisFile.Channel[i]) { DChannel15.Checked = true; CChannel15.Checked = true; }
                                            else
                                            {
                                                DChannel15.BackColor = CheckBoxChColor; DChannel15.Enabled = false;
                                                CChannel15.BackColor = CheckBoxChColor; CChannel15.Enabled = false;
                                            }
                                            break;
                                        }
                                    case 15:
                                        {
                                            if (AnalysisFile.Channel[i]) { DChannel16.Checked = true; CChannel16.Checked = true; }
                                            else
                                            {
                                                DChannel16.BackColor = CheckBoxChColor; DChannel16.Enabled = false;
                                                CChannel16.BackColor = CheckBoxChColor; CChannel16.Enabled = false;
                                            }
                                            break;
                                        }
                                }
                            }
                            break;
                        }
                    case CALCULATION:
                        {
                            break;
                        }
                }
                ChartSample.Series.Clear();
                ChartSample.Series.Add("Null");
                ChartSample.Series[0].Points.AddXY(0, 0);
                ChartSample.ChartAreas[0].AxisY.Maximum = 32768;
                ChartSample.ChartAreas[0].AxisY.Minimum = -32768;
                PanelRadioButtonChart.Enabled = false;
                PanelConfigChart.Enabled = false;
                NumerMaximumX.Value = 0;
                NumerMinimumX.Value = 0;
                NumerMaximumY.Value = 0;
                NumerMinimumY.Value = 0;
                CheckBoxAxisXAutoSize.Checked = true;
                CheckBoxAxisYAutoSize.Checked = true;
                labelChannel.Text = "Channel №";
                StepChart.Minimum = 0;
                StepChart.Value = 0;
                StopButton.Enabled = false;
            }
            Progress.Value = 0;
            SystemSounds.Exclamation.Play();
            OpenFileBtn.Enabled = true;
            StartDecoder.Enabled = true;
            StartWrite.Enabled = true;
            modeBGW = 0;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }


        private void DChannel1_CheckedChanged(object sender, EventArgs e)
        {
            DChannel[0] = DChannel1.Checked;
        }

        private void DChannel2_CheckedChanged(object sender, EventArgs e)
        {
            DChannel[1] = DChannel2.Checked;
        }

        private void DChannel3_CheckedChanged(object sender, EventArgs e)
        {
            DChannel[2] = DChannel3.Checked;
        }

        private void DChannel4_CheckedChanged(object sender, EventArgs e)
        {
            DChannel[3] = DChannel4.Checked;
        }

        private void DChannel5_CheckedChanged(object sender, EventArgs e)
        {
            DChannel[4] = DChannel5.Checked;
        }

        private void DChannel7_CheckedChanged(object sender, EventArgs e)
        {
            DChannel[6] = DChannel7.Checked;
        }

        private void DChannel6_CheckedChanged(object sender, EventArgs e)
        {
            DChannel[5] = DChannel6.Checked;
        }

        private void DChannel8_CheckedChanged(object sender, EventArgs e)
        {
            DChannel[7] = DChannel8.Checked;
        }

        private void DChannel10_CheckedChanged(object sender, EventArgs e)
        {
            DChannel[9] = DChannel10.Checked;
        }

        private void DChannel9_CheckedChanged(object sender, EventArgs e)
        {
            DChannel[8] = DChannel9.Checked;
        }

        private void DChannel11_CheckedChanged(object sender, EventArgs e)
        {
            DChannel[10] = DChannel11.Checked;
        }

        private void DChannel12_CheckedChanged(object sender, EventArgs e)
        {
            DChannel[11] = DChannel12.Checked;
        }

        private void DChannel13_CheckedChanged(object sender, EventArgs e)
        {
            DChannel[12] = DChannel13.Checked;
        }

        private void DChannel14_CheckedChanged(object sender, EventArgs e)
        {
            DChannel[13] = DChannel14.Checked;
        }

        private void DChannel15_CheckedChanged(object sender, EventArgs e)
        {
            DChannel[14] = DChannel15.Checked;
        }

        private void DChannel16_CheckedChanged(object sender, EventArgs e)
        {
            DChannel[15] = DChannel16.Checked;
        }

        private void CChannel1_CheckedChanged(object sender, EventArgs e)
        {
            CChannel[0] = CChannel1.Checked;
        }

        private void CChannel2_CheckedChanged(object sender, EventArgs e)
        {
            CChannel[1] = CChannel2.Checked;
        }

        private void CChannel3_CheckedChanged(object sender, EventArgs e)
        {
            CChannel[2] = CChannel3.Checked;
        }

        private void CChannel4_CheckedChanged(object sender, EventArgs e)
        {
            CChannel[3] = CChannel4.Checked;
        }

        private void CChannel5_CheckedChanged(object sender, EventArgs e)
        {
            CChannel[4] = CChannel5.Checked;
        }

        private void CChannel6_CheckedChanged(object sender, EventArgs e)
        {
            CChannel[5] = CChannel6.Checked;
        }

        private void CChannel7_CheckedChanged(object sender, EventArgs e)
        {
            CChannel[6] = CChannel7.Checked;
        }

        private void CChannel8_CheckedChanged(object sender, EventArgs e)
        {
            CChannel[7] = CChannel8.Checked;
        }

        private void CChannel9_CheckedChanged(object sender, EventArgs e)
        {
            CChannel[8] = CChannel9.Checked;
        }

        private void CChannel10_CheckedChanged(object sender, EventArgs e)
        {
            CChannel[9] = CChannel10.Checked;
        }

        private void CChannel11_CheckedChanged(object sender, EventArgs e)
        {
            CChannel[10] = CChannel11.Checked;
        }

        private void CChannel12_CheckedChanged(object sender, EventArgs e)
        {
            CChannel[11] = CChannel12.Checked;
        }

        private void CChannel13_CheckedChanged(object sender, EventArgs e)
        {
            CChannel[12] = CChannel13.Checked;
        }

        private void CChannel14_CheckedChanged(object sender, EventArgs e)
        {
            CChannel[13] = CChannel14.Checked;
        }

        private void CChannel15_CheckedChanged(object sender, EventArgs e)
        {
            CChannel[14] = CChannel15.Checked;
        }

        private void CChannel16_CheckedChanged(object sender, EventArgs e)
        {
            CChannel[15] = CChannel16.Checked;
        }

        public class BackGroundWorkerTask
        {
            public int Type {  get; set; }
            public string Format { get; set; }

            public BackGroundWorkerTask(int type, string format)
            {
                Type = type;
                Format = format;
            }
        }

        public class BackGroundWorkerResult
        {
            public BackGroundWorkerResult(int type, int percentprogress, int result)
            {
                Type = type;
                Percentprogress = percentprogress;
                Result = result;
            }

            public int Type { get; set; }
            public int Percentprogress { get; set; }
            public int Result { get; set; }


        }

        void UpdateChartSample(BlockData<Adc_Interface> blockdata)
        {

            int index = 1;
            foreach (List<Adc_Interface> item in blockdata)
            {
                if (ReturnChartChooseChannel() == index)
                {
                    ChartSample.Series.Clear();
                    int y = 0;
                    ChartSample.Series.Add(string.Format("Chanell {0}-{1}", 1, y));
                    ChartSample.Series.Last().ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                    ResizedChartSample();
                    if (item.Count() == 0)
                    {
                        ChartSample.Series[0].Points.AddXY(1, 1);
                        continue;
                    }
                    foreach (Adc_Interface adc_Interface in item)
                    {
                        int x = 0;
                        foreach (int sample in adc_Interface.bufsamples)
                        {
                            ChartSample.Series[0].Points.AddXY(12.5 * x, sample);
                            if (sample < MinY)
                            {
                                MinY = (int)(sample * 1.2);
                                ResizedChartSample();
                            }
                            if (sample > MaxY)
                            {
                                MaxY = (int)(sample * 1.2);
                                ResizedChartSample();
                            }
                            x++;
                        }
                        if (CheckBoxAxisXAutoSize.Checked)
                        {
                            NumerMaximumX.Value = (int)ChartSample.ChartAreas[0].AxisX.Maximum;
                            NumerMinimumX.Value = (int)ChartSample.ChartAreas[0].AxisX.Minimum;
                        }
                        if (CheckBoxAxisYAutoSize.Checked)
                        {
                            NumerMaximumY.Value = (int)ChartSample.ChartAreas[0].AxisY.Maximum;
                            NumerMinimumY.Value = (int)ChartSample.ChartAreas[0].AxisY.Minimum;
                        }
                        y++;
                    }
                }
                index++;
            }
        }

        void UpdateChartSample (int[] sample)
        {
            ChartSample.Series.Clear();
            int y = 0;
            ChartSample.Series.Add(string.Format("Chanell {0}-{1}", 1, y));
            ChartSample.Series.Last().ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            int x = 0;
            foreach (int i in sample)
            {
                ChartSample.Series[0].Points.AddXY(12.5 * x, i);
                if (i < MinY)
                {
                    MinY = (int)(i * 1.2);
                    ResizedChartSample();
                }
                if (i > MaxY)
                {
                    MaxY = (int)(i * 1.2);
                    ResizedChartSample();
                }
                x++;
            }
            if (CheckBoxAxisXAutoSize.Checked)
            {
                NumerMaximumX.Value = (int)ChartSample.ChartAreas[0].AxisX.Maximum;
                NumerMinimumX.Value = (int)ChartSample.ChartAreas[0].AxisX.Minimum;
            }
            if (CheckBoxAxisYAutoSize.Checked)
            {
                NumerMaximumY.Value = (int)ChartSample.ChartAreas[0].AxisY.Maximum;
                NumerMinimumY.Value = (int)ChartSample.ChartAreas[0].AxisY.Minimum;
            }
        }

        void ResizedChartSample()
        {
            if (!CheckBoxAxisYAutoSize.Checked) return;
            //ChartSample.ChartAreas[0].AxisX.Maximum = MaxX;
            //ChartSample.ChartAreas[0].AxisX.Minimum = MinX;
            ChartSample.ChartAreas[0].AxisY.Maximum = MaxY;
            ChartSample.ChartAreas[0].AxisY.Minimum = MinY;
        }

        void ChangedStateRadioButtonChannel (bool[] channel)
        {
            bool findFirst = false;
            if (channel.Count() != 16) throw new InvalidOperationException();
            for (int i = 0; i < 16; i++) 
            {
                switch (i)
                {
                    case 0: { radioButtonChannel1.Enabled = channel[i]; break; }
                    case 1: { radioButtonChannel2.Enabled = channel[i]; break; }
                    case 2: { radioButtonChannel3.Enabled = channel[i]; break; }
                    case 3: { radioButtonChannel4.Enabled = channel[i]; break; }
                    case 4: { radioButtonChannel5.Enabled = channel[i]; break; }
                    case 5: { radioButtonChannel6.Enabled = channel[i]; break; }
                    case 6: { radioButtonChannel7.Enabled = channel[i]; break; }
                    case 7: { radioButtonChannel8.Enabled = channel[i]; break; }
                    case 8: { radioButtonChannel9.Enabled = channel[i]; break; }
                    case 9: { radioButtonChannel10.Enabled = channel[i]; break; }
                    case 10: { radioButtonChannel11.Enabled = channel[i]; break; }
                    case 11: { radioButtonChannel12.Enabled = channel[i]; break; }
                    case 12: { radioButtonChannel13.Enabled = channel[i]; break; }
                    case 13: { radioButtonChannel14.Enabled = channel[i]; break; }
                    case 14: { radioButtonChannel15.Enabled = channel[i]; break; }
                    case 15: { radioButtonChannel16.Enabled = channel[i]; break; }
                }
                if (!findFirst && channel[i])
                {
                    findFirst = true;
                    switch (i)
                    {
                        case 0: { radioButtonChannel1.Checked = true; break; }
                        case 1: { radioButtonChannel2.Checked = true; break; }
                        case 2: { radioButtonChannel3.Checked = true; break; }
                        case 3: { radioButtonChannel4.Checked = true; break; }
                        case 4: { radioButtonChannel5.Checked = true; break; }
                        case 5: { radioButtonChannel6.Checked = true; break; }
                        case 6: { radioButtonChannel7.Checked = true; break; }
                        case 7: { radioButtonChannel8.Checked = true; break; }
                        case 8: { radioButtonChannel9.Checked = true; break; }
                        case 9: { radioButtonChannel10.Checked = true; break; }
                        case 10: { radioButtonChannel11.Checked = true; break; }
                        case 11: { radioButtonChannel12.Checked = true; break; }
                        case 12: { radioButtonChannel13.Checked = true; break; }
                        case 13: { radioButtonChannel14.Checked = true; break; }
                        case 14: { radioButtonChannel15.Checked = true; break; }
                        case 15: { radioButtonChannel16.Checked = true; break; }
                    }
                }
            }
        }

        private void ReadConfigFile_Click(object sender, EventArgs e)
        {
            TQDC2Configs.ReadConfigFile();
        }

        private void Inl_config_CheckedChanged(object sender, EventArgs e)
        {
            inl_config = Inl_config.Checked;
        }

        public int ReturnChartChooseChannel()
        {
            if (radioButtonChannel1.Checked) return 1;
            if (radioButtonChannel2.Checked) return 2;
            if (radioButtonChannel3.Checked) return 3;
            if (radioButtonChannel4.Checked) return 4;
            if (radioButtonChannel5.Checked) return 5;
            if (radioButtonChannel6.Checked) return 6;
            if (radioButtonChannel7.Checked) return 7;
            if (radioButtonChannel8.Checked) return 8;
            if (radioButtonChannel9.Checked) return 9;
            if (radioButtonChannel10.Checked) return 10;
            if (radioButtonChannel11.Checked) return 11;
            if (radioButtonChannel12.Checked) return 12;
            if (radioButtonChannel13.Checked) return 13;
            if (radioButtonChannel14.Checked) return 14;
            if (radioButtonChannel15.Checked) return 15;
            if (radioButtonChannel16.Checked) return 16;
            return -1;
        }

        internal static bool IsNeedChannel(int i,bool[] channel) //метод проверки в надобности канала
        {
            return channel[i - 1];
        }

        private void RadioButtonChannel1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonChannel1.Checked)
            {
                labelChannel.Text = "Channel 1";
                if (modeBGW == MANUALCONFIGBASELINE)
                {
                    manualChoosenChannel = 1;
                    numericUpDownManualBaseline.Value = (int)manualBaseline[manualChoosenChannel - 1];
                    UpdateChartSample(TQDC2Configs.StartManualConfigBaseline(manualEventNum, manualChoosenChannel, result.Format, manualBaseline));
                }
            }
        }

        private void RadioButtonChannel2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonChannel2.Checked)
            {
                labelChannel.Text = "Channel 2";
                if (modeBGW == MANUALCONFIGBASELINE)
                {
                    manualChoosenChannel = 2;
                    numericUpDownManualBaseline.Value = (int)manualBaseline[manualChoosenChannel - 1];
                    UpdateChartSample(TQDC2Configs.StartManualConfigBaseline(manualEventNum, manualChoosenChannel, result.Format, manualBaseline));
                }
            }
        }

        private void RadioButtonChannel3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonChannel3.Checked)
            {
                labelChannel.Text = "Channel 3";
                if (modeBGW == MANUALCONFIGBASELINE)
                {
                    manualChoosenChannel = 3;
                    numericUpDownManualBaseline.Value = (int)manualBaseline[manualChoosenChannel - 1];
                    UpdateChartSample(TQDC2Configs.StartManualConfigBaseline(manualEventNum, manualChoosenChannel, result.Format, manualBaseline));
                }
            }
        }

        private void RadioButtonChannel4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonChannel4.Checked)
            {
                labelChannel.Text = "Channel 4";
                if (modeBGW == MANUALCONFIGBASELINE)
                {
                    manualChoosenChannel = 4;
                    numericUpDownManualBaseline.Value = (int)manualBaseline[manualChoosenChannel - 1];
                    UpdateChartSample(TQDC2Configs.StartManualConfigBaseline(manualEventNum, manualChoosenChannel, result.Format, manualBaseline));
                }
            }
        }

        private void RadioButtonChannel5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonChannel5.Checked)
            {
                labelChannel.Text = "Channel 5";
                if (modeBGW == MANUALCONFIGBASELINE)
                {
                    manualChoosenChannel = 5;
                    numericUpDownManualBaseline.Value = (int)manualBaseline[manualChoosenChannel - 1];
                    UpdateChartSample(TQDC2Configs.StartManualConfigBaseline(manualEventNum, manualChoosenChannel, result.Format, manualBaseline));
                }
            }
        }

        private void RadioButtonChannel6_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonChannel6.Checked)
            {
                labelChannel.Text = "Channel 6";
                if (modeBGW == MANUALCONFIGBASELINE)
                {
                    manualChoosenChannel = 6;
                    numericUpDownManualBaseline.Value = (int)manualBaseline[manualChoosenChannel - 1];
                    UpdateChartSample(TQDC2Configs.StartManualConfigBaseline(manualEventNum, manualChoosenChannel, result.Format, manualBaseline));
                }
            }
        }

        private void RadioButtonChannel7_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonChannel7.Checked)
            {
                labelChannel.Text = "Channel 7";
                if (modeBGW == MANUALCONFIGBASELINE)
                {
                    manualChoosenChannel = 7;
                    numericUpDownManualBaseline.Value = (int)manualBaseline[manualChoosenChannel - 1];
                    UpdateChartSample(TQDC2Configs.StartManualConfigBaseline(manualEventNum, manualChoosenChannel, result.Format, manualBaseline));
                }
            }
        }

        private void RadioButtonChannel8_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonChannel8.Checked)
            {
                labelChannel.Text = "Channel 8";
                if (modeBGW == MANUALCONFIGBASELINE)
                {
                    manualChoosenChannel = 8;
                    numericUpDownManualBaseline.Value = (int)manualBaseline[manualChoosenChannel - 1];
                    UpdateChartSample(TQDC2Configs.StartManualConfigBaseline(manualEventNum, manualChoosenChannel, result.Format, manualBaseline));
                }
            }
        }

        private void RadioButtonChannel9_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonChannel9.Checked)
            {
                labelChannel.Text = "Channel 9";
                if (modeBGW == MANUALCONFIGBASELINE)
                {
                    manualChoosenChannel = 9;
                    numericUpDownManualBaseline.Value = (int)manualBaseline[manualChoosenChannel - 1];
                    UpdateChartSample(TQDC2Configs.StartManualConfigBaseline(manualEventNum, manualChoosenChannel, result.Format, manualBaseline));
                }
            }
        }

        private void RadioButtonChannel10_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonChannel10.Checked)
            {
                labelChannel.Text = "Channel 10";
                if (modeBGW == MANUALCONFIGBASELINE)
                {
                    manualChoosenChannel = 10;
                    numericUpDownManualBaseline.Value = (int)manualBaseline[manualChoosenChannel - 1];
                    UpdateChartSample(TQDC2Configs.StartManualConfigBaseline(manualEventNum, manualChoosenChannel, result.Format, manualBaseline));
                }
            }
        }

        private void RadioButtonChannel11_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonChannel11.Checked)
            {
                labelChannel.Text = "Channel 11";
                if (modeBGW == MANUALCONFIGBASELINE)
                {
                    manualChoosenChannel = 11;
                    numericUpDownManualBaseline.Value = (int)manualBaseline[manualChoosenChannel - 1];
                    UpdateChartSample(TQDC2Configs.StartManualConfigBaseline(manualEventNum, manualChoosenChannel, result.Format, manualBaseline));
                }
            }
        }

        private void RadioButtonChannel12_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonChannel12.Checked)
            {
                labelChannel.Text = "Channel 12";
                if (modeBGW == MANUALCONFIGBASELINE)
                {
                    manualChoosenChannel = 12;
                    numericUpDownManualBaseline.Value = (int)manualBaseline[manualChoosenChannel - 1];
                    UpdateChartSample(TQDC2Configs.StartManualConfigBaseline(manualEventNum, manualChoosenChannel, result.Format, manualBaseline));
                }
            }
        }

        private void RadioButtonChannel13_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonChannel13.Checked)
            {
                labelChannel.Text = "Channel 13";
                if (modeBGW == MANUALCONFIGBASELINE)
                {
                    manualChoosenChannel = 13;
                    numericUpDownManualBaseline.Value = (int)manualBaseline[manualChoosenChannel - 1];
                    UpdateChartSample(TQDC2Configs.StartManualConfigBaseline(manualEventNum, manualChoosenChannel, result.Format, manualBaseline));
                }
            }
        }

        private void RadioButtonChannel14_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonChannel14.Checked)
            {
                labelChannel.Text = "Channel 14";
                if (modeBGW == MANUALCONFIGBASELINE)
                {
                    manualChoosenChannel = 14;
                    numericUpDownManualBaseline.Value = (int)manualBaseline[manualChoosenChannel - 1];
                    UpdateChartSample(TQDC2Configs.StartManualConfigBaseline(manualEventNum, manualChoosenChannel, result.Format, manualBaseline));
                }
            }
        }

        private void RadioButtonChannel15_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonChannel15.Checked)
            {
                labelChannel.Text = "Channel 15";
                if (modeBGW == MANUALCONFIGBASELINE)
                {
                    manualChoosenChannel = 15;
                    numericUpDownManualBaseline.Value = (int)manualBaseline[manualChoosenChannel - 1];
                    UpdateChartSample(TQDC2Configs.StartManualConfigBaseline(manualEventNum, manualChoosenChannel, result.Format, manualBaseline));
                }
            }
        }

        private void RadioButtonChannel16_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonChannel16.Checked)
            {
                labelChannel.Text = "Channel 16";
                if (modeBGW == MANUALCONFIGBASELINE)
                {
                    manualChoosenChannel = 16;
                    numericUpDownManualBaseline.Value = (int)manualBaseline[manualChoosenChannel - 1];
                    UpdateChartSample(TQDC2Configs.StartManualConfigBaseline(manualEventNum, manualChoosenChannel, result.Format,manualBaseline));
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (BackGrWorkProgressBar.IsBusy)
            {
                BackGrWorkProgressBar.CancelAsync();
                while (!iscanclose)
                {

                }
            }
        }

        private void CheckBoxAxisYAutoSize_CheckedChanged(object sender, EventArgs e)
        {
            NumerMaximumY.Enabled = !CheckBoxAxisYAutoSize.Checked;
            NumerMinimumY.Enabled = !CheckBoxAxisYAutoSize.Checked;
            if (CheckBoxAxisYAutoSize.Checked)
            {
                ChartSample.ChartAreas[0].AxisY.Maximum = double.NaN;
                ChartSample.ChartAreas[0].AxisY.Minimum = double.NaN;
                ChartSample.ChartAreas[0].RecalculateAxesScale();
            }
        }
        
        private void CheckBoxAxisXAutoSize_CheckedChanged(object sender, EventArgs e)
        {
            NumerMaximumX.Enabled = !CheckBoxAxisXAutoSize.Checked;
            NumerMinimumX.Enabled = !CheckBoxAxisXAutoSize.Checked;
            if (CheckBoxAxisXAutoSize.Checked)
            {
                ChartSample.ChartAreas[0].AxisX.Maximum = double.NaN;
                ChartSample.ChartAreas[0].AxisX.Minimum = double.NaN;
                ChartSample.ChartAreas[0].RecalculateAxesScale();
            }

        }

        private void NumerMinimumY_ValueChanged(object sender, EventArgs e)
        {
            if (!CheckBoxAxisYAutoSize.Checked)
            {
                ChartSample.ChartAreas[0].AxisY.Minimum = (double)NumerMinimumY.Value;
            }
        }

        private void NumerMinimumX_ValueChanged(object sender, EventArgs e)
        {
            if (!CheckBoxAxisXAutoSize.Checked)
            {
                ChartSample.ChartAreas[0].AxisX.Minimum = (double)NumerMinimumX.Value;
            }
        }

        private void NumerMaximumY_ValueChanged(object sender, EventArgs e)
        {
            if (!CheckBoxAxisYAutoSize.Checked)
            {
                ChartSample.ChartAreas[0].AxisY.Maximum = (double)NumerMaximumY.Value;
            }
        }

        private void NumerMaximumX_ValueChanged(object sender, EventArgs e)
        {
            if (!CheckBoxAxisXAutoSize.Checked)
            {
                ChartSample.ChartAreas[0].AxisX.Maximum = (double)NumerMaximumX.Value;
            }
        }

        private void Progress_Click(object sender, EventArgs e)
        {

        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            if (BackGrWorkProgressBar.IsBusy)
            {
                if ( modeBGW == ANALYS)
                {
                    IDText.Text = "";
                    SerialText.Text = "";
                }
                BackGrWorkProgressBar.CancelAsync();
            }
        }


        private void StepChart_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Label5_Click_1(object sender, EventArgs e)
        {

        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void RadioButtonStandartConfigBaseLine_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonStandartConfigBaseLine.Checked)
            {
                ModeBaseline = 1;
            }
        }

        private void RadioButtonReadConfigFileBaseLine_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonReadConfigFileBaseLine.Checked)
            {
                ModeBaseline = 2;
                ReadConfigFile.Enabled = true;
            }

            if (!radioButtonReadConfigFileBaseLine.Checked)
            {
                ReadConfigFile.Enabled = false;
            }
        }

        private void RadioButtonManualConfigBaseLine_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonManualConfigBaseLine.Checked)
            {
                ModeBaseline = 3;
                panelManualConfig.Enabled = true;
                //BackGrWorkProgressBar.RunWorkerAsync(new BackGroundWorkerTask(MANUALCONFIGBASELINE, result.Format));
                PanelRadioButtonChart.Enabled = true;
                modeBGW = MANUALCONFIGBASELINE;
                ChangedStateRadioButtonChannel(AnalysisFile.Channel);
                StepChart.Maximum = AnalysisFile.NumEv / stepchartdivmax;
                StepChart.Minimum = AnalysisFile.NumEv / stepchartdivmin;
                PanelConfigChart.Enabled = true;
                //UpdateChartSample(TQDC2Configs.StartManualConfigBaseline(manualEventNum,manualChoosenChannel,result.Format));
            }
            if (!radioButtonManualConfigBaseLine.Checked)
            {
                panelManualConfig.Enabled = false;
                PanelConfigChart.Enabled = false;
                modeBGW = 0;
            }
        }

        private void EditBaseline ()
        {

        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            manualBaseline[manualChoosenChannel - 1] = (int)numericUpDownManualBaseline.Value;
            UpdateChartSample(TQDC2Configs.StartManualConfigBaseline(manualEventNum, manualChoosenChannel, result.Format, manualBaseline));
        }

        private void buttonManualConfigNext_Click(object sender, EventArgs e)
        {
            if (manualEventNum[manualChoosenChannel - 1] == AnalysisFile.NumEv)
                manualEventNum[manualChoosenChannel - 1] = 0;
            else
                manualEventNum[manualChoosenChannel - 1]++;
            UpdateChartSample(TQDC2Configs.StartManualConfigBaseline(manualEventNum, manualChoosenChannel, result.Format, manualBaseline));
        }

        private void buttonManualConfigPrev_Click(object sender, EventArgs e)
        {
            if (manualEventNum[manualChoosenChannel - 1] == 0)
                manualEventNum[manualChoosenChannel - 1] = AnalysisFile.NumEv;
            else
                manualEventNum[manualChoosenChannel - 1]--;
            UpdateChartSample(TQDC2Configs.StartManualConfigBaseline(manualEventNum, manualChoosenChannel, result.Format, manualBaseline));
        }

        private void PanelConfigChart_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
