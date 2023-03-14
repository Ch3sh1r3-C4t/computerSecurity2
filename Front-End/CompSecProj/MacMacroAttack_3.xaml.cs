using System.Windows.Controls;
using System.Windows;
using System;
using System.IO;
using System.Diagnostics;

namespace trustid
{
    /// <summary>
    /// Interaction logic for ucEnrollStepTwo.xaml
    /// </summary>
    public partial class MacMacroAttack_3 : UserControl
    {

        public MacMacroAttack_3()
        {
            InitializeComponent();
        }

      

        private void filepath_Copy1_TextChanged(object sender, TextChangedEventArgs e)
        {
            ContentsButton.Visibility = Visibility.Visible;

        }

        private void ContentsButton_Click(object sender, RoutedEventArgs e)
        {


            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"C:\Program Files\WindowsApps\PythonSoftwareFoundation.Python.3.10_3.10.2800.0_x64__qbz5n2kfra8p0\python3.10.exe";
            start.Arguments = "C:\\Users\\Elian\\Documents\\elia.py";
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


            string textFile = "C:\\Users\\Elian\\Documents\\eliaki.txt.txt";
            if (File.Exists(textFile))
            {
                // Read entire text file content in one string
                string text = File.ReadAllText(textFile);
                Console.WriteLine(text);
           
            }

           

        }

        private void MacroOne_Click(object sender, RoutedEventArgs e)
        {

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"C:\Program Files\WindowsApps\PythonSoftwareFoundation.Python.3.10_3.10.2800.0_x64__qbz5n2kfra8p0\python3.10.exe";
            start.Arguments = "C:\\Users\\Elian\\Documents\\macro_1.py";
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

            string textFile = @"C:\Users\Elian\Desktop\eliaki.txt.txt";
            if (File.Exists(textFile))
            {
                Process.Start(@"C:\Users\Elian\Desktop\eliaki.txt.txt");
            }
        }
    }
}