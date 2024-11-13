namespace Safad.Models
{
    public class Metric
    {
        public int MetricId { get; set; }
        public string MetricName { get; set; }
        public float Indicator { get; set; }
        public string Measure { get; set; }
        public ICollection<ConfigurationMetric> ConfigurationMetric { get; set; }
        public ICollection<GoalIndicator> GoalIndicator { get; set; }
    }
}
