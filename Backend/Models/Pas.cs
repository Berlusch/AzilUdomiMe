using Backend.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Pas : Entitet
    {
        public string Ime { get; set; } = "";
        public string BrojCipa { get; set; } = "";
        public string Pasmina { get; set; } = "";
        public DateTime Datum_Rodjenja { get; set; }


        public Spol SpolVrsta { get; set; }

        public enum Spol { M, Ž }


        public Velicina VelicinaPsa { get; set; }

        public enum Velicina
        {
            Mali,
            Srednji,
            Veliki
        }


        public Boja BojaPsa { get; set; }

        public enum Boja
        {
            Bijeli,
            Crni,
            Smeđi,
            Šareni
        }

        public string MojaPrica { get; set; } = "";
        public bool Kastracija { get; set; }


        public StatusEnum StatusOpis { get; set; }

        public enum StatusEnum
        {
            Udomljen,
            Rezerviran,
            Slobodan,
            PrivremeniSmjestaj
        }
    }
}
