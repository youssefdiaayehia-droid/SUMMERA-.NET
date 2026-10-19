using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Domain.Entities
{
    public class HotelImage : BaseEntity
    {
        [Required]
        [MaxLength(500)]
        public string ImageUrl { get; set; }

        public bool IsMain { get; set; } = false;

        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }
    }
}
