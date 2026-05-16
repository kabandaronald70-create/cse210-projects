using System;
using System.Collections.Generic;

namespace JournalProgram
{
    public class Entry
    {
        public string Date { get; set; }
        public string PromptText { get; set; }
        public string Mood { get; set; }
        public string EntryText { get; set; }

        // Constructor
        public Entry(string date, string promptText, string mood, string entryText)
        {
            Date = date;
            PromptText = promptText;
            Mood = mood;
            EntryText = entryText;
        }

        // Display method – encapsulation allows future changes without breaking Journal display
        public void Display()
        {
            Console.WriteLine($"Date: {Date}");
            Console.WriteLine($"Prompt: {PromptText}");
            Console.WriteLine($"Mood: {Mood}");
            Console.WriteLine($"Response: {EntryText}");
            Console.WriteLine(new string('-', 40));
        }

        // Convert to CSV string with proper escaping for commas, quotes, and newlines
        public string ToCsvString()
        {
            return $"{EscapeForCsv(Date)},{EscapeForCsv(PromptText)},{EscapeForCsv(Mood)},{EscapeForCsv(EntryText)}";
        }

        private static string EscapeForCsv(string field)
        {
            if (field.Contains("\"") || field.Contains(",") || field.Contains("\n") || field.Contains("\r"))
            {
                field = field.Replace("\"", "\"\"");
                return $"\"{field}\"";
            }
            return field;
        }

        // Create an Entry from a CSV line
        public static Entry FromCsvLine(string line)
        {
            string[] parts = SplitCsvLine(line);
            if (parts.Length == 4)
            {
                return new Entry(parts[0], parts[1], parts[2], parts[3]);
            }
            throw new FormatException("Invalid CSV line format");
        }

        private static string[] SplitCsvLine(string line)
        {
            var result = new List<string>();
            bool inQuotes = false;
            var field = new System.Text.StringBuilder();

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        field.Append('"');
                        i++;
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == ',' && !inQuotes)
                {
                    result.Add(field.ToString());
                    field.Clear();
                }
                else
                {
                    field.Append(c);
                }
            }

            result.Add(field.ToString());
            return result.ToArray();
        }

    }
}