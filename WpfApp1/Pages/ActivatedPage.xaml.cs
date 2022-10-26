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
using WpfApp1.Classes;

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
