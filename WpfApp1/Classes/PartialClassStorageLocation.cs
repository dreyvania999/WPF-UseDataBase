using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public partial class Table_Product_Stock
    {
        public string ChemicalsORHouseholdGoods
        {
            get
            {
                string d = "";
                if (chemical_code==null)
                {

                        d += Table_Household_Goods.name + " от производителя " +
                Table_Household_Goods.Table_Manufacturer.name;
                    
                }
                else if (product_code==null)
                {
                    
                        d+= Table_Household_Chemicals.Table_Purpose_Chemistry.purpose_chemistry + " "
                + Table_Household_Chemicals.name + " от производителя " +
                Table_Household_Chemicals.Table_Manufacturer.name;
                    
                }

                return d;

            }
        }

        
    }
}
