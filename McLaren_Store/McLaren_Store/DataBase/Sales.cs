//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace McLaren_Store.DataBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sales
    {
        public int SaleID { get; set; }
        public Nullable<int> CarID { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public Nullable<System.DateTime> SaleDate { get; set; }
        public Nullable<decimal> SalePrice { get; set; }
    
        public virtual Cars Cars { get; set; }
        public virtual Customers Customers { get; set; }
        public virtual Employees Employees { get; set; }
    }
}