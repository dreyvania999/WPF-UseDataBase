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
                if (tbSurname.Text != "")
                {
                    dgUsers.ItemsSource = DBaseClass.BD.Table_Staff.ToList().Where(x => x.surname.Contains(tbSurname.Text) && x.login.Contains(tbLogin.Text ));
                }
                else
                {
                    dgUsers.ItemsSource = DBaseClass.BD.Table_Staff.ToList().Where(x => x.login.Contains(tbLogin.Text ));
                }
            }
            else
            {
                if (tbSurname.Text != "")
                {
                    dgUsers.ItemsSource = DBaseClass.BD.Table_Staff.ToList().Where(x => x.surname.Contains(tbSurname.Text));
                }
                else
                {
                    dgUsers.ItemsSource = DBaseClass.BD.Table_Staff.ToList();
                    return;
                }
            }
            
        }


        private void btnFiltering_Click(object sender, RoutedEventArgs e)
        {
            dgUsers.ItemsSource = DBaseClass.BD.Table_Staff.ToList().Where(x => x.Table_Gender.gender == cbGender.Text );
            
        }

        private void btnSortAsc_Click(object sender, RoutedEventArgs e)
        {
            dgUsers.ItemsSource = DBaseClass.BD.Table_Staff.ToList().Where(x => x.Table_Gender.gender == cbGender.Text ).OrderBy(x => x.surname);
           
        }

        private void btnSortDesc_Click(object sender, RoutedEventArgs e)
        {
            dgUsers.ItemsSource = DBaseClass.BD.Table_Staff.ToList().Where(x => x.Table_Gender.gender == cbGender.Text ).OrderByDescending(x => x.surname);
            
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
