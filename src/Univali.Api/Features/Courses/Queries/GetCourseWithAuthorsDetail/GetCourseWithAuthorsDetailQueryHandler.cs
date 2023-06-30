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
        var publisher = await _publisherRepository.GetPublisherByIdAsync(request.PublisherId);
        var courseFromDatabase = publisher?.Courses.FirstOrDefault(c => c.Id == request.CourseId);
        return _mapper.Map<CourseForGetCourseWithAuthorsDetailDto>(courseFromDatabase);
    }
}