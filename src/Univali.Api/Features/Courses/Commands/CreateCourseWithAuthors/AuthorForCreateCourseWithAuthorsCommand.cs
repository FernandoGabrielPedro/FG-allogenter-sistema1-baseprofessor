using MediatR;

namespace Univali.Api.Features.Courses.Commands.CreateCourseWithAuthors;

public class AuthorForCreateCourseWithAuthorsCommand : IRequest<AuthorForCreateCourseWithAuthorsDto>
{
    public int AuthorId {get; set;}
}