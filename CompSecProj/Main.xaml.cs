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
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
            btnShowUserProfile.Content = Globals.user_name.Substring(0, 1) + Globals.surname.Substring(0, 1);
            lblDashboard.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#58B4DC"));
            this.contentControl.Content = new ucDashboard();
            //LoadStudentExams();
        }
        public class ListExamsResponse
        {
            public string message { get; set; }
            public string resource_name { get; set; }

            public IList<IDictionary<string, string>> resource_array { get; set; }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            lblDashboard.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#58B4DC"));
            lblExaminations.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333"));
            this.contentControl.Content = new ucDashboard();
        }

        private void btnAttack_Click(object sender, RoutedEventArgs e)
        {
            lblDashboard.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333"));
            lblExaminations.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#58B4DC"));

            this.contentControl.Content = new ucExams();
        }

        private void btnShowUserProfile_Click(object sender, RoutedEventArgs e)
        {
            lblDashboard.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333"));
            lblExaminations.Foreground = new System.Windows.Media.SolidColorBrush((Color)ColorConverter.ConvertFromString("#333333"));
            this.contentControl.Content = new ucUserProfile();
        }
    }
}
