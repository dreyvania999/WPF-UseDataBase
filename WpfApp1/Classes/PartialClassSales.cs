using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                foreach (var item in Table_Sale_Houshould)
                {
                    str = "Колличество товара "+item.quantity+ " наименование товара " + item.Table_Household_Goods.name +" общая стоимость "+ item.quantity * item.Table_Household_Goods.cost+ "\n";
                }
                return str;
            }
        }
    }
}
