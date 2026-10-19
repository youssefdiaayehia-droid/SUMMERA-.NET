using SUMMERA.Application.DTOs.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Application.Services.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewDto>> GetHotelReviewsAsync(int hotelId);
        Task AddReviewAsync(string userId, int hotelId, int rating, string comment);
    }
}
