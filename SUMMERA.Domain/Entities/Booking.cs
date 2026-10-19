using SUMMERA.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Domain.Entities
{
    public class Booking : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string BookingReference { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfGuests { get; set; }
        public int NumberOfNights { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerNight { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SubTotal { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TaxAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        public BookingStatus Status { get; set; } = BookingStatus.Pending;
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Unpaid;

        [Required]
        public string UserId { get; set; }

        public int RoomId { get; set; }
        public Room Room { get; set; }

        [MaxLength(1000)]
        public string SpecialRequests { get; set; }

        public DateTime? ConfirmedAt { get; set; }
        public DateTime? CancelledAt { get; set; }
    }
}
