using Microsoft.AspNetCore.Mvc;

namespace Safad.Models
{
    public class GoalIndicator
    {
        public int GoalIndicatorId { get; set; }
        public int UserAthleteId { get; set; }
        public User_Athlete User_Athlete { get; set; }
        public int MetricId { get; set; }
        public Metric Metric { get; set; }
        public float MeasureAthlete { get; set; }
    }
}
