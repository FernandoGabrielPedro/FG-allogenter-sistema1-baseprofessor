using Univali.Api.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Univali.Api.Features.Publishers.Commands.CreatePublisher;
using Univali.Api.Features.Publishers.Commands.UpdatePublisher;
using Univali.Api.Features.Publishers.Commands.DeletePublisher;
using Univali.Api.Features.Publishers.Queries.GetPublisherDetail;
using Univali.Api.Features.Publishers.Queries.GetPublishersDetail;
using Univali.Api.Features.Publishers.Queries.GetPublishersWithCoursesDetail;
using Univali.Api.Features.Publishers.Queries.GetPublisherWithCoursesDetail;

namespace Univali.Api.Controllers;

[ApiController]
[Route("api/publishers")]
public class PublishersController : MainController {
    private readonly IMapper _mapper;
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMediator _mediator;
    public PublishersController (IMapper mapper, IPublisherRepository publisherRepository, IMediator mediator) {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _publisherRepository = publisherRepository ?? throw new ArgumentNullException(nameof(publisherRepository));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet()]
    public async Task<ActionResult<GetPublishersDetailDto>> GetPublishersAsync()
    {
        GetPublishersDetailQuery getPublisherDetailQuery = new GetPublishersDetailQuery();
        IEnumerable<GetPublishersDetailDto>? publishersToReturn = await _mediator.Send(getPublisherDetailQuery);

        if (publishersToReturn == null) return NotFound();

        return Ok(publishersToReturn);
    }

    [HttpGet("{id}", Name = "GetPublisherById")]
    public async Task<ActionResult<GetPublisherDetailDto>> GetPublisherByIdAsync(int id)
    {
        GetPublisherDetailQuery getPublisherDetailQuery = new GetPublisherDetailQuery {Id = id};
        GetPublisherDetailDto? publisherToReturn = await _mediator.Send(getPublisherDetailQuery);

        if (publisherToReturn == null) return NotFound();

        return Ok(publisherToReturn);
    }

    [HttpGet("with-courses")]
    public async Task<ActionResult<IEnumerable<PublisherForGetPublishersWithCoursesDetailDto>>> GetPublishersWithCoursesAsync()
    {
        GetPublishersWithCoursesDetailQuery getPublishersWithCoursesDetailQuery = new GetPublishersWithCoursesDetailQuery();
        IEnumerable<PublisherForGetPublishersWithCoursesDetailDto?> publishersToReturn = await _mediator.Send(getPublishersWithCoursesDetailQuery);

        if (publishersToReturn == null) return NotFound();

        return Ok(publishersToReturn);
    }

    [HttpGet("with-courses/{id}", Name = "GetPublisherWithCoursesById")]
    public async Task<ActionResult<PublisherForGetPublisherWithCoursesDetailDto>> GetPublisherWithCoursesByIdAsync(int id)
    {
        GetPublisherWithCoursesDetailQuery getPublisherWithCoursesDetailQuery = new GetPublisherWithCoursesDetailQuery{Id = id};
        PublisherForGetPublisherWithCoursesDetailDto? publisherToReturn = await _mediator.Send(getPublisherWithCoursesDetailQuery);

        if (publisherToReturn == null) return NotFound();

        return Ok(publisherToReturn);
    }

    [HttpPost]
    public async Task<ActionResult<CreatePublisherDto>> CreatePublisherAsync(CreatePublisherCommand createPublisherCommand) {

        CreatePublisherDto publisherToReturn = await _mediator.Send(createPublisherCommand);
        
        return CreatedAtRoute
        (
            "GetPublisherById",
            new { id = publisherToReturn.Id },
            publisherToReturn
        );
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePublisherAsync(UpdatePublisherCommand updatePublisherCommand, int id)
    {
        if (id != updatePublisherCommand.Id) return BadRequest();

        bool result = await _mediator.Send(updatePublisherCommand);
        if(!result) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePublisherAsync(int id)
    {
        DeletePublisherCommand deletePublisherCommand = new DeletePublisherCommand {Id = id};
        bool result = await _mediator.Send(deletePublisherCommand);
        if(!result) return NotFound();
        return NoContent();
    }
}