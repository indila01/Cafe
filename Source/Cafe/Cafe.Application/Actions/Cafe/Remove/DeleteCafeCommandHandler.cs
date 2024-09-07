using Cafe.Domain.Core.Errors;
using Cafe.Domain.Repositories;
using Cafe.SharedKernel.Primitives.Result;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Actions.Cafe.Remove
{
    public class DeleteCafeCommandHandler(
        ICafeRepository cafeRepository,
        IUnitOfWork unitOfWork)
        : IRequestHandler<DeleteCafeCommand, Result<Guid>>
    {
        private ICafeRepository cafeRepository { get; set; } = cafeRepository;
        private IUnitOfWork unitOfWork { get; set; } = unitOfWork;

        public async Task<Result<Guid>> Handle(DeleteCafeCommand request, CancellationToken cancellationToken)
        {
            var cafe = await cafeRepository.GetCafeByIdAsync(request.id, cancellationToken);
            if (cafe == null)
            {
                return Result.Failure<Guid>(DomainErrors.Cafe.NotFound);
            }
            cafeRepository.DeleteCafe(cafe);
            var result = await unitOfWork.SaveChangesAsync(cancellationToken);

            if (result == 0)
            {
                return Result.Failure<Guid>(DomainErrors.Cafe.FailedToUpdate);
            }
            return Result.Success(cafe.Id);
        }
    }
}
