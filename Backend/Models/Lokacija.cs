using Backend.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    /// <summary>
    /// Klasa koja predstavlja lokaciju.
    /// </summary>
    public class Lokacija: Entitet

    {
        /// <summary>
        /// Naziv lokacije.
        /// </summary>
        public string? Naziv { get; set; }
        /// <summary>
        /// Adresa lokacije.
        /// </summary>
        public string Adresa { get; set; } = null!;
        /// <summary>
        /// Grad lokacije.
        /// </summary>
        public string Grad { get; set; } = null!;
        /// <summary>
        /// Poštanski broj lokacije.
        /// </summary>
        public int PostanskiBroj { get; set; }

        }
}



