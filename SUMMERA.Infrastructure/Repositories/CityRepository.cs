using SUMMERA.Application.Common.Interfaces;
using SUMMERA.Domain.Entities;
using SUMMERA.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUMMERA.Infrastructure.Repositories
{
    public class CityRepository : Repository<City> , ICityRepository
    {
        public CityRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
