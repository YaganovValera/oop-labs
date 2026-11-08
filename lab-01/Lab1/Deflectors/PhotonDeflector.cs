namespace Lab1.Deflectors;

public sealed class PhotonDeflector : IDeflector
{
    private const int MaxReflections = 3;

    private readonly IDeflector _inner;
    private int _remainingReflections = MaxReflections;

    public PhotonDeflector(IDeflector inner)
    {
        ArgumentNullException.ThrowIfNull(inner);
        _inner = inner;
    }

    public bool IsActive => _inner.IsActive;

    public bool TryAbsorbSmallAsteroid()
    {
        return _inner.TryAbsorbSmallAsteroid();
    }

    public bool TryAbsorbMeteorite()
    {
        return _inner.TryAbsorbMeteorite();
    }

    public bool TryAbsorbSpaceWhale()
    {
        return _inner.TryAbsorbSpaceWhale();
    }

    public bool TryReflectAntimatterFlash()
    {
        if (!_inner.IsActive || _remainingReflections <= 0)
        {
            return false;
        }

        _remainingReflections--;
        return true;
    }

    public void Deactivate()
    {
        _inner.Deactivate();
    }
}
