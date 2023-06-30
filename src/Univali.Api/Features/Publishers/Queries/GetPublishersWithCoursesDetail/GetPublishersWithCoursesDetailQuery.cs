using MediatR;

namespace Univali.Api.Features.Publishers.Queries.GetPublishersWithCoursesDetail;

public class GetPublishersWithCoursesDetailQuery : IRequest<IEnumerable<PublisherForGetPublishersWithCoursesDetailDto>> {}