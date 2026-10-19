using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SUMMERA.Domain.Common;
using SUMMERA.Domain.Entities;
using SUMMERA.Infrastructure.Data;
using SUMMERA.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Infrastructure.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public DbInitializer(
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task InitializeAsync()
        {
            try
            {
                // 1. Apply pending migrations
                if ((await _context.Database.GetPendingMigrationsAsync()).Any())
                {
                    await _context.Database.MigrateAsync();
                }

                // 2. Seed Roles
                await SeedRolesAsync();

                // 3. Seed Admin User
                await SeedAdminUserAsync();

                // 4. Seed Cities
                await SeedCitiesAsync();

                // 5. Seed Amenities
                await SeedAmenitiesAsync();

                // 6. Seed Hotels & Rooms
                await SeedHotelsAndRoomsAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        // ============================================
        // Private Methods
        // ============================================

        private async Task SeedRolesAsync()
        {
            if (!await _roleManager.RoleExistsAsync(SD.Role_Admin))
                await _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin));

            if (!await _roleManager.RoleExistsAsync(SD.Role_Customer))
                await _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer));
        }

        private async Task SeedAdminUserAsync()
        {
            var adminEmail = "admin@summera.com";
            var adminUser = await _userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FullName = "System Administrator",
                    CreatedAt = DateTime.UtcNow
                };

                await _userManager.CreateAsync(adminUser, "Admin@123");
                await _userManager.AddToRoleAsync(adminUser, SD.Role_Admin);
            }
        }

        private async Task SeedCitiesAsync()
        {
            if (await _context.Cities.AnyAsync()) return;

            var cities = new List<City>
            {
                new City { Name = "Cairo", Country = "Egypt", Description = "Capital of Egypt", ImageUrl = "/images/cities/cairo.jpg" },
                new City { Name = "Dubai", Country = "UAE", Description = "City of Gold", ImageUrl = "/images/cities/dubai.jpg" },
                new City { Name = "Sharm El-Sheikh", Country = "Egypt", Description = "City of Peace", ImageUrl = "/images/cities/sharm.jpg" },
                new City { Name = "Alexandria", Country = "Egypt", Description = "Pearl of the Mediterranean", ImageUrl = "/images/cities/alex.jpg" }
            };

            await _context.Cities.AddRangeAsync(cities);
            await _context.SaveChangesAsync();
        }

        private async Task SeedAmenitiesAsync()
        {
            if (await _context.Amenities.AnyAsync()) return;

            var amenities = new List<Amenity>
            {
                new Amenity { Name = "Free Wi-Fi", IconClass = "fa-wifi" },
                new Amenity { Name = "Swimming Pool", IconClass = "fa-swimming-pool" },
                new Amenity { Name = "Gym", IconClass = "fa-dumbbell" },
                new Amenity { Name = "Restaurant", IconClass = "fa-utensils" },
                new Amenity { Name = "Parking", IconClass = "fa-parking" },
                new Amenity { Name = "Air Conditioning", IconClass = "fa-snowflake" },
                new Amenity { Name = "Room Service", IconClass = "fa-concierge-bell" },
                new Amenity { Name = "Spa", IconClass = "fa-spa" }
            };

            await _context.Amenities.AddRangeAsync(amenities);
            await _context.SaveChangesAsync();
        }

        private async Task SeedHotelsAndRoomsAsync()
        {
            if (await _context.Hotels.AnyAsync()) return;

            var cairo = await _context.Cities.FirstAsync(c => c.Name == "Cairo");
            var dubai = await _context.Cities.FirstAsync(c => c.Name == "Dubai");

            var hotels = new List<Hotel>
{
    new Hotel
    {
        Name = "Cairo Grand Hotel",
        Description = "Experience luxury stay in the heart of Cairo with breathtaking views of the Nile.",
        Address = "Downtown, Cairo",
        StarRating = 5,
        StartingPrice = 250m,
        MainImageUrl = "/images/Hotels/Cairo Grand Hotel.png",
        CityId = cairo.Id,
        IsActive = true
    },
    new Hotel
    {
        Name = "Alexandria Palace",
        Description = "Enjoy a historic beachfront stay in Alexandria with vibrant seafood dining.",
        Address = "Corniche, Alexandria",
        StarRating = 4,
        StartingPrice = 180m,
        MainImageUrl = "/images/Hotels/Alexandria Palace.png",
        CityId = cairo.Id,
        IsActive = true
    },
    new Hotel
    {
        Name = "Luxor Nile Hotel",
        Description = "Unwind right next to ancient temples in Luxor with premium modern amenities.",
        Address = "Nile Corniche, Luxor",
        StarRating = 5,
        StartingPrice = 300m,
        MainImageUrl = "/images/Hotels/Luxor Nile Hotel.png",
        CityId = cairo.Id,
        IsActive = true
    },
    new Hotel
    {
        Name = "Aswan Nile Hotel",
        Description = "Relish in serene Nubian hospitality along the scenic banks of the Nile.",
        Address = "Nile View, Aswan",
        StarRating = 4,
        StartingPrice = 220m,
        MainImageUrl = "/images/Hotels/Aswan Nile Hotel.png",
        CityId = cairo.Id,
        IsActive = true
    },
    new Hotel
    {
        Name = "Siwa Oasis Lodge",
        Description = "Escape into a peaceful desert eco-lodge surrounded by natural hot springs.",
        Address = "Siwa Oasis",
        StarRating = 3,
        StartingPrice = 120m,
        MainImageUrl = "/images/Hotels/Siwa Oasis Lodge.png",
        CityId = cairo.Id,
        IsActive = true
    },
    new Hotel
    {
        Name = "Fayoum Lake Hotel",
        Description = "Enjoy picturesque lake views, desert safari adventures, and relaxing weekends.",
        Address = "Lake Qarun, Fayoum",
        StarRating = 4,
        StartingPrice = 150m,
        MainImageUrl = "/images/Hotels/Fayoum Lake Hotel.png",
        CityId = cairo.Id,
        IsActive = true
    }
};

            await _context.Hotels.AddRangeAsync(hotels);
            await _context.SaveChangesAsync();

            var rooms = new List<Room>
            {
                new Room { HotelId = hotels[0].Id, RoomNumber = "101", RoomType = "Single", PricePerNight = 250m, Capacity = 1, IsAvailable = true, Description = "Cozy single room" },
                new Room { HotelId = hotels[0].Id, RoomNumber = "102", RoomType = "Double", PricePerNight = 400m, Capacity = 2, IsAvailable = true, Description = "Double room with Nile view" },
                new Room { HotelId = hotels[1].Id, RoomNumber = "201", RoomType = "Double", PricePerNight = 150m, Capacity = 2, IsAvailable = true, Description = "Double room with pyramids view" },
                new Room { HotelId = hotels[2].Id, RoomNumber = "301", RoomType = "Royal Suite", PricePerNight = 1500m, Capacity = 4, IsAvailable = true, Description = "Luxury suite with sea view" }
            };

            await _context.Rooms.AddRangeAsync(rooms);
            await _context.SaveChangesAsync();
        }
    }
}