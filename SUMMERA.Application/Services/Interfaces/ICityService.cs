using SUMMERA.Application.DTOs.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Application.Services.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<CityDto>> GetAllCitiesAsync();
        Task<CityDto?> GetCityByIdAsync(int id);
    }
}
