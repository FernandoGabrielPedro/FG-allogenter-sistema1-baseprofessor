using AutoMapper;
using Univali.Api.Entities;
using Univali.Api.Repositories;
using MediatR;

namespace Univali.Api.Features.Publishers.Commands.UpdatePublisher;

public class UpdatePublisherCommandHandler : IRequestHandler<UpdatePublisherCommand, bool>
{
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;

    public UpdatePublisherCommandHandler(IPublisherRepository publisherRepository, IMapper mapper)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
    {
        var publisherForUpdate = _mapper.Map<Publisher>(request);
        if(publisherForUpdate == null) return false;

        var rightPublisher = await _publisherRepository.GetPublisherByIdAsync(request.Id);
        _publisherRepository.UpdatePublisher(publisherForUpdate, rightPublisher!);
        await _publisherRepository.SaveChangesAsync();

        return true;
    }
}