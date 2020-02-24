namespace Domain
{
    public class GuestReservations
    {
        public int GuestReservationsId { get; set; }

        public int GuestId { get; set; }

        public Guest? Guest { get; set; }

        public int ReservationId { get; set; }
        public Reservation? Reservation { get; set; }
        
    }
}