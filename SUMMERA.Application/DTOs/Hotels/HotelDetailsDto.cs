using SUMMERA.Application.DTOs.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Application.DTOs.Hotels
{
    public class HotelDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string CityName { get; set; }
        public int StarRating { get; set; }
        public decimal StartingPrice { get; set; }
        public string MainImageUrl { get; set; }
        public List<string> Images { get; set; } = new();
        public List<string> Amenities { get; set; } = new();
        public List<RoomDto> Rooms { get; set; } = new();
    }
}
