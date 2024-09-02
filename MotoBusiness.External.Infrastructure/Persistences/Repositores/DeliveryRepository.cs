using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MotoBusiness.External.Infrastructure.Persistences.Contexts;
using MotoBusiness.External.Infrastructure.Persistences.Repositores.Abstractions;
using MotoBusiness.Internal.Domain.Core.Entities.Deliverers;
using MotoBusiness.Internal.Domain.Core.Results;

namespace MotoBusiness.External.Infrastructure.Persistences.Repositores
{
    public class DeliveryRepository : BaseRepository<Delivery>,
        IDeliveryRepository
    {
        public DeliveryRepository(
            ILogger<Delivery> logger,
            MainContext mainContext) : base(logger, mainContext)
        {
        }

        public async Task<CustomResult> GetDeliveryByCNHNumberAsync(
            string cnhNumber, CancellationToken cancellationToken = default)
        {
            try
            {
                var delivery = await _context.Deliveries.FirstOrDefaultAsync(
                    x => x.CNH.Number == cnhNumber, cancellationToken);

                if (delivery is null)
                {
                    _logger.LogInformation(
                        DeliveryErrors.NotFound.Description);

                    return CustomResult.NotFound(
                        DeliveryErrors.NotFound);
                }

                return CustomResult.Ok(Result.Success(delivery));
            }
            catch (Exception ex)
            {
                var message = ex.Message ?? ex.InnerException?.Message;

                _logger.LogError(message);

                return CustomResult.Exception(
                    new Error("GetDeliveryByCNPJAsync", message));
            }
        }

        public async Task<CustomResult> GetDeliveryByCNPJAsync(
            string cnpj, CancellationToken cancellationToken = default)
        {
            try
            {
                var delivery = await _context.Deliveries.FirstOrDefaultAsync(
                    x => x.CNPJ == cnpj, cancellationToken);

                if (delivery is null)
                {
                    _logger.LogInformation(
                        DeliveryErrors.NotFound.Description);

                    return CustomResult.NotFound(
                        DeliveryErrors.NotFound);
                }

                return CustomResult.Ok(Result.Success(delivery));
            }
            catch (Exception ex)
            {
                var message = ex.Message ?? ex.InnerException?.Message;

                _logger.LogError(message);

                return CustomResult.Exception(
                    new Error("GetDeliveryByCNPJAsync", message));
            }
        }
    }
}

