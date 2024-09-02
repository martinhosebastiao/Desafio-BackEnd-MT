using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MotoBusiness.External.Infrastructure.Persistences.Contexts;
using MotoBusiness.External.Infrastructure.Persistences.Repositores.Abstractions;
using MotoBusiness.Internal.Domain.Core.Entities.Motorbikes;
using MotoBusiness.Internal.Domain.Core.Results;

namespace MotoBusiness.External.Infrastructure.Persistences.Repositores
{
    public class MotorbikeRepository : BaseRepository<Motorbike>,
        IMotorbikeRepository
    {
        public MotorbikeRepository(
            ILogger<Motorbike> logger,
            MainContext mainContext) : base(logger, mainContext)
        {
        }

        public async Task<CustomResult> CheckIfThePlateAlreadyExistsAsync(
            string plate, CancellationToken cancellationToken)
        {
            try
            {
                var motorbike = await _context.Motorbikes.FirstOrDefaultAsync(
                    x => x.Plate == plate, cancellationToken);

                if (motorbike is null)
                {
                    _logger.LogInformation(
                        MotorbikeErrors.NotFound.Description);

                    return CustomResult.NotFound(
                        MotorbikeErrors.NotFound);
                }

                return CustomResult.Ok(Result.Success());
            }
            catch (Exception ex)
            {
                var message = ex.Message ?? ex.InnerException?.Message;

                _logger.LogError(message);

                return CustomResult.Exception(
                    new Error("CheckIfThePlateAlreadyExists", message));
            }
        }

        public async Task<CustomResult> GetByPlateAsync(
            string? plate, CancellationToken cancellationToken)
        {
            try
            {
                var motorbike = await _context.Motorbikes.FirstOrDefaultAsync(
                    x=> x.Plate == plate,cancellationToken );

                if (motorbike is null)
                {
                    _logger.LogInformation(
                        MotorbikeErrors.NotFound.Description);

                    return CustomResult.NotFound(
                        MotorbikeErrors.NotFound);
                }

              return  CustomResult.Ok(Result.Success(motorbike));
            }
            catch (Exception ex)
            {
                var message = ex.Message ?? ex.InnerException?.Message;

                _logger.LogError(message);

                return CustomResult.Exception(
                    new Error("GetByPlateAsync", message));
            }
        }
    }
}

