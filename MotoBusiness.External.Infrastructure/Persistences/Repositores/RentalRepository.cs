using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MotoBusiness.External.Infrastructure.Persistences.Contexts;
using MotoBusiness.External.Infrastructure.Persistences.Repositores.Abstractions;
using MotoBusiness.Internal.Domain.Core.Entities.Motorbikes;
using MotoBusiness.Internal.Domain.Core.Entities.Rentals;
using MotoBusiness.Internal.Domain.Core.Results;

namespace MotoBusiness.External.Infrastructure.Persistences.Repositores
{
    public class RentalRepository : BaseRepository<Rental>, IRentalRepository
    {
        public RentalRepository(
            ILogger<Rental> logger,
            MainContext queroGasContext) : base(logger, queroGasContext)
        {
        }

        public async Task<CustomResult> CheckIfTheMotorbikeAlreadyExistsAsync(
            int motorbikeId, CancellationToken cancellationToken)
        {
            try
            {
                var motorbike = await _context.Rentals.FirstOrDefaultAsync(
                 x => x.MotorbikeId == motorbikeId, cancellationToken);

                if (motorbike is null)
                {
                    _logger.LogInformation(
                        MotorbikeErrors.NotFound.Description);

                    return CustomResult.NotFound(MotorbikeErrors.NotFound);
                }

                return CustomResult.Ok(Result.Success());

            }
            catch (Exception ex)
            {
                var message = ex.Message ?? ex.InnerException?.Message;

                _logger.LogError(message);

                return CustomResult.Exception(
                    new Error("CheckIfTheMotorbikeAlreadyExistsAsync", message));
            }
        }
    }
}

