using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace trustid
{
    /// <summary>
    /// Interaction logic for ucEnrollStepOne.xaml
    /// </summary>
    public partial class ucEnrollStepOne : UserControl
    {
        private WebcamStreaming _webcamStreaming;
        DispatcherTimer dispatcherTimer;

         public class PollVerificationStatusResponse
        {
            public string message { get; set; }
            public string resource_name { get; set; }
            public bool resource_bool { get; set; }
        }

        public ucEnrollStepOne()
        {
            InitializeComponent();
          
            cmbCameraDevices.SelectedIndex = 0;
            cameraLoading.Visibility = Visibility.Collapsed;
        }

        public class StudentIdentificationResponse
        {
            public string message { get; set; }
            public string resource_name { get; set; }
            public string resource_str { get; set; }
        }

        private async void InitCamera()
        {
            cameraLoading.Visibility = Visibility.Visible;
            webcamContainer.Visibility = Visibility.Hidden;


            cameraLoading.Visibility = Visibility.Collapsed;
            webcamContainer.Visibility = Visibility.Visible;
        }

        private void btnGetStarted_Click(object sender, RoutedEventArgs e)
        {
            InitCamera();
            btnGetStarted.Visibility = Visibility.Collapsed;
            btnCapture.Visibility = Visibility.Visible;
        }

        private async void btnCapture_Click(object sender, RoutedEventArgs e)
        {
            btnCapture.IsEnabled = false;
            btnCapture.Background = System.Windows.Media.Brushes.DarkGray;

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

            try
            {
                var client = new RestClient("https://api.trustid-project.eu/backend/student/identification/");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Globals.jwt_access);
                request.AddJsonBody(new
                {
                    image = SigBase64,
                    exam_id = Globals.exam_id
                });
                IRestResponse restResponse = await client.ExecuteAsync(request);
                int status_code = (int)restResponse.StatusCode;

                if (status_code == ((int)HttpStatusCode.OK))
                {
                    Console.WriteLine("Success");
                    StudentIdentificationResponse identification_resp = JsonConvert.DeserializeObject<StudentIdentificationResponse>(restResponse.Content);
                    Console.WriteLine("Identified resource_name: " + identification_resp.resource_name);
                    Console.WriteLine("Identified resource_str: " + identification_resp.resource_str);

                    string current_user = identification_resp.resource_str;
                    bool IsIdentified = false;
                    string IdentifiedUser = current_user;
                    string PopupMessage = string.Empty;

                    if (String.IsNullOrEmpty(current_user))
                    {
                        current_user = "Unknown";
                        IsIdentified = false;
                        PopupMessage = "We were not able to identify you. Please retry capture";
                    }
                    else {
                        IsIdentified = true;
                        PopupMessage = "We identified you as " + current_user;
                    }

                    var oPopup = new popup(IsIdentified, IdentifiedUser, PopupMessage);
                    oPopup.ShowDialog();
                    int PopupResult = oPopup.PopupResult; // 1: Proceed Next Step; 2: Not Me; 3: Dismiss

                    //btnCapture.SetBinding(Button.CommandProperty, new Binding("GotoView2Command"));

                    if (PopupResult == 1)
                    {
                        await _webcamStreaming.Stop();
                        Globals.verification_retry_attempts = 0;

                        btnCapture.Visibility = Visibility.Collapsed;
                        lblGeneralInformationTitle.Text = "All good";
                        lblGeneralInformationSubtitle.Text = "You can proceed to the next step";
                        btnNextStep.Visibility = Visibility.Visible;
                    }
                    else if (PopupResult == 2 || PopupResult == 3)
                    {
                        Globals.verification_retry_attempts += 1;
                        Console.WriteLine("Manual verification");
                    }

                    if (Globals.verification_retry_attempts == 3)
                    {
                        btnCapture.Visibility = Visibility.Collapsed;
                        btnRequestManualApproval.Visibility = Visibility.Visible;
                        Globals.verification_retry_attempts = 0;
                    }
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

            btnCapture.IsEnabled = true;
            btnCapture.Background = new SolidColorBrush(Utilities.ConvertStringToColor("#0E71EB"));
        }

        private void btnRequestManualApproval_Click(object sender, RoutedEventArgs e)
        {
            btnRequestManualApproval.Visibility = Visibility.Collapsed;
            pnlGeneralInformation.Visibility = Visibility.Collapsed;
            pnlWaitingForInstructorsApproval.Visibility = Visibility.Visible;
            request_instructor_approval();

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(poll_verification_status);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1); // make a polling request every 1 sec.
            dispatcherTimer.Start();
        }

        private async void request_instructor_approval()
        {
            try
            {
                var client = new RestClient("https://api.trustid-project.eu/backend/student/request_manual_approval/");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Globals.jwt_access);
                request.AddJsonBody(new
                {
                    exam_id = Globals.exam_id
                });
                IRestResponse restResponse = await client.ExecuteAsync(request);
                int status_code = (int)restResponse.StatusCode;

                if (status_code == ((int)HttpStatusCode.OK))
                {
                    Console.WriteLine("Success");
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

        private async void poll_verification_status(object sender, EventArgs e)
        {
            try
            {
                var client = new RestClient("https://api.trustid-project.eu/backend/student/check_verification_status/");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Globals.jwt_access);
                request.AddJsonBody(new
                {
                    exam_id = Globals.exam_id
                });
                IRestResponse restResponse = await client.ExecuteAsync(request);
                int status_code = (int)restResponse.StatusCode;

                if (status_code == ((int)HttpStatusCode.OK))
                {
                    Console.WriteLine("Success");
                    PollVerificationStatusResponse poll_verification_resp = JsonConvert.DeserializeObject<PollVerificationStatusResponse>(restResponse.Content);
                    Console.WriteLine("Poll verification status resource_bool: " + poll_verification_resp.resource_bool);
                    Console.WriteLine("Poll verification status message: " + poll_verification_resp.message);

                    if (poll_verification_resp.resource_bool == true)
                    {
                        Console.WriteLine("Polling verification status true. Stopping periodic polling task.");
                        dispatcherTimer.Stop();
                        btnCapture.Visibility = Visibility.Collapsed;
                        lblGeneralInformationTitle.Text = "All good";
                        lblGeneralInformationSubtitle.Text = "You can proceed to the next step";
                        btnNextStep.Visibility = Visibility.Visible;
                        pnlGeneralInformation.Visibility = Visibility.Visible;
                        pnlWaitingForInstructorsApproval.Visibility = Visibility.Collapsed;
                    }
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

        //private async void btnStartCamera_Click(object sender, RoutedEventArgs e)
        //{
        //    Console.WriteLine("Send req");
        //    //imgBox.Source = webcamPreview.Source;
        //    var SigBase64 = "";
        //    Bitmap bmpOut = null;

        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        PngBitmapEncoder encoder = new PngBitmapEncoder();
        //        encoder.Frames.Add(BitmapFrame.Create((BitmapSource)webcamPreview.Source));
        //        encoder.Save(ms);

        //        using (Bitmap bmp = new Bitmap(ms))
        //        {
        //            bmpOut = new Bitmap(bmp);
        //            bmpOut.Save("res.bmp");
        //            Bitmap bImage = bmpOut;  // Your Bitmap Image
        //            System.IO.MemoryStream ms1 = new MemoryStream();
        //            bImage.Save(ms1, ImageFormat.Jpeg);
        //            byte[] byteImage = ms1.ToArray();
        //            SigBase64 = Convert.ToBase64String(byteImage);
        //        }
        //    }
        //    Console.WriteLine("sigbase: " + SigBase64);

        //    // Create the http request
        //    const string Url = "https://api.trustid-project.eu/backend/student/identification/";

        //    // Disable ssl certificate errors
        //    // ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

        //    try
        //    {
        //        var request = WebRequest.Create(Url);
        //        request.Method = "POST";
        //        request.Headers["Authorization"] = "Bearer " + Globals.jwt_access;

        //        Dictionary<string, string> values = new Dictionary<string, string>();
        //        values.Add("image", SigBase64);

        //        var json = System.Text.Json.JsonSerializer.Serialize(values);
        //        byte[] byteArray = Encoding.UTF8.GetBytes(json);
        //        request.ContentType = "application/json";
        //        request.ContentLength = byteArray.Length;
        //        var reqStream = request.GetRequestStream();
        //        reqStream.Write(byteArray, 0, byteArray.Length);
        //        var response = request.GetResponse();
        //        var respStream = response.GetResponseStream();
        //        var reader = new StreamReader(respStream);
        //        string data = reader.ReadToEnd();

        //        StudentIdentificationResponse identification_resp = JsonConvert.DeserializeObject<StudentIdentificationResponse>(data);
        //        Console.WriteLine("Identified resource_name: " + identification_resp.resource_name);
        //        Console.WriteLine("Identified resource_str: " + identification_resp.resource_str);

        //        string current_user = identification_resp.resource_str;

        //        if (String.IsNullOrEmpty(current_user))
        //        {
        //            current_user = "Unknown";
        //        }

        //        MessageBoxResult result = MessageBox.Show("We identified you as " + current_user,
        //                                      "Confirmation",
        //                                      MessageBoxButton.YesNo,
        //                                      MessageBoxImage.Question);

        //        if (result == MessageBoxResult.Yes)
        //        {
        //            await _webcamStreaming.Stop();
        //            Globals.verification_retry_attempts = 0;
        //            enroll_step2 newWindow = new enroll_step2();
        //            newWindow.Show();
        //            this.Close();
        //        }
        //        else
        //        {
        //            Globals.verification_retry_attempts += 1;
        //            Console.WriteLine("Manual verification");
        //        }

        //        if (Globals.verification_retry_attempts == 3)
        //        {
        //            btnContinueWithManual.Visibility = Visibility.Visible;
        //            btnStartCamera.IsEnabled = false;
        //            btnStartCamera.Foreground = System.Windows.Media.Brushes.White;
        //            btnStartCamera.Background = System.Windows.Media.Brushes.DarkGray;
        //            Globals.verification_retry_attempts = 0;
        //        }

        //    }
        //    catch (WebException exc)
        //    {
        //        var status_code = ((HttpWebResponse)exc.Response).StatusCode;

        //        if (status_code == HttpStatusCode.Unauthorized)
        //        {
        //            Console.WriteLine("Unauthorized");
        //            //textBoxErrorMsg.Text = "Unauthorized";
        //        }
        //        else if (status_code == HttpStatusCode.NotFound)
        //        {
        //            Console.WriteLine("User not found");
        //            //textBoxErrorMsg.Text = "User not found";
        //        }
        //        else if (status_code == HttpStatusCode.InternalServerError)
        //        {
        //            Console.WriteLine("Internal server error occurred. Try again later.");
        //            //textBoxErrorMsg.Text = "Internal server error occurred. Try again later.";
        //        }
        //    }
        //    catch (Exception exc)
        //    {
        //        Console.WriteLine("Application error occurred. Try again later: {0}", exc.Message);
        //        //textBoxErrorMsg.Text = "Application error occurred. Try again later.";
        //    }
        //}
    }
}
