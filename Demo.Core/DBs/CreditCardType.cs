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
    
    public partial class CreditCardType
    {
        public CreditCardType()
        {
            this.CreditCards = new HashSet<CreditCard>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
    
        public virtual ICollection<CreditCard> CreditCards { get; set; }
    }
}
