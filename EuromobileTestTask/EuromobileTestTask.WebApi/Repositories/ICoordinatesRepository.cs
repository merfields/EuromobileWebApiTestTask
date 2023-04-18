using EuromobileTestTaks.Domain.Models;

namespace EuromobileTestTask.WebApi.Repositories
{
    public interface ICoordinatesRepository
    {
        Coordinate GenerateCoordinates();
        double CalculateDistanceBetweenTwoCoordinatesInMetres(Coordinate coordinate1, Coordinate coordinate2);
        double ConvertMetresToMiles(double metres);
    }
}
