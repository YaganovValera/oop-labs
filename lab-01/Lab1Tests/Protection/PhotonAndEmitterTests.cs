using Lab1.Deflectors;
using Lab1.Hulls;
using Lab1.Obstacles;
using Lab1.Protection;
using Lab1.Ships;

namespace Lab1Tests.Protection;

public class PhotonAndEmitterTests
{
    [Fact]
    public void PhotonDeflector_ReflectsThreeFlashes_AndFourthKillsCrew()
    {
        ShipDefense defense = new(new HullClass1(), new PhotonDeflector(new DeflectorClass1()), antiNitrineEmitter: null, ShipSize.Small);

        new AntimatterFlash().Strike(defense);
        new AntimatterFlash().Strike(defense);
        new AntimatterFlash().Strike(defense);

        Assert.True(defense.IsCrewAlive);
        Assert.True(defense.Deflector!.IsActive);
        Assert.False(defense.IsShipDestroyed);

        new AntimatterFlash().Strike(defense);

        Assert.False(defense.IsCrewAlive);
        Assert.True(defense.Deflector.IsActive);
        Assert.False(defense.IsShipDestroyed);
    }

    [Fact]
    public void PhotonDeflector_DoesNotReflect_WhenInnerDeflectorIsInactive()
    {
        ShipDefense defense = new(new HullClass2(), new PhotonDeflector(new DeflectorClass1()), antiNitrineEmitter: null, ShipSize.Medium);
        new SmallAsteroid().Strike(defense);
        new SmallAsteroid().Strike(defense);
        Assert.False(defense.Deflector!.IsActive);

        new AntimatterFlash().Strike(defense);

        Assert.False(defense.IsCrewAlive);
        Assert.False(defense.IsShipDestroyed);
    }

    [Fact]
    public void AntiNitrineEmitter_PreventsWhaleContact()
    {
        ShipDefense defense = new(new HullClass2(), new DeflectorClass2(), new AntiNitrineEmitter(), ShipSize.Medium);

        new SpaceWhale(4).Strike(defense);

        Assert.False(defense.IsShipDestroyed);
        Assert.True(defense.IsCrewAlive);
        Assert.True(defense.Deflector!.IsActive);

        new SmallAsteroid().Strike(defense);
        Assert.True(defense.Deflector.IsActive);
        Assert.False(defense.IsShipDestroyed);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2)]
    public void SpaceWhale_RejectsNonPositiveCount(int count)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new SpaceWhale(count));
    }
}
