using Microsoft.AspNetCore.Http;
using MotoBusiness.Internal.Domain.Core.Results;

namespace MotoBusiness.Internal.Application.Deliverers
{
    public interface IDeliveryApp
	{
        Task<CustomResult> GetDeliveryAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<CustomResult> RegisterAsync(
            DeliveryRegisterRequest request,
            CancellationToken cancellationToken = default);

        Task<CustomResult> UpdateCNHAsync(
            int id,
            IFormFile cnhImage,
            CancellationToken cancellationToken = default);
    }
}

