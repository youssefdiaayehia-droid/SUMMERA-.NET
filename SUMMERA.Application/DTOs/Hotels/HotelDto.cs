using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Application.DTOs.Hotels
{
    public class HotelDto 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CityName { get; set; }
        public int StarRating { get; set; }
        public decimal StartingPrice { get; set; }
        public string MainImageUrl { get; set; }
    }
}
