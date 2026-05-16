using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JournalProgram
{
    public class Journal
    {
        private List<Entry> _entries;  // encapsulated storage

        public Journal()
        {
            _entries = new List<Entry>();
        }

        // AddEntry hides internal storage changes
        public void AddEntry(Entry entry)
        {
            _entries.Add(entry);
        }

        public void DisplayAll()
        {
            if (_entries.Count == 0)
            {
                Console.WriteLine("The journal is empty.");
                return;
            }
            foreach (Entry entry in _entries)
            {
                entry.Display();
            }
        }

        public void SaveToFile(string filename)
        {
            if (!filename.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            {
                filename += ".csv";
            }

            using StreamWriter writer = new StreamWriter(filename);
            writer.WriteLine("Date,Prompt,Mood,Response");

            foreach (Entry entry in _entries)
            {
                writer.WriteLine(entry.ToCsvString());
            }
            Console.WriteLine($"Journal saved to {filename} ({_entries.Count} entries)");
        }

        public void LoadFromFile(string filename)
        {
            if (!filename.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            {
                filename += ".csv";
            }

            if (!File.Exists(filename))
            {
                Console.WriteLine("File not found.");
                return;
            }

            List<Entry> newEntries = new List<Entry>();
            string[] lines = File.ReadAllLines(filename);

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (i == 0 && line.StartsWith("Date,"))
                {
                    continue;
                }

                if (!string.IsNullOrWhiteSpace(line))
                {
                    try
                    {
                        Entry entry = Entry.FromCsvLine(line);
                        newEntries.Add(entry);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine($"Skipping malformed line {i + 1}: {line}");
                    }
                }
            }
            _entries = newEntries;
            Console.WriteLine($"Journal loaded from {filename} ({_entries.Count} entries)");
        }
    }
}