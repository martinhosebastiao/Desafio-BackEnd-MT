using MotoBusiness.Internal.Domain.Core.Results;

namespace MotoBusiness.Internal.Application.Motorbikes
{
    public interface IMotorbikeApp
    {
        Task<CustomResult> GetMotorbikesAsync(
            CancellationToken cancellationToken = default);

        Task<CustomResult> GetMotorbikeAsync(
            string? plate = null,
            CancellationToken cancellationToken = default);

        Task<CustomResult> RegisterAsync(
            MotorbikeRegisterRequest request,
            CancellationToken cancellationToken = default);

        Task<CustomResult> ChangePlateAsync(
            int id,
            string plate,
            CancellationToken cancellationToken = default);

        Task<CustomResult> RemoveAsync(
            int id,
            CancellationToken cancellationToken = default);
    }
}

