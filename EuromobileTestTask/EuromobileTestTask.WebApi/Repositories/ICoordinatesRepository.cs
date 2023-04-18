using EuromobileTestTaks.Domain.Models;

namespace EuromobileTestTask.WebApi.Repositories
{
    public interface ICoordinatesRepository
    {
        Coordinate GenerateCoordinates();
        double CalculateDistanceBetweenTwoCoordinatesInMeters(Coordinate coordinate1, Coordinate coordinate2);
    }
}
