namespace TQDC16_Summary_Rev_1
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.ConfiguireWindow = new System.Windows.Forms.TabControl();
            this.ConReadFile = new System.Windows.Forms.TabPage();
            this.IDText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SerialText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ConSaveFile = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.OutInfoFile = new System.Windows.Forms.TabPage();
            this.NumEvText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.NumUsCh = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panelAnalysis = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.StartAnalys = new System.Windows.Forms.Button();
            this.OpenPanel = new System.Windows.Forms.Panel();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.StartDecoder = new System.Windows.Forms.Button();
            this.StartWrite = new System.Windows.Forms.Button();
            this.Progress = new System.Windows.Forms.ProgressBar();
            this.BackGrWorkProgressBar = new System.ComponentModel.BackgroundWorker();
            this.Channel1 = new System.Windows.Forms.CheckBox();
            this.Channel2 = new System.Windows.Forms.CheckBox();
            this.Channel3 = new System.Windows.Forms.CheckBox();
            this.Channel4 = new System.Windows.Forms.CheckBox();
            this.Channel5 = new System.Windows.Forms.CheckBox();
            this.Channel6 = new System.Windows.Forms.CheckBox();
            this.Channel7 = new System.Windows.Forms.CheckBox();
            this.Channel8 = new System.Windows.Forms.CheckBox();
            this.Channel9 = new System.Windows.Forms.CheckBox();
            this.Channel10 = new System.Windows.Forms.CheckBox();
            this.Channel11 = new System.Windows.Forms.CheckBox();
            this.Channel12 = new System.Windows.Forms.CheckBox();
            this.Channel13 = new System.Windows.Forms.CheckBox();
            this.Channel14 = new System.Windows.Forms.CheckBox();
            this.Channel15 = new System.Windows.Forms.CheckBox();
            this.Channel16 = new System.Windows.Forms.CheckBox();
            this.ConfiguireWindow.SuspendLayout();
            this.ConReadFile.SuspendLayout();
            this.ConSaveFile.SuspendLayout();
            this.panel2.SuspendLayout();
            this.OutInfoFile.SuspendLayout();
            this.panelAnalysis.SuspendLayout();
            this.OpenPanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(4, 17);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(155, 35);
            this.button1.TabIndex = 0;
            this.button1.Text = "Открыть файл";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ConfiguireWindow
            // 
            this.ConfiguireWindow.Controls.Add(this.ConReadFile);
            this.ConfiguireWindow.Controls.Add(this.ConSaveFile);
            this.ConfiguireWindow.Controls.Add(this.OutInfoFile);
            this.ConfiguireWindow.Enabled = false;
            this.ConfiguireWindow.Location = new System.Drawing.Point(13, 12);
            this.ConfiguireWindow.Name = "ConfiguireWindow";
            this.ConfiguireWindow.SelectedIndex = 0;
            this.ConfiguireWindow.Size = new System.Drawing.Size(355, 428);
            this.ConfiguireWindow.TabIndex = 1;
            // 
            // ConReadFile
            // 
            this.ConReadFile.BackColor = System.Drawing.Color.Silver;
            this.ConReadFile.Controls.Add(this.IDText);
            this.ConReadFile.Controls.Add(this.label2);
            this.ConReadFile.Controls.Add(this.SerialText);
            this.ConReadFile.Controls.Add(this.label1);
            this.ConReadFile.Location = new System.Drawing.Point(4, 25);
            this.ConReadFile.Name = "ConReadFile";
            this.ConReadFile.Padding = new System.Windows.Forms.Padding(3);
            this.ConReadFile.Size = new System.Drawing.Size(347, 399);
            this.ConReadFile.TabIndex = 0;
            this.ConReadFile.Text = "Описание файла";
            // 
            // IDText
            // 
            this.IDText.Location = new System.Drawing.Point(202, 14);
            this.IDText.Name = "IDText";
            this.IDText.ReadOnly = true;
            this.IDText.Size = new System.Drawing.Size(139, 22);
            this.IDText.TabIndex = 3;
            this.IDText.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "ID TQDC";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // SerialText
            // 
            this.SerialText.Location = new System.Drawing.Point(202, 54);
            this.SerialText.Name = "SerialText";
            this.SerialText.ReadOnly = true;
            this.SerialText.Size = new System.Drawing.Size(139, 22);
            this.SerialText.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Серийный номер TQDC";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // ConSaveFile
            // 
            this.ConSaveFile.BackColor = System.Drawing.Color.Silver;
            this.ConSaveFile.Controls.Add(this.panel2);
            this.ConSaveFile.Location = new System.Drawing.Point(4, 25);
            this.ConSaveFile.Name = "ConSaveFile";
            this.ConSaveFile.Padding = new System.Windows.Forms.Padding(3);
            this.ConSaveFile.Size = new System.Drawing.Size(347, 399);
            this.ConSaveFile.TabIndex = 1;
            this.ConSaveFile.Text = "Выходной файл";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panelAnalysis);
            this.panel2.Controls.Add(this.Channel16);
            this.panel2.Controls.Add(this.Channel15);
            this.panel2.Controls.Add(this.Channel14);
            this.panel2.Controls.Add(this.Channel13);
            this.panel2.Controls.Add(this.Channel12);
            this.panel2.Controls.Add(this.Channel11);
            this.panel2.Controls.Add(this.Channel10);
            this.panel2.Controls.Add(this.Channel9);
            this.panel2.Controls.Add(this.Channel8);
            this.panel2.Controls.Add(this.Channel7);
            this.panel2.Controls.Add(this.Channel6);
            this.panel2.Controls.Add(this.Channel5);
            this.panel2.Controls.Add(this.Channel4);
            this.panel2.Controls.Add(this.Channel3);
            this.panel2.Controls.Add(this.Channel2);
            this.panel2.Controls.Add(this.Channel1);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(347, 399);
            this.panel2.TabIndex = 6;
            // 
            // OutInfoFile
            // 
            this.OutInfoFile.BackColor = System.Drawing.Color.White;
            this.OutInfoFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OutInfoFile.Controls.Add(this.NumEvText);
            this.OutInfoFile.Controls.Add(this.label3);
            this.OutInfoFile.Controls.Add(this.NumUsCh);
            this.OutInfoFile.Controls.Add(this.label4);
            this.OutInfoFile.Location = new System.Drawing.Point(4, 25);
            this.OutInfoFile.Name = "OutInfoFile";
            this.OutInfoFile.Padding = new System.Windows.Forms.Padding(3);
            this.OutInfoFile.Size = new System.Drawing.Size(347, 399);
            this.OutInfoFile.TabIndex = 2;
            this.OutInfoFile.Text = "О файле";
            // 
            // NumEvText
            // 
            this.NumEvText.Location = new System.Drawing.Point(200, 6);
            this.NumEvText.Name = "NumEvText";
            this.NumEvText.ReadOnly = true;
            this.NumEvText.Size = new System.Drawing.Size(139, 22);
            this.NumEvText.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Кол.  тригеров";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // NumUsCh
            // 
            this.NumUsCh.Location = new System.Drawing.Point(200, 46);
            this.NumUsCh.Name = "NumUsCh";
            this.NumUsCh.ReadOnly = true;
            this.NumUsCh.Size = new System.Drawing.Size(139, 22);
            this.NumUsCh.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "Кол. каналы";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // panelAnalysis
            // 
            this.panelAnalysis.Controls.Add(this.label5);
            this.panelAnalysis.Controls.Add(this.StartAnalys);
            this.panelAnalysis.Location = new System.Drawing.Point(0, 0);
            this.panelAnalysis.Name = "panelAnalysis";
            this.panelAnalysis.Size = new System.Drawing.Size(347, 399);
            this.panelAnalysis.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(31, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(286, 17);
            this.label5.TabIndex = 5;
            this.label5.Text = "Для начала надо провести анализ файла";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // StartAnalys
            // 
            this.StartAnalys.Location = new System.Drawing.Point(86, 163);
            this.StartAnalys.Name = "StartAnalys";
            this.StartAnalys.Size = new System.Drawing.Size(155, 35);
            this.StartAnalys.TabIndex = 4;
            this.StartAnalys.Text = "Анализ файла";
            this.StartAnalys.UseVisualStyleBackColor = true;
            this.StartAnalys.Click += new System.EventHandler(this.StartAnalys_Click);
            // 
            // OpenPanel
            // 
            this.OpenPanel.Controls.Add(this.button1);
            this.OpenPanel.Location = new System.Drawing.Point(17, 459);
            this.OpenPanel.Name = "OpenPanel";
            this.OpenPanel.Size = new System.Drawing.Size(347, 68);
            this.OpenPanel.TabIndex = 2;
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.StartDecoder);
            this.MainPanel.Controls.Add(this.StartWrite);
            this.MainPanel.Location = new System.Drawing.Point(393, 459);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(784, 67);
            this.MainPanel.TabIndex = 3;
            // 
            // StartDecoder
            // 
            this.StartDecoder.Location = new System.Drawing.Point(3, 17);
            this.StartDecoder.Name = "StartDecoder";
            this.StartDecoder.Size = new System.Drawing.Size(155, 35);
            this.StartDecoder.TabIndex = 6;
            this.StartDecoder.Text = "Декодировать файл";
            this.StartDecoder.UseVisualStyleBackColor = true;
            this.StartDecoder.Click += new System.EventHandler(this.StartDecoder_Click);
            // 
            // StartWrite
            // 
            this.StartWrite.Location = new System.Drawing.Point(626, 17);
            this.StartWrite.Name = "StartWrite";
            this.StartWrite.Size = new System.Drawing.Size(155, 35);
            this.StartWrite.TabIndex = 5;
            this.StartWrite.Text = "Запись файла";
            this.StartWrite.UseVisualStyleBackColor = true;
            this.StartWrite.Click += new System.EventHandler(this.StartWrite_Click);
            // 
            // Progress
            // 
            this.Progress.Location = new System.Drawing.Point(393, 433);
            this.Progress.Maximum = 1000;
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(784, 20);
            this.Progress.TabIndex = 4;
            // 
            // BackGrWorkProgressBar
            // 
            this.BackGrWorkProgressBar.WorkerReportsProgress = true;
            this.BackGrWorkProgressBar.WorkerSupportsCancellation = true;
            this.BackGrWorkProgressBar.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BackGrWorkProgressBar_DoWork);
            this.BackGrWorkProgressBar.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BackGrWorkProgressBar_ProgressChanged);
            this.BackGrWorkProgressBar.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BackGrWorkProgressBar_RunWorkerCompleted);
            // 
            // Channel1
            // 
            this.Channel1.AutoSize = true;
            this.Channel1.Location = new System.Drawing.Point(6, 6);
            this.Channel1.Name = "Channel1";
            this.Channel1.Size = new System.Drawing.Size(83, 21);
            this.Channel1.TabIndex = 0;
            this.Channel1.Text = "1 Канал";
            this.Channel1.UseVisualStyleBackColor = true;
            this.Channel1.CheckedChanged += new System.EventHandler(this.Channel1_CheckedChanged);
            // 
            // Channel2
            // 
            this.Channel2.AutoSize = true;
            this.Channel2.Location = new System.Drawing.Point(6, 24);
            this.Channel2.Name = "Channel2";
            this.Channel2.Size = new System.Drawing.Size(83, 21);
            this.Channel2.TabIndex = 1;
            this.Channel2.Text = "2 Канал";
            this.Channel2.UseVisualStyleBackColor = true;
            this.Channel2.CheckedChanged += new System.EventHandler(this.Channel2_CheckedChanged);
            // 
            // Channel3
            // 
            this.Channel3.AutoSize = true;
            this.Channel3.Location = new System.Drawing.Point(6, 42);
            this.Channel3.Name = "Channel3";
            this.Channel3.Size = new System.Drawing.Size(83, 21);
            this.Channel3.TabIndex = 2;
            this.Channel3.Text = "3 Канал";
            this.Channel3.UseVisualStyleBackColor = true;
            this.Channel3.CheckedChanged += new System.EventHandler(this.Channel3_CheckedChanged);
            // 
            // Channel4
            // 
            this.Channel4.AutoSize = true;
            this.Channel4.Location = new System.Drawing.Point(6, 60);
            this.Channel4.Name = "Channel4";
            this.Channel4.Size = new System.Drawing.Size(83, 21);
            this.Channel4.TabIndex = 3;
            this.Channel4.Text = "4 Канал";
            this.Channel4.UseVisualStyleBackColor = true;
            this.Channel4.CheckedChanged += new System.EventHandler(this.Channel4_CheckedChanged);
            // 
            // Channel5
            // 
            this.Channel5.AutoSize = true;
            this.Channel5.Location = new System.Drawing.Point(6, 78);
            this.Channel5.Name = "Channel5";
            this.Channel5.Size = new System.Drawing.Size(83, 21);
            this.Channel5.TabIndex = 4;
            this.Channel5.Text = "5 Канал";
            this.Channel5.UseVisualStyleBackColor = true;
            this.Channel5.CheckedChanged += new System.EventHandler(this.Channel5_CheckedChanged);
            // 
            // Channel6
            // 
            this.Channel6.AutoSize = true;
            this.Channel6.Location = new System.Drawing.Point(6, 96);
            this.Channel6.Name = "Channel6";
            this.Channel6.Size = new System.Drawing.Size(83, 21);
            this.Channel6.TabIndex = 5;
            this.Channel6.Text = "6 Канал";
            this.Channel6.UseVisualStyleBackColor = true;
            this.Channel6.CheckedChanged += new System.EventHandler(this.Channel6_CheckedChanged);
            // 
            // Channel7
            // 
            this.Channel7.AutoSize = true;
            this.Channel7.Location = new System.Drawing.Point(6, 114);
            this.Channel7.Name = "Channel7";
            this.Channel7.Size = new System.Drawing.Size(83, 21);
            this.Channel7.TabIndex = 6;
            this.Channel7.Text = "7 Канал";
            this.Channel7.UseVisualStyleBackColor = true;
            this.Channel7.CheckedChanged += new System.EventHandler(this.Channel7_CheckedChanged);
            // 
            // Channel8
            // 
            this.Channel8.AutoSize = true;
            this.Channel8.Location = new System.Drawing.Point(6, 132);
            this.Channel8.Name = "Channel8";
            this.Channel8.Size = new System.Drawing.Size(83, 21);
            this.Channel8.TabIndex = 7;
            this.Channel8.Text = "8 Канал";
            this.Channel8.UseVisualStyleBackColor = true;
            this.Channel8.CheckedChanged += new System.EventHandler(this.Channel8_CheckedChanged);
            // 
            // Channel9
            // 
            this.Channel9.AutoSize = true;
            this.Channel9.Location = new System.Drawing.Point(6, 150);
            this.Channel9.Name = "Channel9";
            this.Channel9.Size = new System.Drawing.Size(83, 21);
            this.Channel9.TabIndex = 8;
            this.Channel9.Text = "9 Канал";
            this.Channel9.UseVisualStyleBackColor = true;
            this.Channel9.CheckedChanged += new System.EventHandler(this.Channel9_CheckedChanged);
            // 
            // Channel10
            // 
            this.Channel10.AutoSize = true;
            this.Channel10.Location = new System.Drawing.Point(6, 168);
            this.Channel10.Name = "Channel10";
            this.Channel10.Size = new System.Drawing.Size(91, 21);
            this.Channel10.TabIndex = 9;
            this.Channel10.Text = "10 Канал";
            this.Channel10.UseVisualStyleBackColor = true;
            this.Channel10.CheckedChanged += new System.EventHandler(this.Channel10_CheckedChanged);
            // 
            // Channel11
            // 
            this.Channel11.AutoSize = true;
            this.Channel11.Location = new System.Drawing.Point(6, 186);
            this.Channel11.Name = "Channel11";
            this.Channel11.Size = new System.Drawing.Size(91, 21);
            this.Channel11.TabIndex = 10;
            this.Channel11.Text = "11 Канал";
            this.Channel11.UseVisualStyleBackColor = true;
            this.Channel11.CheckedChanged += new System.EventHandler(this.Channel11_CheckedChanged);
            // 
            // Channel12
            // 
            this.Channel12.AutoSize = true;
            this.Channel12.Location = new System.Drawing.Point(6, 204);
            this.Channel12.Name = "Channel12";
            this.Channel12.Size = new System.Drawing.Size(91, 21);
            this.Channel12.TabIndex = 11;
            this.Channel12.Text = "12 Канал";
            this.Channel12.UseVisualStyleBackColor = true;
            this.Channel12.CheckedChanged += new System.EventHandler(this.Channel12_CheckedChanged);
            // 
            // Channel13
            // 
            this.Channel13.AutoSize = true;
            this.Channel13.Location = new System.Drawing.Point(6, 222);
            this.Channel13.Name = "Channel13";
            this.Channel13.Size = new System.Drawing.Size(91, 21);
            this.Channel13.TabIndex = 12;
            this.Channel13.Text = "13 Канал";
            this.Channel13.UseVisualStyleBackColor = true;
            this.Channel13.CheckedChanged += new System.EventHandler(this.Channel13_CheckedChanged);
            // 
            // Channel14
            // 
            this.Channel14.AutoSize = true;
            this.Channel14.Location = new System.Drawing.Point(6, 240);
            this.Channel14.Name = "Channel14";
            this.Channel14.Size = new System.Drawing.Size(91, 21);
            this.Channel14.TabIndex = 13;
            this.Channel14.Text = "14 Канал";
            this.Channel14.UseVisualStyleBackColor = true;
            this.Channel14.CheckedChanged += new System.EventHandler(this.Channel14_CheckedChanged);
            // 
            // Channel15
            // 
            this.Channel15.AutoSize = true;
            this.Channel15.Location = new System.Drawing.Point(6, 258);
            this.Channel15.Name = "Channel15";
            this.Channel15.Size = new System.Drawing.Size(91, 21);
            this.Channel15.TabIndex = 14;
            this.Channel15.Text = "15 Канал";
            this.Channel15.UseVisualStyleBackColor = true;
            this.Channel15.CheckedChanged += new System.EventHandler(this.Channel15_CheckedChanged);
            // 
            // Channel16
            // 
            this.Channel16.AutoSize = true;
            this.Channel16.Location = new System.Drawing.Point(6, 276);
            this.Channel16.Name = "Channel16";
            this.Channel16.Size = new System.Drawing.Size(91, 21);
            this.Channel16.TabIndex = 15;
            this.Channel16.Text = "16 Канал";
            this.Channel16.UseVisualStyleBackColor = true;
            this.Channel16.CheckedChanged += new System.EventHandler(this.Channel16_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1189, 539);
            this.Controls.Add(this.Progress);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.OpenPanel);
            this.Controls.Add(this.ConfiguireWindow);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ConfiguireWindow.ResumeLayout(false);
            this.ConReadFile.ResumeLayout(false);
            this.ConReadFile.PerformLayout();
            this.ConSaveFile.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.OutInfoFile.ResumeLayout(false);
            this.OutInfoFile.PerformLayout();
            this.panelAnalysis.ResumeLayout(false);
            this.panelAnalysis.PerformLayout();
            this.OpenPanel.ResumeLayout(false);
            this.MainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl ConfiguireWindow;
        private System.Windows.Forms.TabPage ConReadFile;
        private System.Windows.Forms.TabPage ConSaveFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SerialText;
        private System.Windows.Forms.TextBox IDText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel OpenPanel;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Button StartAnalys;
        private System.Windows.Forms.ProgressBar Progress;
        private System.Windows.Forms.TabPage OutInfoFile;
        private System.Windows.Forms.TextBox NumEvText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox NumUsCh;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button StartWrite;
        private System.ComponentModel.BackgroundWorker BackGrWorkProgressBar;
        private System.Windows.Forms.Button StartDecoder;
        private System.Windows.Forms.Panel panelAnalysis;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox Channel16;
        private System.Windows.Forms.CheckBox Channel15;
        private System.Windows.Forms.CheckBox Channel14;
        private System.Windows.Forms.CheckBox Channel13;
        private System.Windows.Forms.CheckBox Channel12;
        private System.Windows.Forms.CheckBox Channel11;
        private System.Windows.Forms.CheckBox Channel10;
        private System.Windows.Forms.CheckBox Channel9;
        private System.Windows.Forms.CheckBox Channel8;
        private System.Windows.Forms.CheckBox Channel7;
        private System.Windows.Forms.CheckBox Channel6;
        private System.Windows.Forms.CheckBox Channel5;
        private System.Windows.Forms.CheckBox Channel4;
        private System.Windows.Forms.CheckBox Channel3;
        private System.Windows.Forms.CheckBox Channel2;
        private System.Windows.Forms.CheckBox Channel1;
    }
}

