using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace WordFinder.Config
{
    public static class WordFinderConfig
    {

        public static WordFinderSection WordFinder
            => WordFinderSection.Instance;
    }
}
