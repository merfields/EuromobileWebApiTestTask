using EuromobileTestTaks.Domain.Models;

namespace EuromobileTestTask.WebApi.Repositories
{
    public class CoordinatesRepository : ICoordinatesRepository
    {
        public SumDistance CalculateSumDistance(Coordinate[] coordinates)
        {
            throw new NotImplementedException();
        }

        public Coordinate GenerateCoordinates()
        {
            double generatedLatitude = GetRandomDoubleNumberInRange(-90, 90);
            double generatedLongitude = GetRandomDoubleNumberInRange(-180, 180);

            return new Coordinate
            {
                Latitude = generatedLatitude,
                Longitude = generatedLongitude
            };
        }

        public double GetRandomDoubleNumberInRange(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}
