using Backend.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    /// <summary>
    /// Klasa koja predstavlja psa.
    /// </summary>
    public class Pas : Entitet
    {
        /// <summary>
        /// Ime psa.
        /// </summary>
        public string Ime { get; set; } = "";

        /// <summary>
        /// Broj čipa psa.
        /// </summary>
        public string BrojCipa { get; set; } = "";

        /// <summary>
        /// Datum rođenja psa.
        /// </summary>
        public DateTime Datum_Rodjenja { get; set; }

        /// <summary>
        /// Spol psa.
        /// </summary>
        public string Spol { get; set; } = "";

        /// <summary>
        /// Opis psa.
        /// </summary>
        public string Opis { get; set; } = "";

        /// <summary>
        /// Kastracija psa.
        /// </summary>
        public bool Kastracija { get; set; }

        /// <summary>
        /// Status psa (vanjski ključ).
        /// </summary>
        [ForeignKey("status")]
        public required Status Status { get; set; }
  
    }
}
