namespace Lab1.Ships;

public enum ShipSize
{
    Small,
    Medium,
    Large,
}

public static class ShipSizeResilience
{
    public static int GetHullResilience(this ShipSize size)
    {
        return size switch
        {
            ShipSize.Small => 1,
            ShipSize.Medium => 2,
            ShipSize.Large => 4,
            _ => throw new ArgumentOutOfRangeException(nameof(size), size, "Unknown ship size."),
        };
    }
}
