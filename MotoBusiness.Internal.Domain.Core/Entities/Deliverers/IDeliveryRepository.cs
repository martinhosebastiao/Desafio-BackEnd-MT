using MotoBusiness.Internal.Domain.Core.Abstractions;
using MotoBusiness.Internal.Domain.Core.Results;

namespace MotoBusiness.Internal.Domain.Core.Entities.Deliverers
{
    public interface IDeliveryRepository : IBaseRepository<Delivery>
	{
        Task<CustomResult> GetDeliveryByCNPJAsync(
            string cnpj,
            CancellationToken cancellationToken = default);

        Task<CustomResult> GetDeliveryByCNHNumberAsync(
           string cnhNumber,
           CancellationToken cancellationToken = default);
    }
}