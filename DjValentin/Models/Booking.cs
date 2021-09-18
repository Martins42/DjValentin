using DjValentin.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DjValentin.Models
{
    public class Booking
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Booking Date")]
        public DateTime BookingDate { get; set; }
        [Required]
        [Display(Name = "Vehicle Size")]
        public VehicleSize VehicleSize { get; set; }
        [Required]
        [Display(Name = "Flexibility")]
        public Flexibility Flexibility { get; set; }
        public ICollection<BookingPerson> BookingPerson { get; set; }

    }
}
