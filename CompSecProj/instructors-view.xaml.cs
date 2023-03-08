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
using System.Windows.Controls;
using RestSharp;
using System.Windows.Threading;

namespace trustid
{
    /// <summary>
    /// Interaction logic for instructors_view.xaml
    /// </summary>
    public partial class instructors_view : Window
    {
        DispatcherTimer dispatcherTimer;

        public instructors_view()
        {
            InitializeComponent();
            ShowEnrolledStudentList();

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(FetchEnrolledStudentsList);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 5); // make a polling request.
            dispatcherTimer.Start();
        }

        public class InstructorListExamResponse
        {
            public string message { get; set; }
            public string resource_name { get; set; }
            public IList<IDictionary<string, dynamic>> resource_array { get; set; }
        }

        private async void FetchEnrolledStudentsList(object sender, EventArgs e)
        {
            try
            {
                var client = new RestClient("https://api.trustid-project.eu/backend/instructor/list_exam/");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Globals.jwt_access);

                IRestResponse restResponse = await client.ExecuteAsync(request);
                int status_code = (int)restResponse.StatusCode;

                if (status_code == ((int)HttpStatusCode.OK))
                {
                    Console.WriteLine("Success");
                    InstructorListExamResponse listexam_resp = JsonConvert.DeserializeObject<InstructorListExamResponse>(restResponse.Content);
                    Console.WriteLine("listexam resource_name: " + listexam_resp.resource_name);
                    Console.WriteLine("listexam resource_array: " + listexam_resp.resource_array);

                    if (listexam_resp.resource_array.Count > 0)
                    {
                        dynamic exam_id = "";
                        dynamic exam_status = "";
                        dynamic scheduled_date = "";
                        dynamic exam_name = "";
                        dynamic enrolled_students;

                        if (listexam_resp.resource_array[0].TryGetValue("name", out exam_name))
                        {
                            Globals.exam_name = exam_name;
                        }

                        if (listexam_resp.resource_array[0].TryGetValue("exam_id", out exam_id))
                        {
                            string str_exam_id = Convert.ToString(exam_id);
                            Globals.exam_id = str_exam_id;
                        }

                        if (listexam_resp.resource_array[0].TryGetValue("status", out exam_status))
                        {
                            Globals.exam_status = exam_status;
                        }

                        if (listexam_resp.resource_array[0].TryGetValue("scheduled_date", out scheduled_date))
                        {
                            string str_scheduled_date = Convert.ToString(scheduled_date);
                            str_scheduled_date = str_scheduled_date.Replace("T", ", ");
                            Globals.exam_scheduled_date = str_scheduled_date;
                        }

                        if (listexam_resp.resource_array[0].TryGetValue("enrolled_students", out enrolled_students))
                        {
                            Globals.enrolled_students = enrolled_students;
                        }
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

            // Finally, refresh the local list of enrolled students
            ShowEnrolledStudentList();
        }


        public void ShowEnrolledStudentList()
        {
            spMain.Children.Clear();

            int i = 0;
            List<StackPanel> listPending = new List<StackPanel>();
            List<StackPanel> listVerified = new List<StackPanel>();
            List<StackPanel> listRequestedManualApproval = new List<StackPanel>();

            foreach (dynamic item in Globals.enrolled_students)
            {
                Console.WriteLine("item email: " + item.email);
                Console.WriteLine("item name: " + item.name);
                Console.WriteLine("item surname: " + item.surname);
                Console.WriteLine("item alerts: " + item.alerts);
                Console.WriteLine("item verification_status: " + item.verification_status);

                StackPanel spInner = new StackPanel()
                {
                    //Name = "spInner" + i,
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 0, 0, 10),
                };
                //RegisterName("spInner" + i, spInner);

                Label lbl1 = new Label()
                {
                    Content = item.name + " " + item.surname + " - " + item.verification_status,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Height = 28,
                };

                Label lbl2 = new Label()
                {
                    Name = "lbl" + i,
                    Content = item.email,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    Height = 28,
                };

                object currLabel = spMain.FindName("lbl" + i);

                if (currLabel != null)
                {
                    UnregisterName("lbl" + i);
                }
                
                RegisterName("lbl" + i, lbl2);

                spInner.Children.Add(lbl1);
                spInner.Children.Add(lbl2);

                if (item.verification_status == "Requested Manual Approval")
                {
                    Button btn = new Button()
                    {
                        Name = "btn" + i,
                        Content = "Approve",
                        HorizontalAlignment = HorizontalAlignment.Left,
                        Margin = new Thickness(lbl2.ActualWidth),
                        Width = 80,

                    };
                    btn.Click += btnApprove_Click;
                    spInner.Children.Add(btn);
                }

                if (item.verification_status == "Pending")
                {
                    listPending.Add(spInner);
                }
                else if (item.verification_status == "Verified")
                {
                    listVerified.Add(spInner);
                }
                else if (item.verification_status == "Requested Manual Approval")
                {
                    listRequestedManualApproval.Add(spInner);
                }

                //spMain.Children.Add(spInner);

                i = i + 1;

            }

            // First add the items that requested manual approval
            foreach (dynamic pending_item in listRequestedManualApproval)
            {
                spMain.Children.Add(pending_item);
            }

            // Then, add the items that are pending
            foreach (dynamic pending_item in listPending)
            {
                spMain.Children.Add(pending_item);
            }

            // Finally, add the items that are already verified
            foreach (dynamic pending_item in listVerified)
            {
                spMain.Children.Add(pending_item);
            }
        }

        private async void btnApprove_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.IsEnabled = false;
            button.Content = "Approving ...";
            string Email = button.Name.ToString().Replace("btn", "");

            Label oNode = (Label)spMain.FindName("lbl" + Email);

            Console.WriteLine(oNode.Content);

            string student_email = oNode.Content.ToString();

            try
            {
                var client = new RestClient("https://api.trustid-project.eu/backend/instructor/manual_approve_student/");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", "Bearer " + Globals.jwt_access);
                request.AddJsonBody(new
                {
                    exam_id = Globals.exam_id,
                    email = oNode.Content.ToString()
                });
                IRestResponse restResponse = await client.ExecuteAsync(request);
                int status_code = (int)restResponse.StatusCode;

                if (status_code == ((int)HttpStatusCode.OK))
                {
                    Console.WriteLine("Success");
                    button.Content = "Approved";
                }
                else if (status_code == ((int)HttpStatusCode.Unauthorized))
                {
                    button.Content = "Approve";
                    button.IsEnabled = true;
                    Console.WriteLine("Unauthorized");
                    //textBoxErrorMsg.Text = "Incorrect login details";
                }
                else if (status_code == ((int)HttpStatusCode.NotFound))
                {
                    button.Content = "Approve";
                    button.IsEnabled = true;
                    Console.WriteLine("User not found");
                    //textBoxErrorMsg.Text = "User not found";
                }
                else if (status_code == ((int)HttpStatusCode.InternalServerError))
                {
                    button.Content = "Approve";
                    button.IsEnabled = true;
                    Console.WriteLine("Internal server error occurred. Try again later.");
                    //textBoxErrorMsg.Text = "Internal server error occurred. Try again later.";
                }
            }
            catch (Exception exc)
            {
                button.Content = "Approve";
                button.IsEnabled = true;
                Console.WriteLine("Application error occurred. Try again later: {0}", exc.Message);
                //textBoxErrorMsg.Text = "Application error occurred. Try again later.";
            }



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

        private void btnCloseExam_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
