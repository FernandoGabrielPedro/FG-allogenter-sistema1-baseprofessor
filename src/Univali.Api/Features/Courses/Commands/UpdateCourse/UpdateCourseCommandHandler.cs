using AutoMapper;
using Univali.Api.Entities;
using Univali.Api.Repositories;
using MediatR;


namespace Univali.Api.Features.Courses.Commands.UpdateCourse;

public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, bool>{
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;

    public UpdateCourseCommandHandler(IPublisherRepository publisherRepository, IMapper mapper)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var courseForUpdate = _mapper.Map<Course>(request);
        if(courseForUpdate == null) return false;

        var rightCourse = await _publisherRepository.GetCourseByIdAsync(request.Id);
        _publisherRepository.UpdateCourse(courseForUpdate, rightCourse!);
        await _publisherRepository.SaveChangesAsync();

        return true;
    }
}