using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using WpfApp1.Classes;
using MessageBox = System.Windows.Forms.MessageBox;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
            rbMen.IsChecked = true;
        }

        public void TextInPasswordBox(object sender, TextCompositionEventArgs e)
        {
             MainWindow.PasswordCheck(pbPassword);
        }

        private bool GetProverkaParol()
        {
            Table_Staff employees = DBaseClass.BD.Table_Staff.FirstOrDefault(x => x.login == tboxLogin.Text);
            return employees != null;
        }

        private bool SearchValue()
        {
            if (tboxSurname.Text.Replace(" ", "") == "")
            {
                 MessageBox.Show("Поле фамилия должно быть заполнено!");
                return false;
            }
            if (tboxName.Text.Replace(" ", "") == "")
            {
                 MessageBox.Show("Поле имя должно быть заполнено!");
                return false;
            }
            if (tboxPatronymic.Text.Replace(" ", "") == "")
            {
                 MessageBox.Show("Поле Отчество должно быть заполнено!");
                return false;
            }
            if (dpBirthday.Text == "")
            {
                 MessageBox.Show("Поле дата рождения должно быть заполнено!");
                return false;
            }
            if (tboxLogin.Text.Replace(" ", "") == "")
            {
                 MessageBox.Show("Поле логин должно быть заполнено!");
                return false;
            }
            return true;
        }
        private void btnReg_Click(object sender, RoutedEventArgs e)
        {



            try
            {
                if (!SearchValue())
                {
                    return;
                }
                if (!MainWindow.PasswordCheck(pbPassword))
                {
                     MessageBox.Show("Пароль не верен");
                    return;
                }
                if (GetProverkaParol())
                {
                     MessageBox.Show("Пользователь с таким логиным уже зарегистрирован!");
                    return;
                }
                int currentGender = 1;
                if (rbMen.IsChecked == true)
                {
                    currentGender = 1;
                }

                if (rbWomen.IsChecked == true)
                {
                    currentGender = 2;
                }

                Table_Staff staffMan = new Table_Staff()
                {
                    surname = tboxSurname.Text,
                    name = tboxName.Text,
                    patronymic = tboxPatronymic.Text,
                    gender = currentGender,
                    date_birth = Convert.ToDateTime(dpBirthday.SelectedDate.ToString()),
                    login = tboxLogin.Text,
                    password = pbPassword.Password.GetHashCode(),
                    role = 2
                };

                 DBaseClass.BD.Table_Staff.Add(staffMan);
                 DBaseClass.BD.SaveChanges();
                DialogResult result = MessageBox.Show("Хотите ли вы добавить фото сотруднику? ", "Фото", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    OpenFileDialog OFD = new OpenFileDialog();  // создаем диалоговое окно
                     OFD.ShowDialog();  // открываем диалоговое окно
                    string Path = OFD.FileName;  // считываем путь выбранного изображения
                     Images.ImageEmploe.AddPhoto(Path, staffMan.id_staff);

                }

                 MessageBox.Show("Вы успешно зарегистрировались");
                 FrameClass.MainFrame.Navigate(new AutarizationPage());
            }
            catch
            {
                 MessageBox.Show("При регистрации пользователя возникла ошибка!");
            }

        }

        private void btnBackMain_Click(object sender, RoutedEventArgs e)
        {
             FrameClass.MainFrame.Navigate(new ActivatedPage());
        }

        private void btnGoToAAutoriz_Click(object sender, RoutedEventArgs e)
        {
             FrameClass.MainFrame.Navigate(new AutarizationPage());
        }
    }
}
