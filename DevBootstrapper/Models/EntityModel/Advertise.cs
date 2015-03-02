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
    
    public partial class Advertise
    {
        public Advertise()
        {
            this.AdvertisePaymentConfirmeds = new HashSet<AdvertisePaymentConfirmed>();
            this.AdvertiseRequests = new HashSet<AdvertiseRequest>();
        }
    
        public long AdvertiseID { get; set; }
        public string Title { get; set; }
        public string Keywords { get; set; }
        public Nullable<long> MediaID { get; set; }
        public long UserID { get; set; }
        public byte AdvertiseTypeID { get; set; }
    
        public virtual AdvertiseType AdvertiseType { get; set; }
        public virtual Medium Medium { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<AdvertisePaymentConfirmed> AdvertisePaymentConfirmeds { get; set; }
        public virtual ICollection<AdvertiseRequest> AdvertiseRequests { get; set; }
    }
}
