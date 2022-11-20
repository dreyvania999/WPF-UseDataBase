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
        private static Table_Sales CurrentSales;
        public AddSalesPage()
        {
            InitializeComponent();
            cbProduct.ItemsSource = DBaseClass.BD.Table_Household_Chemicals.ToList();
            cbProduct.SelectedValuePath = "id_chemicals";
            cbProduct.DisplayMemberPath = "name";
            CurrentSales = new Table_Sales()
            {
                date_sales = DateTime.Now.Date,
                staff_code = StaffClass.CurrentStaffEmploe.id_staff
            };
            DBaseClass.BD.Table_Sales.Add(CurrentSales);
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


            }
            else
            {
                MessageBox.Show("Не удалось найти товар! повторите подбор товара");
                return;
            }
            cbProduct.SelectedIndex = -1;
            tbQonity.Text = "";
            tbProdduct.Text = "";
            dgUsers.ItemsSource = DBaseClass.BD.Table_Sale_Chemicals.ToList().Where(x=>x.sales_code == CurrentSales.id_sales);

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new MainPage());

        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            cbProduct.SelectedIndex =-1;
            tbQonity.Text = "";
            tbProdduct.Text = "";
            dgUsers.ItemsSource = DBaseClass.BD.Table_Sale_Chemicals.ToList().Where(x => x.sales_code == CurrentSales.id_sales);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DBaseClass.BD.SaveChanges();
            MessageBox.Show("Изменения были успешно внесены");
            CurrentSales = new Table_Sales()
            {
                date_sales = DateTime.Now.Date,
                staff_code = StaffClass.CurrentStaffEmploe.id_staff
            };
            DBaseClass.BD.Table_Sales.Add(CurrentSales);
        }
    }
}
