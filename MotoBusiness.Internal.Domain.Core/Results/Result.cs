using System.Text.Json.Serialization;

namespace MotoBusiness.Internal.Domain.Core.Results
{
    public partial record Result
    {
        protected internal Result(bool isSuccess, Error? error = null)
        {
            if (isSuccess && error != null)
            {
                throw new ArgumentException("Erro invalido", nameof(error));
            }

            Error = error;
            IsSuccess = isSuccess;
        }

        public static Result Success() => new(true);
        public static Result Failure(Error error) => new(false, error);

        public static Result<TValue> Success<TValue>(TValue value)
            => new(value, true, null);

        public static Result<TValue> Failure<TValue>(Error error)
            => new(default!, false, error);

        public static Result<TValue> Failure<TValue>(TValue value, Error error)
            => new(value, false, error);

        #region - Properties -
        [JsonPropertyOrder(1)]
        public bool IsSuccess { get; }

        [JsonIgnore]
        public bool IsFailure => !IsSuccess;

        [JsonPropertyOrder(2)]
        public Error? Error { get; }

        [JsonPropertyOrder(3)]
        public object? Data { get; init; }
        #endregion
    }

    public partial record Result<TValue> : Result
    {
        protected internal Result(TValue value, bool isSuccess, Error? error)
            : base(isSuccess, error)
            => Data = value;
    }
}