using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Univali.Api.Features.Publishers.Commands.UpdatePublisher;

public class UpdatePublisherCommand : IRequest<bool>
{
    [Required(ErrorMessage = "You should fill the id")]
    public int Id {get;set;}
    
    [Required(ErrorMessage = "You better fill out the names if you want to live")]
    [MaxLength(60, ErrorMessage = "Names shouldn't have more than 60 characters")]
    public string FirstName {get; set;} = string.Empty;

    [Required(ErrorMessage = "You better fill out the names if you want to live")]
    [MaxLength(60, ErrorMessage = "Names shouldn't have more than 60 characters")]
    public string LastName {get; set;} = string.Empty;

    [Required(ErrorMessage = "Fill out the Cpf")]
    [MaxLength(11, ErrorMessage = "11 digits, no more no less")]
    [MinLength(11, ErrorMessage = "11 digits, no more no less")]
    public string Cpf {get; set;} = string.Empty;
}