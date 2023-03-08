using System;
using System.Collections.Generic;
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

namespace trustid
{
    /// <summary>
    /// Interaction logic for ucDashboard.xaml
    /// </summary>
    public partial class ucUserProfile : UserControl
    {
        public ucUserProfile()
        {
            InitializeComponent();
            txtUserName.Text = Globals.user_name + " ";
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            login newWindow = new login();
            newWindow.Show();

            var closingWindow = Window.GetWindow(this);
            closingWindow.Close();
        }
    }

}
