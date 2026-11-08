namespace Lab1.Engines;

public sealed class ImpulseEngineC : IImpulseEngine
{
    private const double Speed = 10;
    private const double FuelConsumptionRate = 2;
    private const double StartupFuel = 5;
    private const double NitrineSlowdown = 10;

    public double NitrineTimeMultiplier => NitrineSlowdown;

    public double CalculateTravelTime(double distance)
    {
        return CalculateTravelTime(distance, 1);
    }

    public double CalculateTravelTime(double distance, double timeMultiplier)
    {
        EnsureDistanceIsPositive(distance);
        EnsureMultiplierIsPositive(timeMultiplier);
        return distance / Speed * timeMultiplier;
    }

    public double CalculateFuelConsumption(double distance)
    {
        return CalculateFuelConsumption(distance, 1);
    }

    public double CalculateFuelConsumption(double distance, double timeMultiplier)
    {
        double time = CalculateTravelTime(distance, timeMultiplier);
        return StartupFuel + FuelConsumptionRate * time;
    }

    private static void EnsureDistanceIsPositive(double distance)
    {
        if (distance <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(distance), distance, "Distance must be positive.");
        }
    }

    private static void EnsureMultiplierIsPositive(double timeMultiplier)
    {
        if (timeMultiplier <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(timeMultiplier), timeMultiplier, "Time multiplier must be positive.");
        }
    }
}
