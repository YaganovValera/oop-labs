using Lab1.Hulls;
using Lab1.Obstacles;
using Lab1.Protection;
using Lab1.Ships;

namespace Lab1Tests.Protection;

public class HullTests
{
    [Fact]
    public void Class1_SurvivesOneAsteroid_AndBreaksOnFurtherPhysicalDamage()
    {
        ShipDefense defense = BareHull(new HullClass1(), ShipSize.Small);

        new SmallAsteroid().Strike(defense);
        Assert.False(defense.IsShipDestroyed);

        new SmallAsteroid().Strike(defense);
        Assert.True(defense.IsShipDestroyed);

        ShipDefense meteor = BareHull(new HullClass1(), ShipSize.Small);
        new Meteorite().Strike(meteor);
        Assert.True(meteor.IsShipDestroyed);
    }

    [Fact]
    public void Class2_SurvivesFiveAsteroidsOrTwoMeteorites()
    {
        ShipDefense asteroids = BareHull(new HullClass2(), ShipSize.Medium);
        StrikeAsteroids(asteroids, 5);
        Assert.False(asteroids.IsShipDestroyed);
        new SmallAsteroid().Strike(asteroids);
        Assert.True(asteroids.IsShipDestroyed);

        ShipDefense meteors = BareHull(new HullClass2(), ShipSize.Medium);
        StrikeMeteorites(meteors, 2);
        Assert.False(meteors.IsShipDestroyed);
        new Meteorite().Strike(meteors);
        Assert.True(meteors.IsShipDestroyed);
    }

    [Fact]
    public void Class3_SurvivesTwentyAsteroidsOrFiveMeteorites()
    {
        ShipDefense asteroids = BareHull(new HullClass3(), ShipSize.Large);
        StrikeAsteroids(asteroids, 20);
        Assert.False(asteroids.IsShipDestroyed);
        new SmallAsteroid().Strike(asteroids);
        Assert.True(asteroids.IsShipDestroyed);

        ShipDefense meteors = BareHull(new HullClass3(), ShipSize.Large);
        StrikeMeteorites(meteors, 5);
        Assert.False(meteors.IsShipDestroyed);
        new Meteorite().Strike(meteors);
        Assert.True(meteors.IsShipDestroyed);
    }

    [Fact]
    public void Class2_MixedHits_ShareOneProtectionBudget()
    {
        ShipDefense defense = BareHull(new HullClass2(), ShipSize.Medium);
        StrikeAsteroids(defense, 2);
        new Meteorite().Strike(defense);
        Assert.False(defense.IsShipDestroyed);

        new Meteorite().Strike(defense);
        Assert.True(defense.IsShipDestroyed);
    }

    [Fact]
    public void ShipSize_ChangesAsteroidDamageOnTheSameHull()
    {
        ShipDefense small = BareHull(new HullClass1(), ShipSize.Small);
        new SmallAsteroid().Strike(small);
        Assert.False(small.IsShipDestroyed);
        new SmallAsteroid().Strike(small);
        Assert.True(small.IsShipDestroyed);

        ShipDefense medium = BareHull(new HullClass1(), ShipSize.Medium);
        new SmallAsteroid().Strike(medium);
        new SmallAsteroid().Strike(medium);
        Assert.False(medium.IsShipDestroyed);
        new SmallAsteroid().Strike(medium);
        Assert.True(medium.IsShipDestroyed);
    }

    [Fact]
    public void ShipSize_ChangesMeteoriteDamageOnTheSameHull()
    {
        ShipDefense medium = BareHull(new HullClass2(), ShipSize.Medium);
        StrikeMeteorites(medium, 2);
        Assert.False(medium.IsShipDestroyed);

        ShipDefense small = BareHull(new HullClass2(), ShipSize.Small);
        new Meteorite().Strike(small);
        Assert.False(small.IsShipDestroyed);
        new Meteorite().Strike(small);
        Assert.True(small.IsShipDestroyed);
    }

    private static ShipDefense BareHull(Hull hull, ShipSize size)
    {
        return new ShipDefense(hull, deflector: null, antiNitrineEmitter: null, size);
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
