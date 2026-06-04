// ─── REFLECTION ACTIVITY ────────────────────────────────────────────────────
class ReflectionActivity : MindfulnessActivity
{
    private List<string> _prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless.",
            "Think of a time when you overcame a fear.",
            "Think of a time when you made someone smile."
        };

    private List<string> _questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?",
            "Who else was positively impacted by your actions?",
            "What strengths did you demonstrate during this experience?"
        };

    private Queue<string> _promptQueue;
    private Queue<string> _questionQueue;

    public ReflectionActivity() : base(
        "Reflection Activity",
        "This activity will help you reflect on times in your life when you have shown\nstrength and resilience. This will help you recognize the power you have and\nhow you can use it in other aspects of your life.")
    {
        _promptQueue = new Queue<string>(Shuffle(_prompts));
        _questionQueue = new Queue<string>(Shuffle(_questions));
    }

    protected override ConsoleColor GetActivityColor() => ConsoleColor.Magenta;

    public override void Run()
    {
        DisplayStartMessage();

        // Pick a prompt
        string prompt = Dequeue(_promptQueue, _prompts);
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"\n  💭  {prompt}\n");
        Console.ResetColor();
        ShowSpinner(3);

        DateTime endTime = DateTime.Now.AddSeconds(Duration);

        while (DateTime.Now < endTime)
        {
            string question = Dequeue(_questionQueue, _questions);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"\n  ➤  {question} ");
            Console.ResetColor();

            int remaining = Math.Max(3, (int)(endTime - DateTime.Now).TotalSeconds);
            int pause = Math.Min(6, remaining);
            ShowSpinner(pause);

            if (DateTime.Now >= endTime) break;
        }

        DisplayEndMessage();
    }

    private List<T> Shuffle<T>(List<T> list)
    {
        var r = new Random();
        return list.OrderBy(_ => r.Next()).ToList();
    }

    private T Dequeue<T>(Queue<T> queue, List<T> source)
    {
        if (queue.Count == 0)
        {
            foreach (var item in Shuffle(source))
                queue.Enqueue(item);
        }
        return queue.Dequeue();
    }
}