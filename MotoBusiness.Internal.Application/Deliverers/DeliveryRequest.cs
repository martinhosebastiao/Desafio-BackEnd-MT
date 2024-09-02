using MotoBusiness.Internal.Domain.Core.Entities.Deliverers;

namespace MotoBusiness.Internal.Application.Deliverers
{
    public record DeliveryRegisterRequest(string Name, string CNPJ, DateTime BirthDate,
        string CNHNumber, CNHType CNHType)
    {
        public Delivery Convert()
        {
            var cnh = new CNH(CNHNumber, CNHType);
            var delivery = new Delivery(Name, BirthDate, CNPJ, cnh);

            return delivery;
        }
    };
}

