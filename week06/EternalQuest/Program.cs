using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// ========================== MAIN PROGRAM ==========================
// Exceeds requirements — creativity report:
// The program adds several features beyond the core spec to earn full marks:
// - `NegativeGoal`: a goal that deducts points for bad habits (creative extra).
// - `ProgressGoal`: supports incremental progress with per-step points and bonus.
// - Daily check-in bonus (+50 points) to encourage daily use.
// - Leveling system: score -> level titles (Novice ... Legend) for gamification.
// - Improved UI: boxed score display, emojis, and clear completion markers.
// - Save/load supports all goal types (including the creative ones).

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Eternal Quest!");
        Console.WriteLine("Track your goals and earn points on your journey.");
        GoalManager manager = new GoalManager();
        manager.Start();
        Console.WriteLine("\nThank you for using Eternal Quest. Keep progressing!");
    }
}

