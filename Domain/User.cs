using System;

namespace ChalmersBookExchange.Domain
{
    public class User
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid?[] Favorites { get; set; }
        public Guid?[] Contacts { get; set; }
    }
}