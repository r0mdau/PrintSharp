﻿using System;
using System.Windows.Forms;

namespace PrintSharpClient
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MainView());
        }
    }
}