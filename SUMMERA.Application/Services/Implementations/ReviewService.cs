using AutoMapper;
using SUMMERA.Application.Common.Interfaces;
using SUMMERA.Application.DTOs.Reviews;
using SUMMERA.Application.Services.Interfaces;
using SUMMERA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Application.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReviewDto>> GetHotelReviewsAsync(int hotelId)
        {
            var reviews = await _unitOfWork.Reviews.GetAllAsync(
                filter: r => r.HotelId == hotelId);

            return _mapper.Map<IEnumerable<ReviewDto>>(reviews);
        }

        public async Task AddReviewAsync(string userId, int hotelId, int rating, string comment)
        {
            if (rating < 1 || rating > 5)
                throw new Exception("Rating must be between 1 and 5");

            var review = new Review
            {
                UserId = userId,
                HotelId = hotelId,
                Rating = rating,
                Comment = comment
            };

            await _unitOfWork.Reviews.AddAsync(review);
            await _unitOfWork.SaveAsync();
        }
    }
}
