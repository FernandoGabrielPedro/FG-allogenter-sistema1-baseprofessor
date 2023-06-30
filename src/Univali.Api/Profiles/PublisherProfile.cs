using AutoMapper;

namespace Univali.Api.Profiles;

public class PublisherProfile : Profile {
    public PublisherProfile() {
        //1º arg: objeto de origem
        //2º arg: objeto de destino

        CreateMap<Entities.Publisher, Entities.Publisher>();

        CreateMap<Entities.Publisher, Univali.Api.Features.Publishers.Queries.GetPublishersDetail.GetPublishersDetailDto>();
        CreateMap<Entities.Publisher, Univali.Api.Features.Publishers.Queries.GetPublisherDetail.GetPublisherDetailDto>();
        CreateMap<Entities.Publisher, Univali.Api.Features.Publishers.Queries.GetPublishersWithCoursesDetail.PublisherForGetPublishersWithCoursesDetailDto>();
        CreateMap<Entities.Publisher, Univali.Api.Features.Publishers.Queries.GetPublisherWithCoursesDetail.PublisherForGetPublisherWithCoursesDetailDto>();

        CreateMap<Univali.Api.Features.Publishers.Commands.CreatePublisher.CreatePublisherCommand, Entities.Publisher>();
        CreateMap<Entities.Publisher, Univali.Api.Features.Publishers.Commands.CreatePublisher.CreatePublisherDto>();
        CreateMap<Univali.Api.Features.Publishers.Commands.UpdatePublisher.UpdatePublisherCommand, Entities.Publisher>();
    }
}