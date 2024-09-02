using System.Threading;
using Microsoft.AspNetCore.Mvc;
using MotoBusiness.Internal.Application.Rentals;
using MotoBusiness.Internal.Domain.Core.Entities.Deliverers;


namespace MotoBusiness.External.Presentation.API.Controllers
{
    public class RentalController : BaseController<RentalController>
    {
        private readonly IRentalApp _rentalApp;
        public RentalController(
            IRentalApp rentalApp,
            ILogger<RentalController> logger) : base(logger)
        {
            _rentalApp = rentalApp;
        }

        // POST api/values
        /// <summary>
        /// Iniciar uma alocação
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("start")]
        public async Task<IActionResult> Post(
            [FromBody] RegisterRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _rentalApp.StartAsync(
                request, cancellationToken);

            return ResponseApi(result);
        }

        // PUT api/values/5
        /// <summary>
        /// Concluir uma alocação e calcular os valores
        /// </summary>
        /// <param name="id">Identificador da locação</param>
        /// <param name="returnDate">Data de devolução</param>
        /// <returns>Retornar os detalhes e valor da alocação</returns>
        [HttpPut("finish/{id}")]
        public async Task<IActionResult> Put(
            int id, [FromBody] DateTime returnDate,
            CancellationToken cancellationToken)
        {
            var request = new FinishRequest(id, returnDate);

            var result = await _rentalApp.FinishAsync(
               request, cancellationToken);

            return ResponseApi(result);
        }
    }
}

