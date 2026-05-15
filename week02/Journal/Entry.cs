namespace JournalProgram
{
    public class Entry
    {
        public string Date { get; set; }
        public string PromptText { get; set; }
        public string EntryText { get; set; }

        // Constructor
        public Entry(string date, string promptText, string entryText)
        {
            Date = date;
            PromptText = promptText;
            EntryText = entryText;
        }

        // Display method – encapsulation allows future changes without breaking Journal display
        public void Display()
        {
            Console.WriteLine($"Date: {Date}");
            Console.WriteLine($"Prompt: {PromptText}");
            Console.WriteLine($"Response: {EntryText}");
            Console.WriteLine(new string('-', 40));
        }

        // Convert to CSV string with proper escaping for commas and quotes
        public string ToCsvString()
        {
            string escapedDate = EscapeForCsv(Date);
            string escapedPrompt = EscapeForCsv(PromptText);
            string escapedEntry = EscapeForCsv(EntryText);
            return $"{escapedDate}|{escapedPrompt}|{escapedEntry}";
        }

        private string EscapeForCsv(string field)
        {
            if (field.Contains('|') || field.Contains('"'))
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
            if (parts.Length == 3)
            {
                return new Entry(parts[0], parts[1], parts[2]);
            }
            throw new FormatException("Invalid CSV line format");
        }

        private static string[] SplitCsvLine(string line)
        {
            var result = new List<string>();
            bool inQuotes = false;
            int start = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == '"')
                    inQuotes = !inQuotes;
                else if (line[i] == '|' && !inQuotes)
                {
                    result.Add(UnescapeField(line.Substring(start, i - start)));
                    start = i + 1;
                }
            }
            result.Add(UnescapeField(line.Substring(start)));
            return result.ToArray();
        }

        private static string UnescapeField(string field)
        {
            if (field.StartsWith("\"") && field.EndsWith("\""))
            {
                field = field.Substring(1, field.Length - 2);
                field = field.Replace("\"\"", "\"");
            }
            return field;
        }
    }
}