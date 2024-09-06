using Cafe.Domain.Repositories;
using Cafe.SharedKernel.Primitives.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Actions.Cafe.Get
{
    public class GetCafeCommandHandler(ICafeRepository cafeRepository) 
        : IRequestHandler<GetCafeCommand, Result<List<CafeDto>>>
    {
        private ICafeRepository cafeRepository { get; set; } = cafeRepository;

        public async Task<Result<List<CafeDto>>> Handle(GetCafeCommand request, CancellationToken cancellationToken)
        {
            var query = await cafeRepository.GetCafeByLocationAsync(request.location, cancellationToken);
            var result = new List<CafeDto>();
            foreach (var cafe in query)
            {
                var cafeDto = new CafeDto(cafe.Id, cafe.Location, cafe.Description, cafe.Location, Employees: cafe.Employees.Count);
                result.Add(cafeDto);
            }
            return Result.Success(result);
        }
    }
}
