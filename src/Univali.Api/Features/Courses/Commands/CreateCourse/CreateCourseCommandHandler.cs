using AutoMapper;
using Univali.Api.Entities;
using Univali.Api.Repositories;
using MediatR;

namespace Univali.Api.Features.Courses.Commands.CreateCourse;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, CreateCourseDto>{
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;

    public CreateCourseCommandHandler(IPublisherRepository publisherRepository, IMapper mapper)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
    }

    public async Task<CreateCourseDto> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var courseEntity = _mapper.Map<Course>(request);
        _publisherRepository.CreateCourse(courseEntity);
        await _publisherRepository.SaveChangesAsync();
        var courseForReturn = _mapper.Map<CreateCourseDto>(courseEntity);
        return courseForReturn;
    }
}
