using AutoMapper;
using Univali.Api.Entities;
using Univali.Api.Repositories;
using MediatR;

namespace Univali.Api.Features.Publishers.Commands.CreatePublisher;

public class CreatePublisherCommandHandler : IRequestHandler<CreatePublisherCommand, CreatePublisherDto>
{
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;

    public CreatePublisherCommandHandler(IPublisherRepository publisherRepository, IMapper mapper)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
    }

    public async Task<CreatePublisherDto> Handle(CreatePublisherCommand request, CancellationToken cancellationToken)
    {
        var publisherEntity = _mapper.Map<Publisher>(request);
        _publisherRepository.CreatePublisher(publisherEntity);
        await _publisherRepository.SaveChangesAsync();
        var publisherForReturn = _mapper.Map<CreatePublisherDto>(publisherEntity);
        return publisherForReturn;
    }
}