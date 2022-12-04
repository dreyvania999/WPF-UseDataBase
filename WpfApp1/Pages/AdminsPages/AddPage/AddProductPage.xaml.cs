using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1.Pages.AdminsPages.AddPage
{
    /// <summary>
    /// Логика взаимодействия для AddProductPage.xaml
    /// </summary>
    public partial class AddProductPage : Page
    {
        public static bool IsEditing = false;
        private Table_Household_Chemicals HC;
        private Table_Household_Goods HG;

        public AddProductPage()
        {
            InitializeComponent();
            cbManufacturerAd.ItemsSource = DBaseClass.BD.Table_Manufacturer.ToList();
            cbManufacturerAd.SelectedValuePath = "id_manufacturer";
            cbManufacturerAd.DisplayMemberPath = "name";

            cbManufacturer.ItemsSource = DBaseClass.BD.Table_Manufacturer.ToList();
            cbManufacturer.SelectedValuePath = "id_manufacturer";
            cbManufacturer.DisplayMemberPath = "name";

            cbChecimalDestination.ItemsSource = DBaseClass.BD.Table_Purpose_Chemistry.ToList();
            cbChecimalDestination.SelectedValuePath = "id_purpose";
            cbChecimalDestination.DisplayMemberPath = "purpose_chemistry";

            IsEditing = false;
        }


        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            IsEditing = false;
             FrameClass.MainFrame.Navigate(new AddDeliveryPage());
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
             DBaseClass.BD.SaveChanges();
             MessageBox.Show("Изменения были успешно внесены");
        }

        private void btnAddChemicals_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (IsEditing == false)
                {
                    HC = new Table_Household_Chemicals();
                }

                HC.name = tbProductChecimal.Text.ToString();
                HC.destination_code = Convert.ToInt32(cbChecimalDestination.SelectedValue.ToString());
                HC.manufacturer_code = Convert.ToInt32(cbManufacturerAd.SelectedValue.ToString());
                HC.cost = Convert.ToDouble(tbChemicalCost.Text.ToString());

                if (IsEditing == false)
                {
                    DBaseClass.BD.Table_Household_Chemicals.Add(HC);
                }

                DBaseClass.BD.SaveChanges();
                MessageBox.Show("Информация о товаре добавлена");
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Что-то пошло не так. Обратитесь к администратору или повторите попытку.");
               
            }
            
        }

        private void btnAddGoods_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (IsEditing == false)
                {
                    HG = new Table_Household_Goods();
                }

                HG.name = tbProductGoods.Text.ToString();
                HG.manufacturer_code = Convert.ToInt32(cbManufacturer.SelectedValue.ToString());
                HG.cost = Convert.ToDouble(tbGoodsCost.Text.ToString());

                if (IsEditing == false)
                {
                    DBaseClass.BD.Table_Household_Goods.Add(HG);
                }

                DBaseClass.BD.SaveChanges();
                MessageBox.Show("Информация о товаре добавлена");
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Что-то пошло не так. Обратитесь к администратору или повторите попытку.");
               
            }
        }
   

        private void btnAddManufacterer_Click(object sender, RoutedEventArgs e)
        {
             FrameClass.MainFrame.Navigate(new AddManufactererPage());
        }

        private void btnVisibleChemicals_Click(object sender, RoutedEventArgs e)
        {
            spChemicals.Visibility = Visibility.Visible;
            Goods.Visibility = Visibility.Collapsed;
        }

        private void btnVisibleGoods_Click(object sender, RoutedEventArgs e)
        {
            Goods.Visibility = Visibility.Visible;
            spChemicals.Visibility = Visibility.Collapsed;
        }
    }
}
