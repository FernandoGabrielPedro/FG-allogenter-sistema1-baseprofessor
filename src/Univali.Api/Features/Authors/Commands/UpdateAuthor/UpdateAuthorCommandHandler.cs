using AutoMapper;
using Univali.Api.Entities;
using Univali.Api.Repositories;
using MediatR;


namespace Univali.Api.Features.Authors.Commands.UpdateAuthor;

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, bool>{
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;

    public UpdateAuthorCommandHandler(IPublisherRepository publisherRepository, IMapper mapper)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var authorForUpdate = _mapper.Map<Author>(request);
        if(authorForUpdate == null) return false;

        var rightAuthor = await _publisherRepository.GetAuthorByIdAsync(request.Id);
        _publisherRepository.UpdateAuthor(authorForUpdate, rightAuthor!);
        await _publisherRepository.SaveChangesAsync();

        return true;
    }
}