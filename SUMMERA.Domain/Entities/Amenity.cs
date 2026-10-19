using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Domain.Entities
{
    public class Amenity : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string IconClass { get; set; }

        public ICollection<HotelAmenity> HotelAmenities { get; set; } = new List<HotelAmenity>();
    }
}
