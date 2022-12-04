using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Pages.AdminsPages.ViewsPage;

namespace WpfApp1.Pages.AdminsPages.AddPage
{
    /// <summary>
    /// Логика взаимодействия для AddDeliveryPage.xaml
    /// </summary>
    public partial class AddDeliveryPage : Page
    {
        private static Table_Delivery CurrentDelivery;
        private static Table_Household_Delivery EditingHoushouldDelivery;
        private static Table_Chemicals_Delivery EditingChemicalsDelivery;
        public static bool IsEditing = false;
        public AddDeliveryPage()
        {
            InitializeComponent();
            cbManufacturer.ItemsSource = DBaseClass.BD.Table_Suppliers.ToList();
            cbManufacturer.SelectedValuePath = "id_suppliers";
            cbManufacturer.DisplayMemberPath = "name_supplier";

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
               
                ListDeliveryChemicals.ItemsSource = DBaseClass.BD.Table_Chemicals_Delivery.ToList().Where(x => x.delivery_code == CurrentDelivery.id_delivery);
                ListDeliveryHousehold.ItemsSource = DBaseClass.BD.Table_Household_Delivery.ToList().Where(x => x.delivery_code == CurrentDelivery.id_delivery);
            }
        }
        public AddDeliveryPage(Table_Delivery currentDelivery)
        {
            InitializeComponent();
            CurrentDelivery = currentDelivery;

            ListDeliveryChemicals.ItemsSource = DBaseClass.BD.Table_Chemicals_Delivery.ToList().Where(x => x.delivery_code == CurrentDelivery.id_delivery);
            ListDeliveryHousehold.ItemsSource = DBaseClass.BD.Table_Household_Delivery.ToList().Where(x => x.delivery_code == CurrentDelivery.id_delivery);

            IsEditing = true;

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
            DBaseClass.BD.SaveChanges();
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
                if(cbManufacturer==null)
                {
                    MessageBox.Show("Вы не указали производителя! Повторите ввод");
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

                    
                }
                else if (!(bool)rbGoods.IsChecked && (bool)rbChimicals.IsChecked)
                {
                    

                    Table_Chemicals_Delivery deliveryChemicals = new Table_Chemicals_Delivery()
                    {
                        quantity_goods = quan,
                        delivery_code = CurrentDelivery.id_delivery,
                        product_code = Convert.ToInt32(cbProduct.SelectedValue.ToString())
                    };

                     DBaseClass.BD.Table_Chemicals_Delivery.Add(deliveryChemicals);

                }
                else
                {
                     MessageBox.Show("Не удалось найти товар! повторите подбор товара");
                    return;
                }
                cbProduct.SelectedIndex = -1;
                tbQonity.Text = "";
                tbProdduct.Text = "";

            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            IsEditing = false;
            EditingHoushouldDelivery = null;
            EditingChemicalsDelivery = null;
            CurrentDelivery = null;
             FrameClass.MainFrame.Navigate(new ListViewDeliveryPage());
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

            rbGoods.IsChecked = true;

            cbProduct.ItemsSource = DBaseClass.BD.Table_Household_Goods.Where(x => x.name.Contains(tbProdduct.Text)).ToList();
            cbProduct.SelectedValuePath = "id_household_goods";
            cbProduct.DisplayMemberPath = "name";

            cbManufacturer.ItemsSource = DBaseClass.BD.Table_Suppliers.ToList();
            cbManufacturer.SelectedValuePath = "id_suppliers";
            cbManufacturer.DisplayMemberPath = "name_supplier";

            cbProduct.SelectedValue = EditingHoushouldDelivery.product_code;
            tbQonity.Text = EditingHoushouldDelivery.quantity_goods.ToString();
            cbManufacturer.SelectedValue=CurrentDelivery.provider_code;
            tbProdduct.Text = "";
            EditingChemicalsDelivery = null;
        }

        private void StackPanelChemicals_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            StackPanel stackPanel = (StackPanel)sender;
            int id = Convert.ToInt32(stackPanel.Uid);
            EditingChemicalsDelivery = DBaseClass.BD.Table_Chemicals_Delivery.FirstOrDefault(x => x.id_product_in_delivery == id && x.delivery_code == CurrentDelivery.id_delivery);

            rbChimicals.IsChecked = true;

            cbProduct.ItemsSource = DBaseClass.BD.Table_Household_Chemicals.Where(x => x.name.Contains(tbProdduct.Text)).ToList();
            cbProduct.SelectedValuePath = "id_chemicals";
            cbProduct.DisplayMemberPath = "name";

            cbManufacturer.ItemsSource = DBaseClass.BD.Table_Suppliers.ToList();
            cbManufacturer.SelectedValuePath = "id_suppliers";
            cbManufacturer.DisplayMemberPath = "name_supplier";

            cbProduct.SelectedValue = EditingChemicalsDelivery.product_code;
            tbQonity.Text = EditingChemicalsDelivery.quantity_goods.ToString();
            cbManufacturer.SelectedValue = CurrentDelivery.provider_code;

            tbProdduct.Text = "";
            EditingHoushouldDelivery = null;

        }

        private void btnAddGood_Click(object sender, RoutedEventArgs e)
        {
             FrameClass.MainFrame.Navigate(new AddProductPage());
        }

        private void cbManufacturer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CurrentDelivery.provider_code = Convert.ToInt32(cbManufacturer.SelectedValue.ToString());
            DBaseClass.BD.SaveChanges();
        }
    }
}
