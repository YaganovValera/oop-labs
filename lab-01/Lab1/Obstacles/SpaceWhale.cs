using Lab1.Environments;
using Lab1.Protection;

namespace Lab1.Obstacles;

public sealed class SpaceWhale : IObstacle
{
    public SpaceWhale(int count)
    {
        if (count <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), count, "Whale count must be positive.");
        }

        Count = count;
    }

    public int Count { get; }

    public void PlaceInto(SpaceEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(environment);
        environment.AcceptSpaceWhale(this);
    }

    public void Strike(ShipDefense defense)
    {
        ArgumentNullException.ThrowIfNull(defense);
        defense.ReceiveSpaceWhales(Count);
    }
}
