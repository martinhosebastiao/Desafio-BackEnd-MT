using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MotoBusiness.Internal.Domain.Core.Abstractions;
using MotoBusiness.Internal.Domain.Core.Entities.Deliverers;
using MotoBusiness.Internal.Domain.Core.Entities.Motorbikes;
using MotoBusiness.Internal.Domain.Core.Results;

namespace MotoBusiness.Internal.Application.Deliverers
{
    public class DeliveryApp : IDeliveryApp
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeliveryApp> _logger;
        private readonly IStorageService _storageService;

        public DeliveryApp(
            IUnitOfWork unitOfWork,
            ILogger<DeliveryApp> logger,
            IStorageService storageService)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _storageService = storageService;
        }

        public async Task<CustomResult> GetDeliveryAsync(
            int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var delivery = await _unitOfWork.DeliveryRepository
                    .GetByIdAsync(id, cancellationToken);

                if (delivery.StatusCode == HttpStatusCode.OK)
                {
                    _logger.LogInformation(
                        MotorbikeErrors.ExistPlate.Description);

                    return CustomResult.Ok(Result.Success(delivery));
                }
                else if (delivery.StatusCode == HttpStatusCode.NoContent)
                {
                    return CustomResult.NotFound(
                        DeliveryErrors.DeliveryNotFound);
                }

                return delivery;

            }
            catch (Exception ex)
            {
                var message = ex?.Message ?? ex?.InnerException?.Message;

                _logger.LogError(message);

                return CustomResult.Exception(
                    new Error("GetDeliveryAsync", message));
            }
        }

        public async Task<CustomResult> RegisterAsync(
            RegisterRequest request,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var checkExistCNPJ = await _unitOfWork.DeliveryRepository
                    .GetDeliveryByCNPJAsync(request.CNPJ, cancellationToken);

                if (checkExistCNPJ.StatusCode == HttpStatusCode.OK)
                {
                    _logger.LogInformation(
                        DeliveryErrors.CNPJ_AlreadyExists.Description);

                    return CustomResult.Ok(DeliveryErrors.CNPJ_AlreadyExists);
                }

                var checkExistCNHNumber = await _unitOfWork.DeliveryRepository
                    .GetDeliveryByCNPJAsync(
                        request.CNHNumber, cancellationToken);

                if (checkExistCNHNumber.StatusCode == HttpStatusCode.OK)
                {
                    _logger.LogInformation(
                        DeliveryErrors.CNH_AlreadyExists.Description);

                    return CustomResult.Ok(DeliveryErrors.CNPJ_AlreadyExists);
                }

                var delivery = request.Convert();

                var result = await _unitOfWork.DeliveryRepository
                    .CreateAsync(delivery, cancellationToken);

                await _unitOfWork.CommitAsync(cancellationToken);

                return result;

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync(cancellationToken);

                var message = ex?.Message ?? ex?.InnerException?.Message;

                _logger.LogError(message);

                return CustomResult.Exception(
                    new Error("RegisterAsync", message));
            }
        }

        public async Task<CustomResult> UpdateCNHAsync(
            int id, IFormFile cnhImage,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var _delivery = await _unitOfWork.DeliveryRepository
                    .GetByIdAsync(id, cancellationToken);

                if (_delivery.StatusCode == HttpStatusCode.NoContent)
                {
                    _logger.LogInformation(
                        DeliveryErrors.DeliveryNotFound.Description);

                    return CustomResult.NotFound(
                        DeliveryErrors.DeliveryNotFound);
                }
                else if (_delivery.Data is Delivery delivery)
                {
                    var uploadFile = await _storageService
                        .UploadFileAsync(cnhImage);

                    var path = (uploadFile.Data is string _path)
                        ? _path : string.Empty;

                    var result = await _unitOfWork.DeliveryRepository
                        .UpdateAsync(delivery, cancellationToken);

                }

                return _delivery;

            }
            catch (Exception ex)
            {
                var message = ex?.Message ?? ex?.InnerException?.Message;

                _logger.LogError(message);

                return CustomResult.Exception(
                    new Error("UpdateCNHAsync", message));
            }
        }
    }
}

