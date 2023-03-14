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

    public partial class MacManScreen : UserControl
    {
     
         public class PollVerificationStatusResponse
        {
            public string message { get; set; }
            public string resource_name { get; set; }
            public bool resource_bool { get; set; }
        }

        public MacManScreen()
        {
            InitializeComponent();
          
        }

        private void Scenario_Click(object sender, RoutedEventArgs e)
        {

            if (Globals.macroat!=0)
            {
                var viewModel = (MacManipulationViewModel)DataContext;
                if (viewModel.GotoView2Command.CanExecute(null))
                    viewModel.GotoView2Command.Execute(null);
            }
        }

        private void MacroOne_Click_1(object sender, RoutedEventArgs e)
        {
            Globals.macroat =1;
        }

        private void MacroTwo_Copy1_Click(object sender, RoutedEventArgs e)
        {
            Globals.macroat =3;
        }

        private void MacroTwo_Copy2_Click(object sender, RoutedEventArgs e)
        {
            Globals.macroat =4;
        }

        private void MacroTwo_Copy_Click(object sender, RoutedEventArgs e)
        {
            Globals.macroat =2;
        }
    }
}
