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
    
    public partial class Level
    {
        public Level()
        {
            this.Classes = new HashSet<Class>();
        }
    
        public int Id { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public string Subject { get; set; }
        public string Level1 { get; set; }
        public bool IsHidden { get; set; }
        public bool IsValid { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime DateCreated { get; set; }
    
        public virtual ICollection<Class> Classes { get; set; }
    }
}
