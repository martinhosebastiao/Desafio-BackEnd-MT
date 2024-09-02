using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MotoBusiness.Internal.Domain.Core.Abstractions;
using MotoBusiness.Internal.Domain.Core.Results;

namespace MotoBusiness.External.Infrastructure.Storages.Local
{
    public class StorageService : IStorageService
    {
        private readonly string _storagePath;
        private readonly IConfiguration _configuration;
        private readonly ILogger<StorageService> _logger;
        public StorageService(
            IConfiguration configuration,
            ILogger<StorageService> logger
            )
        {
            _logger = logger;
            _configuration = configuration;

            var storagePath = _configuration.GetSection("Storage")
                .GetValue<string>("Path");

            var currentPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "LocalStorage");

            storagePath ??= currentPath;

            _storagePath = storagePath;

            Directory.CreateDirectory(_storagePath);
        }

        public async Task<CustomResult> UploadFileAsync(IFormFile file)
        {
            try
            {
                var filePath = Path.Combine(_storagePath, file.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                return CustomResult.Created(Result.Success(filePath));
            }
            catch (Exception ex)
            {
                var message = ex?.Message ?? ex?.InnerException?.Message;

                _logger.LogError(message);

                return CustomResult.Exception(
                    new Error("UploadFileAsync", message));
            }
        }
    }
}

