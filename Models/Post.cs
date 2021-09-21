using System;

namespace ChalmersBookExchange.Domain
{
    public class Post
    {
        public Guid ID { get; set; }
        public Guid Poster { get; set; }
        public string BookName { get; set; }
        public string CourseCode { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public byte[] Images { get; set; }
        public string Timestamp { get; set; }
        public string Location { get; set; }
        public bool Shippable { get; set; }
        public bool Meetup { get; set; }
    }
}