using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Publishers.Queries.GetPublishersWithCoursesWithAuthorsDetail;

public class GetPublishersWithCoursesWithAuthorsDetailQueryHandler : IRequestHandler<GetPublishersWithCoursesWithAuthorsDetailQuery, IEnumerable<PublisherForGetPublishersWithCoursesWithAuthorsDetailDto>> {
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;

    public GetPublishersWithCoursesWithAuthorsDetailQueryHandler(IPublisherRepository publisherRepository, IMapper mapper)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PublisherForGetPublishersWithCoursesWithAuthorsDetailDto>> Handle(GetPublishersWithCoursesWithAuthorsDetailQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Publisher?> PublisherWithCoursesWithAuthorsFromDatabase = await _publisherRepository.GetPublishersWithCoursesWithAuthorsAsync();
        return _mapper.Map<IEnumerable<PublisherForGetPublishersWithCoursesWithAuthorsDetailDto>>(PublisherWithCoursesWithAuthorsFromDatabase);
    }
}