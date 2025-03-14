using Microsoft.EntityFrameworkCore;

namespace Labb2Webb.Models
{
    public class ECommerceContext : DbContext
    {
        public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(c => c.Address)
                .IsRequired(false);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Cellphone)
                .IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}

