namespace Safad.Models
{
    public class ConfigurationMetric
    {
        public int PhaseId { get; set; }
        public Phase Phase { get; set; }
        public int MetricId { get; set; }
        public Metric Metric { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
