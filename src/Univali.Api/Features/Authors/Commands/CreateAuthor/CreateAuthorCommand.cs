using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Univali.Api.Features.Authors.Commands.CreateAuthor;

public class CreateAuthorCommand : IRequest<CreateAuthorDto>{
    [Required(ErrorMessage = "You should fill out the first name")]
    [MaxLength(30, ErrorMessage = "The name shouldn't have more than 30 characters")]
    public string FirstName {get; set;} = string.Empty;

    [Required(ErrorMessage = "You should fill out the last name")]
    [MaxLength(30, ErrorMessage = "The name shouldn't have more than 30 characters")]
   	public string LastName {get; set;} = string.Empty;
}