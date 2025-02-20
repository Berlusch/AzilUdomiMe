using AutoMapper;
using Backend.Models;
using Backend.Models.DTO;
using System.Text.RegularExpressions;


namespace Backend.Mapping
{
    public class BackendMappingProfile : Profile
    {
        public BackendMappingProfile()
        {
            // kreiramo mapiranja: izvor, odredište
            CreateMap<Status, StatusDTORead>();
            CreateMap<StatusDTOInsertUpdate, Status>();


            CreateMap<Udomitelj, UdomiteljDTORead>();
            CreateMap<UdomiteljDTOInsertUpdate, Udomitelj>();


            CreateMap<Pas, PasDTORead>().ForCtorParam(
                   "StatusNaziv",
                   opt => opt.MapFrom(src => src.Status.Naziv)
               );

            CreateMap<Pas, PasDTOInsertUpdate>().ForMember(
                    dest => dest.StatusSifra,
                    opt => opt.MapFrom(src => src.Status.Sifra)
                );

            CreateMap<PasDTOInsertUpdate, Pas>();


            CreateMap<Upit, UpitDTORead>().ConstructUsing(e => new UpitDTORead(
                e.Sifra,
                e.Pas.Ime,
                e.Udomitelj.Ime + " " + e.Udomitelj.Prezime,
                e.DatumUpita,
                e.StatusUpita,
                e.Napomene
                ));
            CreateMap<Upit, UpitDTOInsertUpdate>().ForMember(
                    dest => dest.PasSifra,
                    opt => opt.MapFrom(src => src.Pas.Sifra)
                ).ForMember(
                    dest => dest.UdomiteljSifra,
                    opt => opt.MapFrom(src => src.Udomitelj.Sifra)
                );
            CreateMap<UpitDTOInsertUpdate, Upit>();

        }
    }
}
