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
        Publisher? publisherEntity = await _publisherRepository.GetPublisherWithCoursesByIdAsync(request.PublisherId);
        Course? courseEntity = publisherEntity?.Courses.FirstOrDefault(c => c.Id == request.CourseForUpdateCourseDto?.Id);
        if(courseEntity == null) return false;

        Course newCourseValues = _mapper.Map<Course>(request.CourseForUpdateCourseDto);
        //_publisherRepository.UpdateCourse(newCourseValues, courseEntity);

        courseEntity.Title = newCourseValues.Title;
        courseEntity.Description = newCourseValues.Description;
        courseEntity.Price = newCourseValues.Price;
        
        await _publisherRepository.SaveChangesAsync();

        return true;
    }
}