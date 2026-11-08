using Lab1.Environments;
using Lab1.Protection;

namespace Lab1.Obstacles;

public sealed class AntimatterFlash : IObstacle
{
    public void PlaceInto(SpaceEnvironment environment)
    {
        ArgumentNullException.ThrowIfNull(environment);
        environment.AcceptAntimatterFlash(this);
    }

    public void Strike(ShipDefense defense)
    {
        ArgumentNullException.ThrowIfNull(defense);
        defense.ReceiveAntimatterFlash();
    }
}
