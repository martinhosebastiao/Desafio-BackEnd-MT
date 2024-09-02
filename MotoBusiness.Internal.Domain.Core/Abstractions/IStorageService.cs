using Microsoft.AspNetCore.Http;
using MotoBusiness.Internal.Domain.Core.Results;

namespace MotoBusiness.Internal.Domain.Core.Abstractions
{
    public interface IStorageService
	{
        Task<CustomResult> UploadFileAsync(IFormFile file);
    }
}

