using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Publishers.Queries.GetPublishersDetail;

public class GetPublishersDetailQueryHandler : IRequestHandler<GetPublishersDetailQuery, IEnumerable<GetPublishersDetailDto>> {
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;

    public GetPublishersDetailQueryHandler(IPublisherRepository PublisherRepository, IMapper mapper)
    {
        _publisherRepository = PublisherRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetPublishersDetailDto>> Handle(GetPublishersDetailQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Publisher?> PublishersFromDatabase = await _publisherRepository.GetPublishersAsync();
        return _mapper.Map<IEnumerable<GetPublishersDetailDto>>(PublishersFromDatabase);
    }
}