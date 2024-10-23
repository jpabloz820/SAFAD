namespace Safad.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public string TeamLogo { get; set; }
        public int MaxPlayers { get; set; }
        public int MinPlayers { get; set; }
        public int CategoryId { get; set; }
        public int UserCoachId { get; set; }
        public Category Category { get; set; }
        public UserCoach UserCoach { get; set; }
        public ICollection<Profesional> Profesionals { get; set; }
        public ICollection<User_Athlete> Athletes { get; set; }
    }
}
