using System.Windows.Controls;
using System.Windows;
using System;
using System.IO;
using System.Diagnostics;
namespace trustid
{

    public partial class MacroAttack_4 : UserControl
    {

        public MacroAttack_4()
        {
            InitializeComponent();
        }



        private void MacroOne_Click(object sender, RoutedEventArgs e)
        {

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"C:\Program Files\WindowsApps\PythonSoftwareFoundation.Python.3.10_3.10.2800.0_x64__qbz5n2kfra8p0\python3.10.exe";
            start.Arguments = "C:\\Users\\Elian\\Documents\\macro_3.py";
            start.UseShellExecute = false;
            start.WorkingDirectory = "";
            start.RedirectStandardOutput = true;


            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    Console.Write(result);
                }


                
            }

        
        }

        private void picture_Click(object sender, RoutedEventArgs e)
        {

            string textFile = @"C:\Users\Elian\Desktop\eliaki.txt.txt";
            if (File.Exists(textFile))
            {
                Process.Start(textFile);
            }
        }

    }
}