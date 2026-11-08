using Lab1.Deflectors;
using Lab1.Engines;
using Lab1.Hulls;
using Lab1.Protection;

namespace Lab1.Ships;

public sealed class Stella : Ship
{
    public Stella()
        : base(
            new ImpulseEngineC(),
            new JumpEngineOmega(),
            new ShipDefense(new HullClass1(), new DeflectorClass1(), antiNitrineEmitter: null, ShipSize.Small),
            ShipSize.Small)
    {
    }

    public override Ship CreateSimulationCopy()
    {
        return new Stella();
    }
}
