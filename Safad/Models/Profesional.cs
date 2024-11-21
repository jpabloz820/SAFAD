using Safad.Models;

public class Profesional
{
    public int ProfesionalId { get; set; }
    public string NameProfesional { get; set; }
    public string DniProfesional { get; set; }
    public string Cellphone { get; set; }
    public string Address { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public TeamProfessional TeamProfessional { get; set; }

    public string? PhotoPath { get; set; }

    public int TypeProfessionalId { get; set; }
    public TypeProfessional TypeProfessional { get; set; }
}
