using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Publishers.Queries.GetPublisherWithCoursesWithAuthorsDetail;

public class GetPublisherWithCoursesWithAuthorsDetailQueryHandler : IRequestHandler<GetPublisherWithCoursesWithAuthorsDetailQuery, PublisherForGetPublisherWithCoursesWithAuthorsDetailDto> {
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;

    public GetPublisherWithCoursesWithAuthorsDetailQueryHandler(IPublisherRepository publisherRepository, IMapper mapper)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
    }

    public async Task<PublisherForGetPublisherWithCoursesWithAuthorsDetailDto> Handle(GetPublisherWithCoursesWithAuthorsDetailQuery request, CancellationToken cancellationToken)
    {
        Publisher? PublisherWithCoursesWithAuthorsFromDatabase = await _publisherRepository.GetPublisherWithCoursesWithAuthorsByIdAsync(request.Id);
        return _mapper.Map<PublisherForGetPublisherWithCoursesWithAuthorsDetailDto>(PublisherWithCoursesWithAuthorsFromDatabase);
    }
}