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