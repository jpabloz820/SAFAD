namespace Safad.Dtos
{
    public class TeamDto
    {
        public string TeamName { get; set; }
        public string CategoryName { get; set; }
        public string DivisionName { get; set; }
        public int NumberAthlete { get; set; }
        public string TeamLogo { get; set; }
        public List<UserAthleteDto> Athletes { get; set; } = new List<UserAthleteDto>();
    }
}
