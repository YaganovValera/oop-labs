namespace Lab1.Hulls;

public sealed class HullClass1 : Hull
{
    private const int ProtectionPoints = 4;
    private const int AsteroidHitCost = 4;
    private const int MeteoriteHitCost = 8;

    public HullClass1()
        : base(ProtectionPoints)
    {
    }

    protected override int SmallAsteroidCost => AsteroidHitCost;

    protected override int? MeteoriteCost => MeteoriteHitCost;
}
