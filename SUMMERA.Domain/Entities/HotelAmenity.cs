using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Domain.Entities
{
    public class HotelAmenity
    {
        public int HotelId { get; set; }
        public Hotel Hotel { get; set; }

        public int AmenityId { get; set; }
        public Amenity Amenity { get; set; }
    }
}
