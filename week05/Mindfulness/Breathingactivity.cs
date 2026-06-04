// ─── BREATHING ACTIVITY ──────────────────────────────────────────────────────
class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity() : base(
        "Breathing Activity",
        "This activity will help you relax by walking you through breathing in and out slowly.\nClear your mind and focus on your breathing.")
    { }

    protected override ConsoleColor GetActivityColor() => ConsoleColor.Cyan;

    public override void Run()
    {
        DisplayStartMessage();

        DateTime endTime = DateTime.Now.AddSeconds(Duration);
        bool breatheIn = true;

        while (DateTime.Now < endTime)
        {
            int remaining = (int)(endTime - DateTime.Now).TotalSeconds;
            int breathSeconds = Math.Min(4, remaining);
            if (breathSeconds <= 0) break;

            if (breatheIn)
                AnimateBreatheIn(breathSeconds);
            else
                AnimateBreatheOut(breathSeconds);

            breatheIn = !breatheIn;
        }

        DisplayEndMessage();
    }

    private void AnimateBreatheIn(int seconds)
    {
        // Text grows: small → large, bar expands
        string[] sizes = { ".", "·", "o", "O", "◎", "◉", "●" };
        Console.WriteLine();
        int steps = seconds * 4;
        for (int i = 0; i < steps; i++)
        {
            int idx = Math.Min(i * sizes.Length / steps, sizes.Length - 1);
            int barLen = (i * 30) / steps;
            string bar = "[" + new string('█', barLen) + new string('░', 30 - barLen) + "]";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"\r  Breathe in...  {sizes[idx]}  {bar}  ");
            Console.ResetColor();
            Thread.Sleep(250);
        }
        Console.Write($"\r  Breathe in...  ●  [██████████████████████████████]  ");
        Thread.Sleep(300);
    }

    private void AnimateBreatheOut(int seconds)
    {
        // Text shrinks: large → small, bar contracts
        string[] sizes = { "●", "◉", "◎", "O", "o", "·", "." };
        Console.WriteLine();
        int steps = seconds * 4;
        for (int i = 0; i < steps; i++)
        {
            int idx = Math.Min(i * sizes.Length / steps, sizes.Length - 1);
            int barLen = 30 - (i * 30) / steps;
            string bar = "[" + new string('█', barLen) + new string('░', 30 - barLen) + "]";
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"\r  Breathe out... {sizes[idx]}  {bar}  ");
            Console.ResetColor();
            Thread.Sleep(250);
        }
        Console.Write($"\r  Breathe out... .  [░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░]  ");
        Thread.Sleep(300);
    }
}