using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace trustid
{
    /// <summary>
    /// Interaction logic for ucDashboard.xaml
    /// </summary>
    public partial class AttackInfo: UserControl
    {
        public AttackInfo()
        {
            InitializeComponent();

        
        }

        public class StudentListExamResponse
        {
            public string message { get; set; }
            public string resource_name { get; set; }
            public IList<IDictionary<string, string>> resource_array { get; set; }
        }

        public class InstructorListExamResponse
        {
            public string message { get; set; }
            public string resource_name { get; set; }
            public IList<IDictionary<string, dynamic>> resource_array { get; set; }
        }

        public class UpdateExamConditionResponse
        {
            public string message { get; set; }
            public string resource_name { get; set; }
        }


        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.ToString());
        }


    }

}
