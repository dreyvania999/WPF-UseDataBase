﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows.Media;

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

        public SolidColorBrush ColorProperty
        {
            get
            {
                SolidColorBrush propertyColor = new SolidColorBrush(Color.FromRgb(111, 111, 111));
                if (product_code != null)
                {
                    propertyColor = new SolidColorBrush(Color.FromRgb(199, 240, 254));
                    return propertyColor;
                }
                else if (chemical_code != null)
                {
                    propertyColor = new SolidColorBrush(Color.FromRgb(83, 168, 225));
                    return propertyColor;
                }
                
                return propertyColor;
            }
        }
        public SolidColorBrush ColorCount
        {
            get
            {
                SolidColorBrush propertyColor = new SolidColorBrush(Color.FromRgb(111, 111, 111));
                if (quantity > 10)
                {
                    propertyColor = new SolidColorBrush(Color.FromRgb(199, 240, 254));
                    return propertyColor;
                }
                else
                {
                    propertyColor = new SolidColorBrush(Color.FromRgb(100, 11, 254));
                    return propertyColor;
                }

                return propertyColor;
            }
        }
    }
}