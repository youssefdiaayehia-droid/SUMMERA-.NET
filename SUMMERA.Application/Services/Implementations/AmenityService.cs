using AutoMapper;
using SUMMERA.Application.Common.Interfaces;
using SUMMERA.Application.DTOs.Amenities;
using SUMMERA.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Application.Services.Implementations
{
    public class AmenityService : IAmenityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AmenityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AmenityDto>> GetAllAmenitiesAsync()
        {
            var amenities = await _unitOfWork.Amenities.GetAllAsync();
            return _mapper.Map<IEnumerable<AmenityDto>>(amenities);
        }

        public async Task<AmenityDto?> GetAmenityByIdAsync(int id)
        {
            var amenity = await _unitOfWork.Amenities.GetAsync(a => a.Id == id);
            return amenity == null ? null : _mapper.Map<AmenityDto>(amenity);
        }
    }
}
