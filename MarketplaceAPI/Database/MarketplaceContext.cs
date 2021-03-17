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
        public DbSet<Cart> Cart { get; set; }
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

            optionsBuilder.EnableSensitiveDataLogging();
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
            
            modelBuilder.Entity<OrderLine>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();

            
            // Populating the tables
            modelBuilder.Entity<Customer>().HasData(new Customer() {Username = "test", Password = "123"});
            modelBuilder.Entity<Cart>().HasData(new Cart(){Id=1,CustomerUsername = "test"});
            
            var category = new Category() {Id = 1, Name = "Computers"};
            var category1 = new Category() {Id = 2, Name = "Toys"};

            modelBuilder.Entity<Category>().HasData(category);
            modelBuilder.Entity<Category>().HasData(category1);
            var product = new Product()
            {
                Id = 1,
                Name = "ACER G502",
                Description = "Lorem",
                Price = 350,
                ThumbnailUrl =
                    "https://res.cloudinary.com/dxfq3iotg/image/upload/v1571750967/Ecommerce/ef192a21ec96.jpg",
                Stock = 19,
                CategoryId = 1
            };

        modelBuilder.Entity<Product>().HasData(product);
            modelBuilder.Entity<Product>().HasData(new Product(){Id = 2,Name = "ACER Predator",Description = "Lorem",Price = 1350,ThumbnailUrl = "https://www.komplett.dk/img/p/1200/1168528.jpg",Stock = 5,CategoryId = 1});
            modelBuilder.Entity<Product>().HasData(new Product(){Id = 3,Name = "Doge",Description = "Lorem",Price = 510,ThumbnailUrl = "https://www.petplanet.co.uk/image/500x500/99_56102_1529569131_bca1d9.jpg",Stock = 500,CategoryId = 2});
          

        }
    }
}
