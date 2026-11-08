namespace Lab1.Protection;

internal sealed class ProtectionReserve
{
    private int _points;

    public ProtectionReserve(int points)
    {
        if (points <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(points), points, "Protection points must be positive.");
        }

        _points = points;
    }

    public bool IsActive => _points > 0;

    public bool TrySpend(int cost)
    {
        if (cost <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(cost), cost, "Hit cost must be positive.");
        }

        if (_points < cost)
        {
            return false;
        }

        _points -= cost;
        return true;
    }

    public void Deplete()
    {
        _points = 0;
    }
}
