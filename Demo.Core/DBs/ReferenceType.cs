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
    
    public partial class ReferenceType
    {
        public ReferenceType()
        {
            this.Clients = new HashSet<Client>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
    
        public virtual ICollection<Client> Clients { get; set; }
    }
}
