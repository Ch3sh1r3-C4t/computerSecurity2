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

namespace trustid
{

    public partial class MacroAttacksScreen : UserControl
    {
     
         public class PollVerificationStatusResponse
        {
            public string message { get; set; }
            public string resource_name { get; set; }
            public bool resource_bool { get; set; }
        }

        public MacroAttacksScreen()
        {
            InitializeComponent();
          
        }

        private void MacroOne_Click_1(object sender, RoutedEventArgs e)
        {
            Attack_1_Inform.Visibility = Visibility.Hidden;
            Attack_begin.Visibility=Visibility.Hidden;

            Attack_1_Inform.Text = "\n \n \n Attack 1: POCDownloadImage.odt";
            Attack_1_Inform.Visibility=Visibility.Visible;

            Attack_begin.Visibility=Visibility.Visible;
            Globals.attack=1;
        }

        private void MacroTwo_Copy_Click(object sender, RoutedEventArgs e)
        { 
            Attack_1_Inform.Visibility = Visibility.Hidden;
            Attack_begin.Visibility=Visibility.Hidden;

            Attack_1_Inform.Text = "\n \n \n Attack 2: Macro2 - POCEnumAndSendBackResults"; 


            Attack_1_Inform.Visibility=Visibility.Visible; 

            Attack_begin.Visibility=Visibility.Visible;
            Globals.attack=2;
        }

        private void MacroTwo_Copy1_Click(object sender, RoutedEventArgs e)
        {
            Attack_1_Inform.Visibility = Visibility.Hidden;
            Attack_begin.Visibility=Visibility.Hidden;

            Attack_1_Inform.Text = "\n \n \n Attack 3: POCKeyLogging";

            Attack_1_Inform.Visibility=Visibility.Visible;

            Attack_begin.Visibility=Visibility.Visible;
            Globals.attack=3;
        }

        private void MacroTwo_Copy2_Click(object sender, RoutedEventArgs e)
        {
            Attack_1_Inform.Visibility = Visibility.Hidden;
            Attack_begin.Visibility=Visibility.Hidden;

            Attack_1_Inform.Text = "\n \n \n Attack 4: POCMsg.odt";

            Attack_1_Inform.Visibility=Visibility.Visible;

            Attack_begin.Visibility=Visibility.Visible;
            Globals.attack=4;
        }

        private void MacroTwo_Copy3_Click(object sender, RoutedEventArgs e)
        {
            Attack_1_Inform.Visibility = Visibility.Hidden;
            Attack_begin.Visibility=Visibility.Hidden;

            Attack_1_Inform.Text = "Attack 5: \n In this attack, the does something \n in the background, the file executes without";

            Attack_1_Inform.Visibility=Visibility.Visible;

            Attack_begin.Visibility=Visibility.Visible;
            Globals.attack=5;
        }

        private void Attack_begin_Click(object sender, RoutedEventArgs e)
        {
            if (Globals.attack==1)
            {
                var viewModel = (MacrosViewModel)DataContext;
                if (viewModel.GotoView2Command.CanExecute(null))
                    viewModel.GotoView2Command.Execute(null);
            }
            else if (Globals.attack==2)
            {
                var viewModel = (MacrosViewModel)DataContext;
                if (viewModel.GotoView4Command.CanExecute(null))
                    viewModel.GotoView4Command.Execute(null);
            }
             
            else if (Globals.attack==3)
            {
                var viewModel = (MacrosViewModel)DataContext;
                if (viewModel.GotoView3Command.CanExecute(null))
                    viewModel.GotoView3Command.Execute(null);

            }
            else if (Globals.attack==4)
            {
                var viewModel = (MacrosViewModel)DataContext;
                if (viewModel.GotoView5Command.CanExecute(null))
                    viewModel.GotoView5Command.Execute(null);
            }
           
        }
    }
}
