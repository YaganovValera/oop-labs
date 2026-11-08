using Lab1.Deflectors;
using Lab1.Engines;
using Lab1.Hulls;
using Lab1.Protection;

namespace Lab1.Ships;

public sealed class Augur : Ship
{
    public Augur()
        : base(
            new ImpulseEngineE(),
            new JumpEngineAlpha(),
            new ShipDefense(new HullClass3(), new DeflectorClass3(), antiNitrineEmitter: null, ShipSize.Large),
            ShipSize.Large)
    {
    }

    public override Ship CreateSimulationCopy()
    {
        return new Augur();
    }
}
