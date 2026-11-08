namespace Lab1.Engines;

public interface IImpulseEngine
{
    double NitrineTimeMultiplier { get; }

    double CalculateTravelTime(double distance);

    double CalculateTravelTime(double distance, double timeMultiplier);

    double CalculateFuelConsumption(double distance);

    double CalculateFuelConsumption(double distance, double timeMultiplier);
}
