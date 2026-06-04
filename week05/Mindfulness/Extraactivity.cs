class GratitudeActivity : MindfulnessActivity
{
    private const string JournalFile = "gratitude_journal.txt";

    private List<string> _prompts = new List<string>
        {
            "What is one small thing that made you smile today?",
            "Who is someone whose kindness you are grateful for?",
            "What is something about your health you are thankful for?",
            "What is a lesson you've learned that you're grateful for?",
            "What is something beautiful you noticed recently?",
            "What opportunity are you grateful to have right now?"
        };

    public GratitudeActivity() : base(
        "Gratitude Journal",
        "This activity guides you through a personal gratitude reflection and saves your\nthoughts to a private journal. Practicing gratitude boosts happiness and wellbeing.")
    { }

    protected override ConsoleColor GetActivityColor() => ConsoleColor.Yellow;

    public override void Run()
    {
        DisplayStartMessage();

        var rng = new Random();
        string prompt = _prompts[rng.Next(_prompts.Count)];

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n  🌸  {prompt}\n");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine("  Take a moment to breathe and reflect...");
        Console.ResetColor();
        ShowSpinner(4);

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("\n  Write your thoughts below (press Enter twice when done):\n");
        Console.ResetColor();

        var lines = new List<string>();
        string lastLine = null;
        DateTime endTime = DateTime.Now.AddSeconds(Duration);

        while (DateTime.Now < endTime)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("  > ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            string line = Console.ReadLine();
            Console.ResetColor();
            if (line == "" && lastLine == "") break;
            if (line != null) lines.Add(line);
            lastLine = line;
        }

        // Save to journal
        SaveToJournal(prompt, lines);

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n  📖  Your entry has been saved to \"{JournalFile}\".");
        Console.ResetColor();

        DisplayEndMessage();
    }

    private void SaveToJournal(string prompt, List<string> lines)
    {
        try
        {
            using (var sw = File.AppendText(JournalFile))
            {
                sw.WriteLine($"\n{'─',50}");
                sw.WriteLine($"Date: {DateTime.Now:dddd, MMMM d, yyyy  h:mm tt}");
                sw.WriteLine($"Prompt: {prompt}");
                sw.WriteLine();
                foreach (var line in lines)
                    sw.WriteLine($"  {line}");
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n  Could not save journal: {ex.Message}");
            Console.ResetColor();
        }
    }
}
