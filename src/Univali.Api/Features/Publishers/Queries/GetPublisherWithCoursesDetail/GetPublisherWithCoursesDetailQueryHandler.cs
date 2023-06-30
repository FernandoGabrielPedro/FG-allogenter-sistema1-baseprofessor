using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Publishers.Queries.GetPublisherWithCoursesDetail;

public class GetPublisherWithCoursesDetailQueryHandler : IRequestHandler<GetPublisherWithCoursesDetailQuery, PublisherForGetPublisherWithCoursesDetailDto> {
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;

    public GetPublisherWithCoursesDetailQueryHandler(IPublisherRepository publisherRepository, IMapper mapper)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
    }

    public async Task<PublisherForGetPublisherWithCoursesDetailDto> Handle(GetPublisherWithCoursesDetailQuery request, CancellationToken cancellationToken)
    {
        Publisher? publisherWithCoursesFromDatabase = await _publisherRepository.GetPublisherWithCoursesByIdAsync(request.Id);
        return _mapper.Map<PublisherForGetPublisherWithCoursesDetailDto>(publisherWithCoursesFromDatabase);
    }
}