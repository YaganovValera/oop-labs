using Lab1.Engines;
using Lab1.Fuel;
using Lab1.Obstacles;
using Lab1.Ships;

namespace Lab1.Environments;

public sealed class HighDensityNebula : SpaceEnvironment
{
    public override SegmentFlight Fly(Ship ship, double distance)
    {
        ArgumentNullException.ThrowIfNull(ship);

        if (ship.JumpEngine is not IJumpEngine jumpEngine || !jumpEngine.CanJump(distance))
        {
            return new SegmentDenied();
        }

        return new SegmentCompleted(
            jumpEngine.CalculateTravelTime(distance),
            new FuelConsumption(0, jumpEngine.CalculateFuelConsumption(distance)));
    }

    internal override void AcceptAntimatterFlash(AntimatterFlash obstacle)
    {
        Store(obstacle);
    }
}
