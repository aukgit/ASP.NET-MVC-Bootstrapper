//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DevBootstrapper.Models.EntityModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class ModifyLog
    {
        public long ModifyLogID { get; set; }
        public long ArticleID { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public long ModifiedByUserID { get; set; }
        public string Reason { get; set; }
        public byte ArticleStateID { get; set; }
        public string IP { get; set; }
        public bool IsCreated { get; set; }
    
        public virtual Article Article { get; set; }
        public virtual ArticleState ArticleState { get; set; }
        public virtual User User { get; set; }
    }
}
