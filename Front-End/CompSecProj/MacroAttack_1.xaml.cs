using System.Windows.Controls;
using System.Windows;
using System;
using System.IO;
using System.Diagnostics;

namespace trustid
{
    public partial class MacroAttack_1 : UserControl
    {

        public MacroAttack_1()
        {
            InitializeComponent();
        }



        private void MacroOne_Click(object sender, RoutedEventArgs e)
        {

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"C:\Users\lucas\AppData\Local\Programs\Python\Python311\python.exe";
            start.Arguments = "C:\\Users\\lucas\\OneDrive\\Desktop\\MacroFiles\\macro_1.py";
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
            picture.Visibility = Visibility.Visible;
        }

        private void picture_Click(object sender, RoutedEventArgs e)
        {

            string textFile = @"C:\Users\lucas\image.png";
            if (File.Exists(textFile))
            {
                Process.Start(@"C:\Users\lucas\image.png");
            }
        }
}
}