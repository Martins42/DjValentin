using DjValentin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DjValentin.Context
{
    public class AppDbContext : IdentityDbContext
    {        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Booking> Bookings { get; set; }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Booking>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Person>()
                .HasKey(p => p.Id);
            modelBuilder.Entity<Booking>()
                .HasOne(p => p.Person);

        }

    }
}
