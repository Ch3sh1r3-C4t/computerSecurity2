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
using System.Windows;
using System.Windows.Controls;

namespace trustid
{

    public partial class ContentManScreen : UserControl
    {
     
         public class PollVerificationStatusResponse
        {
            public string message { get; set; }
            public string resource_name { get; set; }
            public bool resource_bool { get; set; }
        }

        public ContentManScreen()
        {
            InitializeComponent();
          
        }

        private void Scenario_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (ContentManipulationViewModel)DataContext;
            if (viewModel.GotoView2Command.CanExecute(null))
                viewModel.GotoView2Command.Execute(null);
        }
    }
}
