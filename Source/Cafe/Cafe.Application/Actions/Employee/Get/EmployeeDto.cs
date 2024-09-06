using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Actions.Employee.Get
{
    public record EmployeeDto(
        string Id, 
        string Name,
        string Email,
        string PhoneNumber,
        int DaysWorked,
        string? Cafe);
}
