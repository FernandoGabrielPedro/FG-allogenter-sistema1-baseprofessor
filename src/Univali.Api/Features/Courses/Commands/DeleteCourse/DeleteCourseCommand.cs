using MediatR;

namespace Univali.Api.Features.Courses.Commands.DeleteCourse;

public class DeleteCourseCommand : IRequest<bool>{
    public int Id {get;set;}
}
