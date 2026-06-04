// ─── LISTING ACTIVITY ────────────────────────────────────────────────────────
class ListingActivity : MindfulnessActivity
{
    private List<string> _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "Who are some of your personal heroes?",
            "What things in your life bring you joy?",
            "What achievements are you proud of?"
        };

    private Queue<string> _promptQueue;

    public ListingActivity() : base(
        "Listing Activity",
        "This activity will help you reflect on the good things in your life by having\nyou list as many things as you can in a certain area.")
    {
        _promptQueue = new Queue<string>(_prompts.OrderBy(_ => Guid.NewGuid()));
    }

    protected override ConsoleColor GetActivityColor() => ConsoleColor.Green;

    public override void Run()
    {
        DisplayStartMessage();

        // Pick a prompt (no repeats until all used)
        if (_promptQueue.Count == 0)
            _promptQueue = new Queue<string>(_prompts.OrderBy(_ => Guid.NewGuid()));
        string prompt = _promptQueue.Dequeue();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n  📝  {prompt}\n");
        Console.ResetColor();

        Console.WriteLine("  You have 5 seconds to think...");
        ShowCountdown(5, ConsoleColor.Green);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\n  Start listing! Press Enter after each item.\n");
        Console.ResetColor();

        var items = new List<string>();
        DateTime endTime = DateTime.Now.AddSeconds(Duration);

        while (DateTime.Now < endTime)
        {
            int remaining = (int)(endTime - DateTime.Now).TotalSeconds;
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write($"  [{remaining,2}s] ➤ ");
            Console.ForegroundColor = ConsoleColor.White;
            string item = ReadLineWithTimeout(endTime);
            Console.ResetColor();
            if (item == null) break;
            if (!string.IsNullOrWhiteSpace(item))
                items.Add(item.Trim());
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n  🎉 You listed {items.Count} item{(items.Count != 1 ? "s" : "")}!");
        Console.ResetColor();

        DisplayEndMessage();
    }

    private string ReadLineWithTimeout(DateTime endTime)
    {
        string result = "";
        while (DateTime.Now < endTime)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(intercept: false);
                if (key.Key == ConsoleKey.Enter) return result;
                if (key.Key == ConsoleKey.Backspace && result.Length > 0)
                {
                    result = result.Substring(0, result.Length - 1);
                    Console.Write(" \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    result += key.KeyChar;
                }
            }
            else
            {
                Thread.Sleep(50);
            }
        }
        return null;
    }
}