//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Demo.Core.DBs
{
    using System;
    using System.Collections.Generic;
    
    public partial class Activity
    {
        public Activity()
        {
            this.Orders = new HashSet<Order>();
        }
    
        public int Id { get; set; }
        public Nullable<int> ClassId { get; set; }
        public int TermId { get; set; }
        public int ActivityTypeId { get; set; }
        public string Name { get; set; }
        public int Enrolled { get; set; }
        public int MaxNumber { get; set; }
        public Nullable<decimal> Price { get; set; }
        public decimal PriceIncGST { get; set; }
        public Nullable<decimal> PriceConcession { get; set; }
        public Nullable<decimal> PriceMember { get; set; }
        public Nullable<decimal> PriceDiscount { get; set; }
        public Nullable<System.DateTime> DateEarlyBird { get; set; }
        public bool IsDayEvent { get; set; }
        public bool IsHidden { get; set; }
        public bool IsValid { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime DateUpdated { get; set; }
        public System.DateTime DateCreated { get; set; }
    
        public virtual ActivityType ActivityType { get; set; }
        public virtual Class Class { get; set; }
        public virtual Term Term { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
