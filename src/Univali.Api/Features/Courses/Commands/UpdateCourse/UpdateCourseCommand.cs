using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Univali.Api.Features.Courses.Commands.UpdateCourse;

public class UpdateCourseCommand : IRequest<bool>{
    [Required(ErrorMessage = "You should fill the id")]
    public int Id {get;set;}
    
    [Required(ErrorMessage = "You should fill out the title")]
    [MaxLength(60, ErrorMessage = "The title shouldn't have more than 60 characters")]
    public string Title {get; set;} = string.Empty;
    public string Description {get; set;} = string.Empty;

    [Required(ErrorMessage = "You should fill out the price")]
    [Precision(3,2)]
    [Range(0, 999.99, ErrorMessage = "Price should be between $0.00 and $999.99")]
    public double Price {get; set;}
}