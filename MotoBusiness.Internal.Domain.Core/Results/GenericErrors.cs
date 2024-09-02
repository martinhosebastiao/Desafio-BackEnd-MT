namespace MotoBusiness.Internal.Domain.Core.Results
{
    public static class GenericErrors
    {
        public static Error ExpectationFailed => new(
            "Generic.ExpectationFailed", "A informação não foi encontrada.");

        public static Error Exception => new(
            "Generic.Exception", "Foi gerada uma excepção, consulte o suporte ou analisa os detalhes.");

        public static Error ExpectationFailedWithMessage(string message) => new(
          $"Generic.ExpectationFailed", message);

        public static Error ExceptionWithMessage(string? message) => new(
            $"Generic.Exception", message ?? "Foi gerada uma excepção, consulte o suporte ou analisa os detalhes.");

        public static Error ExpectationFailedWithMessage(dynamic layerCode, string message) => new(
          $"{layerCode}.NotFound", message);

        public static Error ExceptionWithMessage(dynamic layerCode, string? message) => new(
            $"{layerCode}.Exception", message ?? "Foi gerada uma excepção, consulte o suporte ou analisa os detalhes.");

    }
}

