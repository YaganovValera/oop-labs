using Lab1.Environments;

namespace Lab1.Routes;

public sealed class RouteSegment
{
    public RouteSegment(double distance, SpaceEnvironment environment)
    {
        if (distance <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(distance), distance, "Distance must be positive.");
        }

        ArgumentNullException.ThrowIfNull(environment);
        Distance = distance;
        Environment = environment;
    }

    public double Distance { get; }

    public SpaceEnvironment Environment { get; }
}
