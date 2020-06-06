using System;

namespace Public.DTO
{
    public class SearchDTO
    {
        public string Input { get; set; }
        public DateTime From { get; set; }
        
        public DateTime To { get; set; }
        public int Adults { get; set; }

        public override string ToString()
        {
            return $"Input: {Input}, From: {From}, To: {To}, Adults: {Adults}";
        }
    }
}