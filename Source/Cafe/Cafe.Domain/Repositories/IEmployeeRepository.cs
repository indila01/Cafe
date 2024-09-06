using Cafe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Domain.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployeebyCafeAsync(string cafe, CancellationToken cancellationToken = default);
    }
}
