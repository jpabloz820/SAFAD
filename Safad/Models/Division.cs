namespace Safad.Models
{
    public class Division
    {
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public ICollection<Team> Teams { get; set; }
        public Category Category { get; set; }
    }
}
