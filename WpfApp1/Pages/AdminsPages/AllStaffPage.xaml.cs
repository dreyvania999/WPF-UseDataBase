using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Classes;

namespace WpfApp1.Pages.AdminsPages
{
    /// <summary>
    /// Логика взаимодействия для AllStaffPage.xaml
    /// </summary>
    public partial class AllStaffPage : Page
    {
        public AllStaffPage()
        {
            InitializeComponent();
            cbGender.ItemsSource = DBaseClass.BD.Table_Gender.ToList();
            cbGender.DisplayMemberPath = "gender";
            cbGender.SelectedIndex = 0;
            dgUsers.ItemsSource = DBaseClass.BD.Table_Staff.ToList();

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new MainPage());
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text != "")
            {
                dgUsers.ItemsSource = tbSurname.Text != ""
                    ? DBaseClass.BD.Table_Staff.ToList().Where(x => x.surname.Contains(tbSurname.Text) && x.login.Contains(tbLogin.Text))
                    : (System.Collections.IEnumerable)DBaseClass.BD.Table_Staff.ToList().Where(x => x.login.Contains(tbLogin.Text));
            }
            else
            {
                dgUsers.ItemsSource = tbSurname.Text != ""
                    ? DBaseClass.BD.Table_Staff.ToList().Where(x => x.surname.Contains(tbSurname.Text))
                    : (System.Collections.IEnumerable)DBaseClass.BD.Table_Staff.ToList();
            }

        }


        private void btnFiltering_Click(object sender, RoutedEventArgs e)
        {
            dgUsers.ItemsSource = DBaseClass.BD.Table_Staff.ToList().Where(x => x.Table_Gender.gender == cbGender.Text);

        }

        private void btnSortAsc_Click(object sender, RoutedEventArgs e)
        {
            dgUsers.ItemsSource = DBaseClass.BD.Table_Staff.ToList().Where(x => x.Table_Gender.gender == cbGender.Text).OrderBy(x => x.surname);

        }

        private void btnSortDesc_Click(object sender, RoutedEventArgs e)
        {
            dgUsers.ItemsSource = DBaseClass.BD.Table_Staff.ToList().Where(x => x.Table_Gender.gender == cbGender.Text).OrderByDescending(x => x.surname);

        }

        private void btInitialState_Click(object sender, RoutedEventArgs e)
        {
            tbLogin.Text = "";
            tbSurname.Text = "";
            cbGender.SelectedIndex = 0;
            dgUsers.ItemsSource = DBaseClass.BD.Table_Staff.ToList();
        }
    }
}
