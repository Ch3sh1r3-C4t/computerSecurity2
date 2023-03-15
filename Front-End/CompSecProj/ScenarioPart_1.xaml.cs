using System.Windows.Controls;
using System.Windows;
using System;
using System.IO;
using System.Diagnostics;

namespace trustid
{
    public partial class ScenarioPart_1 : UserControl
    {

        public ScenarioPart_1()
        {
            InitializeComponent();
        }



        private void MacroOne_Click(object sender, RoutedEventArgs e)
        {

        
                Process.Start(@"C:\Users\lucas\OneDrive\Desktop\ContentManipulation\TrustedDocument");
        

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
            start.FileName = @"C:\Users\lucas\AppData\Local\Programs\Python\Python311\python.exe";
            start.Arguments = "C:\\Users\\lucas\\OneDrive\\Desktop\\ContentManipulation\\contentMan_1.py";
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
           
            
           
            var viewModel = (ContentManipulationViewModel)DataContext;
            if (viewModel.GotoView3Command.CanExecute(null))
                viewModel.GotoView3Command.Execute(null);

        }
    }
}