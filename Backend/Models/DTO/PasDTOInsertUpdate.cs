using System.ComponentModel.DataAnnotations;

namespace Backend.Models.DTO
{
    public record PasDTOInsertUpdate(
        string Ime,
        string BrojCipa,
        string Pasmina,
        DateTime Datum_Rodjenja,
        Spol SpolVrsta,
        Velicina VelicinaPsa,
        Boja BojaPsa,
        string MojaPrica,
        bool Kastracija,
        StatusEnum StatusOpis,
        int? UdomiteljSifra
    );

    public class Enumi
    {
        public enum Spol { M, Ž }

        public enum Velicina
        {
            Mali,
            Srednji,
            Veliki
        }

        public enum Boja
        {
            Bijeli,
            Crni,
            Smeđi,
            Šareni
        }

        public enum StatusEnum
        {
            Udomljen,
            Rezerviran,
            Slobodan,
            PrivremeniSmjestaj
        }
    }
}