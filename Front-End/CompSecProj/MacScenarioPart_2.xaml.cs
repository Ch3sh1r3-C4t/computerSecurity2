using System.Windows.Controls;
using System.Windows;
using System;
using System.IO;
using System.Diagnostics;

namespace trustid
{
    public partial class MacScenarioPart_2 : UserControl
    {

        public MacScenarioPart_2()
        {
            InitializeComponent();
        }



        private void MacroOne_Click(object sender, RoutedEventArgs e)
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

            ProcessStartInfo start2 = new ProcessStartInfo();
            start2.FileName = @"C:\Program Files\WindowsApps\PythonSoftwareFoundation.Python.3.10_3.10.2800.0_x64__qbz5n2kfra8p0\python3.10.exe";
            start2.Arguments = "C:\\Users\\Elian\\Documents\\manipulate2.py";
            start2.UseShellExecute = false;
            start2.WorkingDirectory = "";
            start2.RedirectStandardOutput = true;


            using (Process process = Process.Start(start2))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    Console.Write(result);
                }



            }

            attacker.Visibility = Visibility.Visible;


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
            start.Arguments = "C:\\Users\\Elian\\Documents\\manipulate2.py";
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
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"C:\Program Files\WindowsApps\PythonSoftwareFoundation.Python.3.10_3.10.2800.0_x64__qbz5n2kfra8p0\python3.10.exe";
            start.Arguments = "C:\\Users\\Elian\\Documents\\brendonscode.py";
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

        private void MacroResults_Click(object sender, RoutedEventArgs e)
        {
            if(Globals.macroat == 1)
            {
                var viewModel = (MacManipulationViewModel)DataContext;
                if (viewModel.GotoView4Command.CanExecute(null))
                    viewModel.GotoView4Command.Execute(null);
            }
           else if (Globals.macroat == 2)
            {
                var viewModel = (MacManipulationViewModel)DataContext;
                if (viewModel.GotoView5Command.CanExecute(null))
                    viewModel.GotoView5Command.Execute(null);
            }

            else if (Globals.macroat == 3)
            {
                var viewModel = (MacManipulationViewModel)DataContext;
                if (viewModel.GotoView6Command.CanExecute(null))
                    viewModel.GotoView6Command.Execute(null);
            }

            else if (Globals.macroat == 4)
            {
                var viewModel = (MacManipulationViewModel)DataContext;
                if (viewModel.GotoView7Command.CanExecute(null))
                    viewModel.GotoView7Command.Execute(null);
            }
        }

        private void Attack_Click(object sender, RoutedEventArgs e)
        {

            ProcessStartInfo start = new ProcessStartInfo();

            if (Globals.macroat == 1)
            {
                start.Arguments = @"C:\\Users\\lucas\\OneDrive\\Desktop\\MacroManipulation\\AttackCode\\odf_attack.py --attacktype content --trusted_file .\TrustedDocuments\original_macro_document_signed.odt --edited_file .\InjectedMacroOnes\POCDownloadImage.odt --output_file attackerWIN.odt";

            }
            else if (Globals.macroat == 2)
            {
                start.Arguments = @"C:\\Users\\lucas\\OneDrive\\Desktop\\MacroManipulation\\AttackCode\\odf_attack.py --attacktype content --trusted_file .\TrustedDocuments\original_macro_document_signed.odt --edited_file .\InjectedMacroOnes\POCEnumAndSendBackResults.odt --output_file attackerWIN.odt";

            }
            else if (Globals.macroat == 3)
            {
                start.Arguments = @"C:\\Users\\lucas\\OneDrive\\Desktop\\MacroManipulation\\AttackCode\\odf_attack.py --attacktype content --trusted_file .\TrustedDocuments\original_macro_document_signed.odt --edited_file .\InjectedMacroOnes\KeyLogger.odt --output_file attackerWIN.odt";
            }

            else if (Globals.macroat == 4)
            {
                start.Arguments = @"C:\\Users\\lucas\\OneDrive\\Desktop\\MacroManipulation\\AttackCode\\odf_attack.py --attacktype content --trusted_file .\TrustedDocuments\original_macro_document_signed.odt --edited_file .\InjectedMacroOnes\POC-msf.odt --output_file attackerWIN.odt";
            }





            start.FileName = @"C:\Program Files\WindowsApps\PythonSoftwareFoundation.Python.3.10_3.10.2800.0_x64__qbz5n2kfra8p0\python3.10.exe";
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

            Attack.Visibility = Visibility.Hidden;
            FileOpen.Visibility = Visibility.Visible;



        
    }
    }
}