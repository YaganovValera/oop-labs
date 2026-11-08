using Lab1.Fuel;
using Lab1.Obstacles;
using Lab1.Ships;

namespace Lab1.Environments;

public abstract class SpaceEnvironment
{
    private readonly List<IObstacle> _obstacles = new();

    public IReadOnlyList<IObstacle> Obstacles => _obstacles.AsReadOnly();

    public void AddObstacle(IObstacle obstacle)
    {
        ArgumentNullException.ThrowIfNull(obstacle);
        obstacle.PlaceInto(this);
    }

    public abstract SegmentFlight Fly(Ship ship, double distance);

    internal virtual void AcceptSmallAsteroid(SmallAsteroid obstacle)
    {
        RejectObstacle();
    }

    internal virtual void AcceptMeteorite(Meteorite obstacle)
    {
        RejectObstacle();
    }

    internal virtual void AcceptAntimatterFlash(AntimatterFlash obstacle)
    {
        RejectObstacle();
    }

    internal virtual void AcceptSpaceWhale(SpaceWhale obstacle)
    {
        RejectObstacle();
    }

    protected void Store(IObstacle obstacle)
    {
        _obstacles.Add(obstacle);
    }

    private static void RejectObstacle()
    {
        throw new InvalidOperationException("The environment cannot contain this obstacle.");
    }
}
