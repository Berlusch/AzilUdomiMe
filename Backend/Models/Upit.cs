using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    /// <summary>
    /// Klasa koja predstavlja upit.
    /// </summary>
    public class Upit: Entitet
    {
        /// <summary>
        /// Pas za kojeg je postavljen upit.
        /// </summary>
        [ForeignKey("pas")]
        public required Pas Pas { get; set; }

        /// <summary>
        /// Udomitelj koji postavlja upit.
        /// </summary>
        [ForeignKey("udomitelj")]
        public required Udomitelj Udomitelj { get; set; }

        /// <summary>
        /// Datum upita.
        /// </summary>
        [Column(name: "datum_upita")]
        public DateTime DatumUpita { get; set; }

        /// <summary>
        /// Status upita.
        /// </summary>
        [Column(name: "status_upita")]
        public string StatusUpita { get; set; }="";

        /// <summary>
        /// Sadržaj upita.
        /// </summary>
        [Column(name: "sadrzaj_upita")]
        public string SadrzajUpita { get; set; } = "";
        


    }
}
