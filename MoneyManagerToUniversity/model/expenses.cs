//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MoneyManagerToUniversity.model
{
    using System;
    using System.Collections.Generic;
    
    public partial class expenses
    {
        public int id { get; set; }
        public int expenses_type_id { get; set; }
        public decimal value { get; set; }
        public decimal account_ballance { get; set; }
        public System.DateTime created_at { get; set; }
    
        public virtual expenses_type expenses_type { get; set; }
    }
}