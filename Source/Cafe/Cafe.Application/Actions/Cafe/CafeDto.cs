using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Actions.Cafe
{
    public record CafeDto(Guid Id, string Name, string Description, string Location, int? Employees = 0);
}
