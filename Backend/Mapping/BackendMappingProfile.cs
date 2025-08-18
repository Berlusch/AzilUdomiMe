using AutoMapper;
using Backend.Models;
using Backend.Models.DTO;


namespace Backend.Mapping
{
    /// <summary>
    /// Klasa za definiranje mapiranja između modela i DTO objekata.
    /// </summary>
    public class BackendMappingProfile : Profile
    {
        /// <summary>
        /// Konstruktor u kojem se definiraju mapiranja.
        /// </summary>
        public BackendMappingProfile()
        {
            // kreiramo mapiranja: izvor, odredište
            CreateMap<Status, StatusDTORead>();
            CreateMap<StatusDTOInsertUpdate, Status>();


            CreateMap<Udomitelj, UdomiteljDTORead>();
            CreateMap<UdomiteljDTOInsertUpdate, Udomitelj>();
            CreateMap<Udomitelj, UdomiteljDTOInsertUpdate>();


            CreateMap<Pas, PasDTORead>().ForCtorParam(
                   "StatusNaziv",
                   opt => opt.MapFrom(src => src.Status.Naziv)
               );

            CreateMap<Pas, PasDTOInsertUpdate>().ForMember(
                    dest => dest.StatusSifra,
                    opt => opt.MapFrom(src => src.Status.Sifra)
                );

            CreateMap<PasDTOInsertUpdate, Pas>();
            CreateMap<Pas, PasDTOInsertUpdate>();


            CreateMap<Upit, UpitDTORead>().ConstructUsing(e => new UpitDTORead(
                e.Sifra,
                e.Pas.Ime,
                e.Udomitelj.Ime + " " + e.Udomitelj.Prezime,
                e.DatumUpita,
                e.StatusUpita,
                e.SadrzajUpita
                ));
            CreateMap<Upit, UpitDTOInsertUpdate>().ForMember(
                    dest => dest.PasSifra,
                    opt => opt.MapFrom(src => src.Pas.Sifra)
                ).ForMember(
                    dest => dest.UdomiteljSifra,
                    opt => opt.MapFrom(src => src.Udomitelj.Sifra)
                );
            CreateMap<UpitDTOInsertUpdate, Upit>();

            CreateMap<UpitObrazacDTO, Upit>()
            .ForMember(dest => dest.DatumUpita, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.StatusUpita, opt => opt.MapFrom(src => "zaprimljen"))
            .ForMember(dest => dest.Pas, opt => opt.Ignore())       
            .ForMember(dest => dest.Udomitelj, opt => opt.Ignore());



        }

        /*/// <summary>
        /// Metoda za dobivanje putanje do slike psa.
        /// </summary>
        /// <param name="e">Objekt polaznika.</param>
        /// <returns>Putanja do slike ili null ako slika ne postoji.</returns>
        private static string? PutanjaDatoteke(Pas e)
        {
            try
            {
                var ds = Path.DirectorySeparatorChar;
                string slika = Path.Combine(Directory.GetCurrentDirectory()
                    + ds + "wwwroot" + ds + "slike" + ds + "psi" + ds + e.Sifra + ".png");
                return File.Exists(slika) ? "/slike/psi/" + e.Sifra + ".png" : null;
            }
            catch
            {
                return null;
            }
        }*/
    }
}
