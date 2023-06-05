using AutoMapper;

namespace Univali.Api.Profiles;

public class CustomerProfile : Profile {
    public CustomerProfile() {
        //1º arg: objeto de origem
        //2º arg: objeto de destino
        CreateMap<Entities.Customer, Models.CustomerDto>();
    }
}