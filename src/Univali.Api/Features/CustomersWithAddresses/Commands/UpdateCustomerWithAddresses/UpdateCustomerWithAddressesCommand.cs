using System.ComponentModel.DataAnnotations;
using MediatR;
using Univali.Api.ValidationAttributes;

namespace Univali.Api.Features.CustomersWithAddresses.Commands.UpdateCustomerWithAddresses;

public class UpdateCustomerWithAddressesCommand : IRequest<bool>
{
    [Required(ErrorMessage = "You should fill out a Id")]
    public int Id {get; set;}

    [Required(ErrorMessage = "You should fill out a Name")]
    [MaxLength(100, ErrorMessage = "The name shouldn't have more than 100 characters")]
    public string Name {get; set;} = string.Empty;

    [Required(ErrorMessage = "You should fill out a Cpf")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = 
    "The Cpf should have 11 characters")]
    [CpfMustBeValid(ErrorMessage="The CPF {0} should be a valid number.")]
    public string Cpf {get; set;} = string.Empty;

    public ICollection<AddressToUpdateForUpdateCustomerWithAddressesDto> Addresses { get; set; } = new List<AddressToUpdateForUpdateCustomerWithAddressesDto>();
}