using AutoMapper;
using Univali.Api.Entities;
using Univali.Api.Repositories;
using MediatR;

namespace Univali.Api.Features.Authors.Commands.CreateAuthor;

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, CreateAuthorDto>{
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;

    public CreateAuthorCommandHandler(IPublisherRepository publisherRepository, IMapper mapper)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
    }

    public async Task<CreateAuthorDto> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var authorEntity = _mapper.Map<Author>(request);
        _publisherRepository.CreateAuthor(authorEntity);
        await _publisherRepository.SaveChangesAsync();
        var authorForReturn = _mapper.Map<CreateAuthorDto>(authorEntity);
        return authorForReturn;
    }
}
