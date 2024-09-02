using MotoBusiness.Internal.Domain.Core.Abstractions;
using MotoBusiness.Internal.Domain.Core.Entities.Rentals;

namespace MotoBusiness.Internal.Domain.Core.Entities.Motorbikes
{
    public sealed class Motorbike : BaseEntity
    {
        protected Motorbike()
        {
            Rentals = new();
        }

        public Motorbike(string model, string plate, short year)
        {
            ChangeYear(year);
            ChangeModel(model);
            ChangePlate(plate);
            Rentals = new();
        }

        public int DevileryId { get; private set; }
        public short Year { get; private set; }
        public string Model { get; private set; } = default!;
        public string Plate { get; private set; } = default!;

        public List<Rental> Rentals { get; private set; }

        #region - Business Rules -
        /// <summary>
        /// Regras de negocio referente ao ano de fabrico da Moto.
        /// Todo: Implementar regras adicionais se aplicavel.
        /// </summary>
        /// <param name="year">Ano de fabrico</param>
        public void ChangeYear(short year)
        {
            if (year < 2015)
            {
                var message = MotorbikeErrors.InvalidYear;

                AddError(message.Description!);

                return;
            }

            Year = year;
        }

        /// <summary>
        /// Regras de negócio referente ao Modelo da Moto
        /// Todo: Implementar regras adicionais se aplicavel.
        /// </summary>
        /// <param name="model">Modelo da moto</param>
        public void ChangeModel(string model)
        {
            if (string.IsNullOrEmpty(model) || model.Length < 2)
            {
                var message = MotorbikeErrors.InvalidModel;

                AddError(message.Description!);

                return;
            }

            Model = model;
        }

        /// <summary>
        /// Regras de negócio referente a placa da Moto
        /// Todo: Implementar regras adicionais se aplicavel
        /// </summary>
        /// <param name="plate">Placa da Moto</param>
        public void ChangePlate(string plate)
        {
            if (string.IsNullOrEmpty(plate) || plate.Length < 6)
            {
                var message = MotorbikeErrors.InvalidPlate;

                AddError(message.Description!);

                return;
            }

            Plate = plate;
        }

        /// <summary>
        /// Verificar se o ano de fabrico da Moto é 2024
        /// </summary>
        /// <returns>Retorna true  se o ano é igual a 2024</returns>
        public bool IsManufactureYears2024() => Year == 2024;

        public void AddRental(Rental rental)
        {
            Rentals.Add(rental);
        }
        #endregion
    }
}