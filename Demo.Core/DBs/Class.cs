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
    
    public partial class Class
    {
        public Class()
        {
            this.Activities = new HashSet<Activity>();
        }
    
        public int Id { get; set; }
        public int LevelId { get; set; }
        public string DayTime { get; set; }
        public string Location { get; set; }
        public string Room { get; set; }
        public string Teacher { get; set; }
        public string Duration { get; set; }
        public bool IsValid { get; set; }
        public bool IsDeleted { get; set; }
        public System.DateTime DateCreated { get; set; }
    
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual Level Level { get; set; }
    }
}