using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp1.Classes;
using WpfApp1.Pages.PageForUsers;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для AutarizationPage.xaml
    /// </summary>
    public partial class AutarizationPage : Page
    {
        public AutarizationPage()
        {
            InitializeComponent();
             MainWindow.PasswordCheck(pbRassword);
        }

        public void TextInPasswordBox(object sender, TextCompositionEventArgs e)
        {
             MainWindow.PasswordCheck(pbRassword);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int p = pbRassword.Password.GetHashCode();
            StaffClass.CurrentStaffEmploe = DBaseClass.BD.Table_Staff.FirstOrDefault(x => x.login == tboxLogin.Text && x.password == p);
            if (StaffClass.CurrentStaffEmploe != null)
            {

                switch (StaffClass.CurrentStaffEmploe.role)
                {
                    case 1:

                         FrameClass.MainFrame.Navigate(new MainPage());
                        break;
                    case 2:

                         FrameClass.MainFrame.Navigate(new AcountPage());
                        break;
                    default:
                         MessageBox.Show("Произошла не предвиденная ситуация, повторите ввод логина и пароля");
                        break;
                }
            }
            else
            {
                 MessageBox.Show("Пользователь с таким логиным и паролем не найден!");
            }
        }

        private void btnBackMain_Click(object sender, RoutedEventArgs e)
        {
             FrameClass.MainFrame.Navigate(new ActivatedPage());
        }
    }
}
