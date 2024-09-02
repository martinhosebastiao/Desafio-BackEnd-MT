using MotoBusiness.Internal.Domain.Core.Entities.Motorbikes;
using MotoBusiness.Internal.Domain.Core.Results;

namespace MotoBusiness.Internal.Domain.Core.Entities.Motorbikes
{
    public interface IMotorbikePublisher
	{
		Task<CustomResult> RegisterAsync(
			Motorbike motorbike, CancellationToken cancellationToken);
	}
}