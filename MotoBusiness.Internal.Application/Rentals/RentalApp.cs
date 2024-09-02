using System.Net;
using Microsoft.Extensions.Logging;
using MotoBusiness.Internal.Domain.Core.Abstractions;
using MotoBusiness.Internal.Domain.Core.Entities.Deliverers;
using MotoBusiness.Internal.Domain.Core.Entities.Motorbikes;
using MotoBusiness.Internal.Domain.Core.Entities.Rentals;
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
                var motorbike = await _unitOfWork.MotorbikeRepository
                    .GetByIdAsync(request.MotorbikeId, cancellationToken);

                if (motorbike.Data is not Motorbike _motorbike)
                {
                    _logger.LogInformation(
                       MotorbikeErrors.NotFound.Description);

                    return motorbike;
                }

                var delivery = await _unitOfWork.DeliveryRepository
                    .GetByIdAsync(request.DeliveryId, cancellationToken);

                if (delivery.Data is not Delivery _delivery)
                {
                    _logger.LogInformation(
                       DeliveryErrors.NotFound.Description);

                    return delivery;
                }

                if (!_delivery.CheckSuitableForAllocation())
                {
                    _logger.LogInformation(
                       RentalErrors.Delivery_Rental_Unauthotized.Description);

                    return CustomResult.Ok(
                        RentalErrors.Delivery_Rental_Unauthotized);
                }

                var rental = request.Convert();

                var result = await _unitOfWork.RentalRepository.CreateAsync(
                      rental, cancellationToken);

                await _unitOfWork.CommitAsync();

                return result;

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync(cancellationToken);

                var message = ex?.Message ?? ex?.InnerException?.Message;

                _logger.LogError(message);

                return CustomResult.Exception(
                    new Error("StartAsync", message));
            }
        }

        public async Task<CustomResult> FinishAsync(
            FinishRequest request,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var rental = await _unitOfWork.RentalRepository
                    .GetByIdAsync(request.RentaiId, cancellationToken);

                if (rental.Data is not Rental _rental)
                {
                    _logger.LogInformation(
                        RentalErrors.Notfound.Description);

                    return CustomResult.NotFound(RentalErrors.Notfound);
                }

                _rental.FinishRental(request.ReturnDate);

                var result = await _unitOfWork.RentalRepository.UpdateAsync(
                     _rental, cancellationToken);

                await _unitOfWork.CommitAsync();

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

