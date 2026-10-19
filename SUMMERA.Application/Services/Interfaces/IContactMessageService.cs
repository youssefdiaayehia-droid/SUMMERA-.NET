using SUMMERA.Application.DTOs.ContactMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Application.Services.Interfaces
{
    public interface IContactMessageService
    {
        Task<ContactMessageDto> CreateAsync(CreateContactMessageDto dto);
        Task<IEnumerable<ContactMessageDto>> GetAllAsync();
        Task MarkAsReadAsync(int id);
    }
}
