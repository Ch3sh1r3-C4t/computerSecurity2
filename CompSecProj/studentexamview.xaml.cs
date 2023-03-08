using System;
using System.Windows;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace trustid
{
    /// <summary>
    /// Interaction logic for studentexamview.xaml
    /// </summary>
    public partial class studentexamview : Window
    {
        public studentexamview()
        {
            InitializeComponent();
        }

        private void btnShowAlerts_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Test");
        }

        private void btnGetProcesses_Click(object sender, RoutedEventArgs e)
        {
            Process[] localAll = Process.GetProcesses();

            lblAlerts.Text = "";
            foreach (Process p in localAll)
            {
                //Console.WriteLine(p.ProcessName, p.Id);
                if (p.ProcessName == "chrome") lblAlerts.Text += p.ProcessName + "\n";
                if (p.ProcessName == "firefox") lblAlerts.Text += p.ProcessName + "\n";
                if (p.ProcessName.Contains("team")) lblAlerts.Text += p.ProcessName + "\n";

                lblAlerts.Text += p.ProcessName + "\n";

                /* Kill process
                if (p.ProcessName == "chrome")
                {
                    Process processToKill = Process.GetProcessById(p.Id);
                    processToKill.Kill();
                }
                */
            }
        }
    }

}
