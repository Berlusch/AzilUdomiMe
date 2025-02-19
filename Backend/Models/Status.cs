namespace Backend.Models
{
    public class Status: Entitet
    {
        public string StatusOpis { get; set; } = "";
        public required ICollection<Pas> Psi { get; set; }
    }
}
