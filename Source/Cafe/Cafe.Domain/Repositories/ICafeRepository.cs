using Cafe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Domain.Repositories
{
    public interface ICafeRepository
    {
        Task<List<Cafe.Domain.Entities.Cafe>> GetCafeByLocationAsync(string location, CancellationToken cancellationToken = default);
        Task<Cafe.Domain.Entities.Cafe?> GetCafeByIdAsync(Guid cafeId, CancellationToken cancellationToken = default);
        void AddCafe(Cafe.Domain.Entities.Cafe cafe);
        void UpdateCafe(Entities.Cafe cafe);
        void DeleteCafe(Entities.Cafe cafe);
    }
}
