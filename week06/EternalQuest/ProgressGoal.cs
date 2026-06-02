// This goal allows incremental progress (e.g., read 10% of a book each time)
public class ProgressGoal : Goal
{
    private int _totalSteps;
    private int _currentStep;
    private int _bonusPoints;

    public ProgressGoal(string name, string description, int pointsPerStep, int totalSteps, int bonusPoints)
        : base(name, description, pointsPerStep)
    {
        _totalSteps = totalSteps;
        _bonusPoints = bonusPoints;
        _currentStep = 0;
    }

    public override int RecordEvent()
    {
        if (_currentStep < _totalSteps)
        {
            _currentStep++;
            int earned = GetPoints();
            if (_currentStep == _totalSteps)
            {
                earned += _bonusPoints;
            }
            return earned;
        }
        return 0;
    }

    public override bool IsComplete() => _currentStep >= _totalSteps;

    public override string GetDetailsString()
    {
        string progress = $"{_currentStep}/{_totalSteps} steps";
        return $"[P] {GetName()} ({GetDescription()}) - {progress} ({GetPoints()} pts/step)";
    }

    public override string GetStringRepresentation()
    {
        return $"ProgressGoal|{GetName()}|{GetDescription()}|{GetPoints()}|{_bonusPoints}|{_totalSteps}|{_currentStep}";
    }
}
