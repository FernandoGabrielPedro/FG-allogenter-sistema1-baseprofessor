using System.ComponentModel.DataAnnotations;
using MediatR;
using Univali.Api.ValidationAttributes;

namespace Univali.Api.Features.Addresses.Commands.DeleteAddress;

public class DeleteAddressCommand : IRequest<bool>
{
    public int Id { get; set; }
}