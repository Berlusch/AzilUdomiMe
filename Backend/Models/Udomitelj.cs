using Backend.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    /// <summary>
    /// Klasa koja predstavlja udomitelja.
    /// </summary>
    public class Udomitelj:Entitet
    {
        /// <summary>
        /// Ime udomitelja.
        /// </summary>
        public string Ime { get; set; } = "";

        /// <summary>
        /// Prezime udomitelja.
        /// </summary>
        public string Prezime { get; set; } = "";

        /// <summary>
        /// Adresa udomitelja.
        /// </summary>
        public string Adresa { get; set; } = "";

        /// <summary>
        /// Telefon udomitelja.
        /// </summary>
        public string Telefon { get; set; } = "";

        /// <summary>
        /// Email udomitelja.
        /// </summary>
        public string Email { get; set; } = "";

    }
}
