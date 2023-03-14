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
    public partial class AttacksMenu : UserControl
    {
        public AttacksMenu()
        {
            InitializeComponent();
        
        }

    

       

        private void btnSelectExam1_MouseDown(object sender, RoutedEventArgs e)
        {

            btnSelectExam2.Fill = new SolidColorBrush(Utilities.ConvertStringToColor("#58B4DC"));
            txtExam2.Foreground = new SolidColorBrush(Utilities.ConvertStringToColor("#333333"));

            btnSelectExam1.Fill = new SolidColorBrush(Utilities.ConvertStringToColor("#58B4DC"));
            txtExam1.Foreground = new SolidColorBrush(Utilities.ConvertStringToColor("#ffffff"));
            pnlExam1.Visibility = Visibility.Visible;
            pnlExam2.Visibility = Visibility.Hidden;
        }

        private void btnSelectExam2_MouseDown(object sender, RoutedEventArgs e)
        {
            //var rect = btnSelectExam2.Template.FindName("recExam", btnSelectExam2) as System.Windows.Shapes.Rectangle;
            //rect.Fill = new SolidColorBrush(ConvertStringToColor("#0E72ED"));


            btnSelectExam1.Fill = new SolidColorBrush(Utilities.ConvertStringToColor("#58B4DC"));
            txtExam1.Foreground = new SolidColorBrush(Utilities.ConvertStringToColor("#333333"));

            btnSelectExam2.Fill = new SolidColorBrush(Utilities.ConvertStringToColor("#58B4DC"));
            txtExam2.Foreground = new SolidColorBrush(Utilities.ConvertStringToColor("#ffffff"));
            pnlExam1.Visibility = Visibility.Hidden;
            pnlExam2.Visibility = Visibility.Visible;
        }

   

        private void btnStartExam1_Click(object sender, RoutedEventArgs e)
        {
       

            MacroLists newWindow = new MacroLists();
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

        private void btnStartExam1_Copy_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnStartExam1_Copy2_Click(object sender, RoutedEventArgs e)
        {
            MacContentManLists newWindow = new MacContentManLists();
            newWindow.Show();
        }

    
        private void Attack_2_Click(object sender, RoutedEventArgs e)
        {
            ContentManLists newWindow = new ContentManLists();
            newWindow.Show();
        }

        private void Attack_1_Click(object sender, RoutedEventArgs e)
        {
            MacroLists newWindow = new MacroLists();
            newWindow.Show();
        }
    }
}
