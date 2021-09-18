using DjValentin.Models;
using Microsoft.EntityFrameworkCore;

namespace DjValentin.Context
{
    public class AppDbContext : DbContext
    {        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Booking> Bookings { get; set; }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .HasOne(p => p.Person);
        }
    }
}
