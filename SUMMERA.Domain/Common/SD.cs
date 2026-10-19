using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Domain.Common
{
    public static class SD
    {
        // Roles
        public const string Role_Admin = "Admin";
        public const string Role_Customer = "Customer";

        // Booking
        public const decimal TaxRate = 0.14m;
        public const int MaxGuestsPerRoom = 4;

        // Booking Reference Prefix
        public const string BookingPrefix = "SUM";
    }
}
