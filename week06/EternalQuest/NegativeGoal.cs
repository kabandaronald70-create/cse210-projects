public class NegativeGoal : Goal
{
    public NegativeGoal(string name, string description, int points)
        : base(name, description, points) { }  // points are negative

    public override int RecordEvent()
    {
        // Deduct points (points stored as positive but returned negative)
        return -GetPoints();
    }

    public override bool IsComplete() => false;  // never complete

    public override string GetDetailsString()
    {
        return $"[-] {GetName()} ({GetDescription()}) - Lose {GetPoints()} points";
    }

    public override string GetStringRepresentation()
    {
        return $"NegativeGoal|{GetName()}|{GetDescription()}|{GetPoints()}";
    }
}