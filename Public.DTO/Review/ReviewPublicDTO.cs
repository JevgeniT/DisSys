using System;

namespace Public.DTO
{
    public class ReviewPublicDTO
    {
        public Guid Id { get; set; }
        public int Score { get; set; }
        public string Comment { get; set; }
        public string UserName { get; set; }
        
        public string CreatedAt { get; set; }

    }
}