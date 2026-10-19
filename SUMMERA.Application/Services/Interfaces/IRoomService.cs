using SUMMERA.Application.DTOs.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Application.Services.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetRoomsByHotelAsync(int hotelId);
        Task<RoomDto?> GetRoomByIdAsync(int id);
        Task<RoomDto> CreateRoomAsync(CreateRoomDto dto);
        Task UpdateRoomAsync(int id, CreateRoomDto dto);
        Task DeleteRoomAsync(int id);
    }
}
