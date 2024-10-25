namespace Safad.Models
{
    public class TeamProfessional
    {
        public int TeamProfessionalId { get; set; }
        public int TeamId { get; set; }
        public int ProfesionalId { get; set; }
        public Team Team { get; set; }
        public Profesional Profesional { get; set; }
    }
}
