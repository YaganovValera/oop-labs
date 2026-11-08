namespace Lab1.Engines;

public abstract class JumpEngine : IJumpEngine
{
    private const double JumpSpeed = 10;

    protected JumpEngine(double maxDistance)
    {
        MaxDistance = maxDistance;
    }

    public double MaxDistance { get; }

    public bool CanJump(double distance)
    {
        EnsureDistanceIsPositive(distance);
        return distance <= MaxDistance;
    }

    public double CalculateTravelTime(double distance)
    {
        EnsureDistanceIsWithinRange(distance);
        return distance / JumpSpeed;
    }

    public double CalculateFuelConsumption(double distance)
    {
        EnsureDistanceIsWithinRange(distance);
        return ComputeFuelConsumption(distance);
    }

    private void EnsureDistanceIsWithinRange(double distance)
    {
        EnsureDistanceIsPositive(distance);

        if (distance > MaxDistance)
        {
            throw new ArgumentOutOfRangeException(nameof(distance), distance, "Distance exceeds maximum jump distance.");
        }
    }

    protected abstract double ComputeFuelConsumption(double distance);

    private static void EnsureDistanceIsPositive(double distance)
    {
        if (distance <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(distance), distance, "Distance must be positive.");
        }
    }
}
