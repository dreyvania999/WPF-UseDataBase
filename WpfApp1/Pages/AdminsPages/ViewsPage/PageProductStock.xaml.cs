using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Pages.AdminsPages
{
    /// <summary>
    /// Логика взаимодействия для PageProductStock.xaml
    /// </summary>
    public partial class PageProductStock : Page
    {
        private readonly Pagination Pagination = new Pagination();
        private readonly List<Table_Product_Stock> CurrentProductStockList = new List<Table_Product_Stock>();
        public PageProductStock()
        {
            InitializeComponent();
            Pagination.CountPage = DBaseClass.BD.Table_Sales.ToList().Count;
            DataContext = Pagination;
            CurrentProductStockList = DBaseClass.BD.Table_Product_Stock.ToList();
            ListProduct.ItemsSource = CurrentProductStockList;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
             FrameClass.MainFrame.Navigate(new MainPage());
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
             FrameClass.MainFrame.Navigate(new MainPage());
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
                    Pagination.CurrentPage = CurrentProductStockList.Count;
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
            ListProduct.ItemsSource = CurrentProductStockList.Skip((Pagination.CurrentPage * Pagination.CountPage) - Pagination.CountPage).Take(Pagination.CountPage).ToList();

        }


        private void Search()
        {
            //CurrentProductStockList.Clear();

            //switch (cmbSearcher.SelectedIndex)
            //{
            //    case 1:
            //        CurrentProductStockList = DBaseClass.BD.Table_Product_Stock.Where(x => x.Table_Staff.name.Contains(tbSearch.Text)).ToList();
            //        break;
            //    case 2:
            //        CurrentProductStockList = DBaseClass.BD.Table_Product_Stock.Where(x => x.Table_Staff.name.Contains(tbSearch.Text)).ToList();
            //        break;
            //    case 3:
            //        CurrentProductStockList = DBaseClass.BD.Table_Product_Stock.Where(x => x.Table_Staff.patronymic.Contains(tbSearch.Text)).ToList();
            //        break;
            //    case 4:

            //        foreach (var item in DBaseClass.BD.Table_Sale_Chemicals.ToList())
            //        {
            //            if (item.Table_Household_Chemicals.name.Contains(tbSearch.Text))
            //            {
            //                CurrentProductStockList.Add((Table_Product_Stock)DBaseClass.BD.Table_Product_Stock.Where(x => x.Table_Sale_Chemicals.Equals(item)));
            //            }
            //        }

            //        break;
            //    case 5:
            //        foreach (var item in DBaseClass.BD.Table_Sale_Houshould.ToList())
            //        {
            //            if (item.Table_Household_Goods.name.Contains(tbSearch.Text))
            //            {
            //                CurrentProductStockList.Add((Table_Product_Stock)DBaseClass.BD.Table_Product_Stock.Where(x => x.Table_Sale_Houshould.Equals(item)));
            //            }
            //        }
            //        break;
            //    default:
            //        CurrentProductStockList = DBaseClass.BD.Table_Product_Stock.ToList();
            //        break;
            //}

            //if ((bool)cbPlassSales.IsChecked)
            //{
            //    CurrentProductStockList = CurrentProductStockList.Where(x => x.ColorCost == Brushes.AliceBlue).ToList();
            //}

            //switch (cbSort.SelectedIndex)
            //{
            //    case 1:
            //        CurrentProductStockList.Sort((x, y) => x.Table_Staff.surname.CompareTo(y.Table_Staff.surname));
            //        break;
            //    case 2:
            //        CurrentProductStockList.Sort((x, y) => x.Table_Staff.surname.CompareTo(y.Table_Staff.surname));
            //        CurrentProductStockList.Reverse();

            //        break;
            //    case 3:
            //        CurrentProductStockList.Sort((x, y) => x.Table_Staff.name.CompareTo(y.Table_Staff.name));
            //        break;
            //    case 4:
            //        CurrentProductStockList.Sort((x, y) => x.Table_Staff.name.CompareTo(y.Table_Staff.name));
            //        CurrentProductStockList.Reverse();
            //        break;
            //    case 5:
            //        CurrentProductStockList.Sort((x, y) => x.Table_Staff.patronymic.CompareTo(y.Table_Staff.patronymic));
            //        break;
            //    case 6:
            //        CurrentProductStockList.Sort((x, y) => x.Table_Staff.patronymic.CompareTo(y.Table_Staff.patronymic));
            //        CurrentProductStockList.Reverse();
            //        break;

            //}

            //ListProduct.ItemsSource = CurrentProductStockList;
            //if (CurrentProductStockList.Count != 0) 
            //{
            //    SetPagination();
            //}
        }
        private void SetPagination()
        {
            try
            {
                Pagination.CountPage = Convert.ToInt32(tboxPageCount.Text);
            }
            catch
            {
                Pagination.CountPage = CurrentProductStockList.Count;
            }

            Pagination.CountList = CurrentProductStockList.Count;
            ListProduct.ItemsSource = CurrentProductStockList.Skip(0).Take(Pagination.CountPage).ToList();
            Pagination.CurrentPage = 1;
        }
    }
}
