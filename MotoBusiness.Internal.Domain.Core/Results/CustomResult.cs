using System.Net;
using System.Text.Json.Serialization;

namespace MotoBusiness.Internal.Domain.Core.Results
{
    public record CustomResult
    {
        public CustomResult(HttpStatusCode statusCode, Result result)
        {
            StatusCode = statusCode;
            Result = result;
            Data = result.IsFailure ? Result.Error : Result.Data;
        }

        #region - Properties -
        //[JsonIgnore]
        public Result Result { get; private set; }

        [JsonPropertyOrder(2)]
        public object? Data { get; private set; }

        [JsonPropertyOrder(1)]
        public HttpStatusCode StatusCode { get; private set; }
        #endregion

        #region - Result Errors -
        public static CustomResult UnAuthorized(Result result)
       => new(HttpStatusCode.Unauthorized, result);

        public static CustomResult NotFound(Result result)
            => new(HttpStatusCode.NoContent, result);

        public static CustomResult NotFound(Error error)
           => new(HttpStatusCode.NoContent, Result.Failure(error));

        public static CustomResult Exception(Result result)
            => new(HttpStatusCode.BadRequest, result);

        public static CustomResult PreconditionFailed(Result result)
            => new(HttpStatusCode.PreconditionFailed, result);

        public static CustomResult PreconditionFailed(Error error)
          => new(HttpStatusCode.PreconditionFailed, Result.Failure(error));

        public static CustomResult ExpectationFailed(Result result)
            => new(HttpStatusCode.ExpectationFailed, result);

        public static CustomResult ExpectationFailed(Error error)
           => new(HttpStatusCode.ExpectationFailed, Result.Failure(error));
        #endregion

        #region - Result Success -
        public static CustomResult Ok(Result result) => new(HttpStatusCode.OK, result);
        public static CustomResult Created(Result result) => new(HttpStatusCode.Created, result);
        #endregion

        #region - Response Analyze -
        public static CustomResult GetResponse<T>(T value)
        {
            try
            {
                return (value is not null) ?
                    Ok(Result.Success(value)) :
                    NotFound(Result.Failure<T>(GenericErrors.ExpectationFailed));
            }
            catch (Exception ex)
            {
                var _message = ex.Message ?? ex.InnerException?.Message;
                return Exception(Result.Failure(value, GenericErrors.ExceptionWithMessage(_message)));
            }
        }

        public static CustomResult GetResponse<T>(T value, string message)
        {
            try
            {
                return (value is not null) ?
                    Ok(Result.Success(value)) :
                    NotFound(Result.Failure(value, GenericErrors.ExpectationFailedWithMessage(message)));
            }
            catch (Exception ex)
            {
                var _message = ex.Message ?? ex.InnerException?.Message;
                return Exception(Result.Failure(value, GenericErrors.ExceptionWithMessage(_message)));
            }
        }

        public static CustomResult GetResponse<T>(T value, dynamic layerCode, string? message = default)
        {
            try
            {
                return (value is not null) ?
                    Ok(Result.Success(value)) :
                    NotFound(Result.Failure(value, GenericErrors.ExpectationFailedWithMessage(layerCode, message)));
            }
            catch (Exception ex)
            {
                var _message = ex.Message ?? ex.InnerException?.Message;
                return Exception(Result.Failure(value, GenericErrors.ExceptionWithMessage(layerCode, _message)));
            }
        }

        public static CustomResult GetResponse<T>(ICollection<T>? values)
        {
            try
            {
                return (values?.Count ?? 0) > 0 ?
                   Ok(Result.Success(values)) :
                   NotFound(Result.Failure(values, GenericErrors.ExpectationFailed));
            }
            catch (Exception ex)
            {
                var _message = ex.Message ?? ex.InnerException?.Message;
                return Exception(Result.Failure(values, GenericErrors.ExceptionWithMessage(_message)));
            }
        }

        public static CustomResult GetResponse<T>(ICollection<T>? values, string message)
        {
            try
            {
                return (values?.Count ?? 0) > 0 ?
                    Ok(Result.Success(values)) :
                    NotFound(Result.Failure(values, GenericErrors.ExpectationFailedWithMessage(message)));
            }
            catch (Exception ex)
            {
                var _message = ex.Message ?? ex.InnerException?.Message;
                return Exception(Result.Failure<T>(GenericErrors.ExceptionWithMessage(_message)));
            }
        }

        public static CustomResult GetResponse<T>(ICollection<T>? values, dynamic layerCode, string? message = default)
        {
            try
            {
                return (values?.Count ?? 0) > 0 ?
                    Ok(Result.Success(values)) :
                    NotFound(Result.Failure(values, GenericErrors.ExpectationFailedWithMessage(layerCode, message)));
            }
            catch (Exception ex)
            {
                var _message = ex.Message ?? ex.InnerException?.Message;
                return Exception(Result.Failure(values, GenericErrors.ExceptionWithMessage(layerCode, _message)));
            }
        }

        public static CustomResult GetResponse<T>(List<T>? values)
        {
            try
            {
                if ((values?.Count ?? 0) > 0)
                {
                    return Ok(Result.Success(values));
                }
                else
                {
                    return NotFound(Result.Failure(values, GenericErrors.ExpectationFailed));
                }
            }
            catch (Exception ex)
            {
                var _message = ex.Message ?? ex.InnerException?.Message;
                return Exception(GenericErrors.ExceptionWithMessage(_message));
            }
        }

        public static CustomResult GetResponse<T>(List<T>? values, string message)
        {
            try
            {
                return (values?.Count ?? 0) > 0 ?
                    Ok(Result.Success(values)) :
                    NotFound(GenericErrors.ExpectationFailedWithMessage(message));
            }
            catch (Exception ex)
            {
                var _message = ex.Message ?? ex.InnerException?.Message;
                return Exception(Result.Failure(values, GenericErrors.ExceptionWithMessage(_message)));
            }
        }

        public static CustomResult GetResponse<T>(List<T>? values, dynamic layerCode, string? message = default)
        {
            try
            {
                return (values?.Count ?? 0) > 0 ?
                    Ok(Result.Success(values)) :
                    NotFound(Result.Failure(values, GenericErrors.ExpectationFailedWithMessage(layerCode, message)));
            }
            catch (Exception ex)
            {
                var _message = ex.Message ?? ex.InnerException?.Message;
                return Exception(Result.Failure(values, GenericErrors.ExceptionWithMessage(layerCode, _message)));
            }
        }

        #endregion
    }
}