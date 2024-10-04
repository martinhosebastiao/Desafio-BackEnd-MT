using MotoBusiness.Internal.Application.Motorbikes;

namespace MotoBusiness.Internal.Domain.Core.Tests.Motorbikes;

public class MotorbikeTest
{
    [Theory(DisplayName ="Verifica se o ano de fabrico é 2024")]
    [InlineData(2023, false)]
    [InlineData(2024, true)]
    [InlineData(2015, false)]
    [InlineData(2022, false)]
    public void ChecksIfTheYearOfManufactureIs2024ShouldReturnTrue(
        short year, bool expected)
    {
        //Arrange
        var request = new MotorbikeRegisterRequest("Honda", "ABC1234", year);

        //Action
        var motorbike = request.Convert();
        var response = motorbike.IsManufactureYears2024();

        //Assert
        Assert.Equal(expected, response);
    }
}
