namespace Safad.Models
{
    public class UserCoach
    {
        public int UserCoachId { get; set; }
        public string NameCoach { get; set; }
        public string DniCoach { get; set; }
        public string Cellphone { get; set; }
        public string Address { get; set; }
        public string? PhotoPath { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
