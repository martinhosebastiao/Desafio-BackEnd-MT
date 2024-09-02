using Microsoft.Extensions.Logging;
using MotoBusiness.Internal.Domain.Core.Abstractions;
using MotoBusiness.Internal.Domain.Core.Entities.Motorbikes;
using MotoBusiness.Internal.Domain.Core.Results;

namespace MotoBusiness.Internal.Application.Rentals
{
    public class RentalApp : IRentalApp
    {
        private readonly ILogger<RentalApp> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public RentalApp(ILogger<RentalApp> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<CustomResult> StartAsync(
            RegisterRequest request,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var checkExist = await CheckIfThePlateAlreadyExistsAsync(
                     request.plate, cancellationToken);

                if (checkExist)
                {
                    _logger.LogInformation(
                        MotorbikeErrors.ExistPlate.Description);

                    return CustomResult.Ok(MotorbikeErrors.ExistPlate);
                }

                var motorbike = request.Convert();

                var result = await motorbikePublisher.RegisterAsync(
                      motorbike, cancellationToken);

                return result;

            }
            catch (Exception ex)
            {
                var message = ex?.Message ?? ex?.InnerException?.Message;

                _logger.LogError(message);

                return CustomResult.Exception(
                    new Error("RegisterAsync", message));
            }
        }

        public async Task<CustomResult> FinishAsync(
            FinishRequest request,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var checkExist = await CheckIfThePlateAlreadyExistsAsync(
                     request.plate, cancellationToken);

                if (checkExist)
                {
                    _logger.LogInformation(
                        MotorbikeErrors.ExistPlate.Description);

                    return CustomResult.Ok(MotorbikeErrors.ExistPlate);
                }

                var motorbike = request.Convert();

                var result = await motorbikePublisher.RegisterAsync(
                      motorbike, cancellationToken);

                return result;

            }
            catch (Exception ex)
            {
                var message = ex?.Message ?? ex?.InnerException?.Message;

                _logger.LogError(message);

                return CustomResult.Exception(
                    new Error("RegisterAsync", message));
            }
        }
    }
}

