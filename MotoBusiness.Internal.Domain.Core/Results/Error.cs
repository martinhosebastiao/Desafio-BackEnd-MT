namespace MotoBusiness.Internal.Domain.Core.Results
{
    public record Error(dynamic? Code = default, string? Description = default)
    {
        public static readonly Error None = new(string.Empty, string.Empty);

        public static readonly Error NullValue = new(
            "Error.NullValue",
            "O valor fornecido é nulo");

        public static implicit operator Result(Error error)
            => Result.Failure(error);

        public Result ToResult() => Result.Failure(this);
    }
}

