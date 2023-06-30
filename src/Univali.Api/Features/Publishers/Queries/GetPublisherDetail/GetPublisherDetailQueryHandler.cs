using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Publishers.Queries.GetPublisherDetail;

public class GetPublisherDetailQueryHandler : IRequestHandler<GetPublisherDetailQuery, GetPublisherDetailDto> {
    private readonly IPublisherRepository _PublisherRepository;
    private readonly IMapper _mapper;

    public GetPublisherDetailQueryHandler(IPublisherRepository PublisherRepository, IMapper mapper)
    {
        _PublisherRepository = PublisherRepository;
        _mapper = mapper;
    }

    public async Task<GetPublisherDetailDto> Handle(GetPublisherDetailQuery request, CancellationToken cancellationToken)
    {
        Publisher? PublisherFromDatabase = await _PublisherRepository.GetPublisherByIdAsync(request.Id);
        return _mapper.Map<GetPublisherDetailDto>(PublisherFromDatabase);
    }
}