using MotoBusiness.Internal.Domain.Core.Entities.Motorbikes;

namespace MotoBusiness.Internal.Application.Motorbikes
{
    public record MotorbikeRegisterRequest(string Model, string Plate, short Year)
    {
        public Motorbike Convert()
        {
            var motorbike = new Motorbike(Model, Plate, Year);

            return motorbike;
        }
    };
}