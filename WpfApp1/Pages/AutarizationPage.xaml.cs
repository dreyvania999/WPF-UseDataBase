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
            Table_Staff staffMan = DBaseClass.BD.Table_Staff.FirstOrDefault(x => x.login == tboxLogin.Text && x.password == p);
            if (staffMan != null)
            {
                StaffClass.CurrentStaffEmploe = staffMan;
                switch (staffMan.role)
                {
                    case 1:
                        
                        FrameClass.MainFrame.Navigate(new MainPage());
                        break;
                    case 2:
                        
                        FrameClass.MainFrame.Navigate(new MainPage());
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
