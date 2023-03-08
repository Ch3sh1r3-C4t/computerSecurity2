using RestSharp;
using System;
using System.Net;
using System.Windows.Controls;
using System.Windows;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;
using System.Windows.Threading;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

namespace trustid
{
    /// <summary>
    /// Interaction logic for ucEnrollStepTwo.xaml
    /// </summary>
    public partial class ucExamination : UserControl
    {
        private WebcamStreaming _webcamStreaming;
        DispatcherTimer dispatcherTimer;

        public class MonitoringResponse
        {
            public string message { get; set; }
            public string resource_name { get; set; }
        }

        public ucExamination()
        {
        
        }

        private async void monitor_activity(object sender, EventArgs e)
        {
            if (Globals.is_monitoring_active == false)
            {
                return;
            }

            // Step 1 - Capture image from camera
            var SigBase64 = "";
            Bitmap bmpOut = null;

            using (MemoryStream ms = new MemoryStream())
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)webcamPreview.Source));
                encoder.Save(ms);

                using (Bitmap bmp = new Bitmap(ms))
                {
                    bmpOut = new Bitmap(bmp);
                    bmpOut.Save("res.bmp");
                    Bitmap bImage = bmpOut;  // Your Bitmap Image
                    System.IO.MemoryStream ms1 = new MemoryStream();
                    bImage.Save(ms1, ImageFormat.Jpeg);
                    byte[] byteImage = ms1.ToArray();
                    SigBase64 = Convert.ToBase64String(byteImage);
                }
            }

            // Step 2 - Find running processes
            Process[] localAll = Process.GetProcesses();
            List<string> listOfProcesses = new List<string>();
            foreach (Process p in localAll)
            {
                listOfProcesses.Add(p.ProcessName);
            }

            string[] running_processes = listOfProcesses.Distinct().ToArray();

            try
            {
                var client = new RestClient("https://api.trustid-project.eu/backend/monitoring/");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Globals.jwt_access);
                request.AddJsonBody(new
                {
                    image = SigBase64,
                    exam_id = Globals.exam_id,
                    running_processes = running_processes
                });
                IRestResponse restResponse = await client.ExecuteAsync(request);
                int status_code = (int)restResponse.StatusCode;

                if (status_code == ((int)HttpStatusCode.OK))
                {
                    Console.WriteLine("Success");
                    MonitoringResponse monitoring_resp = JsonConvert.DeserializeObject<MonitoringResponse>(restResponse.Content);
                    Console.WriteLine("Identified resource_name: " + monitoring_resp.resource_name);

                }
                else if (status_code == ((int)HttpStatusCode.Unauthorized))
                {
                    Console.WriteLine("Unauthorized");
                    //textBoxErrorMsg.Text = "Incorrect login details";
                }
                else if (status_code == ((int)HttpStatusCode.NotFound))
                {
                    Console.WriteLine("User not found");
                    //textBoxErrorMsg.Text = "User not found";
                }
                else if (status_code == ((int)HttpStatusCode.InternalServerError))
                {
                    Console.WriteLine("Internal server error occurred. Try again later.");
                    //textBoxErrorMsg.Text = "Internal server error occurred. Try again later.";
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("Application error occurred. Try again later: {0}", exc.Message);
                //textBoxErrorMsg.Text = "Application error occurred. Try again later.";
            }


        }

        private void btnSendUserFeedback_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            submit_feedback();
        }

        private async void InitCamera()
        {
            webcamContainer.Visibility = Visibility.Hidden;

          
        }


        private async void submit_feedback()
        {
            if (btnImpersonation.IsChecked == false && btnForbiddenApp.IsChecked == false)
            {
                Console.WriteLine("No cheat mode selected");
                var oPopup = new popup(false, "", "You must select one feedback mechanism");
                oPopup.ShowDialog();
                return;
            }

            if (txtFeedback.Text.Length == 0)
            {
                Console.WriteLine("No feedback provided");
                var oPopup = new popup(false, "", "You must provide feedback description");
                oPopup.ShowDialog();
                return;
            }

            string cheat_mode = "Impersonation";

            if (btnForbiddenApp.IsChecked == true)
            {
                cheat_mode = "Communication";
            }

            try
            {
                var client = new RestClient("https://api.trustid-project.eu/backend/student/submit_feedback/");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Globals.jwt_access);
                request.AddJsonBody(new
                {
                    cheat_mode = cheat_mode,
                    exam_id = Globals.exam_id,
                    feedback = txtFeedback.Text
                });
                IRestResponse restResponse = await client.ExecuteAsync(request);
                int status_code = (int)restResponse.StatusCode;

                if (status_code == ((int)HttpStatusCode.OK))
                {
                    Console.WriteLine("Success");
                    txtFeedback.Text = "";
                    btnImpersonation.IsChecked = false;
                    btnForbiddenApp.IsChecked = false;
                }
                else if (status_code == ((int)HttpStatusCode.Unauthorized))
                {
                    Console.WriteLine("Unauthorized");
                    //textBoxErrorMsg.Text = "Incorrect login details";
                }
                else if (status_code == ((int)HttpStatusCode.NotFound))
                {
                    Console.WriteLine("User not found");
                    //textBoxErrorMsg.Text = "User not found";
                }
                else if (status_code == ((int)HttpStatusCode.InternalServerError))
                {
                    Console.WriteLine("Internal server error occurred. Try again later.");
                    //textBoxErrorMsg.Text = "Internal server error occurred. Try again later.";
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