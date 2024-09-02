using MotoBusiness.Internal.Domain.Core.Results;

namespace MotoBusiness.Internal.Application.Rentals
{
    public interface IRentalApp
	{
        Task<CustomResult> StartAsync(
            RegisterRequest request,
            CancellationToken cancellationToken = default);

        Task<CustomResult> FinishAsync(
            FinishRequest request,
            CancellationToken cancellationToken = default);
    }
}

