﻿using Cafe.Api.Requests;
using Cafe.API.Extensions;
using Cafe.Application.Actions.Employee.Create;
using Cafe.Application.Actions.Employee.Get;
using Cafe.Application.Actions.Employee.Remove;
using Cafe.Application.Actions.Employee.Update;
using Cafe.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Cafe.Api.Controllers
{
    public class EmployeesController(
            IMediator mediator,
            IOptionsSnapshot<ApplicationConfig> appSettings) : BaseController
    {

        private readonly IMediator mediator = mediator;
        private readonly ApplicationConfig appSettings = appSettings.Value;

        [HttpGet]
        public async Task<IResult> GetEmployees(string? Cafe)
        {
            var result = await mediator.Send(new GetEmployeeCommand(Cafe));
            if (result.IsSuccess)
            {
                return Results.Ok(result.Value);
            }
            return result.ToProblemDetails(appSettings.IncludeExceptionDetailsInResponse);
        }

        [HttpPost]
        public async Task<IResult> CreateEmployee([FromBody] EmployeeRequest request)
        {
            var result = await mediator.Send(new CreateEmployeeCommand(
                 request.Name,
                 request.Gender,
                 request.Email,
                 request.PhoneNumber,
                 request.CafeId));

            return result.IsSuccess ?
                Results.Ok(result.Value)
                : result.ToProblemDetails(appSettings.IncludeExceptionDetailsInResponse);
        }

        [HttpPut]
        public async Task<IResult> UpdateEmployee([FromQuery]string id, [FromBody]EmployeeRequest request)
        {
            var result = await mediator.Send(new UpdateEmployeeCommand(
                id,
                request.Name,
                request.Gender,
                request.Email,
                request.PhoneNumber,
                request.CafeId));

            return result.IsSuccess ?
                Results.Ok(result.Value)
                : result.ToProblemDetails(appSettings.IncludeExceptionDetailsInResponse);
        }

        [HttpDelete]
        public async Task<IResult> RemoveEmployee(string employeeId)
        {
            var result = await mediator.Send(new DeleteEmployeeCommand(employeeId));

            return result.IsSuccess ?
                 Results.Ok(result.Value)
                 : result.ToProblemDetails(appSettings.IncludeExceptionDetailsInResponse);
        }
    }
}
