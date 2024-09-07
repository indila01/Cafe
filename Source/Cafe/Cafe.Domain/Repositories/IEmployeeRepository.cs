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
        Task<bool> CheckUniqueId(string uniqueId);
        Task<List<Employee>> GetEmployeebyCafeAsync(string cafe, CancellationToken cancellationToken = default);
        Task<Employee> GetEmployeeByIdAsync(string id, CancellationToken cancellationToken);
        void AddEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
    }
}
