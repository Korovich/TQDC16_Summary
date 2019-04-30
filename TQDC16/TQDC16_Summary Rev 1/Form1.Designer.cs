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
            this.ConfiguireWindow = new System.Windows.Forms.TabControl();
            this.ConReadFile = new System.Windows.Forms.TabPage();
            this.NumEvText = new System.Windows.Forms.TextBox();
            this.IDText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.NumUsCh = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SerialText = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ConSaveFile = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.ConDecoder = new System.Windows.Forms.TabPage();
            this.DChannel16 = new System.Windows.Forms.CheckBox();
            this.DChannel1 = new System.Windows.Forms.CheckBox();
            this.DChannel2 = new System.Windows.Forms.CheckBox();
            this.DChannel3 = new System.Windows.Forms.CheckBox();
            this.DChannel4 = new System.Windows.Forms.CheckBox();
            this.DChannel15 = new System.Windows.Forms.CheckBox();
            this.DChannel5 = new System.Windows.Forms.CheckBox();
            this.DChannel14 = new System.Windows.Forms.CheckBox();
            this.DChannel6 = new System.Windows.Forms.CheckBox();
            this.DChannel13 = new System.Windows.Forms.CheckBox();
            this.DChannel7 = new System.Windows.Forms.CheckBox();
            this.DChannel12 = new System.Windows.Forms.CheckBox();
            this.DChannel8 = new System.Windows.Forms.CheckBox();
            this.DChannel11 = new System.Windows.Forms.CheckBox();
            this.DChannel9 = new System.Windows.Forms.CheckBox();
            this.DChannel10 = new System.Windows.Forms.CheckBox();
            this.ConCalc = new System.Windows.Forms.TabPage();
            this.CalcMin = new System.Windows.Forms.CheckBox();
            this.CalcMax = new System.Windows.Forms.CheckBox();
            this.CChannel16 = new System.Windows.Forms.CheckBox();
            this.CChannel1 = new System.Windows.Forms.CheckBox();
            this.CChannel2 = new System.Windows.Forms.CheckBox();
            this.CChannel3 = new System.Windows.Forms.CheckBox();
            this.CChannel4 = new System.Windows.Forms.CheckBox();
            this.CChannel15 = new System.Windows.Forms.CheckBox();
            this.CChannel5 = new System.Windows.Forms.CheckBox();
            this.CChannel14 = new System.Windows.Forms.CheckBox();
            this.CChannel6 = new System.Windows.Forms.CheckBox();
            this.CChannel13 = new System.Windows.Forms.CheckBox();
            this.CChannel7 = new System.Windows.Forms.CheckBox();
            this.CChannel12 = new System.Windows.Forms.CheckBox();
            this.CChannel8 = new System.Windows.Forms.CheckBox();
            this.CChannel11 = new System.Windows.Forms.CheckBox();
            this.CChannel9 = new System.Windows.Forms.CheckBox();
            this.CChannel10 = new System.Windows.Forms.CheckBox();
            this.ConConfigFile = new System.Windows.Forms.TabPage();
            this.ReadConfigFile = new System.Windows.Forms.Button();
            this.OpenPanel = new System.Windows.Forms.Panel();
            this.OpenFileBtn = new System.Windows.Forms.Button();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.StartDecoder = new System.Windows.Forms.Button();
            this.StartWrite = new System.Windows.Forms.Button();
            this.Progress = new System.Windows.Forms.ProgressBar();
            this.BackGrWorkProgressBar = new System.ComponentModel.BackgroundWorker();
            this.ConfiguireWindow.SuspendLayout();
            this.ConReadFile.SuspendLayout();
            this.ConSaveFile.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.ConDecoder.SuspendLayout();
            this.ConCalc.SuspendLayout();
            this.ConConfigFile.SuspendLayout();
            this.OpenPanel.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConfiguireWindow
            // 
            this.ConfiguireWindow.Controls.Add(this.ConReadFile);
            this.ConfiguireWindow.Controls.Add(this.ConSaveFile);
            this.ConfiguireWindow.Controls.Add(this.ConConfigFile);
            this.ConfiguireWindow.Enabled = false;
            this.ConfiguireWindow.Location = new System.Drawing.Point(10, 10);
            this.ConfiguireWindow.Margin = new System.Windows.Forms.Padding(2);
            this.ConfiguireWindow.Name = "ConfiguireWindow";
            this.ConfiguireWindow.SelectedIndex = 0;
            this.ConfiguireWindow.Size = new System.Drawing.Size(283, 348);
            this.ConfiguireWindow.TabIndex = 1;
            // 
            // ConReadFile
            // 
            this.ConReadFile.BackColor = System.Drawing.Color.Silver;
            this.ConReadFile.Controls.Add(this.NumEvText);
            this.ConReadFile.Controls.Add(this.IDText);
            this.ConReadFile.Controls.Add(this.label3);
            this.ConReadFile.Controls.Add(this.NumUsCh);
            this.ConReadFile.Controls.Add(this.label2);
            this.ConReadFile.Controls.Add(this.label4);
            this.ConReadFile.Controls.Add(this.SerialText);
            this.ConReadFile.Controls.Add(this.label1);
            this.ConReadFile.Location = new System.Drawing.Point(4, 22);
            this.ConReadFile.Margin = new System.Windows.Forms.Padding(2);
            this.ConReadFile.Name = "ConReadFile";
            this.ConReadFile.Padding = new System.Windows.Forms.Padding(2);
            this.ConReadFile.Size = new System.Drawing.Size(275, 322);
            this.ConReadFile.TabIndex = 0;
            this.ConReadFile.Text = "Описание файла";
            // 
            // NumEvText
            // 
            this.NumEvText.Location = new System.Drawing.Point(148, 74);
            this.NumEvText.Margin = new System.Windows.Forms.Padding(2);
            this.NumEvText.Name = "NumEvText";
            this.NumEvText.ReadOnly = true;
            this.NumEvText.Size = new System.Drawing.Size(105, 20);
            this.NumEvText.TabIndex = 23;
            // 
            // IDText
            // 
            this.IDText.Location = new System.Drawing.Point(148, 11);
            this.IDText.Margin = new System.Windows.Forms.Padding(2);
            this.IDText.Name = "IDText";
            this.IDText.ReadOnly = true;
            this.IDText.Size = new System.Drawing.Size(105, 20);
            this.IDText.TabIndex = 3;
            this.IDText.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 74);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Кол.  тригеров";
            // 
            // NumUsCh
            // 
            this.NumUsCh.Location = new System.Drawing.Point(148, 106);
            this.NumUsCh.Margin = new System.Windows.Forms.Padding(2);
            this.NumUsCh.Name = "NumUsCh";
            this.NumUsCh.ReadOnly = true;
            this.NumUsCh.Size = new System.Drawing.Size(105, 20);
            this.NumUsCh.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "ID TQDC";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 106);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 13);
            this.label4.TabIndex = 20;
            this.label4.Text = "Кол. каналы";
            // 
            // SerialText
            // 
            this.SerialText.Location = new System.Drawing.Point(148, 44);
            this.SerialText.Margin = new System.Windows.Forms.Padding(2);
            this.SerialText.Name = "SerialText";
            this.SerialText.ReadOnly = true;
            this.SerialText.Size = new System.Drawing.Size(105, 20);
            this.SerialText.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 44);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(126, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Серийный номер TQDC";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // ConSaveFile
            // 
            this.ConSaveFile.BackColor = System.Drawing.Color.Silver;
            this.ConSaveFile.Controls.Add(this.panel2);
            this.ConSaveFile.Location = new System.Drawing.Point(4, 22);
            this.ConSaveFile.Margin = new System.Windows.Forms.Padding(2);
            this.ConSaveFile.Name = "ConSaveFile";
            this.ConSaveFile.Padding = new System.Windows.Forms.Padding(2);
            this.ConSaveFile.Size = new System.Drawing.Size(275, 322);
            this.ConSaveFile.TabIndex = 1;
            this.ConSaveFile.Text = "Выходной файл";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(275, 324);
            this.panel2.TabIndex = 6;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.ConDecoder);
            this.tabControl1.Controls.Add(this.ConCalc);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(277, 327);
            this.tabControl1.TabIndex = 6;
            // 
            // ConDecoder
            // 
            this.ConDecoder.Controls.Add(this.DChannel16);
            this.ConDecoder.Controls.Add(this.DChannel1);
            this.ConDecoder.Controls.Add(this.DChannel2);
            this.ConDecoder.Controls.Add(this.DChannel3);
            this.ConDecoder.Controls.Add(this.DChannel4);
            this.ConDecoder.Controls.Add(this.DChannel15);
            this.ConDecoder.Controls.Add(this.DChannel5);
            this.ConDecoder.Controls.Add(this.DChannel14);
            this.ConDecoder.Controls.Add(this.DChannel6);
            this.ConDecoder.Controls.Add(this.DChannel13);
            this.ConDecoder.Controls.Add(this.DChannel7);
            this.ConDecoder.Controls.Add(this.DChannel12);
            this.ConDecoder.Controls.Add(this.DChannel8);
            this.ConDecoder.Controls.Add(this.DChannel11);
            this.ConDecoder.Controls.Add(this.DChannel9);
            this.ConDecoder.Controls.Add(this.DChannel10);
            this.ConDecoder.Location = new System.Drawing.Point(4, 22);
            this.ConDecoder.Margin = new System.Windows.Forms.Padding(2);
            this.ConDecoder.Name = "ConDecoder";
            this.ConDecoder.Padding = new System.Windows.Forms.Padding(2);
            this.ConDecoder.Size = new System.Drawing.Size(269, 301);
            this.ConDecoder.TabIndex = 0;
            this.ConDecoder.Text = "Декодер";
            // 
            // DChannel16
            // 
            this.DChannel16.AutoSize = true;
            this.DChannel16.Location = new System.Drawing.Point(4, 248);
            this.DChannel16.Margin = new System.Windows.Forms.Padding(2);
            this.DChannel16.Name = "DChannel16";
            this.DChannel16.Size = new System.Drawing.Size(72, 17);
            this.DChannel16.TabIndex = 15;
            this.DChannel16.Text = "16 Канал";
            this.DChannel16.UseVisualStyleBackColor = true;
            this.DChannel16.CheckedChanged += new System.EventHandler(this.DChannel16_CheckedChanged);
            // 
            // DChannel1
            // 
            this.DChannel1.AutoSize = true;
            this.DChannel1.Location = new System.Drawing.Point(4, 4);
            this.DChannel1.Margin = new System.Windows.Forms.Padding(2);
            this.DChannel1.Name = "DChannel1";
            this.DChannel1.Size = new System.Drawing.Size(66, 17);
            this.DChannel1.TabIndex = 0;
            this.DChannel1.Text = "1 Канал";
            this.DChannel1.UseVisualStyleBackColor = true;
            this.DChannel1.CheckedChanged += new System.EventHandler(this.DChannel1_CheckedChanged);
            // 
            // DChannel2
            // 
            this.DChannel2.AutoSize = true;
            this.DChannel2.Location = new System.Drawing.Point(4, 20);
            this.DChannel2.Margin = new System.Windows.Forms.Padding(2);
            this.DChannel2.Name = "DChannel2";
            this.DChannel2.Size = new System.Drawing.Size(66, 17);
            this.DChannel2.TabIndex = 1;
            this.DChannel2.Text = "2 Канал";
            this.DChannel2.UseVisualStyleBackColor = true;
            this.DChannel2.CheckedChanged += new System.EventHandler(this.DChannel2_CheckedChanged);
            // 
            // DChannel3
            // 
            this.DChannel3.AutoSize = true;
            this.DChannel3.Location = new System.Drawing.Point(4, 37);
            this.DChannel3.Margin = new System.Windows.Forms.Padding(2);
            this.DChannel3.Name = "DChannel3";
            this.DChannel3.Size = new System.Drawing.Size(66, 17);
            this.DChannel3.TabIndex = 2;
            this.DChannel3.Text = "3 Канал";
            this.DChannel3.UseVisualStyleBackColor = true;
            this.DChannel3.CheckedChanged += new System.EventHandler(this.DChannel3_CheckedChanged);
            // 
            // DChannel4
            // 
            this.DChannel4.AutoSize = true;
            this.DChannel4.Location = new System.Drawing.Point(4, 53);
            this.DChannel4.Margin = new System.Windows.Forms.Padding(2);
            this.DChannel4.Name = "DChannel4";
            this.DChannel4.Size = new System.Drawing.Size(66, 17);
            this.DChannel4.TabIndex = 3;
            this.DChannel4.Text = "4 Канал";
            this.DChannel4.UseVisualStyleBackColor = true;
            this.DChannel4.CheckedChanged += new System.EventHandler(this.DChannel4_CheckedChanged);
            // 
            // DChannel15
            // 
            this.DChannel15.AutoSize = true;
            this.DChannel15.Location = new System.Drawing.Point(4, 232);
            this.DChannel15.Margin = new System.Windows.Forms.Padding(2);
            this.DChannel15.Name = "DChannel15";
            this.DChannel15.Size = new System.Drawing.Size(72, 17);
            this.DChannel15.TabIndex = 14;
            this.DChannel15.Text = "15 Канал";
            this.DChannel15.UseVisualStyleBackColor = true;
            this.DChannel15.CheckedChanged += new System.EventHandler(this.DChannel15_CheckedChanged);
            // 
            // DChannel5
            // 
            this.DChannel5.AutoSize = true;
            this.DChannel5.Location = new System.Drawing.Point(4, 69);
            this.DChannel5.Margin = new System.Windows.Forms.Padding(2);
            this.DChannel5.Name = "DChannel5";
            this.DChannel5.Size = new System.Drawing.Size(66, 17);
            this.DChannel5.TabIndex = 4;
            this.DChannel5.Text = "5 Канал";
            this.DChannel5.UseVisualStyleBackColor = true;
            this.DChannel5.CheckedChanged += new System.EventHandler(this.DChannel5_CheckedChanged);
            // 
            // DChannel14
            // 
            this.DChannel14.AutoSize = true;
            this.DChannel14.Location = new System.Drawing.Point(4, 215);
            this.DChannel14.Margin = new System.Windows.Forms.Padding(2);
            this.DChannel14.Name = "DChannel14";
            this.DChannel14.Size = new System.Drawing.Size(72, 17);
            this.DChannel14.TabIndex = 13;
            this.DChannel14.Text = "14 Канал";
            this.DChannel14.UseVisualStyleBackColor = true;
            this.DChannel14.CheckedChanged += new System.EventHandler(this.DChannel14_CheckedChanged);
            // 
            // DChannel6
            // 
            this.DChannel6.AutoSize = true;
            this.DChannel6.BackColor = System.Drawing.SystemColors.Control;
            this.DChannel6.Location = new System.Drawing.Point(4, 85);
            this.DChannel6.Margin = new System.Windows.Forms.Padding(2);
            this.DChannel6.Name = "DChannel6";
            this.DChannel6.Size = new System.Drawing.Size(66, 17);
            this.DChannel6.TabIndex = 5;
            this.DChannel6.Text = "6 Канал";
            this.DChannel6.UseVisualStyleBackColor = false;
            this.DChannel6.CheckedChanged += new System.EventHandler(this.DChannel6_CheckedChanged);
            // 
            // DChannel13
            // 
            this.DChannel13.AutoSize = true;
            this.DChannel13.Location = new System.Drawing.Point(4, 199);
            this.DChannel13.Margin = new System.Windows.Forms.Padding(2);
            this.DChannel13.Name = "DChannel13";
            this.DChannel13.Size = new System.Drawing.Size(72, 17);
            this.DChannel13.TabIndex = 12;
            this.DChannel13.Text = "13 Канал";
            this.DChannel13.UseVisualStyleBackColor = true;
            this.DChannel13.CheckedChanged += new System.EventHandler(this.DChannel13_CheckedChanged);
            // 
            // DChannel7
            // 
            this.DChannel7.AutoSize = true;
            this.DChannel7.Location = new System.Drawing.Point(4, 102);
            this.DChannel7.Margin = new System.Windows.Forms.Padding(2);
            this.DChannel7.Name = "DChannel7";
            this.DChannel7.Size = new System.Drawing.Size(66, 17);
            this.DChannel7.TabIndex = 6;
            this.DChannel7.Text = "7 Канал";
            this.DChannel7.UseVisualStyleBackColor = true;
            this.DChannel7.CheckedChanged += new System.EventHandler(this.DChannel7_CheckedChanged);
            // 
            // DChannel12
            // 
            this.DChannel12.AutoSize = true;
            this.DChannel12.Location = new System.Drawing.Point(4, 183);
            this.DChannel12.Margin = new System.Windows.Forms.Padding(2);
            this.DChannel12.Name = "DChannel12";
            this.DChannel12.Size = new System.Drawing.Size(72, 17);
            this.DChannel12.TabIndex = 11;
            this.DChannel12.Text = "12 Канал";
            this.DChannel12.UseVisualStyleBackColor = true;
            this.DChannel12.CheckedChanged += new System.EventHandler(this.DChannel12_CheckedChanged);
            // 
            // DChannel8
            // 
            this.DChannel8.AutoSize = true;
            this.DChannel8.Location = new System.Drawing.Point(4, 118);
            this.DChannel8.Margin = new System.Windows.Forms.Padding(2);
            this.DChannel8.Name = "DChannel8";
            this.DChannel8.Size = new System.Drawing.Size(66, 17);
            this.DChannel8.TabIndex = 7;
            this.DChannel8.Text = "8 Канал";
            this.DChannel8.UseVisualStyleBackColor = true;
            this.DChannel8.CheckedChanged += new System.EventHandler(this.DChannel8_CheckedChanged);
            // 
            // DChannel11
            // 
            this.DChannel11.AutoSize = true;
            this.DChannel11.Location = new System.Drawing.Point(4, 167);
            this.DChannel11.Margin = new System.Windows.Forms.Padding(2);
            this.DChannel11.Name = "DChannel11";
            this.DChannel11.Size = new System.Drawing.Size(72, 17);
            this.DChannel11.TabIndex = 10;
            this.DChannel11.Text = "11 Канал";
            this.DChannel11.UseVisualStyleBackColor = true;
            this.DChannel11.CheckedChanged += new System.EventHandler(this.DChannel11_CheckedChanged);
            // 
            // DChannel9
            // 
            this.DChannel9.AutoSize = true;
            this.DChannel9.Location = new System.Drawing.Point(4, 134);
            this.DChannel9.Margin = new System.Windows.Forms.Padding(2);
            this.DChannel9.Name = "DChannel9";
            this.DChannel9.Size = new System.Drawing.Size(66, 17);
            this.DChannel9.TabIndex = 8;
            this.DChannel9.Text = "9 Канал";
            this.DChannel9.UseVisualStyleBackColor = true;
            this.DChannel9.CheckedChanged += new System.EventHandler(this.DChannel9_CheckedChanged);
            // 
            // DChannel10
            // 
            this.DChannel10.AutoSize = true;
            this.DChannel10.Location = new System.Drawing.Point(4, 150);
            this.DChannel10.Margin = new System.Windows.Forms.Padding(2);
            this.DChannel10.Name = "DChannel10";
            this.DChannel10.Size = new System.Drawing.Size(72, 17);
            this.DChannel10.TabIndex = 9;
            this.DChannel10.Text = "10 Канал";
            this.DChannel10.UseVisualStyleBackColor = true;
            this.DChannel10.CheckedChanged += new System.EventHandler(this.DChannel10_CheckedChanged);
            // 
            // ConCalc
            // 
            this.ConCalc.Controls.Add(this.CalcMin);
            this.ConCalc.Controls.Add(this.CalcMax);
            this.ConCalc.Controls.Add(this.CChannel16);
            this.ConCalc.Controls.Add(this.CChannel1);
            this.ConCalc.Controls.Add(this.CChannel2);
            this.ConCalc.Controls.Add(this.CChannel3);
            this.ConCalc.Controls.Add(this.CChannel4);
            this.ConCalc.Controls.Add(this.CChannel15);
            this.ConCalc.Controls.Add(this.CChannel5);
            this.ConCalc.Controls.Add(this.CChannel14);
            this.ConCalc.Controls.Add(this.CChannel6);
            this.ConCalc.Controls.Add(this.CChannel13);
            this.ConCalc.Controls.Add(this.CChannel7);
            this.ConCalc.Controls.Add(this.CChannel12);
            this.ConCalc.Controls.Add(this.CChannel8);
            this.ConCalc.Controls.Add(this.CChannel11);
            this.ConCalc.Controls.Add(this.CChannel9);
            this.ConCalc.Controls.Add(this.CChannel10);
            this.ConCalc.Location = new System.Drawing.Point(4, 22);
            this.ConCalc.Margin = new System.Windows.Forms.Padding(2);
            this.ConCalc.Name = "ConCalc";
            this.ConCalc.Padding = new System.Windows.Forms.Padding(2);
            this.ConCalc.Size = new System.Drawing.Size(269, 301);
            this.ConCalc.TabIndex = 1;
            this.ConCalc.Text = "Вычисление";
            this.ConCalc.UseVisualStyleBackColor = true;
            // 
            // CalcMin
            // 
            this.CalcMin.AutoSize = true;
            this.CalcMin.Location = new System.Drawing.Point(124, 29);
            this.CalcMin.Margin = new System.Windows.Forms.Padding(2);
            this.CalcMin.Name = "CalcMin";
            this.CalcMin.Size = new System.Drawing.Size(128, 17);
            this.CalcMin.TabIndex = 33;
            this.CalcMin.Text = "Минимальная точка";
            this.CalcMin.UseVisualStyleBackColor = true;
            // 
            // CalcMax
            // 
            this.CalcMax.AutoSize = true;
            this.CalcMax.Location = new System.Drawing.Point(124, 5);
            this.CalcMax.Margin = new System.Windows.Forms.Padding(2);
            this.CalcMax.Name = "CalcMax";
            this.CalcMax.Size = new System.Drawing.Size(134, 17);
            this.CalcMax.TabIndex = 32;
            this.CalcMax.Text = "Максимальная точка";
            this.CalcMax.UseVisualStyleBackColor = true;
            // 
            // CChannel16
            // 
            this.CChannel16.AutoSize = true;
            this.CChannel16.Location = new System.Drawing.Point(4, 248);
            this.CChannel16.Margin = new System.Windows.Forms.Padding(2);
            this.CChannel16.Name = "CChannel16";
            this.CChannel16.Size = new System.Drawing.Size(72, 17);
            this.CChannel16.TabIndex = 31;
            this.CChannel16.Text = "16 Канал";
            this.CChannel16.UseVisualStyleBackColor = true;
            this.CChannel16.CheckedChanged += new System.EventHandler(this.CChannel16_CheckedChanged);
            // 
            // CChannel1
            // 
            this.CChannel1.AutoSize = true;
            this.CChannel1.Location = new System.Drawing.Point(4, 4);
            this.CChannel1.Margin = new System.Windows.Forms.Padding(2);
            this.CChannel1.Name = "CChannel1";
            this.CChannel1.Size = new System.Drawing.Size(66, 17);
            this.CChannel1.TabIndex = 16;
            this.CChannel1.Text = "1 Канал";
            this.CChannel1.UseVisualStyleBackColor = true;
            this.CChannel1.CheckedChanged += new System.EventHandler(this.CChannel1_CheckedChanged);
            // 
            // CChannel2
            // 
            this.CChannel2.AutoSize = true;
            this.CChannel2.Location = new System.Drawing.Point(4, 20);
            this.CChannel2.Margin = new System.Windows.Forms.Padding(2);
            this.CChannel2.Name = "CChannel2";
            this.CChannel2.Size = new System.Drawing.Size(66, 17);
            this.CChannel2.TabIndex = 17;
            this.CChannel2.Text = "2 Канал";
            this.CChannel2.UseVisualStyleBackColor = true;
            this.CChannel2.CheckedChanged += new System.EventHandler(this.CChannel2_CheckedChanged);
            // 
            // CChannel3
            // 
            this.CChannel3.AutoSize = true;
            this.CChannel3.Location = new System.Drawing.Point(4, 37);
            this.CChannel3.Margin = new System.Windows.Forms.Padding(2);
            this.CChannel3.Name = "CChannel3";
            this.CChannel3.Size = new System.Drawing.Size(66, 17);
            this.CChannel3.TabIndex = 18;
            this.CChannel3.Text = "3 Канал";
            this.CChannel3.UseVisualStyleBackColor = true;
            this.CChannel3.CheckedChanged += new System.EventHandler(this.CChannel3_CheckedChanged);
            // 
            // CChannel4
            // 
            this.CChannel4.AutoSize = true;
            this.CChannel4.Location = new System.Drawing.Point(4, 53);
            this.CChannel4.Margin = new System.Windows.Forms.Padding(2);
            this.CChannel4.Name = "CChannel4";
            this.CChannel4.Size = new System.Drawing.Size(66, 17);
            this.CChannel4.TabIndex = 19;
            this.CChannel4.Text = "4 Канал";
            this.CChannel4.UseVisualStyleBackColor = true;
            this.CChannel4.CheckedChanged += new System.EventHandler(this.CChannel4_CheckedChanged);
            // 
            // CChannel15
            // 
            this.CChannel15.AutoSize = true;
            this.CChannel15.Location = new System.Drawing.Point(4, 232);
            this.CChannel15.Margin = new System.Windows.Forms.Padding(2);
            this.CChannel15.Name = "CChannel15";
            this.CChannel15.Size = new System.Drawing.Size(72, 17);
            this.CChannel15.TabIndex = 30;
            this.CChannel15.Text = "15 Канал";
            this.CChannel15.UseVisualStyleBackColor = true;
            this.CChannel15.CheckedChanged += new System.EventHandler(this.CChannel15_CheckedChanged);
            // 
            // CChannel5
            // 
            this.CChannel5.AutoSize = true;
            this.CChannel5.Location = new System.Drawing.Point(4, 69);
            this.CChannel5.Margin = new System.Windows.Forms.Padding(2);
            this.CChannel5.Name = "CChannel5";
            this.CChannel5.Size = new System.Drawing.Size(66, 17);
            this.CChannel5.TabIndex = 20;
            this.CChannel5.Text = "5 Канал";
            this.CChannel5.UseVisualStyleBackColor = true;
            this.CChannel5.CheckedChanged += new System.EventHandler(this.CChannel5_CheckedChanged);
            // 
            // CChannel14
            // 
            this.CChannel14.AutoSize = true;
            this.CChannel14.Location = new System.Drawing.Point(4, 215);
            this.CChannel14.Margin = new System.Windows.Forms.Padding(2);
            this.CChannel14.Name = "CChannel14";
            this.CChannel14.Size = new System.Drawing.Size(72, 17);
            this.CChannel14.TabIndex = 29;
            this.CChannel14.Text = "14 Канал";
            this.CChannel14.UseVisualStyleBackColor = true;
            this.CChannel14.CheckedChanged += new System.EventHandler(this.CChannel14_CheckedChanged);
            // 
            // CChannel6
            // 
            this.CChannel6.AutoSize = true;
            this.CChannel6.Location = new System.Drawing.Point(4, 85);
            this.CChannel6.Margin = new System.Windows.Forms.Padding(2);
            this.CChannel6.Name = "CChannel6";
            this.CChannel6.Size = new System.Drawing.Size(66, 17);
            this.CChannel6.TabIndex = 21;
            this.CChannel6.Text = "6 Канал";
            this.CChannel6.UseVisualStyleBackColor = true;
            this.CChannel6.CheckedChanged += new System.EventHandler(this.CChannel6_CheckedChanged);
            // 
            // CChannel13
            // 
            this.CChannel13.AutoSize = true;
            this.CChannel13.Location = new System.Drawing.Point(4, 199);
            this.CChannel13.Margin = new System.Windows.Forms.Padding(2);
            this.CChannel13.Name = "CChannel13";
            this.CChannel13.Size = new System.Drawing.Size(72, 17);
            this.CChannel13.TabIndex = 28;
            this.CChannel13.Text = "13 Канал";
            this.CChannel13.UseVisualStyleBackColor = true;
            this.CChannel13.CheckedChanged += new System.EventHandler(this.CChannel13_CheckedChanged);
            // 
            // CChannel7
            // 
            this.CChannel7.AutoSize = true;
            this.CChannel7.Location = new System.Drawing.Point(4, 102);
            this.CChannel7.Margin = new System.Windows.Forms.Padding(2);
            this.CChannel7.Name = "CChannel7";
            this.CChannel7.Size = new System.Drawing.Size(66, 17);
            this.CChannel7.TabIndex = 22;
            this.CChannel7.Text = "7 Канал";
            this.CChannel7.UseVisualStyleBackColor = true;
            this.CChannel7.CheckedChanged += new System.EventHandler(this.CChannel7_CheckedChanged);
            // 
            // CChannel12
            // 
            this.CChannel12.AutoSize = true;
            this.CChannel12.Location = new System.Drawing.Point(4, 183);
            this.CChannel12.Margin = new System.Windows.Forms.Padding(2);
            this.CChannel12.Name = "CChannel12";
            this.CChannel12.Size = new System.Drawing.Size(72, 17);
            this.CChannel12.TabIndex = 27;
            this.CChannel12.Text = "12 Канал";
            this.CChannel12.UseVisualStyleBackColor = true;
            this.CChannel12.CheckedChanged += new System.EventHandler(this.CChannel12_CheckedChanged);
            // 
            // CChannel8
            // 
            this.CChannel8.AutoSize = true;
            this.CChannel8.Location = new System.Drawing.Point(4, 118);
            this.CChannel8.Margin = new System.Windows.Forms.Padding(2);
            this.CChannel8.Name = "CChannel8";
            this.CChannel8.Size = new System.Drawing.Size(66, 17);
            this.CChannel8.TabIndex = 23;
            this.CChannel8.Text = "8 Канал";
            this.CChannel8.UseVisualStyleBackColor = true;
            this.CChannel8.CheckedChanged += new System.EventHandler(this.CChannel8_CheckedChanged);
            // 
            // CChannel11
            // 
            this.CChannel11.AutoSize = true;
            this.CChannel11.Location = new System.Drawing.Point(4, 167);
            this.CChannel11.Margin = new System.Windows.Forms.Padding(2);
            this.CChannel11.Name = "CChannel11";
            this.CChannel11.Size = new System.Drawing.Size(72, 17);
            this.CChannel11.TabIndex = 26;
            this.CChannel11.Text = "11 Канал";
            this.CChannel11.UseVisualStyleBackColor = true;
            this.CChannel11.CheckedChanged += new System.EventHandler(this.CChannel11_CheckedChanged);
            // 
            // CChannel9
            // 
            this.CChannel9.AutoSize = true;
            this.CChannel9.Location = new System.Drawing.Point(4, 134);
            this.CChannel9.Margin = new System.Windows.Forms.Padding(2);
            this.CChannel9.Name = "CChannel9";
            this.CChannel9.Size = new System.Drawing.Size(66, 17);
            this.CChannel9.TabIndex = 24;
            this.CChannel9.Text = "9 Канал";
            this.CChannel9.UseVisualStyleBackColor = true;
            this.CChannel9.CheckedChanged += new System.EventHandler(this.CChannel9_CheckedChanged);
            // 
            // CChannel10
            // 
            this.CChannel10.AutoSize = true;
            this.CChannel10.Location = new System.Drawing.Point(4, 150);
            this.CChannel10.Margin = new System.Windows.Forms.Padding(2);
            this.CChannel10.Name = "CChannel10";
            this.CChannel10.Size = new System.Drawing.Size(72, 17);
            this.CChannel10.TabIndex = 25;
            this.CChannel10.Text = "10 Канал";
            this.CChannel10.UseVisualStyleBackColor = true;
            this.CChannel10.CheckedChanged += new System.EventHandler(this.CChannel10_CheckedChanged);
            // 
            // ConConfigFile
            // 
            this.ConConfigFile.BackColor = System.Drawing.SystemColors.Control;
            this.ConConfigFile.Controls.Add(this.ReadConfigFile);
            this.ConConfigFile.Location = new System.Drawing.Point(4, 22);
            this.ConConfigFile.Name = "ConConfigFile";
            this.ConConfigFile.Size = new System.Drawing.Size(275, 322);
            this.ConConfigFile.TabIndex = 2;
            this.ConConfigFile.Text = "Файл настроек";
            // 
            // ReadConfigFile
            // 
            this.ReadConfigFile.Location = new System.Drawing.Point(3, 3);
            this.ReadConfigFile.Name = "ReadConfigFile";
            this.ReadConfigFile.Size = new System.Drawing.Size(272, 23);
            this.ReadConfigFile.TabIndex = 5;
            this.ReadConfigFile.Text = "Загрузить конфигурационный файл";
            this.ReadConfigFile.UseVisualStyleBackColor = true;
            this.ReadConfigFile.Click += new System.EventHandler(this.ReadConfigFile_Click);
            // 
            // OpenPanel
            // 
            this.OpenPanel.Controls.Add(this.OpenFileBtn);
            this.OpenPanel.Location = new System.Drawing.Point(13, 373);
            this.OpenPanel.Margin = new System.Windows.Forms.Padding(2);
            this.OpenPanel.Name = "OpenPanel";
            this.OpenPanel.Size = new System.Drawing.Size(260, 55);
            this.OpenPanel.TabIndex = 2;
            // 
            // OpenFileBtn
            // 
            this.OpenFileBtn.Location = new System.Drawing.Point(2, 9);
            this.OpenFileBtn.Margin = new System.Windows.Forms.Padding(2);
            this.OpenFileBtn.Name = "OpenFileBtn";
            this.OpenFileBtn.Size = new System.Drawing.Size(96, 38);
            this.OpenFileBtn.TabIndex = 6;
            this.OpenFileBtn.Text = "Открыть файл";
            this.OpenFileBtn.UseVisualStyleBackColor = true;
            this.OpenFileBtn.Click += new System.EventHandler(this.OpenFileBtn_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.Controls.Add(this.StartDecoder);
            this.MainPanel.Controls.Add(this.StartWrite);
            this.MainPanel.Location = new System.Drawing.Point(295, 373);
            this.MainPanel.Margin = new System.Windows.Forms.Padding(2);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(588, 54);
            this.MainPanel.TabIndex = 3;
            // 
            // StartDecoder
            // 
            this.StartDecoder.Location = new System.Drawing.Point(2, 9);
            this.StartDecoder.Margin = new System.Windows.Forms.Padding(2);
            this.StartDecoder.Name = "StartDecoder";
            this.StartDecoder.Size = new System.Drawing.Size(96, 38);
            this.StartDecoder.TabIndex = 6;
            this.StartDecoder.Text = "Декодировать файл";
            this.StartDecoder.UseVisualStyleBackColor = true;
            this.StartDecoder.Click += new System.EventHandler(this.StartDecoder_Click);
            // 
            // StartWrite
            // 
            this.StartWrite.Location = new System.Drawing.Point(490, 9);
            this.StartWrite.Margin = new System.Windows.Forms.Padding(2);
            this.StartWrite.Name = "StartWrite";
            this.StartWrite.Size = new System.Drawing.Size(96, 38);
            this.StartWrite.TabIndex = 5;
            this.StartWrite.Text = "Декодировать с расчетом";
            this.StartWrite.UseVisualStyleBackColor = true;
            this.StartWrite.Click += new System.EventHandler(this.StartWrite_Click);
            // 
            // Progress
            // 
            this.Progress.Location = new System.Drawing.Point(295, 352);
            this.Progress.Margin = new System.Windows.Forms.Padding(2);
            this.Progress.Maximum = 1000;
            this.Progress.Name = "Progress";
            this.Progress.Size = new System.Drawing.Size(588, 16);
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
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 438);
            this.Controls.Add(this.Progress);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.OpenPanel);
            this.Controls.Add(this.ConfiguireWindow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.Text = "Form1";
            this.ConfiguireWindow.ResumeLayout(false);
            this.ConReadFile.ResumeLayout(false);
            this.ConReadFile.PerformLayout();
            this.ConSaveFile.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ConDecoder.ResumeLayout(false);
            this.ConDecoder.PerformLayout();
            this.ConCalc.ResumeLayout(false);
            this.ConCalc.PerformLayout();
            this.ConConfigFile.ResumeLayout(false);
            this.OpenPanel.ResumeLayout(false);
            this.MainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl ConfiguireWindow;
        private System.Windows.Forms.TabPage ConReadFile;
        private System.Windows.Forms.TabPage ConSaveFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SerialText;
        private System.Windows.Forms.TextBox IDText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel OpenPanel;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.ProgressBar Progress;
        private System.Windows.Forms.Button StartWrite;
        private System.ComponentModel.BackgroundWorker BackGrWorkProgressBar;
        private System.Windows.Forms.Button StartDecoder;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox DChannel16;
        private System.Windows.Forms.CheckBox DChannel15;
        private System.Windows.Forms.CheckBox DChannel14;
        private System.Windows.Forms.CheckBox DChannel13;
        private System.Windows.Forms.CheckBox DChannel12;
        private System.Windows.Forms.CheckBox DChannel11;
        private System.Windows.Forms.CheckBox DChannel10;
        private System.Windows.Forms.CheckBox DChannel9;
        private System.Windows.Forms.CheckBox DChannel8;
        private System.Windows.Forms.CheckBox DChannel7;
        private System.Windows.Forms.CheckBox DChannel6;
        private System.Windows.Forms.CheckBox DChannel5;
        private System.Windows.Forms.CheckBox DChannel4;
        private System.Windows.Forms.CheckBox DChannel3;
        private System.Windows.Forms.CheckBox DChannel2;
        private System.Windows.Forms.CheckBox DChannel1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage ConDecoder;
        private System.Windows.Forms.TabPage ConCalc;
        private System.Windows.Forms.TextBox NumEvText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox NumUsCh;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox CChannel16;
        private System.Windows.Forms.CheckBox CChannel1;
        private System.Windows.Forms.CheckBox CChannel2;
        private System.Windows.Forms.CheckBox CChannel3;
        private System.Windows.Forms.CheckBox CChannel4;
        private System.Windows.Forms.CheckBox CChannel15;
        private System.Windows.Forms.CheckBox CChannel5;
        private System.Windows.Forms.CheckBox CChannel14;
        private System.Windows.Forms.CheckBox CChannel6;
        private System.Windows.Forms.CheckBox CChannel13;
        private System.Windows.Forms.CheckBox CChannel7;
        private System.Windows.Forms.CheckBox CChannel12;
        private System.Windows.Forms.CheckBox CChannel8;
        private System.Windows.Forms.CheckBox CChannel11;
        private System.Windows.Forms.CheckBox CChannel9;
        private System.Windows.Forms.CheckBox CChannel10;
        private System.Windows.Forms.CheckBox CalcMin;
        private System.Windows.Forms.CheckBox CalcMax;
        private System.Windows.Forms.Button OpenFileBtn;
        private System.Windows.Forms.TabPage ConConfigFile;
        private System.Windows.Forms.Button ReadConfigFile;
    }
}

