using Cafe.SharedKernel.Primitives;

namespace Cafe.Domain.Core.Errors;

/// <summary>
/// Contains the domain errors.
/// </summary>
public static class DomainErrors
{
    /// <summary>
    /// Contains the email errors.
    /// </summary>
    public static class Email
    {
        /// <summary>
        /// gets null or empty error
        /// </summary>
        /// <returns>Error</returns>
        public static Error NullOrEmpty => Error.Validation("Email.NullOrEmpty", "The email is required.");

        /// <summary>
        /// gets longer than allowed
        /// </summary>
        /// <returns>Error</returns>
        public static Error LongerThanAllowed => Error.Validation("Email.LongerThanAllowed", "The email is longer than allowed.");

        /// <summary>
        /// gets invalid format error
        /// </summary>
        /// <returns>Error</returns>
        public static Error InvalidFormat => Error.Validation("Email.InvalidFormat", "The email format is invalid.");
    }

    /// <summary>
    /// Contains the PhoneNumber errors.
    /// </summary>
    public static class PhoneNumber
    {
        /// <summary>
        /// gets null or empty error
        /// </summary>
        /// <returns>Error</returns>
        public static Error NullOrEmpty => Error.Validation("PhoneNumber.NullOrEmpty", "The Phone Number is required.");

        /// <summary>
        /// gets invalid format error
        /// </summary>
        /// <returns>Error</returns>
        public static Error InvalidFormat => Error.Validation("PhoneNumber.InvalidFormat", "The Phone number should start with 8 or 9.");

        /// <summary>
        /// gets invalid length error
        /// </summary>
        /// <returns>Error</returns>
        public static Error InvalidLength => Error.Validation("PhoneNumber.InvalidLength", "The phone number should 8 digits.");
    }

    /// <summary>
    /// Contains the Gender errors.
    /// </summary>
    public static class Gender
    {
        /// <summary>
        /// gets null or empty error
        /// </summary>
        /// <returns>Error</returns>
        public static Error NullOrEmpty => Error.Validation("Gender.NullOrEmpty", "The Gender is required.");

        /// <summary>
        /// gets longer than allowed
        /// </summary>
        /// <returns>Error</returns>
        public static Error LongerThanAllowed => Error.Validation("Gender.LongerThanAllowed", "The Gender is longer than allowed.");

        /// <summary>
        /// gets invalid format error
        /// </summary>
        /// <returns>Error</returns>
        public static Error InvalidValue => Error.Validation("Email.InvalidValue", "The Gender is invalid.");
    }

    /// <summary>
    /// Contains general errors.
    /// </summary>
    public static class General
    {
        /// <summary>
        /// gets unproccessed request error
        /// </summary>
        /// <returns>Error</returns>
        public static Error UnProcessableRequest => Error.Failiure(
            "General.UnProcessableRequest",
            "The server could not process the request.");

        /// <summary>
        /// gets server error
        /// </summary>
        /// <returns>Error</returns>
        public static Error ServerError => Error.Failiure("General.ServerError", "The server encountered an unrecoverable error.");
    }

    /// <summary>
    /// Contains the authentication errors.
    /// </summary>
    public static class Authentication
    {
        /// <summary>
        /// gets invalid email or password error
        /// </summary>
        /// <returns>Error</returns>
        public static Error InvalidEmailOrPassword => Error.Validation(
            "Authentication.InvalidEmailOrPassword",
            "The specified email or password are incorrect.");
    }
}