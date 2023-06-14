using System.ComponentModel.DataAnnotations;
using MediatR;
using Univali.Api.ValidationAttributes;

namespace Univali.Api.Features.CustomersWithAddresses.Commands.CreateCustomerWithAddresses;

public class CreateCustomerWithAddressesCommand : IRequest<CustomerToReturnForCreateCustomerWithAddressesDto>
{
    [Required(ErrorMessage = "You should fill out a Name")]
    [MaxLength(100, ErrorMessage = "The name shouldn't have more than 100 characters")]
    public string Name {get; set;} = string.Empty;

    [Required(ErrorMessage = "You should fill out a Cpf")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = 
    "The Cpf should have 11 characters")]
    [CpfMustBeValid(ErrorMessage="The CPF {0} should be a valid number.")]
    public string Cpf {get; set;} = string.Empty;

    public ICollection<AddressToCreationForCreateCustomerWithAddressesDto> Addresses { get; set; } = new List<AddressToCreationForCreateCustomerWithAddressesDto>();
}