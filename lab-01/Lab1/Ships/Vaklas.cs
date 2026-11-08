using Lab1.Deflectors;
using Lab1.Engines;
using Lab1.Hulls;
using Lab1.Protection;

namespace Lab1.Ships;

public sealed class Vaklas : Ship
{
    private readonly bool _hasPhotonDeflector;

    public Vaklas()
        : this(hasPhotonDeflector: false)
    {
    }

    private Vaklas(bool hasPhotonDeflector)
        : base(
            new ImpulseEngineE(),
            new JumpEngineGamma(),
            new ShipDefense(new HullClass2(), CreateDeflector(hasPhotonDeflector), antiNitrineEmitter: null, ShipSize.Medium),
            ShipSize.Medium)
    {
        _hasPhotonDeflector = hasPhotonDeflector;
    }

    public static Vaklas WithPhotonDeflector()
    {
        return new Vaklas(hasPhotonDeflector: true);
    }

    public override Ship CreateSimulationCopy()
    {
        return new Vaklas(_hasPhotonDeflector);
    }

    private static IDeflector CreateDeflector(bool hasPhotonDeflector)
    {
        IDeflector deflector = new DeflectorClass1();
        return hasPhotonDeflector ? new PhotonDeflector(deflector) : deflector;
    }
}
