using System.Collections.Generic;

namespace Configuration.Domain
{
    public class AuthorAndConfigs
    {
        public ConfigAuthor ConfigAuthor { get; set; }

        public IEnumerable<int> ConfigIds { get; set; }

        public long ConfigCount { get; set; }
    }
}