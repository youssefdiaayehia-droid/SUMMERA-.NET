using AutoMapper;
using SUMMERA.Application.Common.Interfaces;
using SUMMERA.Application.DTOs.Cities;
using SUMMERA.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Application.Services.Implementations
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CityDto>> GetAllCitiesAsync()
        {
            var cities = await _unitOfWork.Cities.GetAllAsync();
            return _mapper.Map<IEnumerable<CityDto>>(cities);
        }

        public async Task<CityDto?> GetCityByIdAsync(int id)
        {
            var city = await _unitOfWork.Cities.GetAsync(c => c.Id == id);
            return city == null ? null : _mapper.Map<CityDto>(city);
        }
    }
}
