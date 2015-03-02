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
    
    public partial class AdvertisePaymentConfirmed
    {
        public long AdvertisePaymentConfirmedID { get; set; }
        public long AdvertiseRequestID { get; set; }
        public long AdvertiseID { get; set; }
        public decimal GeneratedPayment { get; set; }
        public decimal Paid { get; set; }
        public decimal Discount { get; set; }
        public byte PaymentMethodID { get; set; }
        public long ConfirmByUserID { get; set; }
        public long PaidByUserID { get; set; }
    
        public virtual Advertise Advertise { get; set; }
        public virtual AdvertiseRequest AdvertiseRequest { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}