using System;

namespace Aniverse.Core.Entites
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime SentDate { get; set; }
        public bool IsSeen { get; set; }
        public int MessageGroupId { get; set; }
        public MessageGroup MessageGroup { get; set; }
    }
}
