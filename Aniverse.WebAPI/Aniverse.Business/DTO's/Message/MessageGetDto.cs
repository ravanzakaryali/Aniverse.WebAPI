using System;

namespace Aniverse.Business.DTO_s.Message
{
    public class MessageGetDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime SentDate { get; set; }
        public bool IsSeen { get; set; }    
    }
}
