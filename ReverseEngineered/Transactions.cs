//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReverseEngineered
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transactions
    {
        public int TransactionId { get; set; }
        public Nullable<int> FromInventoryId { get; set; }
        public int ToInventoryId { get; set; }
        public int ProductId { get; set; }
        public double Quantity { get; set; }
        public System.DateTime Updated { get; set; }
        public string ByUser { get; set; }
    
        public virtual Inventories Inventories { get; set; }
        public virtual Inventories Inventories1 { get; set; }
        public virtual Products Products { get; set; }
    }
}