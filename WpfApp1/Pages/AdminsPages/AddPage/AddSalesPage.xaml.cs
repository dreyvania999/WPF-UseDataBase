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
        private double Result;
        private static Table_Sales CurrentSales;
        private static Table_Sale_Houshould EditingHoushouldSales;
        private static Table_Sale_Chemicals EditingChemicalsSales;
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
                ListSaleHousehold.Visibility = Visibility.Collapsed;
            }
            else
            {
                ListSale.Visibility = Visibility.Collapsed;
                ListSaleRes.Visibility = Visibility.Collapsed;
                ListSaleChemicals.ItemsSource = DBaseClass.BD.Table_Sale_Chemicals.ToList().Where(x => x.sales_code == CurrentSales.id_sales);
                ListSaleHousehold.ItemsSource = DBaseClass.BD.Table_Sale_Houshould.ToList().Where(x => x.sales_code == CurrentSales.id_sales);
            }


        }

        public AddSalesPage(Table_Sales currentSales)
        {
            InitializeComponent();
            CurrentSales = currentSales;
            ListSale.Visibility = Visibility.Collapsed;
            ListSaleRes.Visibility = Visibility.Collapsed;
            ListSaleChemicals.ItemsSource = DBaseClass.BD.Table_Sale_Chemicals.ToList().Where(x => x.sales_code == CurrentSales.id_sales);
            ListSaleHousehold.ItemsSource = DBaseClass.BD.Table_Sale_Houshould.ToList().Where(x => x.sales_code == CurrentSales.id_sales);


            IsEditing = true;

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
                if (tbQonity.Text == null || tbQonity.Text == "")
                {
                     MessageBox.Show("Вы не ввели количество товара! Повторите ввод");
                    return;
                }
                int quan = Convert.ToInt32(tbQonity.Text);


                if (EditingHoushouldSales != null && EditingChemicalsSales == null)
                {
                    if (DBaseClass.BD.Table_Product_Stock.Where(x => x.product_code.ToString() == EditingHoushouldSales.product_code.ToString() && x.quantity >= quan).ToList().Count() == 0)
                    {
                         MessageBox.Show("Товара на складе не достаточно");
                        return;
                    }
                    EditingHoushouldSales.quantity = quan;

                    EditingHoushouldSales.product_code = Convert.ToInt32(cbProduct.SelectedValue.ToString());
                     DBaseClass.BD.SaveChanges();

                }
                else if (EditingHoushouldSales == null && EditingChemicalsSales != null)
                {
                    if (DBaseClass.BD.Table_Product_Stock.Where(x => x.product_code.ToString() == EditingChemicalsSales.product_code.ToString() && x.quantity >= quan).ToList().Count() == 0)
                    {
                         MessageBox.Show("Товара на складе не достаточно");
                        return;
                    }
                    EditingChemicalsSales.quantity = quan;

                    EditingChemicalsSales.product_code = Convert.ToInt32(cbProduct.SelectedValue.ToString());
                     DBaseClass.BD.SaveChanges();
                }
                else
                {
                     MessageBox.Show("Не удалось найти товар! повторите выбор товара");

                }

                ListSaleChemicals.ItemsSource = DBaseClass.BD.Table_Sale_Chemicals.ToList().Where(x => x.sales_code == CurrentSales.id_sales);
                ListSaleHousehold.ItemsSource = DBaseClass.BD.Table_Sale_Houshould.ToList().Where(x => x.sales_code == CurrentSales.id_sales);

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
                    System.Collections.Generic.List<Table_Household_Goods> colvo = DBaseClass.BD.Table_Household_Goods.Where(x => x.id_household_goods.ToString() == cbProduct.SelectedValue.ToString()).ToList();
                    foreach (Table_Household_Goods item in colvo)
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
                    System.Collections.Generic.List<Table_Household_Chemicals> colvo = DBaseClass.BD.Table_Household_Chemicals.Where(x => x.id_chemicals.ToString() == cbProduct.SelectedValue.ToString()).ToList();
                    foreach (Table_Household_Chemicals item in colvo)
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
                    ListSale.Text += "Колличество товара " + saleChemicals.quantity + " стоимость(шт) " + saleChemicals.Table_Household_Chemicals.cost + " наименование товара " + saleChemicals.Table_Household_Chemicals.name + "\nОбщая стоимость " + (saleChemicals.quantity * saleChemicals.Table_Household_Chemicals.cost) + "\n";

                }
                else
                {
                     MessageBox.Show("Не удалось найти товар! повторите подбор товара");
                    return;
                }
                cbProduct.SelectedIndex = -1;
                tbQonity.Text = "";
                tbProdduct.Text = "";

                ListSaleRes.Text = "Общая стоимость всех товаров:" + Result;
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            IsEditing = false;
            EditingHoushouldSales = null;
            EditingChemicalsSales = null;
            CurrentSales = null;
             FrameClass.MainFrame.Navigate(new ListViewSalesPage());
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            cbProduct.SelectedIndex = -1;
            tbQonity.Text = "";
            tbProdduct.Text = "";
            EditingHoushouldSales = null;
            EditingChemicalsSales = null;
            CurrentSales = null;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (IsEditing == true)
            {
                EditingChemicalsSales.product_code = Convert.ToInt32(cbProduct.SelectedValue.ToString());
                EditingChemicalsSales.quantity = Convert.ToInt32(tbQonity.Text.ToString());
            }

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
             FrameClass.MainFrame.Navigate(new AddSalesPage());
        }

        private void btnDeleteChemicals_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = Convert.ToInt32(button.Uid);

            Table_Sale_Chemicals saleChemicals = DBaseClass.BD.Table_Sale_Chemicals.FirstOrDefault(x => x.id_sale_inform == id && x.sales_code == CurrentSales.id_sales);

             DBaseClass.BD.Table_Sale_Chemicals.Remove(saleChemicals);

             DBaseClass.BD.SaveChanges();
             MessageBox.Show("Информация удалена");

            ListSaleChemicals.ItemsSource = DBaseClass.BD.Table_Sale_Chemicals.ToList().Where(x => x.sales_code == CurrentSales.id_sales);

        }
        private void btnDeleteHousehold_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = Convert.ToInt32(button.Uid);

            Table_Sale_Houshould saleHoushould = DBaseClass.BD.Table_Sale_Houshould.FirstOrDefault(x => x.id_sale_inform == id && x.sales_code == CurrentSales.id_sales);

             DBaseClass.BD.Table_Sale_Houshould.Remove(saleHoushould);

             DBaseClass.BD.SaveChanges();
             MessageBox.Show("Информация удалена");
            ListSaleHousehold.ItemsSource = DBaseClass.BD.Table_Sale_Houshould.ToList().Where(x => x.sales_code == CurrentSales.id_sales);
        }



        private void StackPanelHoushols_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            StackPanel stackPanel = (StackPanel)sender;
            int id = Convert.ToInt32(stackPanel.Uid);
            EditingHoushouldSales = DBaseClass.BD.Table_Sale_Houshould.FirstOrDefault(x => x.id_sale_inform == id && x.sales_code == CurrentSales.id_sales);

            cbProduct.ItemsSource = DBaseClass.BD.Table_Household_Goods.Where(x => x.name.Contains(tbProdduct.Text)).ToList();
            cbProduct.SelectedValuePath = "id_household_goods";
            cbProduct.DisplayMemberPath = "name";

            cbProduct.SelectedValue = EditingHoushouldSales.product_code;
            tbQonity.Text = EditingHoushouldSales.quantity.ToString();
            tbProdduct.Text = "";
            EditingChemicalsSales = null;
        }

        private void StackPanelChemicals_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            StackPanel stackPanel = (StackPanel)sender;
            int id = Convert.ToInt32(stackPanel.Uid);
            EditingChemicalsSales = DBaseClass.BD.Table_Sale_Chemicals.FirstOrDefault(x => x.id_sale_inform == id && x.sales_code == CurrentSales.id_sales);

            cbProduct.ItemsSource = DBaseClass.BD.Table_Household_Chemicals.Where(x => x.name.Contains(tbProdduct.Text)).ToList();
            cbProduct.SelectedValuePath = "id_chemicals";
            cbProduct.DisplayMemberPath = "name";

            cbProduct.SelectedValue = EditingChemicalsSales.product_code;
            tbQonity.Text = EditingChemicalsSales.quantity.ToString();
            tbProdduct.Text = "";
            EditingHoushouldSales = null;
        }
    }
}

