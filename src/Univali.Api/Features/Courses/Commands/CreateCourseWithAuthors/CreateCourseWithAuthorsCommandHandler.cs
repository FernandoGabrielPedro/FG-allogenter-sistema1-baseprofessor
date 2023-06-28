using AutoMapper;
using Univali.Api.Entities;
using Univali.Api.Repositories;
using MediatR;

namespace Univali.Api.Features.Courses.Commands.CreateCourseWithAuthors;

public class CreateCourseWithAuthorsCommandHandler : IRequestHandler<CreateCourseWithAuthorsCommand, CourseForCreateCourseWithAuthorsDto>{
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;

    public CreateCourseWithAuthorsCommandHandler(IPublisherRepository publisherRepository, IMapper mapper)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
    }

    public async Task<CourseForCreateCourseWithAuthorsDto> Handle(CreateCourseWithAuthorsCommand request, CancellationToken cancellationToken)
    {
        Author? newAuthor;
        var courseEntity = _mapper.Map<Course>(request);
        foreach(AuthorForCreateCourseWithAuthorsCommand author in request.Authors) {
            newAuthor = await _publisherRepository.GetAuthorByIdAsync(author.AuthorId);
            if(newAuthor == null) continue;
            courseEntity.Authors.Add(newAuthor!);
            newAuthor.Courses.Add(courseEntity);
        }
        _publisherRepository.CreateCourse(courseEntity);
        await _publisherRepository.SaveChangesAsync();
        var courseForReturn = _mapper.Map<CourseForCreateCourseWithAuthorsDto>(courseEntity);
        return courseForReturn;
    }
}
