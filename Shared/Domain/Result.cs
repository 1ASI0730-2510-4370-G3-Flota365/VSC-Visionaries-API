namespace Flota365.Platform.API.Shared.Domain.Model
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string Error { get; }

        protected Result(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error ?? string.Empty;
        }

        public static Result Success() => new(true, string.Empty);
        public static Result Failure(string error) => new(false, error);
        public static Result<T> Success<T>(T value) => new(value, true, string.Empty);
        public static Result<T> Failure<T>(string error) => new(default!, false, error);
    }

    public class Result<T> : Result
    {
        public T Value { get; }
        internal Result(T value, bool isSuccess, string error) : base(isSuccess, error)
        {
            Value = value;
        }
    }
}