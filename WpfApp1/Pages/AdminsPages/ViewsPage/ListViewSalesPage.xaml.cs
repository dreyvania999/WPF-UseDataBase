

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WpfApp1.Pages.AdminsPages.AddPage;

namespace WpfApp1.Pages.AdminsPages
{
    /// <summary>
    /// Логика взаимодействия для ListViewSalesPage.xaml
    /// </summary>
    public partial class ListViewSalesPage : Page
    {
        private readonly Table_Sales TS;
        private readonly Pagination Pagination = new Pagination();
        private List<Table_Sales> CurrentSalesList = new List<Table_Sales>();
        public ListViewSalesPage()
        {
            InitializeComponent();
            CurrentSalesList = DBaseClass.BD.Table_Sales.ToList();
            ListSale.ItemsSource = CurrentSalesList;

            Pagination.CountPage = DBaseClass.BD.Table_Sales.ToList().Count;
            DataContext = Pagination;
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
            CurrentSalesList = DBaseClass.BD.Table_Sales.ToList();
            ListSale.ItemsSource = CurrentSalesList;
        }

        private void StackPanel_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            StackPanel stackPanel = (StackPanel)sender;
            int id = Convert.ToInt32(stackPanel.Uid);
            AddSalesPage.IsEditing = true;
             FrameClass.MainFrame.Navigate(new AddSalesPage(DBaseClass.BD.Table_Sales.FirstOrDefault(x => x.id_sales == id)), TS);
        }

        private void cbPlassSales_Click(object sender, RoutedEventArgs e)
        {
            Search();
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
                    Pagination.CurrentPage = CurrentSalesList.Count;
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
            ListSale.ItemsSource = CurrentSalesList.Skip((Pagination.CurrentPage * Pagination.CountPage) - Pagination.CountPage).Take(Pagination.CountPage).ToList();

        }


        private void Search()
        {
            CurrentSalesList.Clear();

            switch (cmbSearcher.SelectedIndex)
            {
                case 1:
                    CurrentSalesList = DBaseClass.BD.Table_Sales.Where(x => x.Table_Staff.name.Contains(tbSearch.Text)).ToList();
                    break;
                case 2:
                    CurrentSalesList = DBaseClass.BD.Table_Sales.Where(x => x.Table_Staff.name.Contains(tbSearch.Text)).ToList();
                    break;
                case 3:
                    CurrentSalesList = DBaseClass.BD.Table_Sales.Where(x => x.Table_Staff.patronymic.Contains(tbSearch.Text)).ToList();
                    break;
                case 4:
                    List<Table_Sale_Chemicals> listChim = new List<Table_Sale_Chemicals>();
                    foreach (Table_Sale_Chemicals item in DBaseClass.BD.Table_Sale_Chemicals.ToList())
                    {
                        if (item.Table_Household_Chemicals.name.Contains(tbSearch.Text))
                        {
                            listChim.Add(item);
                        }
                    }
                    foreach (Table_Sale_Chemicals item in listChim)
                    {
                        CurrentSalesList.Add(DBaseClass.BD.Table_Sales.FirstOrDefault(x => x.Table_Sale_Chemicals.FirstOrDefault(x1 => x1.id_sale_inform == item.id_sale_inform) != null));
                    }
                    break;
                case 5:
                    List<Table_Sale_Houshould> listHous = new List<Table_Sale_Houshould>();
                    foreach (Table_Sale_Houshould item in DBaseClass.BD.Table_Sale_Houshould.ToList())
                    {
                        if (item.Table_Household_Goods.name.Contains(tbSearch.Text))
                        {
                            listHous.Add(item);
                        }
                    }


                    foreach (Table_Sale_Houshould item in listHous)
                    {
                        CurrentSalesList.Add(DBaseClass.BD.Table_Sales.FirstOrDefault(x => x.Table_Sale_Houshould.FirstOrDefault(x1 => x1.id_sale_inform == item.id_sale_inform) != null));
                    }
                    break;
                default:
                    CurrentSalesList = DBaseClass.BD.Table_Sales.ToList();
                    break;
            }

            if ((bool)cbPlassSales.IsChecked)
            {
                CurrentSalesList = CurrentSalesList.Where(x => x.ColorCost == Brushes.AliceBlue).ToList();
            }

            switch (cbSort.SelectedIndex)
            {
                case 1:
                    CurrentSalesList.Sort((x, y) => x.Table_Staff.surname.CompareTo(y.Table_Staff.surname));
                    break;
                case 2:
                    CurrentSalesList.Sort((x, y) => x.Table_Staff.surname.CompareTo(y.Table_Staff.surname));
                    CurrentSalesList.Reverse();

                    break;
                case 3:
                    CurrentSalesList.Sort((x, y) => x.Table_Staff.name.CompareTo(y.Table_Staff.name));
                    break;
                case 4:
                    CurrentSalesList.Sort((x, y) => x.Table_Staff.name.CompareTo(y.Table_Staff.name));
                    CurrentSalesList.Reverse();
                    break;
                case 5:
                    CurrentSalesList.Sort((x, y) => x.Table_Staff.patronymic.CompareTo(y.Table_Staff.patronymic));
                    break;
                case 6:
                    CurrentSalesList.Sort((x, y) => x.Table_Staff.patronymic.CompareTo(y.Table_Staff.patronymic));
                    CurrentSalesList.Reverse();
                    break;

            }

            ListSale.ItemsSource = CurrentSalesList;
            if (CurrentSalesList.Count != 0)
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
                Pagination.CountPage = CurrentSalesList.Count;
            }

            Pagination.CountList = CurrentSalesList.Count;
            ListSale.ItemsSource = CurrentSalesList.Skip(0).Take(Pagination.CountPage).ToList();
            Pagination.CurrentPage = 1;
        }
    }
}
