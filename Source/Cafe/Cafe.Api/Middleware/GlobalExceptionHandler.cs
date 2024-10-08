﻿using Cafe.Api.Model;
using Cafe.SharedKernel;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;

namespace Cafe.API.Middleware;

/// <summary>
/// Global exception handler.
/// </summary>
public class GlobalExceptionHandler : IExceptionHandler
{
    /// <summary>
    /// The logger
    /// </summary>
    private readonly ILogger<GlobalExceptionHandler> logger;

    /// <summary>
    /// Indicates if the responses should contain exception details.
    /// </summary>
    private readonly bool includeExceptionDetailsInResponse;

    /// <summary>
    /// Initializes a new instance of the <see cref="GlobalExceptionHandler"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    /// <param name="appSettings">The application settings.</param>
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IOptionsSnapshot<ApplicationConfig> appSettings)
    {
        this.logger = logger;
        this.includeExceptionDetailsInResponse = appSettings.Value.IncludeExceptionDetailsInResponse;
    }

    /// <summary>
    /// Tries to handle the specified exception asynchronously within the ASP.NET Core pipeline.
    /// Implementations of this method can provide custom exception-handling logic for different scenarios.
    /// </summary>
    /// <param name="httpContext">The <see cref="T:Microsoft.AspNetCore.Http.HttpContext" /> for the request.</param>
    /// <param name="exception">The unhandled exception.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// A task that represents the asynchronous read operation. The value of its <see cref="P:System.Threading.Tasks.ValueTask`1.Result" />
    /// property contains the result of the handling operation.
    /// <see langword="true" /> if the exception was handled successfully; otherwise <see langword="false" />.
    /// </returns>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        this.logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        var statusCode = StatusCodes.Status500InternalServerError;
        CustomProblemDetails problem = new CustomProblemDetails
        {
            Title = exception.Message,
            Status = statusCode,
            Type = nameof(HttpStatusCode.InternalServerError),
            Detail = this.includeExceptionDetailsInResponse ? exception.StackTrace : string.Empty,
        };

        httpContext.Response.StatusCode = problem.Status.Value;
        var logMessage = JsonConvert.SerializeObject(problem);
        this.logger.LogError(logMessage);
        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);

        return true;
    }
}
