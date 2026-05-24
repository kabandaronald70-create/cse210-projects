using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScriptureMemorizer
{
    class Program
    {
        static void Main(string[] args)
        {
            // ----------------------------------------------
            // CREATIVITY & EXCEEDING REQUIREMENTS REPORT:
            // 1. Scripture Library from file: The program reads multiple scriptures
            //    from "scriptures.txt" and randomly selects one each time.
            // 2. Smart hiding: Only words that are still visible are chosen for hiding,
            //    ensuring every hide action makes progress.
            // 3. Progress indicator: After each hide, the number of remaining visible
            //    words is displayed to help users track memorization.
            // ----------------------------------------------

            Scripture scripture = LoadRandomScriptureFromFile("scriptures.txt");
            if (scripture == null)
            {
                Console.WriteLine("Error: No scriptures found. Using a default scripture.");
                Reference defaultRef = new Reference("John", 3, 16);
                scripture = new Scripture(defaultRef, "For God so loved the world that he gave his only Son.");
            }

            bool quit = false;
            Random random = new Random();

            while (!quit && !scripture.IsCompletelyHidden())
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine($"\n[Words still visible: {scripture.GetVisibleWordCount()}]");
                Console.Write("\nPress Enter to hide more words, or type 'quit' to exit: ");
                string input = Console.ReadLine();

                if (input?.ToLower() == "quit")
                {
                    quit = true;
                }
                else
                {
                    // Hide a few random words (between 2 and 4, but at least 1)
                    int wordsToHide = Math.Min(random.Next(2, 5), scripture.GetVisibleWordCount());
                    if (wordsToHide == 0 && !scripture.IsCompletelyHidden())
                        wordsToHide = 1; // ensure progress if only one word left
                    scripture.HideRandomWords(wordsToHide);
                }
            }

            if (!quit && scripture.IsCompletelyHidden())
            {
                Console.Clear();
                Console.WriteLine(scripture.GetDisplayText());
                Console.WriteLine("\nAll words are hidden! Great memorization practice. Program ending.");
            }
        }

        /// <summary>
        /// Reads scriptures from a text file and returns a random one.
        /// File format: each line -> "reference|text"
        /// Example: "John 3:16|For God so loved the world..."
        /// Multi‑verse example: "Proverbs 3:5-6|Trust in the Lord with all thine heart..."
        /// </summary>
        static Scripture LoadRandomScriptureFromFile(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.WriteLine($"Warning: {filename} not found. Creating a sample file.");
                CreateSampleScriptureFile(filename);
            }

            var lines = File.ReadAllLines(filename);
            var validEntries = new List<(string refText, string scriptureText)>();

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                string[] parts = line.Split('|');
                if (parts.Length == 2)
                {
                    validEntries.Add((parts[0].Trim(), parts[1].Trim()));
                }
            }

            if (validEntries.Count == 0)
                return null;

            Random rand = new Random();
            var chosen = validEntries[rand.Next(validEntries.Count)];
            Reference reference = new Reference(chosen.refText);
            return new Scripture(reference, chosen.scriptureText);
        }

        static void CreateSampleScriptureFile(string filename)
        {
            string[] sampleScriptures = {
                "John 3:16|For God so loved the world that he gave his only Son.",
                "Proverbs 3:5-6|Trust in the Lord with all thine heart and lean not unto thine own understanding.",
                "Psalm 23:1|The Lord is my shepherd; I shall not want.",
                "2 Nephi 2:25|Adam fell that men might be; and men are, that they might have joy.",
                "Moroni 10:4-5|And when ye shall receive these things, I would exhort you that ye would ask God."
            };
            File.WriteAllLines(filename, sampleScriptures);
        }
    }
}