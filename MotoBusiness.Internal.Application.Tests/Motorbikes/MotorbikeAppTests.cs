using System.Threading;
using Microsoft.Extensions.Logging;
using Moq;
using MotoBusiness.Internal.Application.Motorbikes;
using MotoBusiness.Internal.Domain.Core.Abstractions;
using MotoBusiness.Internal.Domain.Core.Entities.Motorbikes;

namespace MotoBusiness.Internal.Application.Tests.Motorbikes;

public class MotorbikeAppTests
{
    private readonly Mock<ILogger<MotorbikeApp>> _logger;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IMotorbikePublisher> _motorbikePublisher;
    private readonly MotorbikeApp _motorbikeApp;
    private readonly CancellationToken cancellationToken = default;

    public MotorbikeAppTests()
    {
        _logger = new Mock<ILogger<MotorbikeApp>>();
        _unitOfWork = new Mock<IUnitOfWork>();
        _motorbikePublisher = new Mock<IMotorbikePublisher>();

        _motorbikeApp = new MotorbikeApp(
            _unitOfWork.Object, _logger.Object, _motorbikePublisher.Object);
    }

    [Fact]
    public async Task Register_ShoudSuccess_WhenIfThePlateIsUnique()
    {
        // Arrage

        var request = new MotorbikeRegisterRequest("Honda", "ABC1234", 2023);

        var motorbike = request.Convert();

        // Act
        var result = await _motorbikeApp.RegisterAsync(request);

        var response = (result.Data is Motorbike mb) ? mb : null;

        // Assert
        _unitOfWork.Verify(x => x.MotorbikeRepository.CreateAsync(
            motorbike, cancellationToken), Times.Once);

        _motorbikePublisher.Verify(x => x.RegisterAsync(
            motorbike, cancellationToken), Times.Once);

        Assert.Equal(motorbike.Plate, response?.Plate);
    }

    [Fact]
    public async Task Register_ShoudAlreadyExists_WhenThePlateIsDuplicate()
    {
        // Arrange
        var request = new MotorbikeRegisterRequest("Honda", "ABC1234", 2023);
        var motorbike = request.Convert();

        _unitOfWork.Setup(x=>x.MotorbikeRepository.GetByPlateAsync(
            It.IsAny<string>(),cancellationToken));

        // Action
        var result = await _motorbikeApp.RegisterAsync(request);
        var response = result.Result.IsFailure;

        // Assert
        Assert.False(response);
    }
}
