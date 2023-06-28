using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Univali.Api.Features.Courses.Commands.CreateCourseWithAuthors;

public class CreateCourseWithAuthorsCommand : IRequest<CourseForCreateCourseWithAuthorsDto>{
    [Required(ErrorMessage = "You should fill out the title")]
    [MaxLength(60, ErrorMessage = "The name shouldn't have more than 60 characters")]
    public string Title {get; set;} = string.Empty;
    public string Description {get; set;} = string.Empty;

    [Required(ErrorMessage = "You should fill out the price")]
    [Precision(3,2)]
    [Range(0, 999.99)]
    public decimal Price {get; set;}
    public IEnumerable<AuthorForCreateCourseWithAuthorsCommand> AuthorsIdsForCreation {get; set;}
}