//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Perseus.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Permission
    {
        public Permission()
        {
            this.Role = new HashSet<Role>();
            this.MenuItem = new HashSet<MenuItem>();
        }
    
        public int PermissionId { get; set; }
        public string Name { get; set; }
        public int ModuleId { get; set; }
    
        public virtual Module Module { get; set; }
        public virtual ICollection<Role> Role { get; set; }
        public virtual ICollection<MenuItem> MenuItem { get; set; }
    }
}
