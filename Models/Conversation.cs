using System;

namespace ChalmersBookExchange.Domain
{
    public class Conversation
    {
        public Guid ID { get; set; }
        public Guid Sender { get; set; }
        public Guid Receiver { get; set; }
        public string Message { get; set; }
        public string TimeStamp { get; set; }
    }
}