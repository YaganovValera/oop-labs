using Lab1.Engines;
using Lab1.Hulls;
using Lab1.Protection;

namespace Lab1.Ships;

public sealed class PleasureShuttle : Ship
{
    public PleasureShuttle()
        : base(
            new ImpulseEngineC(),
            jumpEngine: null,
            new ShipDefense(new HullClass1(), deflector: null, antiNitrineEmitter: null, ShipSize.Small),
            ShipSize.Small)
    {
    }

    public override Ship CreateSimulationCopy()
    {
        return new PleasureShuttle();
    }
}
