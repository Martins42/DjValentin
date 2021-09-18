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
            return _context.Bookings.Include(x => x.Person);
        }

        public async Task CreateAsync(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
        }

        public async Task<Booking> GetById(int? id)
        {
            return await _context.Bookings.FirstOrDefaultAsync(m => m.Id == id);            
        }

        public async Task Update(Booking booking)
        {
            _context.Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Booking booking)
        {            
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
        }
    }
}
