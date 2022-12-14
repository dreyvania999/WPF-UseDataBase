using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Pages.AdminsPages.AddPage
{
    /// <summary>
    /// Логика взаимодействия для AddManufactererPage.xaml
    /// </summary>
    public partial class AddManufactererPage : Page
    {
        public static bool IsEditing = false;
        private Table_Manufacturer TM;
        private Table_Contact_Persons CP;
        public AddManufactererPage()
        {
            InitializeComponent();
            cbContactPerson.ItemsSource = DBaseClass.BD.Table_Contact_Persons.ToList();
            cbContactPerson.SelectedValuePath = "id_contact_person";
            cbContactPerson.DisplayMemberPath = "surname";
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
             FrameClass.MainFrame.Navigate(new AddProductPage());
        }

        private void btnAddPerson_Click(object sender, RoutedEventArgs e)
        {
            ContactPerson.Visibility = Visibility.Visible;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsEditing == false)
                {
                    TM = new Table_Manufacturer();
                }

                TM.name = tbNameManufacturer.Text.ToString();
                TM.address = tbAdress.Text.ToString();
                TM.code_contact_person = Convert.ToInt32(cbContactPerson.SelectedValue.ToString());

                if (IsEditing == false)
                {
                     DBaseClass.BD.Table_Manufacturer.Add(TM);
                }

                 DBaseClass.BD.SaveChanges();
                 MessageBox.Show("Информация о производителе добавлена");
            }
            catch
            {
                 MessageBox.Show("Что-то пошло не так. Обратитесь к администратору или повторите попытку.");
            }
        }
        private void btnSavePerson_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Regex r1 = new Regex(@"^8 9\d{2} \d{3}-\d{2}-\d{2}$");
                Regex r2 = new Regex(@"^[A-Za-z0-9._%+-]+@[A-Za-z0-9-]+.[A-Za-z]{2,4}$");
                if (IsEditing == false)
                {
                    CP = new Table_Contact_Persons();
                }

                if (r1.IsMatch(tboxPhone.Text) == true)
                {
                    if (r2.IsMatch(tboxEmail.Text) == true)
                    {
                        CP.surname = tboxSurname.Text.ToString();
                        CP.name = tboxName.Text.ToString();
                        CP.patronymic = tboxPatronymic.Text.ToString();
                        CP.phone = tboxPhone.Text.ToString();
                        CP.email = tboxEmail.Text.ToString();
                    }
                    else
                    {
                         MessageBox.Show("Некорректно введена почта! Повторите попытку");
                    }
                }
                else
                {
                     MessageBox.Show("Некорректно введен телефон! Повторите попытку");
                }

                if (IsEditing == false)
                {
                     DBaseClass.BD.Table_Contact_Persons.Add(CP);
                }

                 DBaseClass.BD.SaveChanges();
                 MessageBox.Show("Информация о контактном лице добавлена");
                 FrameClass.MainFrame.Navigate(new AddManufactererPage());
            }
            catch
            {
                 MessageBox.Show("Что-то пошло не так. Обратитесь к администратору или повторите попытку.");
            }
        }
    }
}
