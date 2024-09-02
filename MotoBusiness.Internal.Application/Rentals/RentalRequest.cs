using System;
using MotoBusiness.Internal.Domain.Core.Entities.Deliverers;
using MotoBusiness.Internal.Domain.Core.Entities.Rentals;

namespace MotoBusiness.Internal.Application.Rentals
{
    public record RentalRegisterRequest(int MotorbikeId,int DeliveryId,short PlainDays)
    {
        public Rental Convert()
        {
            var delivery = new Rental(MotorbikeId,DeliveryId,PlainDays);

            return delivery;
        }
    };

    public record RentalFinishRequest(int RentaiId, DateTime ReturnDate);
}

