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
    public partial class Enrollment : Window
    {
        public Enrollment()
        {
            InitializeComponent();
        }

        private void btnCancelEnroll_Click(object sender, RoutedEventArgs e)
        {
            update_exam_condition();
            this.Close();
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
                    condition = "Leave",
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
    }
}
