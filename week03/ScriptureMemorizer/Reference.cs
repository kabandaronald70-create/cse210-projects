using System;

namespace ScriptureMemorizer
{
    public class Reference
    {
        private string _book;
        private int _chapter;
        private int _startVerse;
        private int _endVerse;

        // Constructor for single verse (e.g., "John 3:16")
        public Reference(string book, int chapter, int verse)
        {
            _book = book;
            _chapter = chapter;
            _startVerse = verse;
            _endVerse = verse;
        }

        // Constructor for verse range (e.g., "Proverbs 3:5-6")
        public Reference(string book, int chapter, int startVerse, int endVerse)
        {
            _book = book;
            _chapter = chapter;
            _startVerse = startVerse;
            _endVerse = endVerse;
        }

        // Constructor that parses a string like "John 3:16" or "Proverbs 3:5-6"
        public Reference(string referenceString)
        {
            string[] parts = referenceString.Split(' ');
            if (parts.Length < 2)
                throw new ArgumentException("Invalid reference format");

            _book = string.Join(" ", parts.Take(parts.Length - 1));
            string chapterVerse = parts.Last();

            if (chapterVerse.Contains('-'))
            {
                string[] cv = chapterVerse.Split(':');
                string[] verses = cv[1].Split('-');
                _chapter = int.Parse(cv[0]);
                _startVerse = int.Parse(verses[0]);
                _endVerse = int.Parse(verses[1]);
            }
            else
            {
                string[] cv = chapterVerse.Split(':');
                _chapter = int.Parse(cv[0]);
                _startVerse = int.Parse(cv[1]);
                _endVerse = _startVerse;
            }
        }

        public override string ToString()
        {
            if (_startVerse == _endVerse)
                return $"{_book} {_chapter}:{_startVerse}";
            else
                return $"{_book} {_chapter}:{_startVerse}-{_endVerse}";
        }
    }
}