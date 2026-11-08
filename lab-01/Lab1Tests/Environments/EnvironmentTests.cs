using Lab1.Environments;
using Lab1.Obstacles;

namespace Lab1Tests.Environments;

public class EnvironmentTests
{
    [Fact]
    public void NormalSpace_AcceptsAsteroidsAndMeteoritesOnly()
    {
        var space = new NormalSpace();

        space.AddObstacle(new SmallAsteroid());
        space.AddObstacle(new Meteorite());

        Assert.Equal(2, space.Obstacles.Count);
        Assert.IsType<SmallAsteroid>(space.Obstacles[0]);
        Assert.IsType<Meteorite>(space.Obstacles[1]);
        Assert.Throws<InvalidOperationException>(() => space.AddObstacle(new AntimatterFlash()));
        Assert.Throws<InvalidOperationException>(() => space.AddObstacle(new SpaceWhale(1)));
        Assert.Equal(2, space.Obstacles.Count);
    }

    [Fact]
    public void HighDensityNebula_AcceptsAntimatterFlashOnly()
    {
        var nebula = new HighDensityNebula();

        nebula.AddObstacle(new AntimatterFlash());

        Assert.Single(nebula.Obstacles);
        Assert.Throws<InvalidOperationException>(() => nebula.AddObstacle(new SmallAsteroid()));
        Assert.Throws<InvalidOperationException>(() => nebula.AddObstacle(new Meteorite()));
        Assert.Throws<InvalidOperationException>(() => nebula.AddObstacle(new SpaceWhale(1)));
        Assert.Single(nebula.Obstacles);
    }

    [Fact]
    public void NitrineNebula_AcceptsSpaceWhaleOnly()
    {
        var nebula = new NitrineNebula();

        nebula.AddObstacle(new SpaceWhale(2));

        Assert.Single(nebula.Obstacles);
        Assert.Throws<InvalidOperationException>(() => nebula.AddObstacle(new Meteorite()));
        Assert.Throws<InvalidOperationException>(() => nebula.AddObstacle(new SmallAsteroid()));
        Assert.Throws<InvalidOperationException>(() => nebula.AddObstacle(new AntimatterFlash()));
        Assert.Single(nebula.Obstacles);
    }
}
