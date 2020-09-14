using System.Configuration;

namespace WordFinder.Config
{
    public class WordFinderSection : ConfigurationSection
    {
        protected static WordFinderSection instance
            = ConfigurationManager.GetSection("WordFinderSection") as WordFinderSection;

        public static WordFinderSection Instance => instance;

        [ConfigurationProperty("wordsToReturn", DefaultValue = 10, IsRequired = true)]
        public int WordsToReturn
        {
            get => (int)this["wordsToReturn"];
            set => this["wordsToReturn"] = value;
        }

        [ConfigurationProperty("maxColumnSize", DefaultValue = 64, IsRequired = true)]
        public int MaxColumnSize
        {
            get => (int)this["maxColumnSize"];
            set => this["maxColumnSize"] = value;
        }

        [ConfigurationProperty("maxRowSize", DefaultValue = 64, IsRequired = true)]
        public int MaxRowSize
        {
            get => (int)this["maxRowSize"];
            set => this["maxRowSize"] = value;
        }
    }
}