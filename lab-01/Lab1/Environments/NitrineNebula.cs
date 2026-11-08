using Lab1.Fuel;
using Lab1.Obstacles;
using Lab1.Ships;

namespace Lab1.Environments;

public sealed class NitrineNebula : SpaceEnvironment
{
    public override SegmentFlight Fly(Ship ship, double distance)
    {
        ArgumentNullException.ThrowIfNull(ship);
        double multiplier = ship.ImpulseEngine.NitrineTimeMultiplier;
        return new SegmentCompleted(
            ship.ImpulseEngine.CalculateTravelTime(distance, multiplier),
            new FuelConsumption(ship.ImpulseEngine.CalculateFuelConsumption(distance, multiplier), 0));
    }

    internal override void AcceptSpaceWhale(SpaceWhale obstacle)
    {
        Store(obstacle);
    }
}
