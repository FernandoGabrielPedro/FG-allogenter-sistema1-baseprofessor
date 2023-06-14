using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Univali.Api.Entities;
using Univali.Api.ValidationAttributes;

namespace Univali.Api.Features.Customers.Commands.PatchCustomer;

public class PatchCustomerCommand : IRequest<Customer?>
{
    public JsonPatchDocument<PatchCustomerDto> PatchDocument {get; set;}
    public int Id {get; set;}
}