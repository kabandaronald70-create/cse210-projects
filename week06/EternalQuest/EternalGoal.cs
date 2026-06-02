public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points) { }

    public override int RecordEvent()
    {
        // Always award points, goal never completes
        return GetPoints();
    }

    public override bool IsComplete() => false;

    public override string GetDetailsString()
    {
        return $"[∞] {GetName()} ({GetDescription()})";
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal|{GetName()}|{GetDescription()}|{GetPoints()}";
    }
}

