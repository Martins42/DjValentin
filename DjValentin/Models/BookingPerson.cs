namespace DjValentin.Models
{
    public class BookingPerson
    {
        public Person Person { get; set; }
        public int PersonId { get; set; }
        public Booking Booking { get; set; }
        public int BookingId { get; set; }
    }
}
