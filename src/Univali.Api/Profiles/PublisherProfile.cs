using AutoMapper;

namespace Univali.Api.Profiles;

public class PublisherProfile : Profile {
    public PublisherProfile() {
        //1º arg: objeto de origem
        //2º arg: objeto de destino

        CreateMap<Entities.Publisher, Entities.Publisher>();
    }
}