using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Classes;

namespace WpfApp1.Pages.AdminsPages
{
    /// <summary>
    /// Логика взаимодействия для ListViewSalesPage.xaml
    /// </summary>
    public partial class ListViewSalesPage : Page
    {
        public ListViewSalesPage()
        {
            InitializeComponent();
            ListSale.ItemsSource = DBaseClass.BD.Table_Sales.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new MainPage());
        }
    }
}
