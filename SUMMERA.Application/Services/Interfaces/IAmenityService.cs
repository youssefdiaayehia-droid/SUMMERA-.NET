using SUMMERA.Application.DTOs.Amenities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Application.Services.Interfaces
{
    public interface IAmenityService
    {
        Task<IEnumerable<AmenityDto>> GetAllAmenitiesAsync();
        Task<AmenityDto?> GetAmenityByIdAsync(int id);
    }
}
