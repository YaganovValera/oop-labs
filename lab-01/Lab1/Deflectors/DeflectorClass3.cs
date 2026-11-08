namespace Lab1.Deflectors;

public sealed class DeflectorClass3 : Deflector
{
    private const int ProtectionPoints = 40;
    private const int AsteroidHitCost = 1;
    private const int MeteoriteHitCost = 4;
    private const int WhaleHitCost = 40;

    public DeflectorClass3()
        : base(ProtectionPoints)
    {
    }

    protected override int SmallAsteroidCost => AsteroidHitCost;

    protected override int MeteoriteCost => MeteoriteHitCost;

    protected override int? SpaceWhaleCost => WhaleHitCost;
}
