using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Univali.Api.Features.Courses.Commands.UpdateCourse;

public class UpdateCourseCommand : IRequest<bool>{
    public int PublisherId {get; set;}
    public CourseForUpdateCourseDto? CourseForUpdateCourseDto;
}