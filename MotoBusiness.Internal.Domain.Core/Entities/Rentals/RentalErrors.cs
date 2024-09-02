using System;
using MotoBusiness.Internal.Domain.Core.Results;

namespace MotoBusiness.Internal.Domain.Core.Entities.Rentals
{
	public static class RentalErrors
	{
        public static Error Delivery_Rental_Unauthotized => new(
       "Retail.Notfound", $"Entregador não habilitado para essa locação.");

        public static Error Invalid_Plan => new(
        "Retail.Failed", $"Plano inválido.");

        public static Error Notfound => new(
        "Retail.Notfound", $"Locação não encontrada.");
    }
}
