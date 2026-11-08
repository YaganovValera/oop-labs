using Lab1.Engines;
using Lab1.Protection;

namespace Lab1.Ships;

public abstract class Ship
{
    protected Ship(IImpulseEngine impulseEngine, IJumpEngine? jumpEngine, ShipDefense defense, ShipSize size)
    {
        ArgumentNullException.ThrowIfNull(impulseEngine);
        ArgumentNullException.ThrowIfNull(defense);
        ImpulseEngine = impulseEngine;
        JumpEngine = jumpEngine;
        Defense = defense;
        Size = size;
    }

    public IImpulseEngine ImpulseEngine { get; }

    public IJumpEngine? JumpEngine { get; }

    public ShipDefense Defense { get; }

    public ShipSize Size { get; }

    public abstract Ship CreateSimulationCopy();
}
