using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Classes;

namespace WpfApp1.Pages.AdminsPages
{
    /// <summary>
    /// Логика взаимодействия для PageProductStock.xaml
    /// </summary>
    public partial class PageProductStock : Page
    {
        public PageProductStock()
        {
            InitializeComponent();
            ListProduct.ItemsSource = DBaseClass.BD.Table_Product_Stock.ToList();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new MainPage());
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new MainPage());
        }
    }
}
