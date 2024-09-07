using Cafe.Domain.Core.Errors;
using Cafe.Domain.Repositories;
using Cafe.SharedKernel.Primitives.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Actions.Cafe.Update
{
    public class UpdateCafeCommandHandler(
        ICafeRepository cafeRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<UpdateCafeCommand, Result<CafeDto>>
    {
        private ICafeRepository cafeRepository { get; set; } = cafeRepository;
        private IUnitOfWork unitOfWork { get; set; } = unitOfWork;

        public async Task<Result<CafeDto>> Handle(UpdateCafeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var cafe = await cafeRepository.GetCafeByIdAsync(request.id, cancellationToken);
                if (cafe == null)
                {
                    return Result.Failure<CafeDto>(DomainErrors.Cafe.NotFound);
                }

                cafe.UpdateCafe(request.name, request.description, request.location);

                cafeRepository.UpdateCafe(cafe);
                var result = await unitOfWork.SaveChangesAsync(cancellationToken);

                if (result == 0)
                {
                    return Result.Failure<CafeDto>(DomainErrors.Cafe.FailedToUpdate);
                }
                return Result.Success(new CafeDto(cafe.Id, cafe.Name, cafe.Description, cafe.Location));
            }
            catch (Exception)
            {
                return Result.Failure<CafeDto>(DomainErrors.Cafe.FailedToUpdate);
            }
           
        }
    }
}
