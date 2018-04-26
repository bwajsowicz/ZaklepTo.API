using Microsoft.EntityFrameworkCore;
using ZaklepTo.Infrastructure.Entities;
using ZaklepTo.Infrastructure.Services.Interfaces;

namespace ZaklepTo.Infrastructure.Services.Implementations
{
    public class DataBaseService : DbContext, IDataBaseService
    {
        public DataBaseService(DbContextOptions<DataBaseService> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<OwnerEntity> Owners { get; set; }
        public DbSet<EmployeeEntity> Employees { get; set; }
        public DbSet<RestaurantEntity> Restaurants { get; set; }
        public DbSet<TableEntity> Tables { get; set; }
    }
}
