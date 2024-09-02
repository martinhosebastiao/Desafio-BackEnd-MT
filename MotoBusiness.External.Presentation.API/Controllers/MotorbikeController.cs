using Microsoft.AspNetCore.Mvc;
using MotoBusiness.Internal.Application.Motorbikes;


namespace MotoBusiness.External.Presentation.API.Controllers
{
    public class MotorbikeController : BaseController<MotorbikeController>
    {
        private readonly IMotorbikeApp _motorbikeApp;

        public MotorbikeController(ILogger<MotorbikeController> logger,
            IMotorbikeApp motorbikeApp) :
            base(logger)
        {
            _motorbikeApp = motorbikeApp;
        }

        // GET: api/values
        /// <summary>
        /// Obter todas as motos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(
            CancellationToken cancellationToken)
        {
            var result = await _motorbikeApp.GetMotorbikesAsync(
                cancellationToken);

            return ResponseApi(result);
        }

        // GET api/values/5
        /// <summary>
        /// Filtrar moto pela placa
        /// </summary>
        /// <param name="plate"></param>
        /// <returns></returns>
        [HttpGet("{plate}")]
        public async Task<IActionResult> Get(
            string plate, CancellationToken cancellationToken)
        {
            var result = await _motorbikeApp.GetMotorbikeAsync(
                plate, cancellationToken);

            return ResponseApi(result);
        }

        // POST api/values
        [HttpPost("register")]
        public async Task<IActionResult> Post(
            [FromBody] RegisterRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _motorbikeApp.RegisterAsync(
                request, cancellationToken);

            return ResponseApi(result);
        }

        // PUT api/values/5
        /// <summary>
        /// Alterar a placa da moto
        /// </summary>
        /// <param name="id">Identificador da moto</param>
        /// <param name="value"></param>
        [HttpPut("changeplate/{id}")]
        public async Task<IActionResult> Put(
            int id, [FromBody] string plate,
            CancellationToken cancellationToken)
        {
            var result = await _motorbikeApp.ChangePlateAsync(
                id, plate, cancellationToken);

            return ResponseApi(result);
        }

        // DELETE api/values/5
        /// <summary>
        /// Remover uma moto desde que não tenha historico de locação
        /// </summary>
        /// <param name="id">Identificador da moto</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(
            int id, CancellationToken cancellationToken)
        {
            var result = await _motorbikeApp.RemoveAsync(
                id, cancellationToken);

            return ResponseApi(result);
        }
    }
}

