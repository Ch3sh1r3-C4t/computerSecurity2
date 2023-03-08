using System.Windows.Controls;
using System.Windows;

namespace trustid
{
    /// <summary>
    /// Interaction logic for ucEnrollStepTwo.xaml
    /// </summary>
    public partial class ucEnrollStepFinal : UserControl
    {

        public ucEnrollStepFinal()
        {
            InitializeComponent();
        }

        private void btnStartExamination_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Examination newWindow = new Examination();
            newWindow.Show();

            (Window.GetWindow(this)).Close();
        }
    }
}