
namespace ECommerce.Application.Common
{
    public class Result
    {
        protected Result(bool isSuccess, IReadOnlyList<Error> errors)
        {
            IsSuccess = isSuccess;
            Errors = errors;
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public IReadOnlyList<Error> Errors { get; }

        public static Result Success()
            => new(true, Array.Empty<Error>());

        public static Result Failure(Error error)
            => new(false, new[] { error });

        public static Result Failure(IReadOnlyList<Error> errors)
            => new(false, errors);
    }
}
