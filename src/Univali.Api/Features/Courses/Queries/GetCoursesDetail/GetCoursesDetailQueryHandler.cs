using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Courses.Queries.GetCoursesDetail;

public class GetCoursesDetailQueryHandler : IRequestHandler<GetCoursesDetailQuery, IEnumerable<GetCoursesDetailDto>> {
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;

    public GetCoursesDetailQueryHandler(IPublisherRepository PublisherRepository, IMapper mapper)
    {
        _publisherRepository = PublisherRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetCoursesDetailDto>> Handle(GetCoursesDetailQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<Course?> CoursesFromDatabase = await _publisherRepository.GetCoursesAsync();
        return _mapper.Map<IEnumerable<GetCoursesDetailDto>>(CoursesFromDatabase);
    }
}