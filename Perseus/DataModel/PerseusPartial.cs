using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Perseus.DataModel
{
    public partial class User
    {
        public string FullName
        {
            get { return this.LastName + " " + this.FirstName; }
        }
    }

    [MetadataType(typeof(RoleMetadata))]
    public partial class Role
    {
        
    }
    public class RoleMetadata
    {
        [Display(Name = "Név")]
        public string Name { get; set; }
    }
}