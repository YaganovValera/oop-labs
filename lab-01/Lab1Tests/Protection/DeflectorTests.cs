using Lab1.Deflectors;
using Lab1.Hulls;
using Lab1.Obstacles;
using Lab1.Protection;
using Lab1.Ships;

namespace Lab1Tests.Protection;

public class DeflectorTests
{
    [Fact]
    public void Class1_AbsorbsTwoAsteroidsOrOneMeteorite_ThenDeactivates()
    {
        ShipDefense asteroids = Defense(new HullClass1(), new DeflectorClass1());
        StrikeAsteroids(asteroids, 2);
        Assert.False(asteroids.Deflector!.IsActive);
        Assert.False(asteroids.IsShipDestroyed);

        ShipDefense meteor = Defense(new HullClass1(), new DeflectorClass1());
        new Meteorite().Strike(meteor);
        Assert.False(meteor.Deflector!.IsActive);
        Assert.False(meteor.IsShipDestroyed);

        new Meteorite().Strike(meteor);
        Assert.True(meteor.IsShipDestroyed);
    }

    [Fact]
    public void Class2_AbsorbsTenAsteroidsOrThreeMeteorites_ThenDeactivates()
    {
        ShipDefense asteroids = Defense(new HullClass1(), new DeflectorClass2());
        StrikeAsteroids(asteroids, 10);
        Assert.False(asteroids.Deflector!.IsActive);
        Assert.False(asteroids.IsShipDestroyed);

        ShipDefense meteors = Defense(new HullClass1(), new DeflectorClass2());
        StrikeMeteorites(meteors, 3);
        Assert.False(meteors.Deflector!.IsActive);
        Assert.False(meteors.IsShipDestroyed);
    }

    [Fact]
    public void Class3_AbsorbsFortyAsteroidsOrTenMeteoritesOrOneWhale_ThenDeactivates()
    {
        ShipDefense asteroids = Defense(new HullClass1(), new DeflectorClass3());
        StrikeAsteroids(asteroids, 40);
        Assert.False(asteroids.Deflector!.IsActive);
        Assert.False(asteroids.IsShipDestroyed);

        ShipDefense meteors = Defense(new HullClass1(), new DeflectorClass3());
        StrikeMeteorites(meteors, 10);
        Assert.False(meteors.Deflector!.IsActive);
        Assert.False(meteors.IsShipDestroyed);

        ShipDefense whale = Defense(new HullClass1(), new DeflectorClass3());
        new SpaceWhale(1).Strike(whale);
        Assert.False(whale.Deflector!.IsActive);
        Assert.False(whale.IsShipDestroyed);
        new SmallAsteroid().Strike(whale);
        Assert.False(whale.IsShipDestroyed);
    }

    [Fact]
    public void Damage_PersistsBetweenCollisions()
    {
        ShipDefense defense = Defense(new HullClass3(), new DeflectorClass1());

        new SmallAsteroid().Strike(defense);
        Assert.True(defense.Deflector!.IsActive);
        Assert.False(defense.IsShipDestroyed);

        new SmallAsteroid().Strike(defense);
        Assert.False(defense.Deflector.IsActive);
        Assert.False(defense.IsShipDestroyed);
    }

    [Fact]
    public void WeakerDeflector_DoesNotSurviveSpaceWhale()
    {
        ShipDefense defense = Defense(new HullClass3(), new DeflectorClass2());

        new SpaceWhale(1).Strike(defense);

        Assert.True(defense.IsShipDestroyed);
        Assert.False(defense.Deflector!.IsActive);
    }

    [Fact]
    public void SecondSpaceWhale_DestroysShipAfterClass3DeflectorShutsDown()
    {
        ShipDefense defense = Defense(new HullClass3(), new DeflectorClass3());

        new SpaceWhale(2).Strike(defense);

        Assert.True(defense.IsShipDestroyed);
    }

    private static ShipDefense Defense(Hull hull, IDeflector deflector)
    {
        return new ShipDefense(hull, deflector, antiNitrineEmitter: null, ShipSize.Small);
    }

    private static void StrikeAsteroids(ShipDefense defense, int count)
    {
        for (int i = 0; i < count; i++)
        {
            new SmallAsteroid().Strike(defense);
        }
    }

    private static void StrikeMeteorites(ShipDefense defense, int count)
    {
        for (int i = 0; i < count; i++)
        {
            new Meteorite().Strike(defense);
        }
    }
}
