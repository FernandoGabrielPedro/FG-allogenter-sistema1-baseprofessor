using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Authors.Queries.GetAuthorWithCoursesDetail;

public class GetAuthorWithCoursesDetailQueryHandler : IRequestHandler<GetAuthorWithCoursesDetailQuery, AuthorForGetAuthorWithCoursesDetailDto> {
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;

    public GetAuthorWithCoursesDetailQueryHandler(IPublisherRepository publisherRepository, IMapper mapper)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
    }

    public async Task<AuthorForGetAuthorWithCoursesDetailDto> Handle(GetAuthorWithCoursesDetailQuery request, CancellationToken cancellationToken)
    {
        Author? authorWithCoursesFromDatabase = await _publisherRepository.GetAuthorWithCoursesByIdAsync(request.Id);
        return _mapper.Map<AuthorForGetAuthorWithCoursesDetailDto>(authorWithCoursesFromDatabase);
    }
}