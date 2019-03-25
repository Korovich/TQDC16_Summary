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

namespace TQDC16_Summary_Rev_1
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ConfiguireWindow.TabPages.Remove(OutInfoFile);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            TQDC2File.OpenResult result = TQDC2File.Open_File();
            if (result.Selected)
            {
                ConReadFile.BackColor = System.Drawing.Color.White;
                ConSaveFile.BackColor = System.Drawing.Color.White;
                ConfiguireWindow.Enabled = true;
                IDText.Text = result.ID;
                SerialText.Text = result.Serial;
            }

        }
        public static bool[] Channel = new bool[16];
        public static bool isAnalysis = false;
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
            int Type = 2;
            if (BackGrWorkProgressBar.IsBusy != true)
                BackGrWorkProgressBar.RunWorkerAsync(Type);
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void StartWrite_Click(object sender, EventArgs e)
        {
            int Type = 3;
            if (BackGrWorkProgressBar.IsBusy != true)
                BackGrWorkProgressBar.RunWorkerAsync(Type);
        }

        private void BackGrWorkProgressBar_DoWork(object sender, DoWorkEventArgs e)
        {
            var Type = e.Argument;
            switch (Type)
            {
                case 1:
                    {
                        Decoder.StartDecoding(BackGrWorkProgressBar,e);
                        break;
                    }
                case 2:
                    {
                        AnalysisFile.AnChannel(BackGrWorkProgressBar, e);
                        break;
                    }
                case 3:
                    {
                        OutSummaryFile.StartSummary(BackGrWorkProgressBar);
                        break;
                    }
                default:
                    {
                        MessageBox.Show(Type.ToString(), "Ошибка", MessageBoxButtons.RetryCancel);
                        break;
                    }
            }
        }

        private void BackGrWorkProgressBar_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Progress.Value += 1;
        }

        private void BackGrWorkProgressBar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var Type = e.Result;
            switch (Type)
            {
                case 1:
                    {
                        break;
                    }
                case 2:
                    {
                        isAnalysis = true;
                        NumEvText.Text = AnalysisFile.NumEv.ToString();
                        NumUsCh.Text = AnalysisFile.NumCh.ToString();
                        ConfiguireWindow.TabPages.Add(OutInfoFile);
                        panelAnalysis.Visible = false;
                        for (int i =0;i<16;i++)
                        {
                            switch(i)
                            {
                                case 0:
                                    {
                                        if (AnalysisFile.Channel[i]) { Channel1.Checked = true; }
                                        else { Channel1.BackColor = Color.DimGray; Channel1.Enabled = false; }
                                        break;
                                    }
                                case 1:
                                    {
                                        if (AnalysisFile.Channel[i]) { Channel2.Checked = true; }
                                        else { Channel2.BackColor = Color.DimGray; Channel2.Enabled = false; }
                                        break;
                                    }
                                case 2:
                                    {
                                        if (AnalysisFile.Channel[i]) { Channel3.Checked = true; }
                                        else { Channel3.BackColor = Color.DimGray; Channel3.Enabled = false; }
                                        break;
                                    }
                                case 3:
                                    {
                                        if (AnalysisFile.Channel[i]) { Channel4.Checked = true; }
                                        else { Channel4.BackColor = Color.DimGray; Channel4.Enabled = false; }
                                        break;
                                    }
                                case 4:
                                    {
                                        if (AnalysisFile.Channel[i]) { Channel5.Checked = true; }
                                        else { Channel5.BackColor = Color.DimGray; Channel5.Enabled = false; }
                                        break;
                                    }
                                case 5:
                                    {
                                        if (AnalysisFile.Channel[i]) { Channel6.Checked = true; }
                                        else { Channel6.BackColor = Color.DimGray; Channel6.Enabled = false; }
                                        break;
                                    }
                                case 6:
                                    {
                                        if (AnalysisFile.Channel[i]) { Channel7.Checked = true; }
                                        else { Channel7.BackColor = Color.DimGray; Channel7.Enabled = false; }
                                        break;
                                    }
                                case 7:
                                    {
                                        if (AnalysisFile.Channel[i]) { Channel8.Checked = true; }
                                        else { Channel8.BackColor = Color.DimGray; Channel8.Enabled = false; }
                                        break;
                                    }
                                case 8:
                                    {
                                        if (AnalysisFile.Channel[i]) { Channel9.Checked = true; }
                                        else { Channel9.BackColor = Color.DimGray; Channel9.Enabled = false; }
                                        break;
                                    }
                                case 9:
                                    {
                                        if (AnalysisFile.Channel[i]) { Channel10.Checked = true; }
                                        else { Channel10.BackColor = Color.DimGray; Channel10.Enabled = false; }
                                        break;
                                    }
                                case 10:
                                    {
                                        if (AnalysisFile.Channel[i]) { Channel11.Checked = true; }
                                        else { Channel11.BackColor = Color.DimGray; Channel11.Enabled = false; }
                                        break;
                                    }
                                case 11:
                                    {
                                        if (AnalysisFile.Channel[i]) { Channel12.Checked = true; }
                                        else { Channel12.BackColor = Color.DimGray; Channel12.Enabled = false; }
                                        break;
                                    }
                                case 12:
                                    {
                                        if (AnalysisFile.Channel[i]) { Channel13.Checked = true; }
                                        else { Channel13.BackColor = Color.DimGray; Channel13.Enabled = false; }
                                        break;
                                    }
                                case 13:
                                    {
                                        if (AnalysisFile.Channel[i]) { Channel14.Checked = true; }
                                        else { Channel14.BackColor = Color.DimGray; Channel14.Enabled = false; }
                                        break;
                                    }
                                case 14:
                                    {
                                        if (AnalysisFile.Channel[i]) { Channel15.Checked = true; }
                                        else { Channel15.BackColor = Color.DimGray; Channel15.Enabled = false; }
                                        break;
                                    }
                                case 15:
                                    {
                                        if (AnalysisFile.Channel[i]) { Channel16.Checked = true; }
                                        else { Channel16.BackColor = Color.DimGray; Channel16.Enabled = false; }
                                        break;
                                    }
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        break;
                    }
            }
            Progress.Value = 0;
            SystemSounds.Exclamation.Play();
        }

        private void StartDecoder_Click(object sender, EventArgs e)
        {
            int Type = 1;
            if (BackGrWorkProgressBar.IsBusy != true)
                BackGrWorkProgressBar.RunWorkerAsync(Type);
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }


        private void Channel1_CheckedChanged(object sender, EventArgs e)
        {
            Channel[0] = Channel1.Checked;
        }

        private void Channel2_CheckedChanged(object sender, EventArgs e)
        {
            Channel[1] = Channel2.Checked;
        }

        private void Channel3_CheckedChanged(object sender, EventArgs e)
        {
            Channel[2] = Channel3.Checked;
        }

        private void Channel4_CheckedChanged(object sender, EventArgs e)
        {
            Channel[3] = Channel4.Checked;
        }

        private void Channel5_CheckedChanged(object sender, EventArgs e)
        {
            Channel[4] = Channel5.Checked;
        }

        private void Channel7_CheckedChanged(object sender, EventArgs e)
        {
            Channel[6] = Channel7.Checked;
        }

        private void Channel6_CheckedChanged(object sender, EventArgs e)
        {
            Channel[5] = Channel6.Checked;
        }

        private void Channel8_CheckedChanged(object sender, EventArgs e)
        {
            Channel[7] = Channel8.Checked;
        }

        private void Channel10_CheckedChanged(object sender, EventArgs e)
        {
            Channel[9] = Channel10.Checked;
        }

        private void Channel9_CheckedChanged(object sender, EventArgs e)
        {
            Channel[8] = Channel9.Checked;
        }

        private void Channel11_CheckedChanged(object sender, EventArgs e)
        {
            Channel[10] = Channel11.Checked;
        }

        private void Channel12_CheckedChanged(object sender, EventArgs e)
        {
            Channel[11] = Channel12.Checked;
        }

        private void Channel13_CheckedChanged(object sender, EventArgs e)
        {
            Channel[12] = Channel13.Checked;
        }

        private void Channel14_CheckedChanged(object sender, EventArgs e)
        {
            Channel[13] = Channel14.Checked;
        }

        private void Channel15_CheckedChanged(object sender, EventArgs e)
        {
            Channel[14] = Channel15.Checked;
        }

        private void Channel16_CheckedChanged(object sender, EventArgs e)
        {
            Channel[15] = Channel16.Checked;
        }
    }
}
