using MediatR;

namespace Univali.Api.Features.Authors.Commands.DeleteAuthor;

public class DeleteAuthorCommand : IRequest<bool>{
    public int Id {get;set;}
}
