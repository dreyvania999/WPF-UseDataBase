﻿using System.Linq;
using System.Windows.Media;

namespace WpfApp1
{
    public partial class Table_Sales
    {
        public string FullNameStaffEmployee => Table_Staff.name + " " + Table_Staff.patronymic + " " + Table_Staff.surname;

        public string SaleInform
        {
            get
            {
                string str = "";
                double f = 0;
                foreach (Table_Sale_Houshould item in Table_Sale_Houshould.Where(x => x.sales_code.Equals(id_sales)))
                {
                    f += item.quantity * item.Table_Household_Goods.cost;
                    str += "Колличество товара " + item.quantity + " наименование товара " + item.Table_Household_Goods.name + "\nОбщая стоимость " + (item.quantity * item.Table_Household_Goods.cost) + "\n";
                }
                foreach (Table_Sale_Chemicals item in Table_Sale_Chemicals.Where(x => x.sales_code.Equals(id_sales)))
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
                double f = 0;
                foreach (Table_Sale_Houshould item in Table_Sale_Houshould.Where(x => x.sales_code.Equals(id_sales)))
                {
                    f += item.quantity * item.Table_Household_Goods.cost;
                }
                foreach (Table_Sale_Chemicals item in Table_Sale_Chemicals.Where(x => x.sales_code.Equals(id_sales)))
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