using System.Net;
using Microsoft.Extensions.Logging;
using MotoBusiness.Internal.Domain.Core.Abstractions;
using MotoBusiness.Internal.Domain.Core.Entities.Motorbikes;
using MotoBusiness.Internal.Domain.Core.Results;

namespace MotoBusiness.Internal.Application.Motorbikes
{
    public class MotorbikeApp : IMotorbikeApp
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MotorbikeApp> _logger;
        private readonly IMotorbikePublisher motorbikePublisher;

        public MotorbikeApp(
            IUnitOfWork unitOfWork,
            ILogger<MotorbikeApp> logger,
            IMotorbikePublisher motorbikePublisher)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            this.motorbikePublisher = motorbikePublisher;
        }

        public async Task<CustomResult> GetMotorbikesAsync(
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _unitOfWork.MotorbikeRepository
                   .GetsAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                var message = ex?.Message ?? ex?.InnerException?.Message;

                _logger.LogError(message);

                return CustomResult.Exception(
                    new Error("GetMotorbikesAsync", message));
            }
        }

        public async Task<CustomResult> GetMotorbikeAsync(
            string? plate = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _unitOfWork.MotorbikeRepository
                    .GetByPlateAsync(plate, cancellationToken);

                return result;
                ;
            }
            catch (Exception ex)
            {
                var message = ex?.Message ?? ex?.InnerException?.Message;

                _logger.LogError(message);

                return CustomResult.Exception(
                    new Error("GetMotorbikeAsync", message));
            }
        }

        public async Task<CustomResult> RegisterAsync(
            MotorbikeRegisterRequest request,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var checkExist = await CheckIfThePlateAlreadyExistsAsync(
                     request.Plate, cancellationToken);

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

        public async Task<CustomResult> ChangePlateAsync(
            int id,
            string plate,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var _motorbike = await _unitOfWork.MotorbikeRepository
                    .GetByIdAsync(id, cancellationToken);

                if (_motorbike.StatusCode == HttpStatusCode.OK)
                {
                    var checkExist = await CheckIfThePlateAlreadyExistsAsync(
                        plate, cancellationToken);

                    if (checkExist)
                    {
                        _logger.LogInformation(
                            MotorbikeErrors.ExistPlate.Description);

                        return CustomResult.Ok(MotorbikeErrors.ExistPlate);
                    }

                    if (_motorbike.Data is Motorbike motorbike)
                    {
                        motorbike.ChangePlate(plate);

                        if (motorbike.IsValid)
                        {
                            var result = await _unitOfWork.MotorbikeRepository
                                .UpdateAsync(motorbike, cancellationToken);

                            await _unitOfWork.CommitAsync(cancellationToken);

                            return CustomResult.Ok(Result.Success(result));
                        }
                        else
                        {
                            foreach (var item in motorbike.Errors)
                            {
                                var message = string.Concat(
                                    "ChangePlateAsync", item);

                                _logger.LogInformation(message);
                            }
                        }
                    }
                }

                _logger.LogError(MotorbikeErrors.NotFound.Description);

                return CustomResult.NotFound(MotorbikeErrors.NotFound);

            }
            catch (Exception ex)
            {
                var message = ex?.Message ?? ex?.InnerException?.Message;

                _logger.LogError(message);

                return CustomResult.Exception(
                    new Error("ChangePlateAsync", message));
            }
        }

        public async Task<CustomResult> RemoveAsync(
            int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var checkExistRental = await _unitOfWork.RentalRepository
                    .CheckIfTheMotorbikeAlreadyExistsAsync(
                        id, cancellationToken);

                if (checkExistRental.StatusCode == HttpStatusCode.OK)
                {
                    _logger.LogError(MotorbikeErrors.ExistRetail.Description);

                    return CustomResult.Ok(MotorbikeErrors.ExistRetail);
                }
                else if (checkExistRental.StatusCode == HttpStatusCode.OK)
                {
                    var motorbikeResult = await _unitOfWork.RentalRepository
                            .GetByIdAsync(id, cancellationToken);

                    if (motorbikeResult.Data is Motorbike motorbike)
                    {
                        var result = await _unitOfWork.MotorbikeRepository
                            .DeleteAsync(motorbike, cancellationToken);

                        await _unitOfWork.CommitAsync(cancellationToken);

                        return result;
                    }
                    else
                    {
                        _logger.LogError(MotorbikeErrors.NotFound.Description);

                        return CustomResult.NotFound(MotorbikeErrors.NotFound);
                    }
                }
                return checkExistRental;
            }
            catch (Exception ex)
            {
                var message = ex?.Message ?? ex?.InnerException?.Message;

                _logger.LogError(message);

                return CustomResult.Exception(
                    new Error("RemoveAsync", message));
            }
        }

        private async Task<bool> CheckIfThePlateAlreadyExistsAsync(
            string plate, CancellationToken cancellationToken)
        {
            try
            {
                var checkExist = await _unitOfWork.MotorbikeRepository
                 .CheckIfThePlateAlreadyExistsAsync(
                     plate, cancellationToken);

                return checkExist.StatusCode == HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex?.Message ?? ex?.InnerException?.Message);

                return false;
            }
        }
    }
}