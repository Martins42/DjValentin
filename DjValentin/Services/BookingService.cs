using DjValentin.Context;
using DjValentin.Models;
using DjValentin.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DjValentin.Services
{
    public class BookingService
    {
        private readonly PersonService _personService;
        private readonly BookingRepository _bookingRepository;
        public BookingService(AppDbContext appDbContext)
        {
            _bookingRepository = new BookingRepository(appDbContext);
            _personService = new PersonService(appDbContext);
        }

        public void Create(Booking booking)
        {
            _bookingRepository.Create(booking);
        }

        public IQueryable<Booking> GetBookings()
        {
            return _bookingRepository.GetBookings();
        }

        public Task<Booking> GetById(int? id)
        {
            return _bookingRepository.GetById(id);
        }

        public void Update(Booking booking)
        {
            _bookingRepository.Update(booking);
        }

        public void Delete(Booking booking)
        {
            _bookingRepository.Delete(booking);
        }
    }
}
