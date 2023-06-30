using AutoMapper;
using Univali.Api.Repositories;
using MediatR;

namespace Univali.Api.Features.Publishers.Commands.DeletePublisher;

public class DeletePublisherCommandHandler : IRequestHandler<DeletePublisherCommand, bool>{
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMapper _mapper;

    public DeletePublisherCommandHandler(IPublisherRepository publisherRepository, IMapper mapper)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
    {
        var publisher = await _publisherRepository.GetPublisherByIdAsync(request.Id);
        if(publisher == null) return false;
        
        _publisherRepository.DeletePublisher(publisher);
        await _publisherRepository.SaveChangesAsync();
        return true;
    }
}