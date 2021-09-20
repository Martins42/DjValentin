using DjValentin.Context;
using DjValentin.Models;
using DjValentin.Services;
using DjValentin.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
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
        private readonly MessageViewModel message; 

        public BookingsController(AppDbContext context, IEmailSender emailSender, IConfiguration config)
        {
            _bookingService = new BookingService(context);
            _emailSender = emailSender;
            _config = config;
            message = new MessageViewModel();
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
                if (booking.BookingDate < DateTime.Now)
                {                    
                    this.message.Message = "The Booking date can't be smaller than today";
                    this.message.Type = "danger";

                    TempData["Message"] = JsonConvert.SerializeObject(this.message);
                    return View(booking);
                }

                _bookingService.Create(booking);
                
                this.message.Message = "Booking created!";
                this.message.Type = "success";                
                TempData["Message"] = JsonConvert.SerializeObject(this.message);

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
                    if (booking.BookingDate < DateTime.Now)
                    {
                        this.message.Message = "The Booking date can't be smaller than today";
                        this.message.Type = "danger";

                        TempData["Message"] = JsonConvert.SerializeObject(this.message);
                        return View(booking);
                    }

                    _bookingService.Update(booking);
                    this.message.Message = "Booking updated!";
                    this.message.Type = "success";                    

                    TempData["Message"] = JsonConvert.SerializeObject(this.message);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        var message = new MessageViewModel()
                        {
                            Message = "Update fail!",
                            Type = "danger"
                        };

                        TempData["Message"] = JsonConvert.SerializeObject(message);

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


            this.message.Message = "Booking deleted!";
            this.message.Type = "success";            
            TempData["Message"] = JsonConvert.SerializeObject(this.message);

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

            this.message.Message = "Booking Approved!";
            this.message.Type = "success";
            TempData["Message"] = JsonConvert.SerializeObject(this.message);

            try
            {
                if (!entity.EmailSended)
                {
                    var subject = _config.GetValue<string>("EmailSettings:Subject");
                    var mailMessage = _config.GetValue<string>("EmailSettings:Message");
                    var result = _emailSender.SendEmailAsync(entity.Person.Email, subject, mailMessage);
                    if (result.IsCompleted)
                    {
                        entity.EmailSended = true;
                    }                    
                }                
            }
            catch (System.Exception)
            {
                return RedirectToAction(nameof(Index));
            }
            finally
            {
                _bookingService.Update(entity);
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
