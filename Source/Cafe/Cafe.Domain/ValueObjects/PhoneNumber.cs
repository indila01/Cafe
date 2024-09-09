using Cafe.Domain.Core.Errors;
using Cafe.SharedKernel.Primitives;
using Cafe.SharedKernel.Primitives.Result;
using System.Text.RegularExpressions;

namespace Cafe.Domain.ValueObjects;

/// <summary>
/// Represents the phone number value object.
/// </summary>
public sealed class PhoneNumber : ValueObject
{
    /// <summary>
    /// The phone number maximum length.
    /// </summary>
    public const int MaxLength = 8;

    private const string PhoneNumberRegexPattern = @"^[89]\d{7}$";

    private static readonly Lazy<Regex> PhoneNumberFormatRegex =
        new Lazy<Regex>(() => new Regex(PhoneNumberRegexPattern, RegexOptions.Compiled));

    /// <summary>
    /// Initializes a new instance of the <see cref="PhoneNumber"/> class.
    /// </summary>
    /// <param name="value">The phone number value.</param>
    private PhoneNumber(long value) => this.Value = value;

    /// <summary>
    /// Gets the phone number value.
    /// </summary>
    public long Value { get; }

    public static implicit operator long(PhoneNumber phoneNumber) => phoneNumber.Value;

    /// <summary>
    /// Creates a new <see cref="PhoneNumber"/> instance based on the specified value.
    /// </summary>
    /// <param name="phoneNumber">The phone number value.</param>
    /// <returns>The result of the phone number creation process containing the phone number or an error.</returns>
    public static Result<PhoneNumber> Create(string phoneNumber)
    {
        if (!long.TryParse(phoneNumber, out var parsedPhoneNumber))
        {
            return Result.Failure<PhoneNumber>(DomainErrors.PhoneNumber.InvalidFormat);
        }

        return Result.Create(phoneNumber, DomainErrors.PhoneNumber.NullOrEmpty)
            .Ensure(p => !string.IsNullOrWhiteSpace(p), DomainErrors.PhoneNumber.NullOrEmpty)
            .Ensure(p => p.Length == MaxLength, DomainErrors.PhoneNumber.InvalidLength)
            .Ensure(p => PhoneNumberFormatRegex.Value.IsMatch(p.ToString()), DomainErrors.PhoneNumber.InvalidFormat)
            .Map(p => new PhoneNumber(parsedPhoneNumber));
    }

    /// <inheritdoc />
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return this.Value;
    }
}