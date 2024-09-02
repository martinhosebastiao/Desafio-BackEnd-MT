using MotoBusiness.Internal.Domain.Core.Entities.Deliverers;
using MotoBusiness.Internal.Domain.Core.Entities.Motorbikes;
using MotoBusiness.Internal.Domain.Core.Entities.Rentals;
using MotoBusiness.Internal.Domain.Core.Results;

namespace MotoBusiness.Internal.Domain.Core.Abstractions
{
    public interface IUnitOfWork
	{
		IDeliveryRepository DeliveryRepository { get; }
        IMotorbikeRepository MotorbikeRepository { get; }
        IRentalRepository RentalRepository { get; }

        Task<Result> CommitAsync(CancellationToken cancellationToken = default);
		Task RollbackAsync(CancellationToken cancellationToken = default);
	}
}

