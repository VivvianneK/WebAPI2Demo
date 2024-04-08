using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Model
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext>options): base(options)
        {
                
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductForPost> ProductForPosts { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientForPost> ClientForPosts { get; set; }
        public DbSet<ClientForPost> OrderForPosts { get; set; }
        public DbSet<ClientForPost> OrderDetailForPosts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuring one-to-many relationship between Client and Order
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ClientId);

            // Configure many-to-many relationship between Product and Order
            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => new {od.OrderId, od.ProductId});

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(o => o.OrderId);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany(p => p.OrderDetails)
                .HasForeignKey(o => o.ProductId);

            // Ensure email is unique
            modelBuilder.Entity<Client>()
                .HasIndex(c => c.Email)
                .IsUnique();

            // Ensure phone number is unique
            modelBuilder.Entity<Client>()
                .HasIndex(c => c.PhoneNumber)
                .IsUnique();


            modelBuilder.Entity<ProductForPost>().HasNoKey();
            modelBuilder.Entity<ClientForPost>().HasNoKey();
        }
    }
}
