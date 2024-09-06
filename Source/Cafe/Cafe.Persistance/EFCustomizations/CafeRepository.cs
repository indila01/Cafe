using Cafe.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Persistance.EFCustomizations
{
    public class CafeRepository : ICafeRepository
    {
        private readonly CafeDbContext dbContext;

        public CafeRepository(CafeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Domain.Entities.Cafe>> GetCafeByLocationAsync(
            string location,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(location))
            {
                return await dbContext.Cafes
                    .AsNoTracking()
                    .OrderByDescending(c=>c.Employees.Count)
                    .ToListAsync(cancellationToken);
            }
            return await dbContext.Cafes
                .AsNoTracking()
                .Where(c => c.Location == location)
                .OrderByDescending(c => c.Employees.Count)
                .ToListAsync(cancellationToken);
        }
    }
}
