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
        public DbSet<BookingPerson> BookingPersons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookingPerson>()
                .HasKey(bc => new { bc.BookingId, bc.PersonId });
            modelBuilder.Entity<BookingPerson>()
                .HasOne(bc => bc.Booking)
                .WithMany(b => b.BookingPerson)
                .HasForeignKey(bc => bc.BookingId);
            modelBuilder.Entity<BookingPerson>()
                .HasOne(bc => bc.Person)
                .WithMany(c => c.BookingPerson)
                .HasForeignKey(bc => bc.PersonId);
        }
    }
}
