using Cafe.SharedKernel.Primitives.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Actions.Cafe.Update
{
    public record UpdateCafeCommand(
        Guid id,
        string name,
        string description,
        string location
        ) : IRequest<Result<CafeDto>>;
}
