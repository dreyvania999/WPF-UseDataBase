using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Classes;
using WpfApp1.Pages.AdminsPages.AddPage;

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
            FrameClass.MainFrame.Navigate(new AddSalesPage());
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new MainPage());
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = Convert.ToInt32(button.Uid);

            Table_Sales saling = DBaseClass.BD.Table_Sales.FirstOrDefault(x => x.id_sales == id);
            List<Table_Sale_Chemicals> saleChemicals = DBaseClass.BD.Table_Sale_Chemicals.Where(x => x.sales_code == id).ToList();
            List<Table_Sale_Houshould> saleHoushould = DBaseClass.BD.Table_Sale_Houshould.Where(x => x.sales_code == id).ToList();

            DBaseClass.BD.Table_Sales.Remove(saling);

            foreach (Table_Sale_Chemicals item in saleChemicals)
            {
                DBaseClass.BD.Table_Sale_Chemicals.Remove(item);
            }
            foreach (Table_Sale_Houshould item in saleHoushould)
            {
                DBaseClass.BD.Table_Sale_Houshould.Remove(item);
            }

            DBaseClass.BD.SaveChanges();
            MessageBox.Show("Информация удалена");

            ListSale.ItemsSource = DBaseClass.BD.Table_Sales.ToList();
        }

        private void StackPanel_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            StackPanel stackPanel = (StackPanel)sender;
            int id = Convert.ToInt32(stackPanel.Uid);
            AddSalesPage.SetTable_Sales(DBaseClass.BD.Table_Sales.FirstOrDefault(x => x.id_sales == id));
            AddSalesPage.IsEditing = true;
            FrameClass.MainFrame.Navigate(new AddSalesPage());
        }
    }
}
