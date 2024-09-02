using MotoBusiness.Internal.Domain.Core.Results;

namespace MotoBusiness.Internal.Domain.Core.Entities.Deliverers
{
    public static class DeliveryErrors
	{
        public static Error CNPJ_AlreadyExists => new(
        "Delivery.Failed", $"O CNPJ já esta em uso.");

        public static Error CNPJ_Invalid => new(
        "Delivery.Invalid", $"O CNPJ é invalido.");

        public static Error CNH_AlreadyExists => new(
        "Delivery.Failed", $"A CNH já esta em uso.");

        public static Error CNH_NumberInvalid => new(
        "Delivery.Invalid", $"O Numero da CNH é invalido.");

        public static Error CNH_ImageInvalid => new(
        "Delivery.Invalid", $"A fotografia da CNH é invalida.");

        public static Error NotFound => new(
        "Delivery.Notfound", $"O entregador não foi encontrado.");

        public static Error NameInvalid => new(
        "Delivery.Invalid", $"O nome é invalido.");

        public static Error BirthDateInvalid => new(
        "Delivery.Invalid", $"A idade do entregador é invalida ou não permitida.");
    }
}

