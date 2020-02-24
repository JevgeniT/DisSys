namespace Domain
{
    public class Review
    {
        public int ReviewId { get; set; }

        public Reservation Reservation { get; set; }

        public int Score { get; set; }
        
        
        
    }
}