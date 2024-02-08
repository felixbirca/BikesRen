using BikesRent.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BikesRent.DataAccessLayer
{
    public class BikesDbContext : DbContext
    {
        public BikesDbContext()
        {
            
        }
        
        public BikesDbContext(DbContextOptions<BikesDbContext> options) : base(options)
        {
        }
        
        public DbSet<Bike> Bikes { get; set; }
        
        public DbSet<RentHistory> RentHistories { get; set; }
        
        public DbSet<Subscription> Subscriptions { get; set; }
        
        public DbSet<User> Users { get; set; }
    }
}


