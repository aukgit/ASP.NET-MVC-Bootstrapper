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
    
    public partial class Point
    {
        public System.Guid PointID { get; set; }
        public double XP { get; set; }
        public System.DateTime Dated { get; set; }
        public long UserID { get; set; }
        public short XPGainedTypeID { get; set; }
    
        public virtual User User { get; set; }
        public virtual XPGainedType XPGainedType { get; set; }
    }
}