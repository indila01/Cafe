using Cafe.Domain.Core.Errors;
using Cafe.SharedKernel.Primitives;
using Cafe.SharedKernel.Primitives.Result;

namespace Cafe.Domain.ValueObjects;

/// <summary>
/// Represents the gender value object.
/// </summary>
public sealed class Gender : ValueObject
{
    /// <summary>
    /// The gender maximum length.
    /// </summary>
    public const int MaxLength = 50;

    private static readonly List<string> ValidGenders = new List<string> { "Male", "Female" };

    /// <summary>
    /// Initializes a new instance of the <see cref="Gender"/> class.
    /// </summary>
    /// <param name="value">The gender value.</param>
    private Gender(string value) => this.Value = value;

    /// <summary>
    /// Gets the gender value.
    /// </summary>
    public string Value { get; }

    public static implicit operator string(Gender gender) => gender.Value;

    /// <summary>
    /// Creates a new <see cref="Gender"/> instance based on the specified value.
    /// </summary>
    /// <param name="gender">The gender value.</param>
    /// <returns>The result of the gender creation process containing the gender or an error.</returns>
    public static Result<Gender> Create(string gender) =>
        Result.Create(gender, DomainErrors.Gender.NullOrEmpty)
            .Ensure(g => !string.IsNullOrWhiteSpace(g), DomainErrors.Gender.NullOrEmpty)
            .Ensure(g => g.Length <= MaxLength, DomainErrors.Gender.LongerThanAllowed)
            .Ensure(g => ValidGenders.Contains(g), DomainErrors.Gender.InvalidValue)
            .Map(g => new Gender(g));

    /// <inheritdoc />
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return this.Value;
    }
}