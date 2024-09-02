using MotoBusiness.Internal.Domain.Core.Abstractions;
using MotoBusiness.Internal.Domain.Core.Results;

namespace MotoBusiness.Internal.Domain.Core.Entities.Rentals
{
    public interface IRentalRepository: IBaseRepository<Rental>
	{
        Task<CustomResult> CheckIfTheMotorbikeAlreadyExistsAsync(
            int motorbikeId, CancellationToken cancellationToken);
    }
}