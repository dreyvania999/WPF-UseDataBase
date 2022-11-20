using System.Collections.Generic;
using System.Linq;

namespace WpfApp1
{
    public partial class Table_Delivery
    {
        public string DeliveryInform
        {
            get
            {
                string str = "";
                double f = 0;
                List<Table_Household_Delivery> saleHoushould = Table_Household_Delivery.Where(x => x.delivery_code.Equals(id_delivery)).ToList();

                if (saleHoushould.Count > 0)
                {
                    str += "Товары для дома:\n";
                }
                foreach (Table_Household_Delivery item in saleHoushould)
                {
                    f += item.quantity_goods * item.Table_Household_Goods.cost;
                    str += "Колличество товара " + item.quantity_goods + " наименование товара " + item.Table_Household_Goods.name + "\nОбщая стоимость " + (item.quantity_goods * item.Table_Household_Goods.cost) + "\n";
                }
                List<Table_Chemicals_Delivery> saleCimicals = Table_Chemicals_Delivery.Where(x => x.delivery_code.Equals(id_delivery)).ToList();
                if (saleCimicals.Count > 0)
                {
                    str += "Товары для дома:\n";
                }
                foreach (Table_Chemicals_Delivery item in saleCimicals)
                {
                    f += item.quantity_goods * item.Table_Household_Chemicals.cost;
                    str += "Колличество товара " + item.quantity_goods + " наименование товара " + item.Table_Household_Chemicals.name + "\nОбщая стоимость " + (item.quantity_goods * item.Table_Household_Chemicals.cost) + "\n";
                }
                str += "\nСтоимость поставки: " + f;
                return str;
            }
        }
    }
}
