
namespace ECommerce.Application.Common
{
    public class ResultOfT
    {
        public class Result<T> : Result
        {
            private Result(T value)
            : base(true, Array.Empty<Error>())
            {
                Value = value;
            }

            private Result(IReadOnlyList<Error> errors)
                : base(false, errors)
            {
                Value = default!;
            }

            public T Value { get; }

            public static Result<T> Success(T value)
                => new(value);

            public new static Result<T> Failure(Error error)
                => new(new[] { error });

            public new static Result<T> Failure(IReadOnlyList<Error> errors)
                => new(errors);

            public static implicit operator Result<T>(T value)
                => Success(value);

            public static implicit operator Result<T>(Error value)
                => Failure(value);
        }
    }
}
