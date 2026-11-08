using Lab1.Environments;
using Lab1.Protection;

namespace Lab1.Obstacles;

public interface IObstacle
{
    void PlaceInto(SpaceEnvironment environment);

    void Strike(ShipDefense defense);
}
