//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Perseus.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Role
    {
        public Role()
        {
            this.Permisson = new HashSet<Permisson>();
            this.User = new HashSet<User>();
        }
    
        public string Id { get; set; }
        public string Name { get; set; }
    
        public virtual ICollection<Permisson> Permisson { get; set; }
        public virtual ICollection<User> User { get; set; }
    }
}
