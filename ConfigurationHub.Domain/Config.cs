using System;

namespace Configuration.Domain
{
    public class Config
    {
        public int Id { get; set; }
        public DateTime LastModified { get; set; }
        public ConfigAuthor Author { get; set; }
        public ConfigContent ConfigContent { get; set; }
        public System System { get; set; }
    }
}