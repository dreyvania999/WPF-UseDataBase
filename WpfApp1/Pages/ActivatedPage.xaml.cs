using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class ActivatedPage : Page
    {
        public ActivatedPage()
        {
            InitializeComponent();
        }
        private void btnAuto_Click(object sender, RoutedEventArgs e)
        {
             FrameClass.MainFrame.Navigate(new AutarizationPage());
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
             FrameClass.MainFrame.Navigate(new RegistrationPage());
        }
    }
}
