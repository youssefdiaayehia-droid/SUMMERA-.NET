using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Domain.Entities
{
    public class Review : BaseEntity
    {
        [Range(1, 5)]
        public int Rating { get; set; }

        [MaxLength(2000)]
        public string Comment { get; set; }

        [Required]
        public string UserId { get; set; }

        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}
