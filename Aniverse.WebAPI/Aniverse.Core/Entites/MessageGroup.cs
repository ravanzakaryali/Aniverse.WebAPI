using System;
using System.Collections.Generic;
using System.Text;

namespace Aniverse.Core.Entites
{
    public class MessageGroup   
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public AppUser Sender { get; set; }
        public string ReceiverId { get; set; }
        public AppUser Receiver { get; set; }
        public ICollection<Message> Messages { get; set; }
    }
}
