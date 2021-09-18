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
        public bool IsApproved { get; set; }
        public virtual Person Person { get; set; }       
        public int PersonId { get; set; }
    }
}
