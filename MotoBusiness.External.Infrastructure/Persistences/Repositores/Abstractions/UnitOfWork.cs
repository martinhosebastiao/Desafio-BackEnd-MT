using MotoBusiness.External.Infrastructure.Persistences.Contexts;
using MotoBusiness.Internal.Domain.Core.Abstractions;
using MotoBusiness.Internal.Domain.Core.Entities.Deliverers;
using MotoBusiness.Internal.Domain.Core.Entities.Motorbikes;
using MotoBusiness.Internal.Domain.Core.Entities.Rentals;
using MotoBusiness.Internal.Domain.Core.Results;

namespace MotoBusiness.External.Infrastructure.Persistences.Repositores.Abstractions
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MainContext _context;

        public UnitOfWork(
            MainContext context,
            IDeliveryRepository deliveryRepository,
            IMotorbikeRepository motorbikeRepository,
            IRentalRepository rentalRepository
            )
        {
            _context = context;
            DeliveryRepository = deliveryRepository;
            MotorbikeRepository = motorbikeRepository;
            RentalRepository = rentalRepository;
        }

        public IDeliveryRepository DeliveryRepository { get; }
        public IMotorbikeRepository MotorbikeRepository { get; }
        public IRentalRepository RentalRepository { get; }

        public async Task<Result> CommitAsync(
            CancellationToken cancellationToken)
        {
            var result = await _context.SaveChangesAsync(cancellationToken);

            if (result <= 0)
            {
                var error = new Error(
                    "Operations.Commit",
                    $"Não foi possivel concluir o processo." +
                    $"\nDetalhes: {result}");

                return Result.Failure(error);
            }

            return Result.Success(result);
        }

        public async Task RollbackAsync(
            CancellationToken cancellationToken = default)
        {
            await _context.DisposeAsync();
        }

        public void Dispose()
        {
            GC.Collect();
            _context.Dispose();
        }


    }
}

