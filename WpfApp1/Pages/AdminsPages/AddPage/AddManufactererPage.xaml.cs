using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Classes;

namespace WpfApp1.Pages.AdminsPages.AddPage
{
    /// <summary>
    /// Логика взаимодействия для AddManufactererPage.xaml
    /// </summary>
    public partial class AddManufactererPage : Page
    {
        public static bool IsEditing = false;
        Table_Manufacturer TM;
        Table_Contact_Persons CP;
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
                if (IsEditing == false)
                {
                    CP = new Table_Contact_Persons();
                }

                CP.surname = tboxSurname.Text.ToString();
                CP.name = tboxName.Text.ToString();
                CP.patronymic = tboxPatronymic.Text.ToString();
                CP.phone = tboxPhone.Text.ToString();
                CP.email = tboxEmail.Text.ToString();

                if (IsEditing == false)
                {
                    DBaseClass.BD.Table_Contact_Persons.Add(CP);
                }

                DBaseClass.BD.SaveChanges();
                MessageBox.Show("Информация о контактном лице добавлена");
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так. Обратитесь к администратору или повторите попытку.");
            }
        }
    }
}
