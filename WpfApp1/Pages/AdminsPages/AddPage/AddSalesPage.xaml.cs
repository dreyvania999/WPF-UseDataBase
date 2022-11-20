using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Classes;

namespace WpfApp1.Pages.AdminsPages.AddPage
{
    /// <summary>
    /// Логика взаимодействия для AddSalesPage.xaml
    /// </summary>
    public partial class AddSalesPage : Page
    {
        double Result;
        private static Table_Sales CurrentSales;
        public static bool IsEditing = false;
        public AddSalesPage()
        {
            InitializeComponent();
            Result = 0;
            cbProduct.ItemsSource = DBaseClass.BD.Table_Household_Chemicals.ToList();
            cbProduct.SelectedValuePath = "id_chemicals";
            cbProduct.DisplayMemberPath = "name";
            if (!IsEditing)
            {
                CurrentSales = new Table_Sales()
                {
                    date_sales = DateTime.Now.Date,
                    staff_code = StaffClass.CurrentStaffEmploe.id_staff
                };
                DBaseClass.BD.Table_Sales.Add(CurrentSales);
                ListSaleChemicals.Visibility = Visibility.Collapsed;
            }
            else
            {
                ListSale.Visibility= Visibility.Collapsed;
                ListSaleRes.Visibility = Visibility.Collapsed;
                ListSaleChemicals.ItemsSource = DBaseClass.BD.Table_Sale_Chemicals.ToList().Where(x => x.sales_code == CurrentSales.id_sales);
            }
            

        }

        public static void SetTable_Sales(Table_Sales sales)
        {
            CurrentSales = sales;
        }

        private void rbChimicals_Checked(object sender, RoutedEventArgs e)
        {
            cbProduct.ItemsSource = DBaseClass.BD.Table_Household_Chemicals.Where(x => x.name.Contains(tbProdduct.Text)).ToList();
            cbProduct.SelectedValuePath = "id_chemicals";
            cbProduct.DisplayMemberPath = "name";
        }

        private void rbGoods_Checked(object sender, RoutedEventArgs e)
        {
            cbProduct.ItemsSource = DBaseClass.BD.Table_Household_Goods.Where(x => x.name.Contains(tbProdduct.Text)).ToList();
            cbProduct.SelectedValuePath = "id_household_goods";
            cbProduct.DisplayMemberPath = "name";
        }

        private void tbProdduct_TextChanged(object sender, TextChangedEventArgs e)
        {
            if ((bool)rbGoods.IsChecked && !(bool)rbChimicals.IsChecked)
            {
                cbProduct.ItemsSource = DBaseClass.BD.Table_Household_Goods.Where(x => x.name.Contains(tbProdduct.Text)).ToList();
                cbProduct.SelectedValuePath = "id_household_goods";
                cbProduct.DisplayMemberPath = "name";
            }
            else if (!(bool)rbGoods.IsChecked && (bool)rbChimicals.IsChecked)
            {
                cbProduct.ItemsSource = DBaseClass.BD.Table_Household_Chemicals.Where(x => x.name.Contains(tbProdduct.Text)).ToList();
                cbProduct.SelectedValuePath = "id_chemicals";
                cbProduct.DisplayMemberPath = "name";
            }
            else
            {
                MessageBox.Show("Не удалось найти товар! повторите поиск товара");
            }

        }

        private void btnFiltering_Click(object sender, RoutedEventArgs e)
        {
            if (IsEditing)
            {
                ListSaleChemicals.ItemsSource = DBaseClass.BD.Table_Sale_Chemicals.ToList().Where(x => x.sales_code == CurrentSales.id_sales);
            }
            else
            {
                if (tbQonity.Text == null || tbQonity.Text == "")
                {
                    MessageBox.Show("Вы не ввели количество товара! Повторите ввод");
                    return;
                }
                int quan = Convert.ToInt32(tbQonity.Text);
                if ((bool)rbGoods.IsChecked && !(bool)rbChimicals.IsChecked)
                {
                    var colvo = DBaseClass.BD.Table_Household_Goods.Where(x => x.id_household_goods.ToString() == cbProduct.SelectedValue.ToString()).ToList();
                    foreach (var item in colvo)
                    {
                        if (DBaseClass.BD.Table_Product_Stock.Where(x => x.product_code.ToString() == item.id_household_goods.ToString() && x.quantity >= quan).ToList().Count() == 0)
                        {
                            MessageBox.Show("Товара на складе не достаточно");
                            return;
                        }
                    }

                    Table_Sale_Houshould saleHoushould = new Table_Sale_Houshould()
                    {
                        quantity = quan,
                        sales_code = CurrentSales.id_sales,
                        product_code = Convert.ToInt32(cbProduct.SelectedValue.ToString())
                    };

                    DBaseClass.BD.Table_Sale_Houshould.Add(saleHoushould);
                    double f = saleHoushould.quantity * saleHoushould.Table_Household_Goods.cost;
                    Result += f;
                    ListSale.Text += "Колличество товара " + saleHoushould.quantity + " наименование товара " + saleHoushould.Table_Household_Goods.name + "\nОбщая стоимость " + (saleHoushould.quantity * saleHoushould.Table_Household_Goods.cost) + "\n";

                }
                else if (!(bool)rbGoods.IsChecked && (bool)rbChimicals.IsChecked)
                {
                    var colvo = DBaseClass.BD.Table_Household_Chemicals.Where(x => x.id_chemicals.ToString() == cbProduct.SelectedValue.ToString()).ToList();
                    foreach (var item in colvo)
                    {
                        if (DBaseClass.BD.Table_Product_Stock.Where(x => x.chemical_code.ToString() == item.id_chemicals.ToString() && x.quantity >= quan).ToList().Count() == 0)
                        {
                            MessageBox.Show("Товара на складе не достаточно");
                            return;
                        }
                    }

                    Table_Sale_Chemicals saleChemicals = new Table_Sale_Chemicals()
                    {
                        quantity = quan,
                        sales_code = CurrentSales.id_sales,
                        product_code = Convert.ToInt32(cbProduct.SelectedValue.ToString())
                    };
                    
                    DBaseClass.BD.Table_Sale_Chemicals.Add(saleChemicals);

                    double f = saleChemicals.quantity * saleChemicals.Table_Household_Chemicals.cost;
                    Result += f;
                    ListSale.Text += "Колличество товара " + saleChemicals.quantity+" стоимость(шт) "+ saleChemicals.Table_Household_Chemicals.cost + " наименование товара " + saleChemicals.Table_Household_Chemicals.name + "\nОбщая стоимость " + (saleChemicals.quantity * saleChemicals.Table_Household_Chemicals.cost) + "\n";

                }
                else
                {
                    MessageBox.Show("Не удалось найти товар! повторите подбор товара");
                    return;
                }
                cbProduct.SelectedIndex = -1;
                tbQonity.Text = "";
                tbProdduct.Text = "";
                ListSaleRes.Text = "Общая стоимость всех товаров:" +Result;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            IsEditing = false;
            FrameClass.MainFrame.Navigate(new MainPage());
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            cbProduct.SelectedIndex = -1;
            tbQonity.Text = "";
            tbProdduct.Text = "";

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ListSale.Text = "";
            ListSaleRes.Text = "";
            DBaseClass.BD.SaveChanges();
            MessageBox.Show("Изменения были успешно внесены");
            CurrentSales = new Table_Sales()
            {
                date_sales = DateTime.Now.Date,
                staff_code = StaffClass.CurrentStaffEmploe.id_staff
            };
            DBaseClass.BD.Table_Sales.Add(CurrentSales);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StackPanel_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
