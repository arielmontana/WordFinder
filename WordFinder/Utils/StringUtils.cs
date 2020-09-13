using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WordFinder.Utils
{
    public static class StringUtils
    {
        public static int CountWordIntoString(string wordToSearch, string text)
        {
            if (string.IsNullOrEmpty(text)) return default;
            if (string.IsNullOrEmpty(wordToSearch)) return default;

            int maxIndexToUse = text.Length - wordToSearch.Length;
            int counter = 0;
            for (int i = 0; i <= maxIndexToUse; i++)
            {
                var subWord = text.Substring(i, wordToSearch.Length);
                if (wordToSearch.Equals(subWord))
                {
                    counter++;
                }
            }
            return counter;
        }

        public static IEnumerable<string> InvertStringMatrix(IEnumerable<string> matrix) 
        {
            if (matrix == null) return new List<string>();
            if (!matrix.Any()) return new List<string>();

            int columnLenght = matrix.Max(x => x.Length);
            List<string> invertedMatrix = new List<string>();
            for (int column = 0; column < columnLenght; column++)
            {
                string columnAsRow = string.Empty;
                foreach (var row in matrix)
                {
                    columnAsRow = string.Concat(columnAsRow, row.Substring(column, 1));
                }
                invertedMatrix.Add(columnAsRow);
            }
            return invertedMatrix;
        }
    }
}
