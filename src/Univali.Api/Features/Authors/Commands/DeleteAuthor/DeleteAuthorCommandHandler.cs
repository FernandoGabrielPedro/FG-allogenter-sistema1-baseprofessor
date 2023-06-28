using AutoMapper;
using Univali.Api.Repositories;
using MediatR;

namespace Univali.Api.Features.Authors.Commands.DeleteAuthor;

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, bool>{
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;

    public DeleteAuthorCommandHandler(IPublisherRepository publisherRepository, IMapper mapper)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _publisherRepository.GetAuthorByIdAsync(request.Id);
        if(author == null) return false;
        
        _publisherRepository.DeleteAuthor(author);
        await _publisherRepository.SaveChangesAsync();
        return true;

    }
}
