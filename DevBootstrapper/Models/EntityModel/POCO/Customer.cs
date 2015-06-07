#region using block

using System.Collections.Generic;

#endregion

namespace DevBootstrapper.Models.EntityModel.POCO {
    public class Customer {
        public Customer() {
            ProductOrders = new HashSet<ProductOrder>();
            CustomerDemographics = new HashSet<CustomerDemographic>();
        }

        public string CustomerId { get; set; }
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
        public virtual ICollection<CustomerDemographic> CustomerDemographics { get; set; }
    }
}