using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Classes;
using WpfApp1.Pages.AdminsPages.AddPage;

namespace WpfApp1.Pages.AdminsPages.ViewsPage
{
    /// <summary>
    /// Логика взаимодействия для ListViewDeliveryPage.xaml
    /// </summary>
    public partial class ListViewDeliveryPage : Page
    {
        public ListViewDeliveryPage()
        {
            InitializeComponent();
            ListDelivery.ItemsSource = DBaseClass.BD.Table_Delivery.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new AddDeliveryPage());
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new MainPage());
        }
    }
}
