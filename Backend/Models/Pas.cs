using Backend.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Pas : Entitet
    {
        public string Ime { get; set; } = "";
        public string BrojCipa { get; set; } = "";
        public DateTime Datum_Rodjenja { get; set; }

        public string Spol { get; set; } = "";

        public string Opis { get; set; } = "";  
                
              
        public bool Kastracija { get; set; }

        [ForeignKey("status")]
        public required Status Status { get; set; }
  
    }
}
