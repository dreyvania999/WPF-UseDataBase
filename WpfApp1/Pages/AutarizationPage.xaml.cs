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
            Table_Staff employees = DBaseClass.BD.Table_Staff.FirstOrDefault(x => x.login == tboxLogin.Text && x.password == p);
            if (employees == null)
            {
                MessageBox.Show("Пользователь с таким логиным и паролем не найден!");
            }
            else
            {
                switch (employees.Table_Role.role)
                {
                    case "Администратор":
                        MessageBox.Show("Администратор");
                        FrameClass.MainFrame.Navigate(new AutarizationPage());
                        break;
                    case "Пользователь":
                        MessageBox.Show("Пользователь");
                        FrameClass.MainFrame.Navigate(new AutarizationPage());
                        break;
                    default:
                        MessageBox.Show("");
                        break;
                }
            }
        }
    }
}
