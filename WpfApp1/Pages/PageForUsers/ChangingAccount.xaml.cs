using System.Linq;
using System.Windows;
using WpfApp1.Classes;

namespace WpfApp1.Pages.PageForUsers
{
    /// <summary>
    /// Логика взаимодействия для ChangingAccount.xaml
    /// </summary>
    public partial class ChangingAccount : Window
    {
        public ChangingAccount()
        {
            InitializeComponent();
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (checkData())
            {
                StaffClass.CurrentStaffEmploe.login = tboxLogin.Text;
                StaffClass.CurrentStaffEmploe.password = pbPassword.Password.GetHashCode();

                try
                {
                    DBaseClass.BD.SaveChanges();
                    Close();
                    MessageBox.Show("Данные успешно сохранены", "Учётная запись", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show("Возникла ошибка! Данные не были обновлены", "Учётная запись", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private bool checkData()
        {
            Table_Staff employees = DBaseClass.BD.Table_Staff.FirstOrDefault(x => x.login == tboxLogin.Text);

            if (tboxLogin.Text.Length == 0)
            {
                MessageBox.Show("Введите логин", "Учётная запись", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (StaffClass.CurrentStaffEmploe != null && StaffClass.CurrentStaffEmploe.id_staff != employees.id_staff)
            {
                MessageBox.Show("Пользователь с таким логином уже зарегистрирован", "Учётная запись", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            else if (MainWindow.PasswordCheck(pbPassword))
            {
                return false;
            }
            else if (pbPassword.Password != pbRepeatPassword.Password)
            {
                MessageBox.Show("Пароли не совпадают", "Учётная запись", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }

        private void pbPassword_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            MainWindow.PasswordCheck(pbPassword);
        }
        private void pbRepeatPassword_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            MainWindow.PasswordCheck(pbRepeatPassword);
        }
    }
}
