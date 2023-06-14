using System.ComponentModel.DataAnnotations;
using MediatR;
using Univali.Api.ValidationAttributes;

namespace Univali.Api.Features.Addresses.Commands.UpdateAddress;

public class UpdateAddressCommand : IRequest<bool>
{
    [Required(ErrorMessage = "You should fill out an Id")]
    public int Id { get; set; }

    [Required(ErrorMessage = "You should fill out a Street")]
    [MaxLength(100, ErrorMessage = "The street shouldn't have more than 100 characters")]
    public string Street { get; set; } = string.Empty;

    [Required(ErrorMessage = "You should fill out a City")]
    [MaxLength(100, ErrorMessage = "The city shouldn't have more than 100 characters")]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "You should fill out a CustomerId")]
    public int CustomerId { get; set; }
}