using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DjValentin.Models
{
    public class Person
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Contact Number")]
        public string ContactNumber { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email {  get; set; }

        public ICollection<BookingPerson> BookingPerson {  get; set; }
    }
}
