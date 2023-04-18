using EuromobileTestTaks.Domain.Models;
using System.Diagnostics.Metrics;

namespace EuromobileTestTask.WebApi.Repositories;

public class CoordinatesRepository : ICoordinatesRepository
{
    //distance = 2r * sin^-1(sqrt(sin^2( (fi2 - fi1)/2 ) + cos(fi1) * cos(fi2) * sin^2( (lamda2 - lambda1)/2 ) )
    public double CalculateDistanceBetweenTwoCoordinatesInMeters(Coordinate coordinate1, Coordinate coordinate2)
    {
        const double EARTH_RADIUS = 6371000d;

        double latitude2InRadians = CoordinatesMathHelper.ConvertDegreesToRadians(coordinate2.Latitude);
        double latitude1InRadians = CoordinatesMathHelper.ConvertDegreesToRadians(coordinate1.Latitude);
        double latitudeDifference = latitude2InRadians - latitude1InRadians;

        double longitude2InRadians = CoordinatesMathHelper.ConvertDegreesToRadians(coordinate2.Longitude);
        double longitude1InRadians = CoordinatesMathHelper.ConvertDegreesToRadians(coordinate1.Longitude);
        double longitudeDifference = longitude2InRadians - longitude1InRadians;

        double latitudeMultiplier = Math.Sin(latitudeDifference / 2);
        double longitudeMultiplier = Math.Sin(longitudeDifference / 2);

        double insideOfSquareRoot = latitudeMultiplier * latitudeMultiplier + Math.Cos(latitude1InRadians) * Math.Cos(latitude2InRadians) * longitudeMultiplier * longitudeMultiplier;
        double distanceBetweenTwoCoordinatesInMeters = 2 * EARTH_RADIUS * Math.Asin(Math.Sqrt(insideOfSquareRoot));

        return distanceBetweenTwoCoordinatesInMeters;
    }

    public Coordinate GenerateCoordinates()
    {
        double generatedLatitude = CoordinatesMathHelper.GetRandomDoubleNumberInRange(-90, 90);
        double generatedLongitude = CoordinatesMathHelper.GetRandomDoubleNumberInRange(-180, 180);

        return new Coordinate
        {
            Latitude = generatedLatitude,
            Longitude = generatedLongitude
        };
    }
}

