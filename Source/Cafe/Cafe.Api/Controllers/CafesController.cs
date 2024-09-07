using Cafe.Api.Requests;
using Cafe.API.Extensions;
using Cafe.Application.Actions.Cafe.Create;
using Cafe.Application.Actions.Cafe.Get;
using Cafe.Application.Actions.Cafe.Remove;
using Cafe.Application.Actions.Cafe.Update;
using Cafe.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace Cafe.Api.Controllers
{
    public class CafesController(
        IMediator mediator,
        IOptionsSnapshot<ApplicationConfig> appSettings) : BaseController
    {

        private readonly IMediator mediator = mediator;
        private readonly ApplicationConfig appSettings = appSettings.Value;

        [HttpGet]
        public async Task<IResult> GetCafes(string? location)
        {
            var result = await mediator.Send(new GetCafeCommand(location));
            if (result.IsSuccess)
            {

                return result.Value.Any() ? Results.Ok(result.Value) : Results.NoContent();
            }
            return result.ToProblemDetails(appSettings.IncludeExceptionDetailsInResponse);
        }

        [HttpPost]
        public async Task<IResult> CreateCafe([FromBody] CafeRequest request)
        {
            var result = await mediator.Send(new CreateCafeCommand(
                request.Name,
                request.Description,
                request.Location));

            return result.IsSuccess ?
                Results.Ok(result.Value)
                : result.ToProblemDetails(appSettings.IncludeExceptionDetailsInResponse);
        }
        [HttpPut]
        public async Task<IResult> UpdateCafe([FromBody] CafeRequestUpdate request)
        {
            var result = await mediator.Send(new UpdateCafeCommand(
                request.Id,
                request.Name,
                request.Description,
                request.Location));

            return result.IsSuccess ?
                Results.Ok(result.Value)
                : result.ToProblemDetails(appSettings.IncludeExceptionDetailsInResponse);
        }

        [HttpDelete]
        public async Task<IResult> RemoveCafe(Guid cafeId)
        {
            var result = await mediator.Send(new DeleteCafeCommand(cafeId));

            return result.IsSuccess ?
                Results.Ok(result.Value)
                : result.ToProblemDetails(appSettings.IncludeExceptionDetailsInResponse);
        }
    }
}
