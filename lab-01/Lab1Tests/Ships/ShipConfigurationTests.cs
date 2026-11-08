using System.Reflection;
using Lab1.Deflectors;
using Lab1.Engines;
using Lab1.Environments;
using Lab1.Fuel;
using Lab1.Obstacles;
using Lab1.Routes;
using Lab1.Ships;

namespace Lab1Tests.Ships;

public class ShipConfigurationTests
{
    [Fact]
    public void Configurations_MatchLaboratoryLayouts()
    {
        var shuttle = new PleasureShuttle();
        Assert.IsType<ImpulseEngineC>(shuttle.ImpulseEngine);
        Assert.Null(shuttle.JumpEngine);
        Assert.Null(shuttle.Defense.Deflector);
        Assert.Equal(ShipSize.Small, shuttle.Size);

        var vaklas = new Vaklas();
        Assert.IsType<ImpulseEngineE>(vaklas.ImpulseEngine);
        Assert.IsType<JumpEngineGamma>(vaklas.JumpEngine);
        Assert.IsType<DeflectorClass1>(vaklas.Defense.Deflector);
        Assert.Equal(ShipSize.Medium, vaklas.Size);

        var photonVaklas = Vaklas.WithPhotonDeflector();
        Assert.IsType<PhotonDeflector>(photonVaklas.Defense.Deflector);
        Assert.IsType<JumpEngineGamma>(photonVaklas.JumpEngine);
        Assert.Equal(ShipSize.Medium, photonVaklas.Size);

        var meridian = new Meridian();
        Assert.IsType<ImpulseEngineE>(meridian.ImpulseEngine);
        Assert.Null(meridian.JumpEngine);
        Assert.IsType<DeflectorClass2>(meridian.Defense.Deflector);
        Assert.Equal(ShipSize.Medium, meridian.Size);

        var stella = new Stella();
        Assert.IsType<ImpulseEngineC>(stella.ImpulseEngine);
        Assert.IsType<JumpEngineOmega>(stella.JumpEngine);
        Assert.IsType<DeflectorClass1>(stella.Defense.Deflector);
        Assert.Equal(ShipSize.Small, stella.Size);

        var augur = new Augur();
        Assert.IsType<ImpulseEngineE>(augur.ImpulseEngine);
        Assert.IsType<JumpEngineAlpha>(augur.JumpEngine);
        Assert.IsType<DeflectorClass3>(augur.Defense.Deflector);
        Assert.Equal(ShipSize.Large, augur.Size);
    }

    [Fact]
    public void CreateSimulationCopy_PreservesPlainAndPhotonVaklas()
    {
        var vaklas = new Vaklas();
        Ship plainCopy = vaklas.CreateSimulationCopy();

        Assert.IsType<Vaklas>(plainCopy);
        Assert.NotSame(vaklas, plainCopy);
        Assert.IsType<DeflectorClass1>(plainCopy.Defense.Deflector);
        Assert.IsType<JumpEngineGamma>(plainCopy.JumpEngine);

        var photon = Vaklas.WithPhotonDeflector();
        Ship photonCopy = photon.CreateSimulationCopy();

        Assert.IsType<Vaklas>(photonCopy);
        Assert.NotSame(photon, photonCopy);
        Assert.IsType<PhotonDeflector>(photonCopy.Defense.Deflector);
        Assert.IsType<JumpEngineGamma>(photonCopy.JumpEngine);
    }

    [Fact]
    public void Vaklas_PublicApiCannotInstallAnotherDeflectorClass()
    {
        ConstructorInfo[] constructors = typeof(Vaklas).GetConstructors();

        Assert.Contains(constructors, constructor => constructor.GetParameters().Length == 0);
        Assert.All(
            constructors,
            constructor => Assert.DoesNotContain(
                constructor.GetParameters(),
                parameter => typeof(IDeflector).IsAssignableFrom(parameter.ParameterType)));
    }

    [Fact]
    public void MeridianEmitter_LeavesDeflectorActiveAfterWhales()
    {
        var nebula = new NitrineNebula();
        nebula.AddObstacle(new SpaceWhale(3));
        var ship = new Meridian();

        var result = Simulator().Traverse(new Route(new RouteSegment(15, nebula)), ship);

        Assert.IsType<Success>(result);
        Assert.True(ship.Defense.Deflector!.IsActive);
        Assert.False(ship.Defense.IsShipDestroyed);
    }

    private static RouteSimulator Simulator()
    {
        return new RouteSimulator(new FuelExchange(1, 1));
    }
}
