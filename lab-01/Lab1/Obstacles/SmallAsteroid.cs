using Lab1.Environments;
using Lab1.Protection;

namespace Lab1.Obstacles;

public sealed class SmallAsteroid : IObstacle
{
    public void PlaceInto(SpaceEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(environment);
        environment.AcceptSmallAsteroid(this);
    }

    public void Strike(ShipDefense defense)
    {
        ArgumentNullException.ThrowIfNull(defense);
        defense.ReceiveSmallAsteroid();
    }
}
