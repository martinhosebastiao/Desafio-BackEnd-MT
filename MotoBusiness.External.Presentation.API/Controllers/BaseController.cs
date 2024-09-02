using System.Net;
using Microsoft.AspNetCore.Mvc;
using MotoBusiness.Internal.Domain.Core.Results;


namespace MotoBusiness.External.Presentation.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public abstract class BaseController<TEntity> : ControllerBase where
        TEntity : class
    {
        internal ILogger<TEntity> _logger;

        public BaseController(ILogger<TEntity> logger)
        {
            _logger = logger;
        }

        #region - Padronização dos ResponseCode da API -
        protected IActionResult ResponseApi(CustomResult custom)
            => ResponseResult(custom);
        #endregion

        #region - Result API -
        private static JsonResult ResponseBase(
            HttpStatusCode statusCode, object? result)
        {
            var jsonResult = new JsonResult(result) { StatusCode = (int)statusCode };

            return jsonResult;
        }

        private static JsonResult ResponseResult(CustomResult custom)
            => ResponseBase(custom.StatusCode, custom.Data);
        #endregion
    }
}

