using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ZaklepTo.Core.Domain;

namespace ZaklepTo.Infrastructure.EntityFramwerork
{
    public class ZaklepToContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        private readonly IConfiguration configuration;

        public ZaklepToContext(DbContextOptions<ZaklepToContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetSection("ConnectionStrings:DataBaseConnectionString").Value);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasKey(x => x.Login);
        }
    }
}