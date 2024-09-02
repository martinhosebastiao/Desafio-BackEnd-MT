using Microsoft.AspNetCore.Mvc;
using MotoBusiness.Internal.Application.Deliverers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MotoBusiness.External.Presentation.API.Controllers
{
    public class DeliveryController : BaseController<DeliveryController>
    {
        private readonly IDeliveryApp _deliveryApp;
        public DeliveryController(
            IDeliveryApp deliveryApp, ILogger<DeliveryController> logger) :
            base(logger)
        {
            _deliveryApp = deliveryApp;
        }

        // GET api/values/5
        /// <summary>
        /// Filtrar entregador pelo identificador
        /// </summary>
        /// <param name="deliveryId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(
            int deliveryId, CancellationToken cancellationToken)
        {
            var result = await _deliveryApp.GetDeliveryAsync(
                deliveryId, cancellationToken);

            return ResponseApi(result);
        }

        // POST api/values
        /// <summary>
        /// Cadastrar um novo entregador
        /// </summary>
        /// <param name="request"></param>
        [HttpPost("register")]
        public async Task<IActionResult> Post(
            [FromBody] DeliveryRegisterRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _deliveryApp.RegisterAsync(
                request, cancellationToken);

            return ResponseApi(result);
        }

        // PUT api/values/5
        /// <summary>
        /// Efectuar upload da imagem da CNH
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cnhImage"></param>
        [HttpPut("uploadcnh/{id}")]
        public async Task<IActionResult> Put(
            int id,
            [FromForm] IFormFile cnhImage,
            CancellationToken cancellationToken)
        {
            var result = await _deliveryApp.UpdateCNHAsync(
               id, cnhImage, cancellationToken);

            return ResponseApi(result);
        }
    }
}

