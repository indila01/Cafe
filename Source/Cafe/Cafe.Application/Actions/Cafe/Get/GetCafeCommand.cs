using Cafe.SharedKernel.Primitives.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Actions.Cafe.Get
{
    public record GetCafeCommand(string? location) : IRequest<Result<List<CafeDto>>>;
}
