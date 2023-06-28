using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Courses.Queries.GetCourseWithAuthorsDetail;

public class GetCourseWithAuthorsDetailQueryHandler : IRequestHandler<GetCourseWithAuthorsDetailQuery, CourseForGetCourseWithAuthorsDetailDto> {
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;

    public GetCourseWithAuthorsDetailQueryHandler(IPublisherRepository publisherRepository, IMapper mapper)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
    }

    public async Task<CourseForGetCourseWithAuthorsDetailDto> Handle(GetCourseWithAuthorsDetailQuery request, CancellationToken cancellationToken)
    {
        Course? courseFromDatabase = await _publisherRepository.GetCourseWithAuthorsByIdAsync(request.Id);
        return _mapper.Map<CourseForGetCourseWithAuthorsDetailDto>(courseFromDatabase);
    }
}