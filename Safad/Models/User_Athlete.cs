namespace Safad.Models
{
    public class User_Athlete
    {
        public int UserAthleteId { get; set; }
        public string NameAthlete { get; set; }
        public string DniAthlete { get; set; }
        public string Cellphone { get; set; }
        public string Address { get; set; }
        public int Age { get; set; }
        public float Weight { get; set; }
        public string? PhotoPath { get; set; }
        public float Height { get; set; }
        public string Position { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<TeamUserAthlete> TeamUserAthletes { get; set; }
        public ICollection<GoalIndicator> GoalIndicator { get; set; }
    }
}
