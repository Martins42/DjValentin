using DjValentin.Context;
using DjValentin.Models;
using DjValentin.Services;
using DjValentin.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace DjValentin.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private readonly BookingService _bookingService;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _config;

        public BookingsController(AppDbContext context, IEmailSender emailSender, IConfiguration config)
        {
            _bookingService = new BookingService(context);
            _emailSender = emailSender;
            _config = config;
        }

        // GET: Bookings
        public IActionResult Index()
        {
            return View(_bookingService.GetBookings());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _bookingService.GetById(id);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,BookingDate,VehicleSize,Flexibility,Person")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _bookingService.Create(booking);
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _bookingService.GetById(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,BookingDate,VehicleSize,Flexibility, Person")] Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bookingService.Update(booking);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _bookingService.GetById(id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var booking = _bookingService.GetById(id);
            if (booking == null)
            {
                NotFound();
            }

            _bookingService.Delete(booking.Result);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Approve(int id)
        {
            var booking = _bookingService.GetById(id);
            if (booking == null)
            {
                NotFound();
            }

            var entity = booking.Result;
            entity.IsApproved = true;            

            try
            {
                if (!entity.EmailSended)
                {
                    var subject = _config.GetValue<string>("EmailSettings:Subject");
                    var message = _config.GetValue<string>("EmailSettings:Message");
                    var result = _emailSender.SendEmailAsync(entity.Person.Email, subject, message);
                    if (result.IsCompleted)
                    {
                        entity.EmailSended = true;
                        _bookingService.Update(entity);
                    }                    
                }                
            }
            catch (System.Exception)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            var bookings = _bookingService.GetBookings();
            return bookings.Any(e => e.Id == id);
        }
    }
}
