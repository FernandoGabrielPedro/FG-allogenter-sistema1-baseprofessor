using MediatR;

namespace Univali.Api.Features.Publishers.Queries.GetPublisherWithCoursesDetail;

public class GetPublisherWithCoursesDetailQuery : IRequest<PublisherForGetPublisherWithCoursesDetailDto> {
    public int Id {get; set;}
}