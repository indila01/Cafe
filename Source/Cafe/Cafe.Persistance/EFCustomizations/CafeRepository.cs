using Cafe.Domain.Entities;
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

        public async Task<Domain.Entities.Cafe?> GetCafeByIdAsync(
            Guid? cafeId,
            CancellationToken cancellationToken = default)
            => await dbContext.Cafes
                .Include(x => x.Employees)
                .FirstOrDefaultAsync(c => c.Id == cafeId);

        public async Task<List<Domain.Entities.Cafe>> GetCafeByLocationAsync(
            string location,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(location))
            {
                return await dbContext.Cafes
                    .AsNoTracking()
                    .Include(x => x.Employees)
                    .OrderByDescending(c => c.Employees.Count)
                    .ToListAsync(cancellationToken);
            }
            return await dbContext.Cafes
                .AsNoTracking()
                .Include(x => x.Employees)
                .Where(c => c.Location == location)
                .OrderByDescending(c => c.Employees.Count)
                .ToListAsync(cancellationToken);
        }

        public void UpdateCafe(Domain.Entities.Cafe cafe)
            => dbContext.Cafes.Update(cafe);

        public void AddCafe(Domain.Entities.Cafe cafe)
          => dbContext.Cafes.Add(cafe);

        public void DeleteCafe(Domain.Entities.Cafe cafe)
            => dbContext.Cafes.Remove(cafe);
    }
}
