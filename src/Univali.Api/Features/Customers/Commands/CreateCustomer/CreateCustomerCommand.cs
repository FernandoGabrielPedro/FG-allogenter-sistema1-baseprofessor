using System.ComponentModel.DataAnnotations;
using MediatR;
using Univali.Api.ValidationAttributes;

namespace Univali.Api.Features.Customers.Commands.CreateCustomer;

public class CreateCustomerCommand : IRequest<CreateCustomerCommandResponse>
{
    public string Name {get; set;} = string.Empty;
    public string Cpf {get; set;} = string.Empty;
}