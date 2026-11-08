using Lab1.Protection;

namespace Lab1.Deflectors;

public abstract class Deflector : IDeflector
{
    private readonly ProtectionReserve _reserve;

    protected Deflector(int protectionPoints)
    {
        _reserve = new ProtectionReserve(protectionPoints);
    }

    public bool IsActive => _reserve.IsActive;

    protected abstract int SmallAsteroidCost { get; }

    protected abstract int MeteoriteCost { get; }

    protected virtual int? SpaceWhaleCost => null;

    public bool TryAbsorbSmallAsteroid()
    {
        return _reserve.TrySpend(SmallAsteroidCost);
    }

    public bool TryAbsorbMeteorite()
    {
        return _reserve.TrySpend(MeteoriteCost);
    }

    public bool TryAbsorbSpaceWhale()
    {
        return SpaceWhaleCost is int cost && _reserve.TrySpend(cost);
    }

    public bool TryReflectAntimatterFlash()
    {
        return false;
    }

    public void Deactivate()
    {
        _reserve.Deplete();
    }
}
