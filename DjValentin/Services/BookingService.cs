using DjValentin.Models;
using DjValentin.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

public class BookingService
{
    private readonly BookingRepository _bookingRepository;
    public BookingService()
    {
        _bookingRepository = new BookingRepository();
    }

    public async void Create(Booking booking)
    {
        await _bookingRepository.CreateAsync(booking);
    }

    public IQueryable<Booking> GetBookings()
    {
        return _bookingRepository.GetBookings();        
    }

    public Task<Booking> GetById(int? id)
    {
        return _bookingRepository.GetById(id);        
    }
}