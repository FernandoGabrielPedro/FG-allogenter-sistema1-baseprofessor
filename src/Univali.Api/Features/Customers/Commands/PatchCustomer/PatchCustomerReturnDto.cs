using System.ComponentModel.DataAnnotations;
using MediatR;
using Univali.Api.ValidationAttributes;

namespace Univali.Api.Features.Customers.Commands.PatchCustomer;

public class PatchCustomerReturnDto
{
    public int Id {get; set;}
    public string Name {get; set;} = string.Empty;
    public string Cpf {get; set;} = string.Empty;
}