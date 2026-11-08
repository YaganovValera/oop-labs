using Lab1.Deflectors;
using Lab1.Hulls;
using Lab1.Ships;

namespace Lab1.Protection;

public sealed class ShipDefense
{
    private readonly Hull _hull;
    private readonly AntiNitrineEmitter? _antiNitrineEmitter;
    private readonly ShipSize _size;

    public ShipDefense(Hull hull, IDeflector? deflector, AntiNitrineEmitter? antiNitrineEmitter, ShipSize size)
    {
        ArgumentNullException.ThrowIfNull(hull);
        _hull = hull;
        Deflector = deflector;
        _antiNitrineEmitter = antiNitrineEmitter;
        _size = size;
    }

    public IDeflector? Deflector { get; }

    public bool IsShipDestroyed { get; private set; }

    public bool IsCrewAlive { get; private set; } = true;

    public void ReceiveSmallAsteroid()
    {
        if (IsTerminal)
        {
            return;
        }

        if (TryDeflect(static deflector => deflector.TryAbsorbSmallAsteroid()))
        {
            return;
        }

        if (_hull.TryAbsorbSmallAsteroid(_size))
        {
            return;
        }

        IsShipDestroyed = true;
    }

    public void ReceiveMeteorite()
    {
        if (IsTerminal)
        {
            return;
        }

        if (TryDeflect(static deflector => deflector.TryAbsorbMeteorite()))
        {
            return;
        }

        if (_hull.TryAbsorbMeteorite(_size))
        {
            return;
        }

        IsShipDestroyed = true;
    }

    public void ReceiveAntimatterFlash()
    {
        if (IsTerminal)
        {
            return;
        }

        if (Deflector is not null && Deflector.TryReflectAntimatterFlash())
        {
            return;
        }

        IsCrewAlive = false;
    }

    public void ReceiveSpaceWhales(int count)
    {
        if (count <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(count), count, "Whale count must be positive.");
        }

        if (IsTerminal || _antiNitrineEmitter is not null)
        {
            return;
        }

        for (int i = 0; i < count; i++)
        {
            if (TryDeflect(static deflector => deflector.TryAbsorbSpaceWhale()))
            {
                continue;
            }

            IsShipDestroyed = true;
            return;
        }
    }

    private bool IsTerminal => IsShipDestroyed || !IsCrewAlive;

    private bool TryDeflect(Func<IDeflector, bool> absorb)
    {
        if (Deflector is not { IsActive: true })
        {
            return false;
        }

        if (absorb(Deflector))
        {
            return true;
        }

        Deflector.Deactivate();
        return false;
    }
}
