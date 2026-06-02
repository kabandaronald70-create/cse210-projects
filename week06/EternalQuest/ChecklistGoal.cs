public class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _currentCount;
    private int _bonusPoints;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints)
        : base(name, description, points)
    {
        _targetCount = targetCount;
        _bonusPoints = bonusPoints;
        _currentCount = 0;
    }

    public override int RecordEvent()
    {
        if (_currentCount < _targetCount)
        {
            _currentCount++;
            int earned = GetPoints();
            if (_currentCount == _targetCount)
            {
                earned += _bonusPoints;
            }
            return earned;
        }
        return 0;
    }

    public override bool IsComplete() => _currentCount >= _targetCount;

    public override string GetDetailsString()
    {
        string checkbox = IsComplete() ? "[X]" : "[ ]";
        return $"{checkbox} {GetName()} ({GetDescription()}) -- Completed {_currentCount}/{_targetCount} times";
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal|{GetName()}|{GetDescription()}|{GetPoints()}|{_bonusPoints}|{_targetCount}|{_currentCount}";
    }
}
