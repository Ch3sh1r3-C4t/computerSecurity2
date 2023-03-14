using System.Windows.Controls;
using System.Windows;
using System;
using System.IO;
using System.Diagnostics;

namespace trustid
{
    public partial class MacScenarioPart_1 : UserControl
    {

        public MacScenarioPart_1()
        {
            InitializeComponent();
        }



        private void MacroOne_Click(object sender, RoutedEventArgs e)
        {

        
                Process.Start(@"C:\Users\Elian\Documents");
        

        }

        private void picture_Click(object sender, RoutedEventArgs e)
        {

            string textFile = @"C:\Users\Elian\Desktop\ucl.png";
            if (File.Exists(textFile))
            {
                Process.Start(@"C:\Users\Elian\Desktop\ucl.png");
            }
        }

        private void FileOpen_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"C:\Program Files\WindowsApps\PythonSoftwareFoundation.Python.3.10_3.10.2800.0_x64__qbz5n2kfra8p0\python3.10.exe";
            start.Arguments = "C:\\Users\\Elian\\Documents\\contentMan1.py";
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

            attacker.Visibility = Visibility.Visible;
            Next.Visibility = Visibility.Visible;

        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            string arg = "";
            if (Globals.macroat==1)
            {
                 arg = "C:\\Users\\Elian\\Documents\\brendonscode.py";
            }
            else if (Globals.macroat==2)
            {
               arg = "C:\\Users\\Elian\\Documents\\brendonscode.py";
            }

            else if (Globals.macroat==3)
            {
                arg = "C:\\Users\\Elian\\Documents\\brendonscode.py";
            }

            else if (Globals.macroat==4)
            {
                arg = "C:\\Users\\Elian\\Documents\\brendonscode.py";
            }

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"C:\Program Files\WindowsApps\PythonSoftwareFoundation.Python.3.10_3.10.2800.0_x64__qbz5n2kfra8p0\python3.10.exe";
            start.Arguments = arg;
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
           
            var viewModel = (MacManipulationViewModel)DataContext;
            if (viewModel.GotoView3Command.CanExecute(null))
                viewModel.GotoView3Command.Execute(null);

        }
    }
}