using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Classes;

namespace WpfApp1.Pages.AdminsPages.AddPage
{
    /// <summary>
    /// Логика взаимодействия для AddDeliveryPage.xaml
    /// </summary>
    public partial class AddDeliveryPage : Page
    {
        private double Result;
        private static Table_Delivery CurrentDelivery;
        private static Table_Household_Delivery EditingHoushouldDelivery;
        private static Table_Chemicals_Delivery EditingChemicalsDelivery;
        public static bool IsEditing = false;
        public AddDeliveryPage()
        {
            InitializeComponent();
            Result = 0;
            cbProduct.ItemsSource = DBaseClass.BD.Table_Household_Chemicals.ToList();
            cbProduct.SelectedValuePath = "id_chemicals";
            cbProduct.DisplayMemberPath = "name";
            if (!IsEditing)
            {
                CurrentDelivery = new Table_Delivery() { delivery_date = DateTime.Now };
                DBaseClass.BD.Table_Delivery.Add(CurrentDelivery);
                ListDeliveryChemicals.Visibility = Visibility.Collapsed;
                ListDeliveryHousehold.Visibility = Visibility.Collapsed;
            }
            else
            {
                ListSale.Visibility = Visibility.Collapsed;
                ListSaleRes.Visibility = Visibility.Collapsed;
                ListDeliveryChemicals.ItemsSource = DBaseClass.BD.Table_Chemicals_Delivery.ToList().Where(x => x.delivery_code == CurrentDelivery.id_delivery);
                ListDeliveryHousehold.ItemsSource = DBaseClass.BD.Table_Household_Delivery.ToList().Where(x => x.delivery_code == CurrentDelivery.id_delivery);
            }


        }

        public static void SetTable_Sales(Table_Delivery currentDelivery)
        {
            CurrentDelivery = currentDelivery;
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


                if (EditingHoushouldDelivery != null && EditingChemicalsDelivery == null)
                {

                    EditingHoushouldDelivery.quantity_goods = quan;

                    EditingHoushouldDelivery.product_code = Convert.ToInt32(cbProduct.SelectedValue.ToString());
                    DBaseClass.BD.SaveChanges();

                }
                else if (EditingHoushouldDelivery == null && EditingChemicalsDelivery != null)
                {

                    EditingChemicalsDelivery.quantity_goods = quan;

                    EditingChemicalsDelivery.product_code = Convert.ToInt32(cbProduct.SelectedValue.ToString());
                    DBaseClass.BD.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Не удалось найти товар! повторите выбор товара");
                    return;
                }

                ListDeliveryChemicals.ItemsSource = DBaseClass.BD.Table_Chemicals_Delivery.ToList().Where(x => x.delivery_code == CurrentDelivery.id_delivery);
                ListDeliveryHousehold.ItemsSource = DBaseClass.BD.Table_Household_Delivery.ToList().Where(x => x.delivery_code == CurrentDelivery.id_delivery);
                DBaseClass.BD.SaveChanges();
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


                    Table_Household_Delivery saleHoushould = new Table_Household_Delivery()
                    {
                        quantity_goods = quan,
                        delivery_code = CurrentDelivery.id_delivery,
                        product_code = Convert.ToInt32(cbProduct.SelectedValue.ToString())
                    };

                    DBaseClass.BD.Table_Household_Delivery.Add(saleHoushould);

                    double f = saleHoushould.quantity_goods * saleHoushould.Table_Household_Goods.cost;
                    Result += f;
                    ListSale.Text += "Колличество товара " + saleHoushould.quantity_goods + " наименование товара " + saleHoushould.Table_Household_Goods.name + "\nОбщая стоимость " + (saleHoushould.quantity_goods * saleHoushould.Table_Household_Goods.cost) + "\n";

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

                    Table_Chemicals_Delivery deliveryChemicals = new Table_Chemicals_Delivery()
                    {
                        quantity_goods = quan,
                        delivery_code = CurrentDelivery.id_delivery,
                        product_code = Convert.ToInt32(cbProduct.SelectedValue.ToString())
                    };

                    DBaseClass.BD.Table_Chemicals_Delivery.Add(deliveryChemicals);


                    double f = deliveryChemicals.quantity_goods * deliveryChemicals.Table_Household_Chemicals.cost;
                    Result += f;
                    ListSale.Text += "Колличество товара " + deliveryChemicals.quantity_goods + " стоимость(шт) " + deliveryChemicals.Table_Household_Chemicals.cost + " наименование товара " + deliveryChemicals.Table_Household_Chemicals.name + "\nОбщая стоимость " + (deliveryChemicals.quantity_goods * deliveryChemicals.Table_Household_Chemicals.cost) + "\n";

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
            EditingHoushouldDelivery = null;
            EditingChemicalsDelivery = null;
            CurrentDelivery = null;
            FrameClass.MainFrame.Navigate(new MainPage());
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            cbProduct.SelectedIndex = -1;
            tbQonity.Text = "";
            tbProdduct.Text = "";
            EditingHoushouldDelivery = null;
            EditingChemicalsDelivery = null;
            CurrentDelivery = null;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            ListSale.Text = "";
            ListSaleRes.Text = "";
            DBaseClass.BD.SaveChanges();
            MessageBox.Show("Изменения были успешно внесены");
            CurrentDelivery = new Table_Delivery()
            {
                delivery_date = DateTime.Now.Date
            };
            DBaseClass.BD.Table_Delivery.Add(CurrentDelivery);
        }

