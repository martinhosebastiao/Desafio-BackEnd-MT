using System;
using MotoBusiness.Internal.Domain.Core.Abstractions;
using MotoBusiness.Internal.Domain.Core.Results;

namespace MotoBusiness.Internal.Domain.Core.Entities.Motorbikes
{
	public interface IMotorbikeRepository: IBaseRepository<Motorbike>
	{
		Task<CustomResult> GetByPlateAsync(
			string? plate, CancellationToken cancellationToken);

		Task<CustomResult> CheckIfThePlateAlreadyExistsAsync(
            string plate, CancellationToken cancellationToken);

    }
}
