using DjValentin.Context;
using DjValentin.Models;
using DjValentin.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

public class BookingService
{
    private readonly BookingRepository _bookingRepository;
    public BookingService(AppDbContext appDbContext)
    {
        _bookingRepository = new BookingRepository(appDbContext);
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

    public async void Update(Booking booking)
    {
        await _bookingRepository.Update(booking);
    }

    public async Task Delete(Booking booking)
    {
        await _bookingRepository.Delete(booking);
    }
}