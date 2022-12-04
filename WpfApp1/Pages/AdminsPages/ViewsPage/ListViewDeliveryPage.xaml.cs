using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Pages.AdminsPages.AddPage;

namespace WpfApp1.Pages.AdminsPages.ViewsPage
{
    /// <summary>
    /// Логика взаимодействия для ListViewDeliveryPage.xaml
    /// </summary>
    public partial class ListViewDeliveryPage : Page
    {
        private  Pagination Pagination = new Pagination();
        private  List<Table_Delivery> CurrentDeliveryList = new List<Table_Delivery>();
        private  Table_Delivery TD;
        public ListViewDeliveryPage()
        {
            InitializeComponent();

            Pagination.CountPage = DBaseClass.BD.Table_Delivery.ToList().Count;
            DataContext = Pagination;
            CurrentDeliveryList = DBaseClass.BD.Table_Delivery.ToList();
            ListDelivery.ItemsSource = CurrentDeliveryList;
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

        private void StackPanel_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            StackPanel stackPanel = (StackPanel)sender;
            int id = Convert.ToInt32(stackPanel.Uid);
            AddSalesPage.IsEditing = true;
             FrameClass.MainFrame.Navigate(new AddDeliveryPage(DBaseClass.BD.Table_Delivery.FirstOrDefault(x => x.id_delivery == id)), TD);
        }
      
        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }

        private void CbSortSotr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }

        private void cmbSearcher_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }

        private void tboxPageCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            SetPagination();
        }

        private void EditingCurrentPage_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;

            switch (tb.Uid)
            {
                case "first":
                    Pagination.CurrentPage = 1;
                    break;
                case "last":
                    Pagination.CurrentPage = CurrentDeliveryList.Count;
                    break;
                case "back":
                    Pagination.CurrentPage--;
                    break;
                case "next":
                    Pagination.CurrentPage++;
                    break;
                default:
                    Pagination.CurrentPage = Convert.ToInt32(tb.Text);
                    break;
            }
            ListDelivery.ItemsSource = CurrentDeliveryList.Skip((Pagination.CurrentPage * Pagination.CountPage) - Pagination.CountPage).Take(Pagination.CountPage).ToList();

        }


        private void Search()
        {
            CurrentDeliveryList.Clear();

            switch (cmbSearcher.SelectedIndex)
            {
                case 1:
                    CurrentDeliveryList = DBaseClass.BD.Table_Delivery.Where(x => x.Table_Staff.name.Contains(tbSearch.Text)).ToList();
                    break;
                case 2:
                    CurrentDeliveryList = DBaseClass.BD.Table_Delivery.Where(x => x.Table_Staff.name.Contains(tbSearch.Text)).ToList();
                    break;
                case 3:
                    CurrentDeliveryList = DBaseClass.BD.Table_Delivery.Where(x => x.Table_Staff.patronymic.Contains(tbSearch.Text)).ToList();
                    break;
                case 4:
                    CurrentDeliveryList = DBaseClass.BD.Table_Delivery.Where(x => x.Table_Suppliers.name_supplier.Contains(tbSearch.Text)).ToList();
                    break;
                default:
                    CurrentDeliveryList = DBaseClass.BD.Table_Delivery.ToList();
                    break;
            }

           
            switch (cbSort.SelectedIndex)
            {
                case 1:
                    CurrentDeliveryList.Sort((x, y) => x.Table_Staff.surname.CompareTo(y.Table_Staff.surname));
                    break;
                case 2:
                    CurrentDeliveryList.Sort((x, y) => x.Table_Staff.surname.CompareTo(y.Table_Staff.surname));
                    CurrentDeliveryList.Reverse();

                    break;
                case 3:
                    CurrentDeliveryList.Sort((x, y) => x.Table_Staff.name.CompareTo(y.Table_Staff.name));
                    break;
                case 4:
                    CurrentDeliveryList.Sort((x, y) => x.Table_Staff.name.CompareTo(y.Table_Staff.name));
                    CurrentDeliveryList.Reverse();
                    break;
                case 5:
                    CurrentDeliveryList.Sort((x, y) => x.Table_Staff.patronymic.CompareTo(y.Table_Staff.patronymic));
                    break;
                case 6:
                    CurrentDeliveryList.Sort((x, y) => x.Table_Staff.patronymic.CompareTo(y.Table_Staff.patronymic));
                    CurrentDeliveryList.Reverse();
                    break;
                case 7:
                    CurrentDeliveryList.Sort((x, y) => x.Table_Suppliers.name_supplier.CompareTo(y.Table_Staff.patronymic));
                    break;
                case 8:
                    CurrentDeliveryList.Sort((x, y) => x.Table_Suppliers.name_supplier.CompareTo(y.Table_Staff.patronymic));
                    CurrentDeliveryList.Reverse();
                    break;
            }

            ListDelivery.ItemsSource = CurrentDeliveryList;
            if (CurrentDeliveryList.Count != 0)
            {
                SetPagination();
            }
        }
        private void SetPagination()
        {
            try
            {
                Pagination.CountPage = Convert.ToInt32(tboxPageCount.Text);
            }
            catch
            {
                Pagination.CountPage = CurrentDeliveryList.Count;
            }

            Pagination.CountList = CurrentDeliveryList.Count;
            ListDelivery.ItemsSource = CurrentDeliveryList.Skip(0).Take(Pagination.CountPage).ToList();
            Pagination.CurrentPage = 1;
        }
    }
}
