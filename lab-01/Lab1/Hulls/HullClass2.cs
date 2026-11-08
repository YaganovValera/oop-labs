namespace Lab1.Hulls;

public sealed class HullClass2 : Hull
{
    private const int ProtectionPoints = 20;
    private const int AsteroidHitCost = 8;
    private const int MeteoriteHitCost = 20;

    public HullClass2()
        : base(ProtectionPoints)
    {
    }

    protected override int SmallAsteroidCost => AsteroidHitCost;

    protected override int? MeteoriteCost => MeteoriteHitCost;
}
