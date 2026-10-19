using SUMMERA.Application.DTOs.Hotels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Application.Services.Interfaces
{
    public interface IHotelService
    {
        Task<IEnumerable<HotelDto>> GetAllHotelsAsync();
        Task<HotelDetailsDto?> GetHotelByIdAsync(int id);
        Task<IEnumerable<HotelDto>> GetHotelsByCityAsync(int cityId);
        Task<HotelDto> CreateHotelAsync(CreateHotelDto dto);
        Task UpdateHotelAsync(int id, CreateHotelDto dto);
        Task DeleteHotelAsync(int id);
    }
}
