using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Application.Common.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICityRepository Cities { get; }
        IHotelRepository Hotels { get; }
        IRoomRepository Rooms { get; }
        IAmenityRepository Amenities { get; }
        IBookingRepository Bookings { get; }
        IReviewRepository Reviews { get; }
        IContactMessageRepository ContactMessages { get; }

        Task<int> SaveAsync();
    }
}
