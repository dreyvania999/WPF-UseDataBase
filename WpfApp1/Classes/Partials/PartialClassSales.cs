using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace WpfApp1
{
    public partial class Table_Sales
    {
        public string FullNameStaffEmployee => Table_Staff.name + " " + Table_Staff.patronymic + " " + Table_Staff.surname;

        public string ProdictOfSale => "";

        public string SaleInform
        {
            get
            {
                string str = "";
                double f = 0;
                List<Table_Sale_Houshould> saleHoushould = Table_Sale_Houshould.Where(x => x.sales_code.Equals(id_sales)).ToList();

                if (saleHoushould.Count > 0)
                {
                    str += "Товары для дома:\n";
                }
                foreach (Table_Sale_Houshould item in saleHoushould)
                {
                    f += item.quantity * item.Table_Household_Goods.cost;
                    str += "Колличество товара " + item.quantity + " наименование товара " + item.Table_Household_Goods.name + "\nОбщая стоимость " + (item.quantity * item.Table_Household_Goods.cost) + "\n";
                }
                List<Table_Sale_Chemicals> saleCimicals = Table_Sale_Chemicals.Where(x => x.sales_code.Equals(id_sales)).ToList();
                if (saleCimicals.Count > 0)
                {
                    str += "Бытовая химия:\n";
                }
                foreach (Table_Sale_Chemicals item in saleCimicals)
                {
                    f += item.quantity * item.Table_Household_Chemicals.cost;
                    str += "Колличество товара " + item.quantity + " наименование товара " + item.Table_Household_Chemicals.name + "\nОбщая стоимость " + (item.quantity * item.Table_Household_Chemicals.cost) + "\n";
                }
                str += "\nИтого: " + f;
                return str;
            }
        }
        public SolidColorBrush ColorCost
        {
            get
            {
                List<Table_Sale_Chemicals> saleCimicals = Table_Sale_Chemicals.Where(x => x.sales_code.Equals(id_sales)).ToList();

                List<Table_Sale_Houshould> saleHoushould = Table_Sale_Houshould.Where(x => x.sales_code.Equals(id_sales)).ToList();

                double f = 0;
                foreach (Table_Sale_Houshould item in saleHoushould)
                {
                    f += item.quantity * item.Table_Household_Goods.cost;
                }
                foreach (Table_Sale_Chemicals item in saleCimicals)
                {
                    f += item.quantity * item.Table_Household_Chemicals.cost;
                }
                if (f >= 500)
                {
                    return Brushes.AliceBlue; ;
                }
                else
                {
                    return Brushes.Bisque;
                }


            }
        }
    }
}