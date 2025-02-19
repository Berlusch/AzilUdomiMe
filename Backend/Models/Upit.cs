using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Upit: Entitet
    {
        [ForeignKey("pas")]
        public required Pas Pas { get; set; }
        [ForeignKey("udomitelj")]
        public required Udomitelj Udomitelj { get; set; }
        [Column(name: "datum_upita")]
        public DateTime DatumUpita { get; set; }
        [Column(name: "status_upita")]
        public string StatusUpita { get; set; }="";
        [Column(name: "napomene")]
        public string SadrzajNapomene { get; set; } = "";
        


    }
}
