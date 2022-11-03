using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp1
{
    public partial class Table_Sales
    {
        public string FullNameStaffEmployee
        {
            get
            {
                return Table_Staff.name + " " + Table_Staff.patronymic + " " + Table_Staff.surname;
            }
        }

        public string SaleInform
        {
            get
            {
                string str = "";

                foreach (var item in Table_Sale_Houshould.Where(x => x.sales_code.Equals(id_sales)))
                {
                    str += "Колличество товара "+item.quantity+ " наименование товара " + item.Table_Household_Goods.name +"\nОбщая стоимость "+ item.quantity * item.Table_Household_Goods.cost+ "\n";
                }
                return str;
            }
        }
        public SolidColorBrush ColorCost
        {
            get
            {
                double f =0;
                foreach (var item in Table_Sale_Houshould.Where(x => x.sales_code.Equals(id_sales)))
                {
                     f +=item.quantity * item.Table_Household_Goods.cost ;
                }
                if (f >= 500)
                {
                    return new SolidColorBrush(Color.FromRgb(199, 240, 254));
                }
                else
                { 
                    return new SolidColorBrush(Color.FromRgb(100, 11, 254));
                }

                
            }
        }
    }
}
