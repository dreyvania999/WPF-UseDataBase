//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Table_Chemicals_Delivery
    {
        public int id_product_in_delivery { get; set; }
        public int product_code { get; set; }
        public int delivery_code { get; set; }
        public int quantity_goods { get; set; }
    
        public virtual Table_Delivery Table_Delivery { get; set; }
        public virtual Table_Household_Chemicals Table_Household_Chemicals { get; set; }
    }
}
