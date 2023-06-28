using AutoMapper;
using MediatR;
using Univali.Api.Entities;
using Univali.Api.Repositories;

namespace Univali.Api.Features.Courses.Queries.GetCourseDetail;

public class GetCourseDetailQueryHandler : IRequestHandler<GetCourseDetailQuery, GetCourseDetailDto> {
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;

    public GetCourseDetailQueryHandler(IPublisherRepository PublisherRepository, IMapper mapper)
    {
        _publisherRepository = PublisherRepository;
        _mapper = mapper;
    }

    public async Task<GetCourseDetailDto> Handle(GetCourseDetailQuery request, CancellationToken cancellationToken)
    {
        Course? CourseFromDatabase = await _publisherRepository.GetCourseByIdAsync(request.Id);
        return _mapper.Map<GetCourseDetailDto>(CourseFromDatabase);
    }
}