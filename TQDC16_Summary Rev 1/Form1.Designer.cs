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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.StartAnalys = new System.Windows.Forms.Button();
            this.OutInfoFile = new System.Windows.Forms.TabPage();
            this.NumEvText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.NumUsCh = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.OpenPanel = new System.Windows.Forms.Panel();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.StartDecoder = new System.Windows.Forms.Button();
            this.StartWrite = new System.Windows.Forms.Button();
            this.Progress = new System.Windows.Forms.ProgressBar();
            this.BackGrWorkProgressBar = new System.ComponentModel.BackgroundWorker();
            this.ConfiguireWindow.SuspendLayout();
            this.ConReadFile.SuspendLayout();
            this.ConSaveFile.SuspendLayout();
            this.panel1.SuspendLayout();
            this.OutInfoFile.SuspendLayout();
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
            this.ConSaveFile.Controls.Add(this.panel1);
            this.ConSaveFile.Location = new System.Drawing.Point(4, 25);
            this.ConSaveFile.Name = "ConSaveFile";
            this.ConSaveFile.Padding = new System.Windows.Forms.Padding(3);
            this.ConSaveFile.Size = new System.Drawing.Size(347, 399);
            this.ConSaveFile.TabIndex = 1;
            this.ConSaveFile.Text = "Выходной файл";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.StartAnalys);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(347, 399);
            this.panel1.TabIndex = 5;
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
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.OutInfoFile.ResumeLayout(false);
            this.OutInfoFile.PerformLayout();
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
    }
}

