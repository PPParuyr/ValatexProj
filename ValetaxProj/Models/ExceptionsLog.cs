namespace ValetaxProj.Models
{
    public class ExceptionsLog
    {
        public Guid Id { get; set; }
        public DateTime Time { get; set; }
        public string QueryParametres { get; set; }
        public string BodyParametres { get; set; }
        public string StackTrace { get; set; }
    }
}
