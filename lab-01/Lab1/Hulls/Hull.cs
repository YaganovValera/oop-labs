using Lab1.Protection;
using Lab1.Ships;

namespace Lab1.Hulls;

public abstract class Hull
{
    private readonly ProtectionReserve _reserve;

    protected Hull(int protectionPoints)
    {
        _reserve = new ProtectionReserve(protectionPoints);
    }

    protected abstract int SmallAsteroidCost { get; }

    protected virtual int? MeteoriteCost => null;

    public bool TryAbsorbSmallAsteroid(ShipSize size)
    {
        return _reserve.TrySpend(Scale(SmallAsteroidCost, size));
    }

    public bool TryAbsorbMeteorite(ShipSize size)
    {
        return MeteoriteCost is int cost && _reserve.TrySpend(Scale(cost, size));
    }

    private static int Scale(int baseCost, ShipSize size)
    {
        int resilience = size.GetHullResilience();
        if (baseCost % resilience != 0)
        {
            throw new InvalidOperationException("Hull hit cost must be divisible by ship size resilience.");
        }

        return baseCost / resilience;
    }
}
