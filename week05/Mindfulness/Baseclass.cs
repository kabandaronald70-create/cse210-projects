// ─── BASE CLASS ──────────────────────────────────────────────────────────────
abstract class MindfulnessActivity
{
    private string _name;
    private string _description;
    private int _duration; // seconds

    protected string Name => _name;
    protected string Description => _description;
    protected int Duration => _duration;

    public MindfulnessActivity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    // Common starting message
    public void DisplayStartMessage()
    {
        Console.Clear();
        PrintHeader(_name);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"\n{_description}\n");
        Console.ResetColor();

        Console.Write("How long would you like this session to last (in seconds)? ");
        while (!int.TryParse(Console.ReadLine(), out _duration) || _duration < 5)
        {
            Console.Write("Please enter a valid number (minimum 5 seconds): ");
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nGet ready to begin...");
        Console.ResetColor();
        ShowSpinner(4);
    }

    // Common ending message
    public void DisplayEndMessage()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n\nWell done! 🌟");
        ShowSpinner(3);
        Console.WriteLine($"\nYou have completed the \"{_name}\".");
        Console.WriteLine($"Total time: {_duration} seconds.");
        Console.ResetColor();
        ShowSpinner(4);

        // Log the activity
        ActivityLogger.LogActivity(_name, _duration);
    }

    // Abstract method each activity must implement
    public abstract void Run();

    // ── Animation Helpers ──────────────────────────────────────────────────

    protected void ShowSpinner(int seconds)
    {
        string[] frames = { "◐", "◓", "◑", "◒" };
        int totalFrames = seconds * 8;
        for (int i = 0; i < totalFrames; i++)
        {
            Console.Write($"\r  {frames[i % frames.Length]}  ");
            Thread.Sleep(125);
        }
        Console.Write("\r     \r");
    }

    protected void ShowCountdown(int seconds, ConsoleColor color = ConsoleColor.Cyan)
    {
        for (int i = seconds; i >= 1; i--)
        {
            Console.ForegroundColor = color;
            Console.Write($"\r  [{i,2}s remaining]  ");
            Console.ResetColor();
            Thread.Sleep(1000);
        }
        Console.Write("\r                    \r");
    }

    protected void PrintHeader(string title)
    {
        Console.ForegroundColor = GetActivityColor();
        string border = new string('═', title.Length + 8);
        Console.WriteLine($"╔{border}╗");
        Console.WriteLine($"║    {title.ToUpper()}    ║");
        Console.WriteLine($"╚{border}╝");
        Console.ResetColor();
    }

    protected virtual ConsoleColor GetActivityColor() => ConsoleColor.Cyan;
}
