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

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DChannel = new bool[16];
            CChannel = new bool[16];
            isAnalysis = false;
            inl_config = false;
            ChartSample.Series.Add("Null");
            ChartSample.Series[0].Points.AddXY(0, 0);
            //ChartSample.ChartAreas[0].AxisX.Maximum = 5;
            //ChartSample.ChartAreas[0].AxisY.Maximum = 1;
        }
        public static bool[] DChannel;
        public static bool[] CChannel;
        public static bool isAnalysis;
        readonly Color CheckBoxChColor = SystemColors.ButtonShadow;
        TQDC2File.OpenResult result;
        public static bool inl_config;
        private int MinY = 0;
        private int MaxY = 0;
        private int MinX = 0;
        private int MaxX = 0;

        
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

        private void StartAnalys_Click(object sender, EventArgs e)
        {
            if (BackGrWorkProgressBar.IsBusy != true)
            {
                BackGrWorkProgressBar.RunWorkerAsync(new BackGroundWorkerTask(2, result.Format));
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void StartWrite_Click(object sender, EventArgs e)
        {
            if (BackGrWorkProgressBar.IsBusy != true)
            {
                BackGrWorkProgressBar.RunWorkerAsync(new BackGroundWorkerTask(3, result.Format));
                ChangedStateRadioButtonChannel(CChannel);
                OpenFileBtn.Enabled = false;
                StartDecoder.Enabled = false;
                StartWrite.Enabled = false;
                Progress.Maximum = AnalysisFile.NumEv;
            }
            //ChartSample.ChartAreas[0].AxisX.MaximumAutoSize = 1;
        }

        private void StartDecoder_Click(object sender, EventArgs e)
        {
            if (BackGrWorkProgressBar.IsBusy != true)
            {
                ChangedStateRadioButtonChannel(DChannel);
                BackGrWorkProgressBar.RunWorkerAsync(new BackGroundWorkerTask(1, result.Format));
                OpenFileBtn.Enabled = false;
                StartDecoder.Enabled = false;
                StartWrite.Enabled = false;
                Progress.Maximum = AnalysisFile.NumEv;
            }
            //ChartSample.ChartAreas[0].AxisX.MaximumAutoSize = 1;
        }

        private void BackGrWorkProgressBar_DoWork(object sender, DoWorkEventArgs e)
        {
            BackGroundWorkerTask Task;
            if (e.Argument is BackGroundWorkerTask)
            {
                Task = e.Argument as BackGroundWorkerTask;
                
            }
            else { throw new InvalidOperationException(); }
            switch (Task.Type)
            {
                case 1:
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
                case 2:
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
                case 3:
                    {
                        switch (Task.Format)
                        {
                            case "txt":
                                {
                                    Calculation.StartCalcText(BackGrWorkProgressBar, AnalysisFile.Channel, e);
                                    break;
                                }
                            case "dat":
                                {
                                    Calculation.StartCalcBinary(BackGrWorkProgressBar, AnalysisFile.Channel, e);
                                    break;
                                }
                        }
                        break;
                    }
                default:
                    {
                        MessageBox.Show(Task.Type.ToString(), "Ошибка", MessageBoxButtons.RetryCancel);
                        break;
                    }
            }
        }

        private void BackGrWorkProgressBar_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            BlockData<Adc_Interface> adcdata;
            Progress.Value = (e.ProgressPercentage);
            if (e.UserState is BlockData<Adc_Interface> && e.UserState != null)
            {
                adcdata = (BlockData<Adc_Interface>)e.UserState;
                UpdateChartSample(adcdata);
                //UpdateChartSample(adcdata);
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
                            ChartSample.Series.Clear();
                            ChartSample.Series.Add("Null");
                            ChartSample.Series[0].Points.AddXY(1, 0);
                            break;
                        }
                }
            }
            Progress.Value = 0;
            SystemSounds.Exclamation.Play();
            OpenFileBtn.Enabled = true;
            StartDecoder.Enabled = true;
            StartWrite.Enabled = true;
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
                    BackGrWorkProgressBar.RunWorkerAsync(new BackGroundWorkerTask(2,result.Format));
                }
                IDText.Text = result.ID;
                SerialText.Text = result.Serial;
            }

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

        public const int OK = 0x01;
        public const int CANCEL = 0x02;
        public const int ERROR = 0x03;
        public const int ANALYS = 0x04;
        public const int DECODER = 0x05;
        public const int CALCULATION = 0x06;
        public const int GRAPHIC = 0x10;


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
                        y++;
                    }
                }
                index++;
            }
        }

        void ResizedChartSample()
        {
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
            labelChannel.Text = "Channel 1";
        }

        private void RadioButtonChannel2_CheckedChanged(object sender, EventArgs e)
        {
            labelChannel.Text = "Channel 2";
        }

        private void RadioButtonChannel3_CheckedChanged(object sender, EventArgs e)
        {
            labelChannel.Text = "Channel 3";
        }

        private void RadioButtonChannel4_CheckedChanged(object sender, EventArgs e)
        {
            labelChannel.Text = "Channel 4";
        }

        private void RadioButtonChannel5_CheckedChanged(object sender, EventArgs e)
        {
            labelChannel.Text = "Channel 5";
        }

        private void RadioButtonChannel6_CheckedChanged(object sender, EventArgs e)
        {
            labelChannel.Text = "Channel 6";
        }

        private void RadioButtonChannel7_CheckedChanged(object sender, EventArgs e)
        {
            labelChannel.Text = "Channel 7";
        }

        private void RadioButtonChannel8_CheckedChanged(object sender, EventArgs e)
        {
            labelChannel.Text = "Channel 8";
        }

        private void RadioButtonChannel9_CheckedChanged(object sender, EventArgs e)
        {
            labelChannel.Text = "Channel 9";
        }

        private void RadioButtonChannel10_CheckedChanged(object sender, EventArgs e)
        {
            labelChannel.Text = "Channel 10";
        }

        private void RadioButtonChannel11_CheckedChanged(object sender, EventArgs e)
        {
            labelChannel.Text = "Channel 11";
        }

        private void RadioButtonChannel12_CheckedChanged(object sender, EventArgs e)
        {
            labelChannel.Text = "Channel 12";
        }

        private void RadioButtonChannel13_CheckedChanged(object sender, EventArgs e)
        {
            labelChannel.Text = "Channel 13";
        }

        private void RadioButtonChannel14_CheckedChanged(object sender, EventArgs e)
        {
            labelChannel.Text = "Channel 14";
        }

        private void RadioButtonChannel15_CheckedChanged(object sender, EventArgs e)
        {
            labelChannel.Text = "Channel 15";
        }

        private void RadioButtonChannel16_CheckedChanged(object sender, EventArgs e)
        {
            labelChannel.Text = "Channel 16";
        }
    }
}
