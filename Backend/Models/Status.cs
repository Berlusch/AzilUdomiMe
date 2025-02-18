namespace Backend.Models
{
    public class Status: Entitet
    {
        public string StatusOpis { get; set; } = "";
        public ICollection<Pas> Psi { get; set; }
    }
}
