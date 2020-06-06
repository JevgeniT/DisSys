using System.Collections.Generic;

namespace Public.DTO
{
    public class MessageDTO
    {
        public MessageDTO()
        {
            
        }

        public MessageDTO(params string[] messages)
        {
            Messages = messages;
        }
        
         public IList<string> Messages { get; set; } = new List<string>();
    }

}