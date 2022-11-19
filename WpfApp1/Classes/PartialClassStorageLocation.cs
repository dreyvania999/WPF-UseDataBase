﻿using System.Windows.Media;

namespace WpfApp1
{
    public partial class Table_Product_Stock
    {
        public string ChemicalsORHouseholdGoods
        {
            get
            {
                string d = "";
                if (chemical_code == null)
                {

                    d += Table_Household_Goods.name + " от производителя " +
            Table_Household_Goods.Table_Manufacturer.name + "\nЦена " + Table_Household_Goods.cost;

                }
                else if (product_code == null)
                {

                    d += Table_Household_Chemicals.Table_Purpose_Chemistry.purpose_chemistry + " "
            + Table_Household_Chemicals.name + " от производителя " +
            Table_Household_Chemicals.Table_Manufacturer.name + "\nЦена " + Table_Household_Chemicals.cost;

                }

                return d;

            }
        }

        public string PhotoProduct
        {
            get
            {

                if (chemical_code != null)
                {

                    return Table_Household_Chemicals.photo_chemicals;

                }
                else if (product_code != null)
                {

                    return Table_Household_Goods.photo_household;

                }

                return null;

            }
        }

        public SolidColorBrush ColorProperty
        {
            get
            {
                SolidColorBrush propertyColor = new SolidColorBrush(Color.FromRgb(111, 111, 111));
                if (product_code != null)
                {
                    propertyColor = new SolidColorBrush(Color.FromRgb(250, 128, 114));
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
                _ = new SolidColorBrush(Color.FromRgb(111, 111, 111));
                SolidColorBrush propertyColor;
                if (quantity > 10)
                {
                    propertyColor = new SolidColorBrush(Color.FromRgb(199, 240, 254));
                    return propertyColor;
                }
                else
                {
                    propertyColor = new SolidColorBrush(Color.FromRgb(210, 31, 60));
                    return propertyColor;
                }
            }
        }
    }
}