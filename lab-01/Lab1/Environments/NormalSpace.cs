using Lab1.Fuel;
using Lab1.Obstacles;
using Lab1.Ships;

namespace Lab1.Environments;

public sealed class NormalSpace : SpaceEnvironment
{
    public override SegmentFlight Fly(Ship ship, double distance)
    {
        ArgumentNullException.ThrowIfNull(ship);
        return new SegmentCompleted(
            ship.ImpulseEngine.CalculateTravelTime(distance),
            new FuelConsumption(ship.ImpulseEngine.CalculateFuelConsumption(distance), 0));
    }

    internal override void AcceptSmallAsteroid(SmallAsteroid obstacle)
    {
        Store(obstacle);
    }

    internal override void AcceptMeteorite(Meteorite obstacle)
    {
        Store(obstacle);
    }
}
