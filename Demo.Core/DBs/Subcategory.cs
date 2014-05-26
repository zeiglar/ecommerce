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
    
    public partial class Subcategory
    {
        public Subcategory()
        {
            this.Categories = new HashSet<Category>();
            this.Subjects = new HashSet<Subject>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> Code { get; set; }
        public bool IsValid { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime DateCreated { get; set; }
    
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
    }
}
