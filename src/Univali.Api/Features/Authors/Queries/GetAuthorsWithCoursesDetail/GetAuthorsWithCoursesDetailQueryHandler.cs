using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Authors.Queries.GetAuthorsWithCoursesDetail;

public class GetAuthorsWithCoursesDetailQueryHandler : IRequestHandler<GetAuthorsWithCoursesDetailQuery, IEnumerable<AuthorForGetAuthorsWithCoursesDetailDto>> {
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;

    public GetAuthorsWithCoursesDetailQueryHandler(IPublisherRepository publisherRepository, IMapper mapper)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AuthorForGetAuthorsWithCoursesDetailDto>> Handle(GetAuthorsWithCoursesDetailQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Author?> authorWithCoursesFromDatabase = await _publisherRepository.GetAuthorsWithCoursesAsync();
        return _mapper.Map<IEnumerable<AuthorForGetAuthorsWithCoursesDetailDto>>(authorWithCoursesFromDatabase);
    }
}