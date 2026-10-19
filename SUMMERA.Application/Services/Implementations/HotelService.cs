    using AutoMapper;
using SUMMERA.Application.Common.Interfaces;
using SUMMERA.Application.DTOs.Hotels;
using SUMMERA.Application.DTOs.Rooms;
using SUMMERA.Application.Services.Interfaces;
using SUMMERA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Application.Services.Implementations
{
    public class HotelService : IHotelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HotelService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<HotelDto>> GetAllHotelsAsync()
        {
            var hotels = await _unitOfWork.Hotels.GetAllAsync(
                filter: h => h.IsActive,
                includeProperties: "City");

            return _mapper.Map<IEnumerable<HotelDto>>(hotels);
        }

        public async Task<HotelDetailsDto?> GetHotelByIdAsync(int id)
        {
            var hotel = await _unitOfWork.Hotels.GetAsync(
                filter: h => h.Id == id,
                includeProperties: "City,Rooms,Images,HotelAmenities.Amenity,Reviews");

            if (hotel == null) return null;

            var dto = _mapper.Map<HotelDetailsDto>(hotel);
            dto.Images = hotel.Images.Select(i => i.ImageUrl).ToList();
            dto.Amenities = hotel.HotelAmenities.Select(ha => ha.Amenity.Name).ToList();
            dto.Rooms = _mapper.Map<List<RoomDto>>(hotel.Rooms.Where(r => !r.IsDeleted && r.IsAvailable).ToList());
            return dto;
        }

        public async Task<IEnumerable<HotelDto>> GetHotelsByCityAsync(int cityId)
        {
            var hotels = await _unitOfWork.Hotels.GetAllAsync(
                filter: h => h.CityId == cityId && h.IsActive,
                includeProperties: "City");

            return _mapper.Map<IEnumerable<HotelDto>>(hotels);
        }

        public async Task<HotelDto> CreateHotelAsync(CreateHotelDto dto)
        {
            var hotel = _mapper.Map<Hotel>(dto);
            await _unitOfWork.Hotels.AddAsync(hotel);
            await _unitOfWork.SaveAsync();

            // Reload with City included
            var created = await _unitOfWork.Hotels.GetAsync(
                h => h.Id == hotel.Id,
                includeProperties: "City");

            return _mapper.Map<HotelDto>(created);
        }

        public async Task UpdateHotelAsync(int id, CreateHotelDto dto)
        {
            var hotel = await _unitOfWork.Hotels.GetAsync(h => h.Id == id);
            if (hotel == null) throw new Exception("Hotel not found");

            _mapper.Map(dto, hotel);
            hotel.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Hotels.Update(hotel);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteHotelAsync(int id)
        {
            var hotel = await _unitOfWork.Hotels.GetAsync(h => h.Id == id);
            if (hotel == null) throw new Exception("Hotel not found");

            hotel.IsDeleted = true;
            hotel.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Hotels.Update(hotel);
            await _unitOfWork.SaveAsync();
        }
    }
}
