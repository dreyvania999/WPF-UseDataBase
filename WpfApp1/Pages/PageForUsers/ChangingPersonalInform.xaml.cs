using System;
using System.Windows;
using WpfApp1.Classes;

namespace WpfApp1.Pages.PageForUsers
{
    /// <summary>
    /// Логика взаимодействия для ChangingPersonalInform.xaml
    /// </summary>
    public partial class ChangingPersonalInform : Window
    {
        public ChangingPersonalInform()
        {
            InitializeComponent();
            tboxSurname.Text = StaffClass.CurrentStaffEmploe.surname;
            tboxName.Text = StaffClass.CurrentStaffEmploe.name;
            tboxPatronymic.Text = StaffClass.CurrentStaffEmploe.patronymic;
            dpBirthdate.SelectedDate = StaffClass.CurrentStaffEmploe.date_birth;


            dpBirthdate.DisplayDateStart = new DateTime(DateTime.Now.Year - 70, 1, 1);
            dpBirthdate.DisplayDateEnd = new DateTime(DateTime.Now.Year - 16, DateTime.Now.Month, DateTime.Now.Day);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (checkData())
            {
                StaffClass.CurrentStaffEmploe.surname = tboxSurname.Text;
                StaffClass.CurrentStaffEmploe.name = tboxName.Text;
                StaffClass.CurrentStaffEmploe.patronymic = tboxPatronymic.Text;
                StaffClass.CurrentStaffEmploe.date_birth = dpBirthdate.SelectedDate.Value;


                try
                {
                     DBaseClass.BD.SaveChanges();
                    Close();
                     MessageBox.Show("Данные успешно сохранены", "Персональные данные", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                     MessageBox.Show("Возникла ошибка! Данные не были обновлены", "Персональные данные", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private bool checkData()
        {

            if (dpBirthdate.SelectedDate.Value == DateTime.Today)
            {
                 MessageBox.Show("Выберите дату рождения", "Изменение личных данных", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            return true;
        }
    }
}