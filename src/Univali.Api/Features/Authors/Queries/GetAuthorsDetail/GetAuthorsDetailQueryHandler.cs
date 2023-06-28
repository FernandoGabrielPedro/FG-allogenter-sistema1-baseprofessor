using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Authors.Queries.GetAuthorsDetail;

public class GetAuthorsDetailQueryHandler : IRequestHandler<GetAuthorsDetailQuery, IEnumerable<GetAuthorsDetailDto>> {
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;

    public GetAuthorsDetailQueryHandler(IPublisherRepository PublisherRepository, IMapper mapper)
    {
        _publisherRepository = PublisherRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetAuthorsDetailDto>> Handle(GetAuthorsDetailQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Author?> AuthorsFromDatabase = await _publisherRepository.GetAuthorsAsync();
        return _mapper.Map<IEnumerable<GetAuthorsDetailDto>>(AuthorsFromDatabase);
    }
}