using AutoMapper;

namespace Univali.Api.Profiles;

public class AddressProfile : Profile {
    public AddressProfile() {
        //1º arg: objeto de origem
        //2º arg: objeto de destino
        CreateMap<Entities.Address, Models.AddressDto>();
        CreateMap<Models.AddressForCreationDto, Entities.Address>();
        CreateMap<Models.AddressForUpdateDto, Entities.Address>();
        CreateMap<Entities.Address, Entities.Address>();
    }
}