﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace TQDC16_Summary_Rev_1
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TQDC16_Summary());
        }
    }
}
