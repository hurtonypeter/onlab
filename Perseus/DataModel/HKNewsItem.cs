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
    
    public partial class HKNewsItem
    {
        public int ItemId { get; set; }
        public int MailId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Link { get; set; }
    
        public virtual HKNewsPaper HKNewsPaper { get; set; }
    }
}
