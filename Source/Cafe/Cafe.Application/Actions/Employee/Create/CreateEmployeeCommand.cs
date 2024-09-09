using Cafe.SharedKernel.Primitives.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Actions.Employee.Create
{
    public record CreateEmployeeCommand(
        string name, 
        string gender, 
        string email, 
        long phoneNumber, 
        Guid cafeId ) 
        : IRequest<Result<string>>;
}
