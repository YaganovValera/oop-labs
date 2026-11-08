namespace Lab1.Engines;

public sealed class ImpulseEngineE : IImpulseEngine
{
    private const double InitialSpeed = 5;
    private const double Acceleration = 0.1;
    private const double FuelConsumptionRate = 8;
    private const double StartupFuel = 15;
    private const double NitrineSlowdown = 2;

    public double NitrineTimeMultiplier => NitrineSlowdown;

    public double CalculateTravelTime(double distance)
    {
        return CalculateTravelTime(distance, 1);
    }

    public double CalculateTravelTime(double distance, double timeMultiplier)
    {
        EnsureDistanceIsPositive(distance);
        EnsureMultiplierIsPositive(timeMultiplier);
        return Math.Log(1 + distance * Acceleration / InitialSpeed) / Acceleration * timeMultiplier;
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
