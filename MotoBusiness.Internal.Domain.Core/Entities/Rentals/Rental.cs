using MotoBusiness.Internal.Domain.Core.Abstractions;
using MotoBusiness.Internal.Domain.Core.Entities.Deliverers;
using MotoBusiness.Internal.Domain.Core.Entities.Motorbikes;

namespace MotoBusiness.Internal.Domain.Core.Entities.Rentals
{
    public sealed class Rental : BaseEntity
    {
        protected Rental() { }

        public Rental(int motorbikeId, int deliveryId, short plainDays)
        {
            StartRental(motorbikeId, deliveryId, plainDays);
        }

        public int RentalId { get; private set; }
        public int DeliveryId { get; private set; }
        public int MotorbikeId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime ActualEndDate { get; private set; }
        public DateTime ExpectedEndDate { get; private set; }
        public decimal Amount { get; private set; }
        public decimal Fine { get; private set; }
        public decimal Total { get => Amount + Fine; }

        public Motorbike Motorbike { get; private set; }
        public Delivery Delivery { get; private set; }

        #region - Business Rules -
        private decimal CalculateAmount(short plainDay)
        {
            decimal amountday = PlainAmount(plainDay);

            amountday *= plainDay;

            return amountday;
        }

        private decimal PlainAmount(short plainDay)
        {
            decimal amountday = 0;

            switch (plainDay)
            {
                case 7:
                    amountday = 30.00m;
                    break;
                case 15:
                    amountday = 28.00m;
                    break;
                case 30:
                    amountday = 22.00m;
                    break;
                case 45:
                    amountday = 20.00m;
                    break;
                case 50:
                    amountday = 18.00m;
                    break;
                default:
                    var error = RentalErrors.Invalid_Plan;
                    AddError(error.Description!);
                    break;
            }

            return amountday;
        }

        private decimal CalculateFine(short extendedDays)
        {
            var amountFine = extendedDays * Amount /
                ExpectedEndDate.Subtract(StartDate).Days;

            var _amountFine = Amount * (ExpectedEndDate
                    .Subtract(StartDate).Days
                switch
            {
                7 => 0.20m,
                15 => 0.40m,
                _ => 0.0m
            }) * amountFine;

            return _amountFine;
        }

        private void StartRental(
            int motorbikeId, int deliveryId, short plainDays)
        {
            DeliveryId = deliveryId;
            MotorbikeId = motorbikeId;
            StartDate = DateTime.UtcNow.AddDays(1);
            ExpectedEndDate = DateTime.UtcNow.AddDays(plainDays + 1);
            Amount = CalculateAmount(plainDays);
        }

        /// <summary>
        /// Finalizar a locação, calculando multas e diarias adicionais.
        /// </summary>
        public void FinishRental(DateTime returnDate)
        {
            ActualEndDate = returnDate;

            if (ActualEndDate < ExpectedEndDate)
            {
                var extendedDays = (short)ExpectedEndDate
                    .Subtract(ActualEndDate).Days;

                Fine = CalculateFine(extendedDays);
            }
            else if (ActualEndDate > ExpectedEndDate)
            {
                var adicionalDays = ActualEndDate
                    .Subtract(ExpectedEndDate).Days;
                Amount += adicionalDays * 50.00m;
            }
        }
        #endregion
    }
}