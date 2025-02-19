namespace Backend.Models
{
    public class Upit: Entitet
    {
        public int PasSifra { get; set; }
        public required Pas Pas { get; set; }
        public int UdomiteljSifra { get; set; }
        public required Udomitelj Udomitelj { get; set; }
        public DateTime DatumUpita { get; set; }
        public string StatusUpita { get; set; }="";
        public string SadrzajNapomene { get; set; } = "";
        


    }
}
