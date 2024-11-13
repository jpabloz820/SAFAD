namespace Safad.Dtos
{
    public class MetricDto
    {
        public int MetricId { get; set; }
        public string MetricName { get; set; }
        public float Indicator { get; set; }
        public string Measure { get; set; }
        public int PhaseId { get; set; }
        public int CategoryId { get; set; }
    }
}
