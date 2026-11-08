namespace Lab1.Engines;

public interface IJumpEngine
{
    double MaxDistance { get; }

    bool CanJump(double distance);

    double CalculateTravelTime(double distance);

    double CalculateFuelConsumption(double distance);
}
