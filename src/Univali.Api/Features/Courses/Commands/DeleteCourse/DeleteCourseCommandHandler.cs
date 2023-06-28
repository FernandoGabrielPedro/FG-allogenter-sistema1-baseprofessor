using AutoMapper;
using Univali.Api.Repositories;
using MediatR;

namespace Univali.Api.Features.Courses.Commands.DeleteCourse;

public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, bool>{
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;

    public DeleteCourseCommandHandler(IPublisherRepository publisherRepository, IMapper mapper)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _publisherRepository.GetCourseByIdAsync(request.Id);
        if(course == null) return false;
        
        _publisherRepository.DeleteCourse(course);
        await _publisherRepository.SaveChangesAsync();
        return true;

    }
}
