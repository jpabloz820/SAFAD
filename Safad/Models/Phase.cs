namespace Safad.Models
{
    public class Phase
    {
        public int PhaseId { get; set; }
        public string PhaseName { get; set; }
        public ICollection<ConfigurationMetric> ConfigurationMetric { get; set; }
    }
}
