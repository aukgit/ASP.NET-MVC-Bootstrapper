using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using DevBootstrapper.Models.EntityModel.POCO;

namespace DevBootstrapper.Models.Context
{
    public partial class NorthwindEntities : BaseDbContext
    {
        public NorthwindEntities()
            : base("name=NorthwindEntities")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerDemographic> CustomerDemographics { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<ProductOrder> ProductOrders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<Shipper> Shippers { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Territory> Territories { get; set; }
    }
}
