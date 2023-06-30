using MediatR;

namespace Univali.Api.Features.Publishers.Queries.GetPublisherWithCoursesWithAuthorsDetail;

public class GetPublisherWithCoursesWithAuthorsDetailQuery : IRequest<PublisherForGetPublisherWithCoursesWithAuthorsDetailDto> {
    public int Id {get; set;}
}