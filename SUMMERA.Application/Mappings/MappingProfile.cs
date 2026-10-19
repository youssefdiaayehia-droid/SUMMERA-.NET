using AutoMapper;
using SUMMERA.Application.DTOs.Amenities;
using SUMMERA.Application.DTOs.Bookings;
using SUMMERA.Application.DTOs.Cities;
using SUMMERA.Application.DTOs.ContactMessages;
using SUMMERA.Application.DTOs.Hotels;
using SUMMERA.Application.DTOs.Reviews;
using SUMMERA.Application.DTOs.Rooms;
using SUMMERA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // ============================================
            // City
            // ============================================
            CreateMap<City, CityDto>();

            // ============================================
            // Amenity
            // ============================================
            CreateMap<Amenity, AmenityDto>();

            // ============================================
            // Hotel
            // ============================================
            CreateMap<Hotel, HotelDto>()
                .ForMember(dest => dest.CityName,
                    opt => opt.MapFrom(src => src.City.Name));

            CreateMap<Hotel, HotelDetailsDto>()
                .ForMember(dest => dest.CityName,
                    opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.Images, opt => opt.Ignore())
                .ForMember(dest => dest.Amenities, opt => opt.Ignore())
                .ForMember(dest => dest.Rooms, opt => opt.Ignore());

            CreateMap<CreateHotelDto, Hotel>();

            // ============================================
            // Room
            // ============================================
            CreateMap<Room, RoomDto>();
            CreateMap<CreateRoomDto, Room>();

            // ============================================
            // Booking
            // ============================================
            CreateMap<Booking, BookingDto>()
                .ForMember(dest => dest.HotelName,
                    opt => opt.MapFrom(src => src.Room.Hotel.Name))
                .ForMember(dest => dest.RoomType,
                    opt => opt.MapFrom(src => src.Room.RoomType));

            // ============================================
            // Review
            // ============================================
            CreateMap<Review, ReviewDto>()
                .ForMember(dest => dest.UserName,
                    opt => opt.MapFrom(src => "User")); // ← مؤقتاً

            // ============================================
            // ContactMessage
            // ============================================
            CreateMap<ContactMessage, ContactMessageDto>();
            CreateMap<CreateContactMessageDto, ContactMessage>();
        }
    }
}
