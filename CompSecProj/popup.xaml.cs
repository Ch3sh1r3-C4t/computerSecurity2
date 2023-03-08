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
using System.Windows.Shapes;

namespace trustid
{
    /// <summary>
    /// Interaction logic for popup.xaml
    /// </summary>
    public partial class popup : Window
    {
        private int oPopupResult; // 1: Proceed Next Step; 2: Not Me; 3: Dismiss

        public int PopupResult 
        {
            get { return oPopupResult; }
        }

        public popup(bool IsIdentified, string IdentifiedUser, string Message)
        {
            InitializeComponent();

            if (IsIdentified) {
                pnlIsIdentified.Visibility = Visibility.Visible;
                pnlIsNotIdentified.Visibility = Visibility.Collapsed;
                btnNotMe.Content = "I am not " + IdentifiedUser;
            }
            else {
                pnlIsNotIdentified.Visibility = Visibility.Visible;
                pnlIsIdentified.Visibility = Visibility.Collapsed;
            }

            lblMessage.Text = Message;
        }

        private void btnNextStep_Click(object sender, RoutedEventArgs e)
        {
            oPopupResult = 1;
            this.Close();
        }

        private void btnNotMe_Click(object sender, RoutedEventArgs e)
        {
            oPopupResult = 2;
            this.Close();
        }

        private void btnDismiss_Click(object sender, RoutedEventArgs e)
        {
            oPopupResult = 3;
            this.Close();
        }
    }
}
