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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly CafeDbContext dbContext;

        public EmployeeRepository(CafeDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<Employee>> GetEmployeebyCafeAsync(string cafe, CancellationToken cancellationToken = default)
        {
            if(string.IsNullOrWhiteSpace(cafe))
            {
                return await dbContext.Employees
                    .AsNoTracking()
                    .ToListAsync(cancellationToken);
            }
            return await dbContext.Employees
                .AsNoTracking()
                .Where(e => e.Cafe.Name == cafe)
                .ToListAsync(cancellationToken);
        }
    }
}
