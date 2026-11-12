using System;
public class Clock : IEquatable<Clock>
{
    private const int MinutesPerDay = 24 * 60;
    private readonly int _totalMinutes;
    public Clock(int hours, int minutes)
    {
        long total = (long)hours * 60 + minutes;
        _totalMinutes = (int)(((total % MinutesPerDay) + MinutesPerDay) % MinutesPerDay);
    }
    public Clock Add(int minutesToAdd)
    {
        return new Clock(0, _totalMinutes + minutesToAdd);
    }
    public Clock Subtract(int minutesToSubtract)
    {
        return new Clock(0, _totalMinutes - minutesToSubtract);
    }
    public bool Equals(Clock other)
    {
        return other != null && _totalMinutes == other._totalMinutes;
    }
    public override bool Equals(object obj)
    {
        return Equals(obj as Clock);
    }
    public override int GetHashCode()
    {
        return _totalMinutes.GetHashCode();
    }
    public override string ToString()
    {
        return $"{_totalMinutes / 60:00}:{_totalMinutes % 60:00}";
    }
}
