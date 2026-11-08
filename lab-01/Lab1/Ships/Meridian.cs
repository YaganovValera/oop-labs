using Lab1.Deflectors;
using Lab1.Engines;
using Lab1.Hulls;
using Lab1.Protection;

namespace Lab1.Ships;

public sealed class Meridian : Ship
{
    public Meridian()
        : base(
            new ImpulseEngineE(),
            jumpEngine: null,
            new ShipDefense(new HullClass2(), new DeflectorClass2(), new AntiNitrineEmitter(), ShipSize.Medium),
            ShipSize.Medium)
    {
    }

    public override Ship CreateSimulationCopy()
    {
        return new Meridian();
    }
}