        private void btnDeleteChemicals_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = Convert.ToInt32(button.Uid);

            Table_Chemicals_Delivery saleChemicals = DBaseClass.BD.Table_Chemicals_Delivery.FirstOrDefault(x => x.id_product_in_delivery == id && x.delivery_code == CurrentDelivery.id_delivery);

            DBaseClass.BD.Table_Chemicals_Delivery.Remove(saleChemicals);

            DBaseClass.BD.SaveChanges();
            MessageBox.Show("Информация удалена");

            ListDeliveryChemicals.ItemsSource = DBaseClass.BD.Table_Chemicals_Delivery.ToList().Where(x => x.delivery_code == CurrentDelivery.id_delivery);

        }
        private void btnDeleteHousehold_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int id = Convert.ToInt32(button.Uid);

            Table_Household_Delivery saleHoushould = DBaseClass.BD.Table_Household_Delivery.FirstOrDefault(x => x.id_product_in_delivery == id && x.delivery_code == CurrentDelivery.id_delivery);

            DBaseClass.BD.Table_Household_Delivery.Remove(saleHoushould);

            DBaseClass.BD.SaveChanges();
            MessageBox.Show("Информация удалена");

            ListDeliveryHousehold.ItemsSource = DBaseClass.BD.Table_Household_Delivery.ToList().Where(x => x.delivery_code == CurrentDelivery.id_delivery);

        }



        private void StackPanelHoushols_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            StackPanel stackPanel = (StackPanel)sender;
            int id = Convert.ToInt32(stackPanel.Uid);
            EditingHoushouldDelivery = DBaseClass.BD.Table_Household_Delivery.FirstOrDefault(x => x.id_product_in_delivery == id && x.delivery_code == CurrentDelivery.id_delivery);

            cbProduct.ItemsSource = DBaseClass.BD.Table_Household_Goods.Where(x => x.name.Contains(tbProdduct.Text)).ToList();
            cbProduct.SelectedValuePath = "id_household_goods";
            cbProduct.DisplayMemberPath = "name";

            cbProduct.SelectedValue = EditingHoushouldDelivery.product_code;
            tbQonity.Text = EditingHoushouldDelivery.quantity_goods.ToString();
            tbProdduct.Text = "";
            EditingChemicalsDelivery = null;
        }

        private void StackPanelChemicals_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            StackPanel stackPanel = (StackPanel)sender;
            int id = Convert.ToInt32(stackPanel.Uid);
            EditingChemicalsDelivery = DBaseClass.BD.Table_Chemicals_Delivery.FirstOrDefault(x => x.id_product_in_delivery == id && x.delivery_code == CurrentDelivery.id_delivery);

            cbProduct.ItemsSource = DBaseClass.BD.Table_Household_Chemicals.Where(x => x.name.Contains(tbProdduct.Text)).ToList();
            cbProduct.SelectedValuePath = "id_chemicals";
            cbProduct.DisplayMemberPath = "name";

            cbProduct.SelectedValue = EditingChemicalsDelivery.product_code;
            tbQonity.Text = EditingChemicalsDelivery.quantity_goods.ToString();
            tbProdduct.Text = "";
            EditingHoushouldDelivery = null;

        }

        private void btnAddGood_Click(object sender, RoutedEventArgs e)
        {
            FrameClass.MainFrame.Navigate(new AddProductPage());
        }
    }
}
