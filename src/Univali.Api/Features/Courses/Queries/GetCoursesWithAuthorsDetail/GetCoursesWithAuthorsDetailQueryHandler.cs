using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Courses.Queries.GetCoursesWithAuthorsDetail;

public class GetCoursesWithAuthorsDetailQueryHandler : IRequestHandler<GetCoursesWithAuthorsDetailQuery, IEnumerable<CourseForGetCoursesWithAuthorsDetailDto>> {
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;

    public GetCoursesWithAuthorsDetailQueryHandler(IPublisherRepository publisherRepository, IMapper mapper)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CourseForGetCoursesWithAuthorsDetailDto>> Handle(GetCoursesWithAuthorsDetailQuery request, CancellationToken cancellationToken)
    {
        Publisher? publisher = await _publisherRepository.GetPublisherWithCoursesWithAuthorsByIdAsync(request.PublisherId);
        IEnumerable<Course?> coursesFromDatabase = await _publisherRepository.GetCoursesWithAuthorsAsync();
        return _mapper.Map<IEnumerable<CourseForGetCoursesWithAuthorsDetailDto>>(coursesFromDatabase);
    }
}