using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using NUnit.Framework;
using WordFinder.Config;

namespace WordFinder.Test
{
    [TestFixture]
    public class WordFinderTest
    {
        [SetUp]
        public void Setup()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);           
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

        [Test, Description("WordFinder should find a word horizontally")]
        public void WordFinder_FindMethod__ShouldFind_OneWord_Horizontally()
        {
            IEnumerable<string> matrix = GetHorizontalTestMatrix();
            IEnumerable<string> wordstream = new List<string>() { "one" };
            var wordFinder = new WordFinder(matrix);
            var result = wordFinder.Find(wordstream);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 10);
            Assert.IsTrue(result.Contains("one"));
            Assert.AreEqual(result.Where(x => x != string.Empty).Count(), 1);
            Assert.AreEqual(result.Where(x => x == string.Empty).Count(), 9);
        }

        [Test, Description("WordFinder should find more than one word (not empty) horizontally")]
        public void WordFinder_FindMethod__ShouldFind_ManyWords_Horizontally()
        {
            IEnumerable<string> matrix = GetHorizontalTestMatrix();
            IEnumerable<string> wordstream = new List<string>() { "one", "two" };
            var wordFinder = new WordFinder(matrix);
            var result = wordFinder.Find(wordstream);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 10);
            Assert.IsTrue(result.Contains("one"));
            Assert.IsTrue(result.Contains("two"));
            Assert.IsTrue(result.Contains("one"));
            Assert.IsTrue(result.Contains("two"));
            Assert.AreEqual(result.Where(x => x == string.Empty).Count(), 8);
        }

        [Test, Description("WordFinder should find a word vertically")]
        public void WordFinder_FindMethod__ShouldFind_OneWord_Vertically()
        {
            IEnumerable<string> matrix = GetVerticalTestMatrix();
            IEnumerable<string> wordstream = new List<string>() { "one" };
            var wordFinder = new WordFinder(matrix);
            var result = wordFinder.Find(wordstream);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 10);
            Assert.AreEqual(result.Where(x => x != string.Empty).Count(), 1);
            Assert.IsTrue(result.Contains("one"));
            Assert.AreEqual(result.Where(x => x == string.Empty).Count(), 9);
        }

        [Test, Description("WordFinder should find more than one word (not empty) vertically")]
        public void WordFinder_FindMethod__ShouldFind_ManyWords_Vertically()
        {
            IEnumerable<string> matrix = GetVerticalTestMatrix();
            IEnumerable<string> wordstream = new List<string>() { "one", "two" };
            var wordFinder = new WordFinder(matrix);
            var result = wordFinder.Find(wordstream);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 10);
            Assert.IsTrue(result.Contains("one"));
            Assert.IsTrue(result.Contains("two"));
            Assert.AreEqual(result.Where(x => x != string.Empty).Count(), 2);
            Assert.AreEqual(result.Where(x => x == string.Empty).Count(), 8);
        }

        [Test, Description("WordFinder should find more than one word vertically")]
        public void WordFinder_FindMethod__ShouldFindWords_HorizontallyAndVertically()
        {
            IEnumerable<string> matrix = GetVerticalTestMatrix();
            IEnumerable<string> wordstream = new List<string>() { "one", "two" };
            var wordFinder = new WordFinder(matrix);
            var result = wordFinder.Find(wordstream);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 10);
            Assert.AreEqual(result.Where(x => x != string.Empty).Count(), 2);
            Assert.AreEqual(result.Where(x => x == string.Empty).Count(), 8);
        }

        [Test, Description("Find method should return an empty list when wordstream parameter is null")]
        public void WordFinder_FindMethod__ShouldReturnsEmpty_WhenWordIsNull()
        {
            IEnumerable<string> matrix = GetHorizontalTestMatrix();
            IEnumerable<string> wordstream = null;
            var wordFinder = new WordFinder(matrix);
            var result = wordFinder.Find(wordstream);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 10);
            Assert.AreEqual(result.Where(x => x == string.Empty).Count(), 10);
        }

        [Test, Description("Find method should return an empty list when wordstream parameter is empty")]
        public void WordFinder_FindMethod__ShouldReturnsEmpty_WhenWordStreamIsEmpty()
        {
            IEnumerable<string> matrix = GetHorizontalTestMatrix();
            IEnumerable<string> wordstream = new List<string>();
            var wordFinder = new WordFinder(matrix);
            var result = wordFinder.Find(wordstream);
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 10);
            Assert.AreEqual(result.Where(x => x == string.Empty).Count(), 10);
        }

        [Test, Description("WordFinder should find all words and returns only top 10 repeated words")]
        public void WordFinder_FindMethod__ShouldReturnsTenValues()
        {
            IEnumerable<string> matrix = GetHorizontalTestMatrix();
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
            IEnumerable<string> matrix = GetHorizontalTestMatrix();

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

        private IEnumerable<string> GetHorizontalTestMatrix() 
        {
            return new List<string>()
            {
                "onelkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfnb", "twolkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfnb",
                "twolkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfnb", "threelkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdb",
                "threelkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdb", "threelkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdb",
                "fourlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfb", "fourlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfb",
                "fourlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfb", "fourlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfb",
                "fivelkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfb", "fivelkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfb",
                "fivelkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfb", "fivelkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfb",
                "fivelkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfb", "sixlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfnb",
                "sixlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfnb", "sixlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfnb",
                "sixlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfnb", "sixlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfnb",
                "sixlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfnb", "sevenlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdb",
                "sevenlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdb", "sevenlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdb",
                "sevenlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdb", "sevenlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdb",
                "sevenlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdb", "sevenlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdb",
                "eightlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdb", "eightlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdb",
                "eightlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdb", "eightlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdb",
                "eightlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdb", "eightlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdb",
                "eightlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdb", "eightlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdb",
                "ninelkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfb", "ninelkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfb",
                "ninelkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfb", "ninelkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfb",
                "ninelkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfb", "ninelkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfb",
                "ninelkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfb", "ninelkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfb",
                "ninelkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfb", "tenlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfnb",
                "tenlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfnb", "tenlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfnb",
                "tenlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfnb", "tenlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfnb",
                "tenlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfnb", "tenlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfnb",
                "tenlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfnb", "tenlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfnb",
                "tenlkajelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfnb", "elevenelevenelevenelevenelevenelevenelevenelevenelevenelevendfnb",
                "elevenjelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfnb", "twelvetwelvetwelvetwelvetwelvetwelveosfhbañosdifnaoñfdhnaoñsdfnb",
                "twelvejelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfnb", "twelvejelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfnb",
                "twelvejelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfnb", "twelvejelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfnb",
                "twelvejelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfnb", "twelvejelñnqwñlthñlhbiodbnasoifanhaiosfhbañosdifnaoñfdhnaoñsdfnb"
            };
        }

        private IEnumerable<string> GetVerticalTestMatrix()
        {
            return new List<string>()
            {
                "ottffssenteettxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "nwhoiieiiellwwxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "eorwvxvgnneeeexxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xteresehetvvllxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xweffintneeevvxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xotoixseinnneexxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxhuvseintextxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxrreivgeelxwxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxeffxehnnexexxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxeoisntitvxlxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxtuviseneexvxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxhrexeiennxexxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxrffsvgntextxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxeoiiehielxwxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxeuvxntnnexexxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxxresseetvxlxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxxxfieineexvxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxxxixvginnxexxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxxxvxehntextxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxxxexnteelxwxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxxxxxsennexexxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxxxxxeiitvxlxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxxxxxvgneexvxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxxxxxehennxexxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxxxxxntntextxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxxxxxseielxwxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxxxxxeinnexexxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxxxxxvgetvxlxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxxxxxehneexvxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxxxxxntinnxexxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxxxxxsenxextxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxxxxxeiexlxwxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxxxxxvgnxexexxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxxxxxehixvxlxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxxxxxntnxexvxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxxxxxxeexnxexxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxxxxxxixxextxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxxxxxxgxxlxwxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxxxxxxhxxexexxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxxxxxxtxxvxlxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxxxxxxxxxexvxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxxxxxxxxxnxexxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxxxxxxxxxextxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxxxxxxxxxlxwxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxxxxxxxxxexexxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxxxxxxxxxvxlxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxxxxxxxxxexvxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxxxxxxxxxnxexxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxxxxxxxxxextxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxxxxxxxxxlxwxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxxxxxxxxxexexxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxxxxxxxxxvxlxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxxxxxxxxxexvxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxxxxxxxxxnxexxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxxxxxxxxxextxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxxxxxxxxxlxwxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxxxxxxxxxexexxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxxxxxxxxxvxlxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxxxxxxxxxexvxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxxxxxxxxxnxexxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx",
                "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx", "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx"
            };
        }
    }
}