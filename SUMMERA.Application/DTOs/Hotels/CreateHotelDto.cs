using System.ComponentModel.DataAnnotations;

namespace SUMMERA.Application.DTOs.Hotels
{
    public class CreateHotelDto
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

        [Required]
        public int CityId { get; set; }

        [Range(1, 5)]
        public int StarRating { get; set; }

        [Range(0, double.MaxValue)]
        public decimal StartingPrice { get; set; }

        public string? MainImageUrl { get; set; }
    }
}