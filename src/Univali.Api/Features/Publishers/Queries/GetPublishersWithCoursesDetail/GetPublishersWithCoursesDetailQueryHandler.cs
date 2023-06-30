using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Publishers.Queries.GetPublishersWithCoursesDetail;

public class GetPublishersWithCoursesDetailQueryHandler : IRequestHandler<GetPublishersWithCoursesDetailQuery, IEnumerable<PublisherForGetPublishersWithCoursesDetailDto>> {
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;

    public GetPublishersWithCoursesDetailQueryHandler(IPublisherRepository publisherRepository, IMapper mapper)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PublisherForGetPublishersWithCoursesDetailDto>> Handle(GetPublishersWithCoursesDetailQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Publisher?> PublishersWithCoursesFromDatabase = await _publisherRepository.GetPublishersWithCoursesAsync();
        return _mapper.Map<IEnumerable<PublisherForGetPublishersWithCoursesDetailDto>>(PublishersWithCoursesFromDatabase);
    }
}