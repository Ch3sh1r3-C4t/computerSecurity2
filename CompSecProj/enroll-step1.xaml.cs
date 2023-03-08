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

namespace trustid
{
    /// <summary>
    /// Interaction logic for enroll_step1.xaml
    /// </summary>
    public partial class enroll_step1 : Window
    {
        private WebcamStreaming _webcamStreaming;

        public enroll_step1()
        {
            InitializeComponent();
        
            cmbCameraDevices.SelectedIndex = 0;
            cameraLoading.Visibility = Visibility.Collapsed;
            InitCamera();
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

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private async void btnStartCamera_Click(object sender, RoutedEventArgs e)
        {
            var SigBase64 = "";
            Bitmap bmpOut = null;
            enroll_step2 newWindow = new enroll_step2();

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
                    image = SigBase64
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

                    if (String.IsNullOrEmpty(current_user))
                    {
                        current_user = "Unknown";
                    }

                    MessageBoxResult result = MessageBox.Show("We identified you as " + current_user,
                                                  "Confirmation",
                                                  MessageBoxButton.YesNo,
                                                  MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        await _webcamStreaming.Stop();
                        Globals.verification_retry_attempts = 0;
                        newWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        Globals.verification_retry_attempts += 1;
                        Console.WriteLine("Manual verification");
                    }

                    if (Globals.verification_retry_attempts == 3)
                    {
                        btnContinueWithManual.Visibility = Visibility.Visible;
                        btnStartCamera.IsEnabled = false;
                        btnStartCamera.Foreground = System.Windows.Media.Brushes.White;
                        btnStartCamera.Background = System.Windows.Media.Brushes.DarkGray;
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

        private void btnCancelEnroll_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnContinueWithManual_Click(object sender, RoutedEventArgs e)
        {
            await _webcamStreaming.Stop();
            enroll_step2 newWindow = new enroll_step2();
            newWindow.Show();
            this.Close();
        }
    }
}
