using Cafe.Application.Actions.Cafe.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.Api.Controllers
{
    public class EmployeesController : BaseController
    {
        private readonly IMediator mediator;
        public EmployeesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IResult> GetEmployees(string? Cafe)
        {
            var result = await mediator.Send(new GetCafeCommand(Cafe));
            return Results.Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<object>> CreateEmployee()
        {
            //var result = await mediator.Send(new GetProductCommand
            //{
            //    ProductName = productName
            //});
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<object>> UpdateEmployee()
        {
            //var result = await mediator.Send(new GetProductCommand
            //{
            //    ProductName = productName
            //});
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult<object>> RemoveEmployee()
        {
            //var result = await mediator.Send(new GetProductCommand
            //{
            //    ProductName = productName
            //});
            return Ok();
        }
    }
}
