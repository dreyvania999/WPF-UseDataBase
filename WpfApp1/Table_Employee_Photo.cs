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
    
    public partial class Table_Employee_Photo
    {
        public int id_photo { get; set; }
        public Nullable<int> id_staff { get; set; }
        public byte[] binary_photo { get; set; }
        public string path_photo { get; set; }
    
        public virtual Table_Staff Table_Staff { get; set; }
    }
}
