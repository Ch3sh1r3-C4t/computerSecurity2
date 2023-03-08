using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using RestSharp;

namespace trustid
{
    /// <summary>
    /// Interaction logic for ucExams.xaml
    /// </summary>
    public partial class ucExamsInstructors : UserControl
    {
        public ucExamsInstructors()
        {
            InitializeComponent();
            examNameLeftPanel.Text = Globals.exam_name;
            examIdLeftPanel.Text = examIdLeftPanel.Text + Globals.exam_id;
            examStatusLeftPanel.Text = examStatusLeftPanel.Text + Globals.exam_status;
            examScheduledLeftPanel.Text = examScheduledLeftPanel.Text + Globals.exam_scheduled_date;

            examNameRightPanel.Text = Globals.exam_name;
            examIdRightPanel.Text = examIdRightPanel.Text + Globals.exam_id;
            examStatusRightPanel.Text = examStatusRightPanel.Text + Globals.exam_status;
            examScheduledRightPanel.Text = examScheduledRightPanel.Text + Globals.exam_scheduled_date;
        }

        public class ListExamsResponse
        {
            public string message { get; set; }
            public string resource_name { get; set; }

            public IList<IDictionary<string, string>> resource_array { get; set; }
        }

        public class UpdateExamConditionResponse
        {
            public string message { get; set; }
            public string resource_name { get; set; }
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

                    var fill_brush = new SolidColorBrush(Utilities.ConvertStringToColor("#F2F2F5"));
                    var txt_block_fg_brush = new SolidColorBrush(Utilities.ConvertStringToColor("#333333"));

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

        private void btnSelectExam1_MouseDown(object sender, RoutedEventArgs e)
        {

            btnSelectExam2.Fill = new SolidColorBrush(Utilities.ConvertStringToColor("#F2F2F5"));
            txtExam2.Foreground = new SolidColorBrush(Utilities.ConvertStringToColor("#333333"));

            btnSelectExam1.Fill = new SolidColorBrush(Utilities.ConvertStringToColor("#0E72ED"));
            txtExam1.Foreground = new SolidColorBrush(Utilities.ConvertStringToColor("#ffffff"));
            pnlExam1.Visibility = Visibility.Visible;
            pnlExam2.Visibility = Visibility.Hidden;
        }

        private void btnSelectExam2_MouseDown(object sender, RoutedEventArgs e)
        {
            //var rect = btnSelectExam2.Template.FindName("recExam", btnSelectExam2) as System.Windows.Shapes.Rectangle;
            //rect.Fill = new SolidColorBrush(ConvertStringToColor("#0E72ED"));


            btnSelectExam1.Fill = new SolidColorBrush(Utilities.ConvertStringToColor("#F2F2F5"));
            txtExam1.Foreground = new SolidColorBrush(Utilities.ConvertStringToColor("#333333"));

            btnSelectExam2.Fill = new SolidColorBrush(Utilities.ConvertStringToColor("#0E72ED"));
            txtExam2.Foreground = new SolidColorBrush(Utilities.ConvertStringToColor("#ffffff"));
            pnlExam1.Visibility = Visibility.Hidden;
            pnlExam2.Visibility = Visibility.Visible;
        }

        private async void update_exam_condition()
        {
            try
            {
                var client = new RestClient("https://api.trustid-project.eu/backend/student/update_exam_condition/");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Globals.jwt_access);
                request.AddJsonBody(new
                {
                    condition = "Join",
                    exam_id = Globals.exam_id
                });
                IRestResponse restResponse = await client.ExecuteAsync(request);
                int status_code = (int)restResponse.StatusCode;

                if (status_code == ((int)HttpStatusCode.OK))
                {
                    Console.WriteLine("Success");
                    UpdateExamConditionResponse update_exam_resp = JsonConvert.DeserializeObject<UpdateExamConditionResponse>(restResponse.Content);
                    Console.WriteLine("Update exam resource_name: " + update_exam_resp.resource_name);
                    Console.WriteLine("Update exam message: " + update_exam_resp.message);
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

        private void btnStartExam1_Click(object sender, RoutedEventArgs e)
        {
            // First update exam condition
            update_exam_condition();

            instructors_view newWindow = new instructors_view();
            newWindow.Show();
        }

        private void btnViewInformation_Click(object sender, RoutedEventArgs e)
        {
            pnlInformation.Visibility = Visibility.Visible;
            pnlPolicy.Visibility = Visibility.Collapsed;
        }

        private void btnViewPolicy_Click(object sender, RoutedEventArgs e)
        {
            pnlInformation.Visibility = Visibility.Collapsed;
            pnlPolicy.Visibility = Visibility.Visible;
        }
    }
}
