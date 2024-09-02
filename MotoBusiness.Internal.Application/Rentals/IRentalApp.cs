using MotoBusiness.Internal.Domain.Core.Results;

namespace MotoBusiness.Internal.Application.Rentals
{
    public interface IRentalApp
	{
        Task<CustomResult> StartAsync(
            RentalRegisterRequest request,
            CancellationToken cancellationToken = default);

        Task<CustomResult> FinishAsync(
            RentalFinishRequest request,
            CancellationToken cancellationToken = default);
    }
}

