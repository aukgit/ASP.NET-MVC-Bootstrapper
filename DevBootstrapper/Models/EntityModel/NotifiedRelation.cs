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
    
    public partial class NotifiedRelation
    {
        public long NotifiedRelationID { get; set; }
        public long NotifyID { get; set; }
        public Nullable<long> SeenByUserID { get; set; }
    
        public virtual User User { get; set; }
    }
}
