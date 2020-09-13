﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
            if (matrix.Min(x => x.Length) != matrix.Max(x => x.Length))
                throw new Exception("All matrix's strings should have the same length");
            
            this.matrix = matrix;
        }

        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            if (wordstream == null) return new List<string>();

            var wordsCounter = CreateWordsCounter(wordstream);
            //Search words horizontally and update counter
            SearchWord(wordsCounter, matrix);
            //Search words vertically and update counter
            SearchWord(wordsCounter, InvertedMatrix);
            return (from wc in wordsCounter where wc.Value > 0 orderby wc.Value descending select wc.Key).Take(10);
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
        
        
        #endregion

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