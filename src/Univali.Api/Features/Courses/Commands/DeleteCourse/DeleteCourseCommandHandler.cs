using AutoMapper;
using Univali.Api.Repositories;
using MediatR;
using Univali.Api.Entities;

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
        Publisher? publisherEntity = await _publisherRepository.GetPublisherWithCoursesByIdAsync(request.PublisherId);
        Course? courseEntity = publisherEntity?.Courses.FirstOrDefault(c => c.Id == request.CourseId);
        if(courseEntity == null) return false;
        
        publisherEntity?.Courses.Remove(courseEntity);
        await _publisherRepository.SaveChangesAsync();
        return true;
    }
}
