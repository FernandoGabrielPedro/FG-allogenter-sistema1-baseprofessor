using System.ComponentModel.DataAnnotations;
using Univali.Api.ValidationAttributes;

namespace Univali.Api.Features.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommand
{
    public int Id {get; set;}
}