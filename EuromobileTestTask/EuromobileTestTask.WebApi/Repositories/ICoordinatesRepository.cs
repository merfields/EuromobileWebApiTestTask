using EuromobileTestTaks.Domain.Models;

namespace EuromobileTestTask.WebApi.Repositories;

public interface ICoordinatesRepository
{
    Coordinate GenerateCoordinates();
    TotalDistance CalculateTotalDistanceBetweenCoordinates(Coordinate[] coordinates);
}

