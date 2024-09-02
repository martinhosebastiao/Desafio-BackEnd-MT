using MotoBusiness.Internal.Domain.Core.Results;

namespace MotoBusiness.Internal.Domain.Core.Entities.Motorbikes
{
    public static class MotorbikeErrors
	{
        public static Error NotFound => new(
        "Motorbike.Notfound", $"A moto não foi encontrada.");

        public static Error InvalidYear => new(
        "Motorbike.Invalid", $"O Ano da moto é invalido.");

        public static Error DiffYear(short year) => new(
        "Motorbike.Error", $"Moto fabricada em {year} diferente de 2024.");

        public static Error InvalidModel => new(
        "Motorbike.Invalid", $"O Modelo da moto é invalido.");

        public static Error InvalidPlate => new(
        "Motorbike.Invalid", $"A placa da moto é invalida.");

        public static Error ExistPlate => new(
        "Motorbike.Exist", $"A placa da moto já esta em uso.");

        public static Error ExistRetail => new(
       "Motorbike.Exist", $"Existe registro de locação para a moto.");
    }
}

