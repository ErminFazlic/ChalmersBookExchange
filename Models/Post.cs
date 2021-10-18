using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChalmersBookExchange.Domain
{
    public class Post
    {
        public Guid ID { get; set; }
        public Guid Poster { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string BookName { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string CourseCode { get; set; }
        [Required]
        public string Description { get; set; }
        [Range(1, 9999)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public int Price { get; set; }
        [RegularExpression(@"([a-zA-Z0-9\s_.-:])+(.png|.jpg|.gif)$", ErrorMessage = "Only Image files allowed.")]
        public byte[] Images { get; set; }
        public string Timestamp { get; set; }
        public string Location { get; set; }
        public bool Shippable { get; set; }
        public bool Meetup { get; set; }
    }
}