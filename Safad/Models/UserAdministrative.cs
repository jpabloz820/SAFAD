namespace Safad.Models
{
    public class UserAdministrative
    {
        public int UserAdministrativeId { get; set; }
        public string NameAdministrative { get; set; }
        public string DniAdministrative { get; set; }
        public string Cellphone { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
