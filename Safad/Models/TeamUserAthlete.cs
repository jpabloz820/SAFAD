namespace Safad.Models
{
    public class TeamUserAthlete
    {
        public int TeamUserAthleteId { get; set; }
        public int TeamId { get; set; }
        public int UserAthleteId { get; set; }
        public string FootballNumber { get; set; }
        public Team Team { get; set; }
        public User_Athlete User_Athlete { get; set; }
    }
}
