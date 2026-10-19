using AutoMapper;
using SUMMERA.Application.Common.Interfaces;
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
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoomService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoomDto>> GetRoomsByHotelAsync(int hotelId)
        {
            var rooms = await _unitOfWork.Rooms.GetAllAsync(
                filter: r => r.HotelId == hotelId && r.IsAvailable);

            return _mapper.Map<IEnumerable<RoomDto>>(rooms);
        }

        public async Task<RoomDto?> GetRoomByIdAsync(int id)
        {
            var room = await _unitOfWork.Rooms.GetAsync(r => r.Id == id);
            return room == null ? null : _mapper.Map<RoomDto>(room);
        }

        public async Task<RoomDto> CreateRoomAsync(CreateRoomDto dto)
        {
            var room = _mapper.Map<Room>(dto);
            await _unitOfWork.Rooms.AddAsync(room);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<RoomDto>(room);
        }

        public async Task UpdateRoomAsync(int id, CreateRoomDto dto)
        {
            var room = await _unitOfWork.Rooms.GetAsync(r => r.Id == id);
            if (room == null) throw new Exception("Room not found");

            _mapper.Map(dto, room);
            room.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Rooms.Update(room);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteRoomAsync(int id)
        {
            var room = await _unitOfWork.Rooms.GetAsync(r => r.Id == id);
            if (room == null) throw new Exception("Room not found");

            room.IsDeleted = true;
            room.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.Rooms.Update(room);
            await _unitOfWork.SaveAsync();
        }
    }
}
