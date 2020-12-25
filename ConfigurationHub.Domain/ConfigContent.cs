namespace Configuration.Domain
{
    public class ConfigContent
    {
        public int Id { get; set; }
        public string Content { get; set; }
        
        public Config Config { get; set; }
        public int ConfigId { get; set; }
    }
}