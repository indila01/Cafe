using Microsoft.AspNetCore.Mvc;

namespace Cafe.Api.Model;

/// <summary>
/// custom problem details class
/// </summary>
/// <seealso cref="Microsoft.AspNetCore.Mvc.ProblemDetails" />
public class CustomProblemDetails : ProblemDetails
{
    /// <summary>
    /// Gets or sets the errors.
    /// </summary>
    /// <value>
    /// The errors.
    /// </value>
    public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
}