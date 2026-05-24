using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptureMemorizer
{
    public class Scripture
    {
        private Reference _reference;
        private List<Word> _words;

        public Scripture(Reference reference, string text)
        {
            _reference = reference;
            // Split text into words (preserve punctuation as part of word)
            _words = text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                         .Select(w => new Word(w)).ToList();
        }

        public void HideRandomWords(int count)
        {
            // Get all visible (not hidden) words
            var visibleWords = _words.Where(w => !w.IsHidden()).ToList();
            if (visibleWords.Count == 0) return;

            Random rand = new Random();
            int wordsToHide = Math.Min(count, visibleWords.Count);
            // Select random visible words without replacement
            var selected = visibleWords.OrderBy(x => rand.Next()).Take(wordsToHide);
            foreach (var word in selected)
            {
                word.Hide();
            }
        }

        public string GetDisplayText()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(_reference.ToString());
            sb.Append(string.Join(" ", _words.Select(w => w.GetDisplayText())));
            return sb.ToString();
        }

        public bool IsCompletelyHidden()
        {
            return _words.All(w => w.IsHidden());
        }

        public int GetVisibleWordCount()
        {
            return _words.Count(w => !w.IsHidden());
        }
    }
}