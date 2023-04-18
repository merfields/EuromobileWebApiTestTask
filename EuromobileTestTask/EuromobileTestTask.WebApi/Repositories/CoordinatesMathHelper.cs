namespace EuromobileTestTask.WebApi.Repositories;

public static class CoordinatesMathHelper
{
    public static double GetRandomDoubleNumberInRange(double minimum, double maximum)
    {
        Random random = new Random();
        return random.NextDouble() * (maximum - minimum) + minimum;
    }

    public static double ConvertDegreesToRadians(double degrees)
    {
        double radians = (Math.PI / 180) * degrees;
        return radians;
    }

    public static double ConvertMetersToMiles(double meters)
    {
        return meters * 0.000621371192d;
    }
}

