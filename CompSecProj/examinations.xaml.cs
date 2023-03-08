using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace trustid
{
    /// <summary>
    /// Interaction logic for dashboard.xaml
    /// </summary>
    public partial class examinations : Window
    {
        public examinations()
        {
            InitializeComponent();
            //LoadStudentExams();
        }
        public class ListExamsResponse
        {
            public string message { get; set; }
            public string resource_name { get; set; }

            public IList<IDictionary<string, string>> resource_array { get; set; }
        }

        private void LoadStudentExams()
        {
            // Create the http request
            const string Url = "https://api.trustid-project.eu/backend/student/list_exam/";

            // Disable ssl certificate errors
            // ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

            try
            {
                var request = WebRequest.Create(Url);
                request.Method = "GET";
                request.Headers["Authorization"] = "Bearer " + Globals.jwt_access;
                request.ContentType = "application/json";
                var response = request.GetResponse();
                var respStream = response.GetResponseStream();
                var reader = new StreamReader(respStream);
                string data = reader.ReadToEnd();
                ListExamsResponse list_exams_resp = JsonConvert.DeserializeObject<ListExamsResponse>(data);

                Console.WriteLine("data: " + list_exams_resp);
                int rectangleIdx = 0;

                foreach (IDictionary<string, string> item in list_exams_resp.resource_array)
                {
                    var exam_id = "";
                    var status = "";
                    var scheduled_date = "";
                    var exam_name = "";
                    item.TryGetValue("exam_id", out exam_id);
                    item.TryGetValue("status", out status);
                    item.TryGetValue("scheduled_date", out scheduled_date);
                    item.TryGetValue("name", out exam_name);

                    var fill_brush = new SolidColorBrush(ConvertStringToColor("#F2F2F5"));
                    var txt_block_fg_brush = new SolidColorBrush(ConvertStringToColor("#333333"));

                    Rectangle rec = new Rectangle()
                    {
                        Width = 270,
                        Height = 70,
                        Margin = new Thickness(10, 10, 10, 10),
                        Fill = fill_brush,
                    };

                    TextBlock txtBlock = new TextBlock();
                    txtBlock.Width = 270;
                    txtBlock.Height = 70;
                    txtBlock.Foreground = txt_block_fg_brush;
                    txtBlock.Text = exam_name;
                    txtBlock.FontWeight = FontWeights.Bold;
                    txtBlock.FontSize = 14;
                    txtBlock.Margin = new Thickness(30, 10, 10, 10);
                    txtBlock.VerticalAlignment = VerticalAlignment.Center;
                    txtBlock.HorizontalAlignment = HorizontalAlignment.Left;

                    TextBlock txtBlockScheduledDate = new TextBlock();
                    txtBlockScheduledDate.Width = 270;
                    txtBlockScheduledDate.Height = 70;
                    txtBlockScheduledDate.Foreground = txt_block_fg_brush;
                    txtBlockScheduledDate.Text = scheduled_date;
                    txtBlockScheduledDate.Margin = new Thickness(30, 50, 10, 10);
                    txtBlockScheduledDate.VerticalAlignment = VerticalAlignment.Center;
                    txtBlockScheduledDate.HorizontalAlignment = HorizontalAlignment.Left;

                    ListOfExams.Children.Add(rec);
                    ListOfExams.Children.Add(txtBlock);
                    ListOfExams.Children.Add(txtBlockScheduledDate);
                    Grid.SetRow(rec, rectangleIdx);
                    Grid.SetRow(txtBlock, rectangleIdx);
                    Grid.SetRow(txtBlockScheduledDate, rectangleIdx);
                    rectangleIdx += 1;

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
                    //textBoxErrorMsg.Text = "Internal server error occurred. Try again later.";
                }
            }
            catch (Exception exc)
            {
                Console.WriteLine("Application error occurred. Try again later: {0}", exc.Message);
                //textBoxErrorMsg.Text = "Application error occurred. Try again later.";
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnSelectExam1_MouseDown(object sender, RoutedEventArgs e)
        {
            
            btnSelectExam2.Fill = new SolidColorBrush(ConvertStringToColor("#F2F2F5"));
            txtExam2.Foreground = new SolidColorBrush(ConvertStringToColor("#333333"));

            btnSelectExam1.Fill = new SolidColorBrush(ConvertStringToColor("#0E72ED"));
            txtExam1.Foreground = new SolidColorBrush(ConvertStringToColor("#ffffff"));
            pnlExam1.Visibility = Visibility.Visible;
            pnlExam2.Visibility = Visibility.Hidden;
        }

        private void btnSelectExam2_MouseDown(object sender, RoutedEventArgs e)
        {
            //var rect = btnSelectExam2.Template.FindName("recExam", btnSelectExam2) as System.Windows.Shapes.Rectangle;
            //rect.Fill = new SolidColorBrush(ConvertStringToColor("#0E72ED"));
            
            
            btnSelectExam1.Fill = new SolidColorBrush(ConvertStringToColor("#F2F2F5"));
            txtExam1.Foreground = new SolidColorBrush(ConvertStringToColor("#333333"));

            btnSelectExam2.Fill = new SolidColorBrush(ConvertStringToColor("#0E72ED"));
            txtExam2.Foreground = new SolidColorBrush(ConvertStringToColor("#ffffff"));
            pnlExam1.Visibility = Visibility.Hidden;
            pnlExam2.Visibility = Visibility.Visible;
        }

        public System.Windows.Media.Color ConvertStringToColor(String hex)
        {
            //remove the # at the front
            hex = hex.Replace("#", "");

            byte a = 255;
            byte r = 255;
            byte g = 255;
            byte b = 255;

            int start = 0;

            //handle ARGB strings (8 characters long)
            if (hex.Length == 8)
            {
                a = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                start = 2;
            }

            //convert RGB characters to bytes
            r = byte.Parse(hex.Substring(start, 2), System.Globalization.NumberStyles.HexNumber);
            g = byte.Parse(hex.Substring(start + 2, 2), System.Globalization.NumberStyles.HexNumber);
            b = byte.Parse(hex.Substring(start + 4, 2), System.Globalization.NumberStyles.HexNumber);

            return System.Windows.Media.Color.FromArgb(a, r, g, b);
        }

        private void btnStartExam1_Click(object sender, RoutedEventArgs e)
        {
            enroll_step1 newWindow = new enroll_step1();
            newWindow.Show();
        }
    }
}
