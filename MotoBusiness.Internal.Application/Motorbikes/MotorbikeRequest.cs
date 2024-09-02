using MotoBusiness.Internal.Domain.Core.Entities.Motorbikes;

namespace MotoBusiness.Internal.Application.Motorbikes
{
    public record RegisterRequest(string model, string plate, short year)
    {
        public Motorbike Convert()
        {
            var motorbike = new Motorbike(model, plate, year);

            return motorbike;
        }
    };
}