using System;
using Microsoft.Extensions.Logging;
using MotoBusiness.External.Infrastructure.Persistences.Contexts;
using MotoBusiness.External.Infrastructure.Persistences.Repositores.Abstractions;
using MotoBusiness.Internal.Domain.Core.Entities.Deliverers;
using MotoBusiness.Internal.Domain.Core.Entities.Rentals;

namespace MotoBusiness.External.Infrastructure.Persistences.Repositores
{
    public class DeliveryRepository : BaseRepository<Delivery>, IDeliveryRepository
    {
        public DeliveryRepository(
            ILogger<Delivery> logger,
            MainContext mainContext) : base(logger, mainContext)
        {
        }
    }
}

