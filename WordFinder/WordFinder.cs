using System;
using System.Collections.Generic;
using System.Linq;
using WordFinder.Config;
using WordFinder.Utils;

namespace WordFinder
{
    public class WordFinder
    {
        private readonly IEnumerable<string> matrix;

        private IEnumerable<string> _invertedMatrix;

        private IEnumerable<string> InvertedMatrix
        {
            get
            {
                if (_invertedMatrix == null)
                {
                    _invertedMatrix = StringUtils.InvertStringMatrix(this.matrix);
                }
                return _invertedMatrix;
            }
        }

        #region Public Methods

        public WordFinder(IEnumerable<string> matrix)
        {
            // New instance validations
            if (matrix == null)
                throw new ArgumentNullException(nameof(matrix));
            if (!matrix.Any())
                throw new Exception($"{nameof(matrix)} must have values");
            if (matrix.Any(x => x.Length != WordFinderConfig.WordFinder.MaxColumnSize))
                throw new Exception("The matrix's strings must have 64 chars");
            if (matrix.Count() != WordFinderConfig.WordFinder.MaxRowSize)
                throw new Exception("The matrix must have 64 string items");

            this.matrix = matrix;
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            var wordsCounter = CreateWordsCounter(wordstream);
            //Search words horizontally and update counter
            SearchWord(wordsCounter, matrix);
            //Search words vertically and update counter
            SearchWord(wordsCounter, InvertedMatrix);
            //Returns Top 10 words (complete with empty when is lower)
            return NormalizeResult(wordsCounter);
        }

        private void SearchWord(Dictionary<string, int> wordsCounter, IEnumerable<string> matrix)
        {
            if (!wordsCounter.Keys.Any()) return;

            var words = wordsCounter.Keys.ToList();
            foreach (var row in matrix)
            {
                foreach (string word in words)
                {
                    wordsCounter[word] += StringUtils.CountWordIntoString(word, row);
                }
            }
        }

        private IEnumerable<string> NormalizeResult(Dictionary<string, int> wordsCounter)
        {
            var numbersToTake = WordFinderConfig.WordFinder.WordsToReturn;
            var words = (from wc in wordsCounter where wc.Value > 0 orderby wc.Value descending select wc.Key).Take(numbersToTake).ToList();
            while (words.Count() < numbersToTake)
            {
                words.Add(string.Empty);
            }
            return words;
        }

        #endregion Public Methods

        private Dictionary<string, int> CreateWordsCounter(IEnumerable<string> wordstream)
        {
            if (wordstream == null) return new Dictionary<string, int>();

            var wordsCounter = new Dictionary<string, int>();
            foreach (var word in wordstream)
            {
                if (wordsCounter.ContainsKey(word)) continue;
                wordsCounter.Add(word, 0);
            }
            return wordsCounter;
        }
    }
}