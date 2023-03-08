
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace trustid
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Window
    {
        public login()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        public class LoginResponse
        {
            public string message { get; set; }
            public string resource_name { get; set; }

            public IDictionary<string, string> resource_obj { get; set; }
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            btnLogin.IsEnabled = false;
            btnLogin.Background = System.Windows.Media.Brushes.DarkGray;

            textBoxErrorMsg.Text = "";
            await Task.Delay(100);

            if (textBoxEmail.Text.Length == 0 || passwordBox1.Password.Length == 0)
            {
                textBoxErrorMsg.Text = "Please provide both email and password fields";
                btnLogin.IsEnabled = true;
                btnLogin.Background = new SolidColorBrush(Utilities.ConvertStringToColor("#99CCFF"));
                return;
            }

            try
            {
                string user = "CompSec";
                string password = "attack";

                if (textBoxEmail.Text == user &&   passwordBox1.Password == password)
                {
                    Globals.user_name = textBoxEmail.Text;
                    Globals.surname = passwordBox1.Password;

                    Console.WriteLine("Success");

                    textBoxErrorMsg.Foreground = Brushes.Green;
                    textBoxErrorMsg.Text = "Login success";

                    await Task.Delay(750);
                    Main newWindow = new Main();
                    newWindow.Show();
                    this.Close();
                }
                else
                {
                    Console.WriteLine("Unauthorized");
                    textBoxErrorMsg.Text = "Incorrect login credentials";
                }
              
            }
            catch (Exception exc)
            {
                Console.WriteLine("Application error occurred. Try again later: {0}", exc.Message);
                textBoxErrorMsg.Text = "Application error occurred. Try again later.";
            }

            btnLogin.IsEnabled = true;
            btnLogin.Background = new SolidColorBrush(Utilities.ConvertStringToColor("#58B4DC"));
        }
    }
}
