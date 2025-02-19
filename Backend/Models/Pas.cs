using Backend.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Pas : Entitet
    {
        public string Ime { get; set; } = "";
        public string BrojCipa { get; set; } = "";
        public DateTime Datum_Rodjenja { get; set; }

        public Spol Spol { get; set; }

        public string Opis { get; set; } = "";  
                
              
        public bool Kastracija { get; set; }

        public int StatusSifra { get; set; }  // Strani ključ prema statusu
        public required Status StatusOpis { get; set; }
                
        public int? UdomiteljSifra { get; set; }
        public Udomitelj? Udomitelj { get; set; }
        public ICollection<Upit> Upiti { get; set; } = new List<Upit>();
    }
}
