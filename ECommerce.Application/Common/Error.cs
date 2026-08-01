
namespace ECommerce.Application.Common
{
    public sealed record Error(
        string Code,
        string Description,
        ErrorType Type = ErrorType.Failure)
    {
        public static readonly Error None = new(string.Empty, string.Empty);

        public static Error Failure(
            string code = "FAILURE",
            string description = "An unexpected error occurred.")
            => new(code, description);

        public static Error Validation(
            string code = "VALIDATION",
            string description = "One or more validation errors occurred.")
            => new(code, description, ErrorType.Validation);

        public static Error NotFound(
            string code = "NOT_FOUND",
            string description = "The requested resource was not found.")
            => new(code, description, ErrorType.NotFound);

        public static Error Unauthorized(
            string code = "UNAUTHORIZED",
            string description = "Authentication is required.")
            => new(code, description, ErrorType.Unauthorized);

        public static Error Forbidden(
            string code = "FORBIDDEN",
            string description = "You are not allowed to perform this action.")
            => new(code, description, ErrorType.Forbidden);

        public static Error Conflict(
            string code = "CONFLICT",
            string description = "A conflict occurred.")
            => new(code, description, ErrorType.Conflict);

        public static Error BadRequest(
            string code = "BAD_REQUEST",
            string description = "The request is invalid.")
            => new(code, description, ErrorType.BadRequest);

        public static Error Internal(
            string code = "INTERNAL_SERVER_ERROR",
            string description = "An internal server error occurred.")
            => new(code, description, ErrorType.InternalServerError);

        public static Error InvalidCredentials(
            string code = "INVALID_CREDENTIALS", 
            string description= "The provided credentials are invalid.") 
            => new(code, description, ErrorType.InvalidCredentials);
    }
}
