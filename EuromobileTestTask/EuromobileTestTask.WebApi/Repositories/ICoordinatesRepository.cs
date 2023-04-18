using EuromobileTestTaks.Domain.Models;

namespace EuromobileTestTask.WebApi.Repositories
{
    public interface ICoordinatesRepository
    {
        Coordinate GenerateCoordinates();
        SumDistance CalculateSumDistance(Coordinate[] coordinates);
        double GetRandomDoubleNumberInRange(double minimum, double maximum);
    }
}
