namespace Safad.Models
{
    public class TypeProfessional
    {
        public int TypeId { get; set; }
        public string NameType { get; set; }


        public ICollection<Profesional> Profesionals { get; set; }
    }
}
