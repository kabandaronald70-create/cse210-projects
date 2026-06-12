public class Swimming : Activity
{
    private int _laps;
    private const double LapLengthMiles = 50.0 / 1000.0 * 0.621371; // 50 meters to miles

    public Swimming(DateTime date, int minutes, int laps) : base(date, minutes)
    {
        _laps = laps;
    }

    public override double GetDistance() => _laps * LapLengthMiles;
    public override double GetSpeed() => (GetDistance() / Minutes) * 60;
    public override double GetPace() => Minutes / GetDistance();
}