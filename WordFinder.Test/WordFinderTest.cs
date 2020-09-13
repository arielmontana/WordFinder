using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using NUnit.Framework;

namespace WordFinder.Test
{
    [TestFixture]
    public class WordFinderTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test, Description("WordFinder constructor should throw an exception when parameter is empty")]
        public void WordFinder_ConsturctorPameter_Empty_ShouldThrowException()
        {
            var ex = Assert.Throws<Exception>(() => new WordFinder(Enumerable.Empty<string>()));
            Assert.AreEqual("matrix must have values", ex.Message);
        }

        [Test, Description("WordFinder constructor should throw an exception when parameter is null")]
        public void WordFinder_ConsturctorPameter_Null_ShouldThrowException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new WordFinder(null));
            Assert.AreEqual("matrix", ex.ParamName);
        }

        [Test, Description("WordFinder constructor should throw an exception when IEnumerable contains strings with different lengths")]
        public void WordFinder_ConsturctorPameter_EnumerableInvalidStrings_ShouldThrowException()
        {
            IEnumerable<string> list = new List<string>() { "one", "two", "three" };
            var ex = Assert.Throws<Exception>(() => new WordFinder(list));
            Assert.AreEqual("All matrix's strings should have the same length", ex.Message);
        }

        [Test, Description("WordFinder should find a word horizontally")]
        public void WordFinder_FindMethod__ShouldFind_OneWord_Horizontally()
        {
            IEnumerable<string> matrix = new List<string>() { "abonek", "detwof", "gthrte" };
            IEnumerable<string> wordstream = new List<string>() { "one" };
            var wordFinder = new WordFinder(matrix);
            var result = wordFinder.Find(wordstream);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 1);
        }

        [Test, Description("WordFinder should find more than one word horizontally")]
        public void WordFinder_FindMethod__ShouldFind_ManyWords_Horizontally()
        {
            IEnumerable<string> matrix = new List<string>() { "abonek", "detwof", "gthrte" };
            IEnumerable<string> wordstream = new List<string>() { "one", "two" };
            var wordFinder = new WordFinder(matrix);
            var result = wordFinder.Find(wordstream);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 2);
        }

        [Test, Description("WordFinder should find a word vertically")]
        public void WordFinder_FindMethod__ShouldFind_OneWord_Vertically()
        {
            IEnumerable<string> matrix = new List<string>() { "aobgxk", "dnntwf", "gethrt" };
            IEnumerable<string> wordstream = new List<string>() { "one" };
            var wordFinder = new WordFinder(matrix);
            var result = wordFinder.Find(wordstream);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 1);
        }

        [Test, Description("WordFinder should find more than one word vertically")]
        public void WordFinder_FindMethod__ShouldFind_ManyWords_Vertically()
        {
            IEnumerable<string> matrix = new List<string>() { "obtnek", "newxof", "etorte" };
            IEnumerable<string> wordstream = new List<string>() { "one", "two" };
            var wordFinder = new WordFinder(matrix);
            var result = wordFinder.Find(wordstream);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 2);
        }

        [Test, Description("WordFinder should find more than one word vertically")]
        public void WordFinder_FindMethod__ShouldFindWords_HorizontallyAndVertically()
        {
            IEnumerable<string> matrix = new List<string>() { "onetwo", "nexwof", "etrote" };
            IEnumerable<string> wordstream = new List<string>() { "one", "two" };
            var wordFinder = new WordFinder(matrix);
            var result = wordFinder.Find(wordstream);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 2);
        }

        [Test, Description("Find method should return an empty list when wordstream parameter is null")]
        public void WordFinder_FindMethod__ShouldReturnsEmpty_WhenWordIsNull()
        {
            IEnumerable<string> matrix = new List<string>() { "onetwo", "nexwof", "etrote" };
            IEnumerable<string> wordstream = null;
            var wordFinder = new WordFinder(matrix);
            var result = wordFinder.Find(wordstream);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 0);
        }

        [Test, Description("Find method should return an empty list when wordstream parameter is empty")]
        public void WordFinder_FindMethod__ShouldReturnsEmpty_WhenWordStreamIsEmpty()
        {
            IEnumerable<string> matrix = new List<string>() { "onetwo", "nexwof", "etrote" };
            IEnumerable<string> wordstream = new List<string>();
            var wordFinder = new WordFinder(matrix);
            var result = wordFinder.Find(wordstream);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 0);
        }

        [Test, Description("WordFinder should find all words and returns only top 10 repeated words")]
        public void WordFinder_FindMethod__ShouldReturnsTenValues()
        {
            IEnumerable<string> matrix = new List<string>() 
            {
                "onetwothrefourfivesi", "xseveneightninetenon", "etwothreefourfivesix", "seveneightninetenone", "twothreefourfivesixs",
                "veneightninetenonetw","htreefourfivesixseve","neightninetenonetwot","othreefourfivesixsev","eveneightninetenonet",
                "onetwothrefourfivesi","neightelevennonetwot","htreefourfivetwelvee","eneightninetenonetwo","wothreefourfivesixse",
                "eleveneightninetenon","elevenefourfivesixse","elevenninetenonetwot","threefourfivesixseve","veneightninetenonetw"
            };
            IEnumerable<string> wordstream = new List<string>() 
            { 
                "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve" 
            };
            var wordFinder = new WordFinder(matrix);
            var result = wordFinder.Find(wordstream);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 10);
        }

        [Test, Description("WordFinder should find all words and returns top 10 ordered by repeated words")]
        public void WordFinder_FindMethod__ShouldReturnsTenValues_OrderByRepetition()
        {
            IEnumerable<string> matrix = new List<string>()
            {
                "onetwotwothreethreex", "threefourfourfourfou", "fourfivefivefivefive", "fivesixsixsixsixsixs", "sixsevensevensevense",
                "sevensevensevenseven","eighteighteighteight","eighteighteighteight","nineninenineninenine","nineninenineninetent",
                "tentententententente","tententeneleveneleve","elevenelevenelevenel","elevenelevenelevenel","elevenelevenelevenel",
                "eleventwelvetwelveef","twelvetwelvetwelvess","twelvetwelvetwelvefh","twelvetwelvetwelvevs","twelvedskalñfañljkkf"
            };
            IEnumerable<string> wordstream = new List<string>()
            {
                "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve"
            };
            var wordFinder = new WordFinder(matrix);
            var result = wordFinder.Find(wordstream);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 10);
            Assert.AreEqual(result.ElementAt(0), "twelve");
            Assert.AreEqual(result.ElementAt(1), "eleven");
            Assert.AreEqual(result.ElementAt(2), "ten");
            Assert.AreEqual(result.ElementAt(3), "nine");
            Assert.AreEqual(result.ElementAt(4), "eight");
            Assert.AreEqual(result.ElementAt(5), "seven");
            Assert.AreEqual(result.ElementAt(6), "six");
            Assert.AreEqual(result.ElementAt(7), "five");
            Assert.AreEqual(result.ElementAt(8), "four");
            Assert.AreEqual(result.ElementAt(9), "three");
        }
    }
}