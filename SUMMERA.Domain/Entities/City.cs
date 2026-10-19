using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Domain.Entities
{
    public class City : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Country { get; set; }

        [MaxLength(500)]
        public string ImageUrl { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public ICollection<Hotel> Hotels { get; set; } = new List<Hotel>();
    }
}
