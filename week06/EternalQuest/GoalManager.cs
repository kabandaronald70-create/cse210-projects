using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class GoalManager
{
    private List<Goal> _goals;
    private int _score;
    private string _lastSavedDate;   // for daily bonus

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
        _lastSavedDate = "";
    }

    public void Start()
    {
        // Load any previous data
        LoadGoals();

        // Daily bonus check
        string today = DateTime.Now.ToString("yyyy-MM-dd");
        if (_lastSavedDate != today)
        {
            Console.WriteLine($"\n🌟 Daily bonus! You get +50 points for checking in today! 🌟");
            _score += 50;
            _lastSavedDate = today;
        }

        bool running = true;
        while (running)
        {
            DisplayPlayerInfo();
            Console.WriteLine("\nMenu Options:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Record Event");
            Console.WriteLine("  4. Save Goals");
            Console.WriteLine("  5. Load Goals");
            Console.WriteLine("  6. Quit");
            Console.Write("Select a choice from the menu: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": CreateGoal(); break;
                case "2": ListGoals(); break;
                case "3": RecordEvent(); break;
                case "4": SaveGoals(); break;
                case "5": LoadGoals(); break;
                case "6": running = false; break;
                default: Console.WriteLine("Invalid choice. Try again."); break;
            }
        }
    }

    private void DisplayPlayerInfo()
    {
        int level = _score / 1000 + 1;
        string levelTitle = GetLevelTitle(level);
        Console.WriteLine($"\n╔════════════════════════════════════╗");
        Console.WriteLine($"║  Score: {_score}  |  {levelTitle}  ║");
        Console.WriteLine($"╚════════════════════════════════════╝");
    }

    private string GetLevelTitle(int level)
    {
        switch (level)
        {
            case 1: return "Level 1: Novice";
            case 2: return "Level 2: Apprentice";
            case 3: return "Level 3: Journeyman";
            case 4: return "Level 4: Expert";
            case 5: return "Level 5: Master";
            case 6: return "Level 6: Grandmaster";
            default: return $"Level {level}: Legend";
        }
    }

    private void CreateGoal()
    {
        Console.WriteLine("\nThe types of goals are:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");
        Console.WriteLine("  4. Negative Goal (Bad Habit) - loses points");
        Console.WriteLine("  5. Progress Goal (incremental steps)");
        Console.Write("Which type of goal would you like to create? ");
        string typeChoice = Console.ReadLine();

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine();
        Console.Write("What is a short description of it? ");
        string desc = Console.ReadLine();

        switch (typeChoice)
        {
            case "1": // Simple
                Console.Write("How many points is this goal worth? ");
                int points = int.Parse(Console.ReadLine());
                _goals.Add(new SimpleGoal(name, desc, points));
                break;
            case "2": // Eternal
                Console.Write("How many points per completion? ");
                points = int.Parse(Console.ReadLine());
                _goals.Add(new EternalGoal(name, desc, points));
                break;
            case "3": // Checklist
                Console.Write("How many points per completion? ");
                points = int.Parse(Console.ReadLine());
                Console.Write("How many times to complete? ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("What is the bonus for finishing? ");
                int bonus = int.Parse(Console.ReadLine());
                _goals.Add(new ChecklistGoal(name, desc, points, target, bonus));
                break;
            case "4": // Negative
                Console.Write("How many points to LOSE each time? (enter positive) ");
                int losePoints = int.Parse(Console.ReadLine());
                _goals.Add(new NegativeGoal(name, desc, losePoints));
                break;
            case "5": // Progress
                Console.Write("Points per step? ");
                points = int.Parse(Console.ReadLine());
                Console.Write("Total number of steps? ");
                int steps = int.Parse(Console.ReadLine());
                Console.Write("Bonus points when fully complete? ");
                int bonusProgress = int.Parse(Console.ReadLine());
                _goals.Add(new ProgressGoal(name, desc, points, steps, bonusProgress));
                break;
            default:
                Console.WriteLine("Invalid goal type.");
                break;
        }
        Console.WriteLine("Goal created!");
    }

    private void ListGoals()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("\nNo goals created yet.");
            return;
        }
        Console.WriteLine("\nYour Goals:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    private void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("\nNo goals to record. Create a goal first.");
            return;
        }
        ListGoals();
        Console.Write("\nWhich goal did you accomplish? (enter number): ");
        int index = int.Parse(Console.ReadLine()) - 1;
        if (index >= 0 && index < _goals.Count)
        {
            Goal goal = _goals[index];
            int pointsEarned = goal.RecordEvent();
            if (pointsEarned != 0)
            {
                _score += pointsEarned;
                Console.WriteLine($"\n🎉 Congratulations! You earned {pointsEarned} points! 🎉");
                if (pointsEarned < 0)
                    Console.WriteLine("⚠️  Remember: this is a negative goal. Try to avoid it next time! ⚠️");
            }
            else
            {
                Console.WriteLine("\nThis goal is already complete (or cannot be recorded further).");
            }
            // Show updated score
            DisplayPlayerInfo();
        }
        else
        {
            Console.WriteLine("Invalid goal number.");
        }
    }

    public void SaveGoals()
    {
        using (StreamWriter outputFile = new StreamWriter("goals.txt"))
        {
            // Save score and last saved date
            outputFile.WriteLine($"{_score}|{_lastSavedDate}");
            foreach (Goal goal in _goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }
        }
        Console.WriteLine("Goals saved successfully!");
    }

    public void LoadGoals()
    {
        if (!File.Exists("goals.txt"))
        {
            Console.WriteLine("No save file found. Starting fresh.");
            return;
        }
        string[] lines = File.ReadAllLines("goals.txt");
        if (lines.Length == 0) return;

        // First line: score and date
        string[] firstParts = lines[0].Split('|');
        _score = int.Parse(firstParts[0]);
        _lastSavedDate = firstParts.Length > 1 ? firstParts[1] : "";

        _goals.Clear();
        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split('|');
            string type = parts[0];
            switch (type)
            {
                case "SimpleGoal":
                    SimpleGoal sg = new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]));
                    if (bool.Parse(parts[4])) sg.RecordEvent(); // mark complete if true
                    _goals.Add(sg);
                    break;
                case "EternalGoal":
                    _goals.Add(new EternalGoal(parts[1], parts[2], int.Parse(parts[3])));
                    break;
                case "ChecklistGoal":
                    ChecklistGoal cg = new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[5]), int.Parse(parts[4]));
                    int current = int.Parse(parts[6]);
                    for (int j = 0; j < current; j++) cg.RecordEvent();
                    _goals.Add(cg);
                    break;
                case "NegativeGoal":
                    _goals.Add(new NegativeGoal(parts[1], parts[2], int.Parse(parts[3])));
                    break;
                case "ProgressGoal":
                    ProgressGoal pg = new ProgressGoal(parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[5]), int.Parse(parts[4]));
                    int step = int.Parse(parts[6]);
                    for (int j = 0; j < step; j++) pg.RecordEvent();
                    _goals.Add(pg);
                    break;
            }
        }
        Console.WriteLine("Goals loaded successfully!");
    }
}
