using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Univali.Api.Entities;
using Univali.Api.ValidationAttributes;

namespace Univali.Api.Features.Customers.Commands.PatchCustomer;

public class PatchCustomerCommand : IRequest<PatchCustomerReturnDto?>
{
    public JsonPatchDocument<PatchCustomerDto> PatchDocument {get; set;} = new();
    public int Id {get; set;}
}