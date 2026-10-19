using AutoMapper;
using SUMMERA.Application.Common.Interfaces;
using SUMMERA.Application.DTOs.ContactMessages;
using SUMMERA.Application.Services.Interfaces;
using SUMMERA.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Application.Services.Implementations
{
    public class ContactMessageService : IContactMessageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContactMessageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ContactMessageDto> CreateAsync(CreateContactMessageDto dto)
        {
            var message = _mapper.Map<ContactMessage>(dto);
            await _unitOfWork.ContactMessages.AddAsync(message);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<ContactMessageDto>(message);
        }

        public async Task<IEnumerable<ContactMessageDto>> GetAllAsync()
        {
            var messages = await _unitOfWork.ContactMessages.GetAllAsync();
            return _mapper.Map<IEnumerable<ContactMessageDto>>(messages);
        }

        public async Task MarkAsReadAsync(int id)
        {
            var message = await _unitOfWork.ContactMessages.GetAsync(m => m.Id == id);
            if (message == null) throw new Exception("Message not found");

            message.IsRead = true;
            message.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.ContactMessages.Update(message);
            await _unitOfWork.SaveAsync();
        }
    }
}
