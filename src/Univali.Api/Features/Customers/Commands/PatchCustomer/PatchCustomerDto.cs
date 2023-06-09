using System.ComponentModel.DataAnnotations;
using Univali.Api.ValidationAttributes;

namespace Univali.Api.Features.Customers.Commands.PatchCustomer;

public class PatchCustomerDto
{
    public string Name {get; set;} = string.Empty;
    public string Cpf {get; set;} = string.Empty;
}