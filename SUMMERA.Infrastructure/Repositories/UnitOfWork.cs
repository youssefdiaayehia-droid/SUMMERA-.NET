using SUMMERA.Application.Common.Interfaces;
using SUMMERA.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ICityRepository Cities { get; private set; }
        public IHotelRepository Hotels { get; private set; }
        public IRoomRepository Rooms { get; private set; }
        public IAmenityRepository Amenities { get; private set; }
        public IBookingRepository Bookings { get; private set; }
        public IReviewRepository Reviews { get; private set; }
        public IContactMessageRepository ContactMessages { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Cities = new CityRepository(_context);
            Hotels = new HotelRepository(_context);
            Rooms = new RoomRepository(_context);
            Amenities = new AmenityRepository(_context);
            Bookings = new BookingRepository(_context);
            Reviews = new ReviewRepository(_context);
            ContactMessages = new ContactMessageRepository(_context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
