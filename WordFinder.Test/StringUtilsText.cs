using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using WordFinder.Utils;

namespace WordFinder.Test
{
    [TestFixture]
    public class StringUtilsTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test, Description("CountWordIntoString method should return 0 when there are no matches")]
        public void StringUtils_CountWordIntoString_ShouldReturnZero_WhenNoMatches ()
        {
            string text = "helloworldandhelloworldfinder";
            string word = "indestructible";
            var amount = StringUtils.CountWordIntoString(word, text);
            Assert.AreEqual(0, amount);
        }

        [Test, Description("CountWordIntoString method should return 0 when text is empty")]
        public void StringUtils_CountWordIntoString_ShouldReturnZero_WhenTextIsEmpty()
        {
            string text = "";
            string word = "indestructible";
            var amount = StringUtils.CountWordIntoString(word, text);
            Assert.AreEqual(0, amount);
        }

        [Test, Description("CountWordIntoString method should return 0 when text is null")]
        public void StringUtils_CountWordIntoString_ShouldReturnZero_WhenTextIsNull()
        {
            string text = null;
            string word = "indestructible";
            var amount = StringUtils.CountWordIntoString(word, text);
            Assert.AreEqual(0, amount);
        }

        [Test, Description("CountWordIntoString method should return 0 when word is empty")]
        public void StringUtils_CountWordIntoString_ShouldReturnZero_WhenWordIsEmpty()
        {
            string text = "helloworldandhelloworldfinder";
            string word = "";
            var amount = StringUtils.CountWordIntoString(word, text);
            Assert.AreEqual(0, amount);
        }

        [Test, Description("CountWordIntoString method should return 0 when word is null")]
        public void StringUtils_CountWordIntoString_ShouldReturnZero_WhenWordIsNull()
        {
            string text = "helloworldandhelloworldfinder";
            string word = null;
            var amount = StringUtils.CountWordIntoString(word, text);
            Assert.AreEqual(0, amount);
        }

        [Test, Description("CountWordIntoString method should return number of words matches")]
        public void StringUtils_CountWordIntoString_ShouldReturn_MatchesWords()
        {
            string text = "helloworldandhelloworldfinderhellohelo";
            string word = "hello";
            var amount = StringUtils.CountWordIntoString(word, text);
            Assert.AreEqual(3, amount);
        }

        [Test, Description("InvertStringMatrix method should return empty instance if parameter used is null")]
        public void StringUtils_InvertStringMatrix_ShouldReturnEmpty_WhenParameterIsNull()
        {
            IEnumerable<string> paramter = null;
            var invertedMatrix = StringUtils.InvertStringMatrix(paramter);
            Assert.IsNotNull(invertedMatrix);
            Assert.AreEqual(0, invertedMatrix.Count());
        }

        [Test, Description("InvertStringMatrix method should return an empty instance if parameter used is empty")]
        public void StringUtils_InvertStringMatrix_ShouldReturnEmpty_WhenParameterIsEmpty()
        {
            IEnumerable<string> paramter = new List<string>();
            var invertedMatrix = StringUtils.InvertStringMatrix(paramter);
            Assert.IsNotNull(invertedMatrix);
            Assert.AreEqual(0, invertedMatrix.Count());
        }

        [Test, Description("InvertStringMatrix method should return string matrix inverted")]
        public void StringUtils_InvertStringMatrix_ShouldReturn_StringMatrixInverted()
        {
            IEnumerable<string> paramter = new List<string>() { "abc", "def", "ghi"};
            IEnumerable<string> expectedResult = new List<string>() { "adg", "beh", "cfi" };
            var invertedMatrix = StringUtils.InvertStringMatrix(paramter);
            Assert.IsNotNull(invertedMatrix);
            Assert.AreEqual(expectedResult.ElementAt(0), invertedMatrix.ElementAt(0));
            Assert.AreEqual(expectedResult.ElementAt(1), invertedMatrix.ElementAt(1));
            Assert.AreEqual(expectedResult.ElementAt(2), invertedMatrix.ElementAt(2));
        }
    }
}
