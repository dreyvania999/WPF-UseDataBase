using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public partial class Table_Product_Stock
    {
        public string Chemicals
        {
            get
            {
                return Table_Household_Chemicals.Table_Purpose_Chemistry.purpose_chemistry+" "
                + Table_Household_Chemicals.name +" от производителя "+
                Table_Household_Chemicals.Table_Manufacturer.name;
            }
        }

        public string HouseholdGoods
        {
            get
            {
                return Table_Household_Goods.name + " от производителя " + 
                Table_Household_Goods.Table_Manufacturer.name;
            }
        }
    }
}
