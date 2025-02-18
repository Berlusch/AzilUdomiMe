using System.ComponentModel.DataAnnotations;

namespace Backend.Models.DTO
{
    public record PasDTOInsertUpdate(
        string Ime,
        string BrojCipa,
        string Pasmina,
        DateTime Datum_Rodjenja,
        Spol SpolVrsta,
        string Opis,
        bool Kastracija,
        StatusEnum StatusOpis,
        int? UdomiteljSifra
    );

    public class Enumi
    {
        public enum Spol { M, Ž }

        public enum StatusEnum
        {
            Udomljen,
            Rezerviran,
            Slobodan,
            PrivremeniSmjestaj
        }
    }
}