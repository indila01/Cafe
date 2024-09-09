using Cafe.Domain.Entities;
using Cafe.Domain.Repositories;
using Cafe.SharedKernel.Primitives.Result;
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
            if (!string.IsNullOrWhiteSpace(cafe))
            {
                return await dbContext.Employees
                    .AsNoTracking()
                    .Include(c => c.Cafe)
                    .Where(e => e.Cafe.Name.Equals(cafe, StringComparison.OrdinalIgnoreCase))
                    .ToListAsync(cancellationToken);
            }
            return await dbContext.Employees
                .AsNoTracking()
                .Include(c => c.Cafe)
                .ToListAsync(cancellationToken);
        }

        public async Task<Employee?> GetEmployeeByIdAsync(string id, CancellationToken cancellationToken)
            => await dbContext.Employees
                .FirstOrDefaultAsync(e => e.Id.Equals(id, StringComparison.OrdinalIgnoreCase), cancellationToken);

        public async Task<bool> CheckUniqueId(string uniqueId)
            => !await dbContext.Employees.AnyAsync(e => e.Id == uniqueId);

        public void UpdateEmployee(Employee employee)
            => dbContext.Employees.Update(employee);

        public void AddEmployee(Employee employee)
            => dbContext.Employees.Add(employee);

        public void DeleteEmployee(Employee employee)
            => dbContext.Employees.Remove(employee);
    }
}
