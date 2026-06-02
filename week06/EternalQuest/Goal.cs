public abstract class Goal
{
    private string _name;
    private string _description;
    private int _points;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
    }

    public string GetName() => _name;
    public string GetDescription() => _description;
    public int GetPoints() => _points;

    // Abstract methods to be overridden by derived classes
    public abstract int RecordEvent();
    public abstract bool IsComplete();
    public abstract string GetDetailsString();
    public abstract string GetStringRepresentation();

    // Virtual method that can be overridden (used by ProgressGoal)
    public virtual int GetBonus() => 0;
}
