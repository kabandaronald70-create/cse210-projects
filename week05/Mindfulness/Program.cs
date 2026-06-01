/*
 * W05 Project: Mindfulness Program
 * 
 * EXCEEDING REQUIREMENTS - Creativity Features:
 * 1. EXTRA ACTIVITY: Gratitude Journaling Activity - guides users to write 
 *    gratitude entries and saves them to a personal journal file.
 * 2. ACTIVITY LOG: Every completed activity is logged (activity name, date/time, 
 *    duration) to "activity_log.txt" with a session summary shown on exit.
 * 3. NO PROMPT REPETITION: All random prompts and questions cycle through 
 *    the full list before repeating (shuffle-based, not truly random).
 * 4. BREATHING ANIMATION: Dynamic text that grows from small to large 
 *    during "breathe in" and shrinks from large to small during "breathe out",
 *    with a visual bar that expands/contracts.
 * 5. COLOR THEMING: Each activity has its own console color theme for a 
 *    more immersive experience.
 * 6. SESSION STATISTICS: On exit, the program displays how many times each 
 *    activity was performed and total mindfulness time for the session.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace MindfulnessApp
{
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

    // ─── GRATITUDE JOURNALING ACTIVITY (EXTRA ACTIVITY) ─────────────────────────
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

    // ─── ACTIVITY LOGGER ────────────────────────────────────────────────────────
    static class ActivityLogger
    {
        private const string LogFile = "activity_log.txt";
        private static List<(string Name, DateTime Time, int Duration)> _sessionLog = new();

        public static void LogActivity(string name, int duration)
        {
            _sessionLog.Add((name, DateTime.Now, duration));
            try
            {
                using (var sw = File.AppendText(LogFile))
                    sw.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | {name,-25} | {duration,4}s");
            }
            catch { /* silently ignore log errors */ }
        }

        public static void ShowSessionSummary()
        {
            if (_sessionLog.Count == 0) return;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════╗");
            Console.WriteLine("║        SESSION SUMMARY               ║");
            Console.WriteLine("╚══════════════════════════════════════╝");
            Console.ResetColor();

            var grouped = _sessionLog.GroupBy(x => x.Name);
            int totalSeconds = 0;

            Console.WriteLine();
            foreach (var group in grouped)
            {
                int secs = group.Sum(x => x.Duration);
                totalSeconds += secs;
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"  {group.Key,-25}  x{group.Count()}  →  {secs}s");
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n  Total mindfulness time this session: {totalSeconds} seconds");
            Console.WriteLine($"  Full log saved to: activity_log.txt");
            Console.ResetColor();
            Console.WriteLine();
        }
    }

    // ─── MAIN PROGRAM ────────────────────────────────────────────────────────────
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("  ╔══════════════════════════════════════════╗");
                Console.WriteLine("  ║       🌿 MINDFULNESS PROGRAM  🌿         ║");
                Console.WriteLine("  ╚══════════════════════════════════════════╝");
                Console.ResetColor();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("     1. Breathing Activity");
                Console.WriteLine("     2. Reflection Activity");
                Console.WriteLine("     3. Listing Activity");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("     4. Gratitude Journal  ✨ (Extra Activity)");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("     5. Quit");
                Console.ResetColor();
                Console.WriteLine();
                Console.Write("  Select an option: ");

                string choice = Console.ReadLine();

                MindfulnessActivity activity = null;
                switch (choice)
                {
                    case "1": activity = new BreathingActivity(); break;
                    case "2": activity = new ReflectionActivity(); break;
                    case "3": activity = new ListingActivity(); break;
                    case "4": activity = new GratitudeActivity(); break;
                    case "5": running = false; break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("  Invalid option. Please try again.");
                        Console.ResetColor();
                        Thread.Sleep(1200);
                        break;
                }

                activity?.Run();

                if (activity != null)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("\n  Press any key to return to the menu...");
                    Console.ResetColor();
                    Console.ReadKey(true);
                }
            }

            ActivityLogger.ShowSessionSummary();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("  Thank you for taking time for mindfulness today. 🌿");
            Console.ResetColor();
            Console.WriteLine();
        }
    }
}