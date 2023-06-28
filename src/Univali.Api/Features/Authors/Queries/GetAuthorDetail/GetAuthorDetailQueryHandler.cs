using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Authors.Queries.GetAuthorDetail;

public class GetAuthorDetailQueryHandler : IRequestHandler<GetAuthorDetailQuery, GetAuthorDetailDto> {
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;

    public GetAuthorDetailQueryHandler(IPublisherRepository PublisherRepository, IMapper mapper)
    {
        _publisherRepository = PublisherRepository;
        _mapper = mapper;
    }

    public async Task<GetAuthorDetailDto> Handle(GetAuthorDetailQuery request, CancellationToken cancellationToken)
    {
        Author? AuthorFromDatabase = await _publisherRepository.GetAuthorByIdAsync(request.Id);
        return _mapper.Map<GetAuthorDetailDto>(AuthorFromDatabase);
    }
}