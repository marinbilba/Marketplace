using MarketplaceAPI.Model;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MarketplaceAPI.Database
{
    public  class MarketplaceContext : DbContext
    { 
        public  DbSet<Customer> Customer { get; set; }
        public DbSet<Category> Category { get; set; }
      public DbSet<CustomerOrder> CustomerOrder { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<OrderHistory> OrderHistory { get; set; }
        public DbSet<OrderLine> OrderLine { get; set; }
        public DbSet<Product> Product { get; set; }
        public MarketplaceContext()
        {
        }

        public MarketplaceContext(DbContextOptions<MarketplaceContext> options)
            : base(options)
        {
        }

       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=Marketplace;Username=postgres;Password=Ma31072000");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerOrder>()
                .HasOne(b => b.OrderDetails)
                .WithOne(i => i.CustomerOrder)
                .HasForeignKey<OrderDetails>(b => b.CustomerOrderId);
            
            modelBuilder.Entity<Customer>()
                .HasOne(b => b.Cart)
                .WithOne(i => i.Customer)
                .HasForeignKey<Cart>(b => b.CustomerUsername);
            
            modelBuilder.Entity<Customer>()
                .HasOne(b => b.OrderHistory)
                .WithOne(i => i.Customer)
                .HasForeignKey<OrderHistory>(b => b.CustomerUsername);
            
            
            
      
         

        }

 
    }
}
