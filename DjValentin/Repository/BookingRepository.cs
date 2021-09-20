using DjValentin.Context;
using DjValentin.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DjValentin.Repository
{
    public class BookingRepository
    {
        private readonly AppDbContext _context;

        public BookingRepository(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public IQueryable<Booking> GetBookings()
        {
            return _context.Bookings.Include(x => x.Person).OrderBy(x => x.BookingDate);
        }

        public void Create(Booking booking)
        {
            _context.Bookings.Add(booking);
            _context.SaveChanges();
        }

        public async Task<Booking> GetById(int? id)
        {
            return await _context.Bookings.Include(x => x.Person).FirstOrDefaultAsync(m => m.Id == id);
        }

        public void Update(Booking booking)
        {
            _context.Update(booking);
            _context.SaveChanges();
        }

        public void Delete(Booking booking)
        {
            _context.Bookings.Remove(booking);
            _context.SaveChanges();
        }
    }
}
