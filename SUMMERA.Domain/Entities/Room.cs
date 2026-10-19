using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Domain.Entities
{
    public class Room : BaseEntity
    {
        [Required]
        [MaxLength(20)]
        public string RoomNumber { get; set; }

        [Required]
        [MaxLength(50)]
        public string RoomType { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerNight { get; set; }

        public int Capacity { get; set; }

        public bool IsAvailable { get; set; } = true;

        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public ICollection<RoomImage> Images { get; set; } = new List<RoomImage>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
