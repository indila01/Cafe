using Cafe.Domain.Repositories;
using Cafe.SharedKernel.Primitives.Result;
using MediatR;


namespace Cafe.Application.Actions.Cafe.Create
{
    public class CreateCafeCommandHandler(
        ICafeRepository cafeRepository,
        IUnitOfWork unitOfWork) 
        : IRequestHandler<CreateCafeCommand, Result<Guid>>
    {
        private ICafeRepository cafeRepository { get; set; } = cafeRepository;
        private IUnitOfWork unitOfWork { get; set; } = unitOfWork;

        public async Task<Result<Guid>> Handle(CreateCafeCommand request, CancellationToken cancellationToken)
        {
            var cafe = Domain.Entities.Cafe.CreateCafe(request.name, request.description, request.location);
            this.cafeRepository.AddCafe(cafe);
            await this.unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(cafe.Id);
        }
    }
}
