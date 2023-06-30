using Univali.Api.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Univali.Api.Features.Publishers.Commands.CreatePublisher;
using Univali.Api.Features.Publishers.Commands.UpdatePublisher;
using Univali.Api.Features.Publishers.Commands.DeletePublisher;

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

    /*
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetCoursesDetailDto>>> GetCoursesAsync()
    {
        GetCoursesDetailQuery getCoursesDetailQuery = new GetCoursesDetailQuery();
        IEnumerable<GetCoursesDetailDto?> coursesToReturn = await _mediator.Send(getCoursesDetailQuery);

        return Ok(coursesToReturn);
    }

    [HttpGet("{id}", Name = "GetCourseById")]
    public async Task<ActionResult<GetCourseDetailDto>> GetCourseByIdAsync(int id)
    {
        GetCourseDetailQuery getCourseDetailQuery = new GetCourseDetailQuery {Id = id};
        GetCourseDetailDto? courseToReturn = await _mediator.Send(getCourseDetailQuery);

        if (courseToReturn == null) return NotFound();

        return Ok(courseToReturn);
    }

    [HttpGet("with-authors")]
    public async Task<ActionResult<IEnumerable<CourseForGetCoursesWithAuthorsDetailDto>>> GetCoursesWithAuthorsAsync()
    {
        GetCoursesWithAuthorsDetailQuery getCoursesWithAuthorsDetailQuery = new GetCoursesWithAuthorsDetailQuery();
        IEnumerable<CourseForGetCoursesWithAuthorsDetailDto?> coursesToReturn = await _mediator.Send(getCoursesWithAuthorsDetailQuery);

        return Ok(coursesToReturn);
    }

    [HttpGet("with-authors/{id}", Name = "GetCourseWithAuthorsById")]
    public async Task<ActionResult<CourseForGetCourseWithAuthorsDetailDto>> GetCourseWithAuthorsByIdAsync(int id)
    {
        GetCourseWithAuthorsDetailQuery getCourseDetailQuery = new GetCourseWithAuthorsDetailQuery {Id = id};
        CourseForGetCourseWithAuthorsDetailDto? courseToReturn = await _mediator.Send(getCourseDetailQuery);

        if (courseToReturn == null) return NotFound();

        return Ok(courseToReturn);
    }
    */

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