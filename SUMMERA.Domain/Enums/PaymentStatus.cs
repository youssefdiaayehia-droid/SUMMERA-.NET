using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Domain.Enums
{
    public enum PaymentStatus
    {
        Unpaid = 1,
        PartiallyPaid = 2,
        Paid = 3,
        Refunded = 4
    }
}
