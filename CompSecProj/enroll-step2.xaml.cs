using System.Windows;
using System.Windows.Input;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
using System;
using System.Windows.Media;

namespace trustid
{
    /// <summary>
    /// Interaction logic for enroll_step2.xaml
    /// </summary>
    public partial class enroll_step2 : Window
    {
        public enroll_step2()
        {
            InitializeComponent();
        }

        public class CheckupProcessResponse
        {
            public string message { get; set; }
            public string resource_name { get; set; }
            public IList<string> resource_array { get; set; }
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnCancelEnroll_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnProceed_Click(object sender, RoutedEventArgs e)
        {
            webview newWindow = new webview();
            newWindow.Show();
            this.Close();
        }

        private async void btnRunMonitoring_Click(object sender, RoutedEventArgs e)
        {
            Process[] localAll = Process.GetProcesses();
            List<string> listOfProcesses = new List<string>();

            lblAlerts.Text = "";
            foreach (Process p in localAll)
            {
                //Console.WriteLine(p.ProcessName, p.Id);
                /*if (p.ProcessName == "chrome") lblProcesses.Text += p.ProcessName + "\n";
                if (p.ProcessName == "firefox") lblProcesses.Text += p.ProcessName + "\n";
                if (p.ProcessName.Contains("team")) lblProcesses.Text += p.ProcessName + "\n";

                lblProcesses.Text += p.ProcessName + "\n";*/
                listOfProcesses.Add(p.ProcessName);
                /* Kill process
                if (p.ProcessName == "chrome")
                {
                    Process processToKill = Process.GetProcessById(p.Id);
                    processToKill.Kill();
                }
                */
            }

            // Create the http request
            const string Url = "https://api.trustid-project.eu/backend/student/checkup_process/";

            // Disable ssl certificate errors
            // ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            string[] running_processes = listOfProcesses.Distinct().ToArray();

            try
            {
                var request = WebRequest.Create(Url);
                request.Method = "POST";
                request.Headers["Authorization"] = "Bearer " + Globals.jwt_access;

                Dictionary<string, IList<string>> values = new Dictionary<string, IList<string>>();
                values.Add("running_processes", running_processes);

                var json = System.Text.Json.JsonSerializer.Serialize(values);
                byte[] byteArray = Encoding.UTF8.GetBytes(json);
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;
                var reqStream = request.GetRequestStream();
                reqStream.Write(byteArray, 0, byteArray.Length);
                var response = request.GetResponse();
                var respStream = response.GetResponseStream();
                var reader = new StreamReader(respStream);
                string data = reader.ReadToEnd();

                CheckupProcessResponse checkup_resp = JsonConvert.DeserializeObject<CheckupProcessResponse>(data);

                if (checkup_resp.resource_array.Count > 0) {
                    lblProcesses.Text = "";
                    foreach (string item in checkup_resp.resource_array)
                    {
                        lblProcesses.Text = lblProcesses.Text + item + "\n";
                    }
                }
                else
                {
                    Console.WriteLine("Proceed");
                    lblProcesses.Text = "Checkup processes successfully completed. Please proceed.";
                    SolidColorBrush success_color = new SolidColorBrush(Globals.ConvertStringToColor("#19A05F"));

                    Step2.Foreground = success_color;
                    IdentityVerification.Text = "Identity Verification Completed";
                    IdentityVerification.Foreground = success_color;
                    btnProceed.Visibility = Visibility.Visible;
                    btnRunMonitoring.IsEnabled = false;
                    btnRunMonitoring.Foreground = System.Windows.Media.Brushes.White;
                    btnRunMonitoring.Background = System.Windows.Media.Brushes.DarkGray;
                }
            }
            catch (WebException exc)
            {
                var status_code = ((HttpWebResponse)exc.Response).StatusCode;

                if (status_code == HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("Unauthorized");
                    //textBoxErrorMsg.Text = "Unauthorized";
                }
                else if (status_code == HttpStatusCode.NotFound)
                {
                    Console.WriteLine("User not found");
                    //textBoxErrorMsg.Text = "User not found";
                }
                else if (status_code == HttpStatusCode.InternalServerError)
                {
                    Console.WriteLine("Internal server error occurred. Try again later.");
                    // textBoxErrorMsg.Text = "Internal server error occurred. Try again later.";
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("Application error occurred. Try again later: {0}", exc.Message);
                //textBoxErrorMsg.Text = "Application error occurred. Try again later.";
            }
        }
    }
}
