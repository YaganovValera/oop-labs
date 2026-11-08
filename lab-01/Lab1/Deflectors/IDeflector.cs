namespace Lab1.Deflectors;

public interface IDeflector
{
    bool IsActive { get; }

    bool TryAbsorbSmallAsteroid();

    bool TryAbsorbMeteorite();

    bool TryAbsorbSpaceWhale();

    bool TryReflectAntimatterFlash();

    void Deactivate();
}
