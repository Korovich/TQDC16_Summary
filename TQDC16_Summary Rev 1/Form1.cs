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
            TQDC2File.Open_File();
            if(TQDC2File.Sel)
            {
                ConReadFile.BackColor = System.Drawing.Color.White;
                ConSaveFile.BackColor = System.Drawing.Color.White;
                ConfiguireWindow.Enabled = true;
            }
        }
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
                        AnalysisFile.AnChannel(BackGrWorkProgressBar);
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
                        NumEvText.Text = AnalysisFile.NumEv.ToString();
                        NumUsCh.Text = AnalysisFile.NumCh.ToString();
                        ConfiguireWindow.TabPages.Add(OutInfoFile);
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
    }
}
