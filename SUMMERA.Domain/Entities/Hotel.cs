using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Domain.Entities
{
    public class Hotel : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Description { get; set; }

        [Required]
        [MaxLength(300)]
        public string Address { get; set; }

        [MaxLength(500)]
        public string MainImageUrl { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal StartingPrice { get; set; }

        [Range(1, 5)]
        public int StarRating { get; set; }

        public bool IsActive { get; set; } = true;

        // Foreign Key
        public int CityId { get; set; }
        public City City { get; set; }

        // Navigation Properties
        public ICollection<Room> Rooms { get; set; } = new List<Room>();
        public ICollection<HotelImage> Images { get; set; } = new List<HotelImage>();
        public ICollection<HotelAmenity> HotelAmenities { get; set; } = new List<HotelAmenity>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
