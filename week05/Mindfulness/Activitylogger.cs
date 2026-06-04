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