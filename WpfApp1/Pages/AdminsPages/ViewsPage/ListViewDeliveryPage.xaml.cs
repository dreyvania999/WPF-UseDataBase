using System.Collections.Generic;
using System;
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
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = Convert.ToInt32(button.Uid);

            Table_Delivery delivery = DBaseClass.BD.Table_Delivery.FirstOrDefault(x => x.id_delivery == id);
            List<Table_Chemicals_Delivery> deliveryChemicals = DBaseClass.BD.Table_Chemicals_Delivery.Where(x => x.delivery_code == id).ToList();
            List<Table_Household_Delivery> deliveryHoushould = DBaseClass.BD.Table_Household_Delivery.Where(x => x.delivery_code == id).ToList();

            DBaseClass.BD.Table_Delivery.Remove(delivery);

            foreach (Table_Chemicals_Delivery item in deliveryChemicals)
            {
                DBaseClass.BD.Table_Chemicals_Delivery.Remove(item);
            }
            foreach (Table_Household_Delivery item in deliveryHoushould)
            {
                DBaseClass.BD.Table_Household_Delivery.Remove(item);
            }

            DBaseClass.BD.SaveChanges();
            MessageBox.Show("Информация удалена");

            ListDelivery.ItemsSource = DBaseClass.BD.Table_Delivery.ToList();
        }
    }
}
