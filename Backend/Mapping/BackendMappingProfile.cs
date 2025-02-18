using AutoMapper;
using Backend.Models;
using Backend.Models.DTO;


namespace Backend.Mapping
{
    public class BackendMappingProfile : Profile
    {
        public BackendMappingProfile()
        {
            // kreiramo mapiranja: izvor, odredište
            CreateMap<Udomitelj, UdomiteljDTORead>();
            CreateMap<UdomiteljDTOInsertUpdate, Udomitelj>();

            CreateMap<Pas, PasDTORead>();
            CreateMap<PasDTOInsertUpdate, Pas>();

            CreateMap<Status, StatusDTORead>();
            CreateMap<StatusDTOInsertUpdate, Status>();

            CreateMap<Upit, UpitDTORead>();
            CreateMap<UpitDTOInsertUpdate, Upit>();

        }
    }
}

