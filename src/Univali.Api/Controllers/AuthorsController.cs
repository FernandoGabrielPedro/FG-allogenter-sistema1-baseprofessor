using Univali.Api.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Univali.Api.Features.Authors.Queries.GetAuthorsDetail;
using Univali.Api.Features.Authors.Queries.GetAuthorDetail;
using Univali.Api.Features.Authors.Queries.GetAuthorsWithCoursesDetail;
using Univali.Api.Features.Authors.Queries.GetAuthorWithCoursesDetail;
using Univali.Api.Features.Authors.Commands.UpdateAuthor;
using Univali.Api.Features.Authors.Commands.CreateAuthor;
using Univali.Api.Features.Authors.Commands.DeleteAuthor;

namespace Univali.Api.Controllers;

[ApiController]
[Route("api/authors")]
public class AuthorsController : MainController {
    private readonly IMapper _mapper;
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMediator _mediator;
    public AuthorsController (IMapper mapper, IPublisherRepository publisherRepository, IMediator mediator) {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _publisherRepository = publisherRepository ?? throw new ArgumentNullException(nameof(publisherRepository));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetAuthorsDetailDto>>> GetAuthorsAsync()
    {
        GetAuthorsDetailQuery getAuthorsDetailQuery = new GetAuthorsDetailQuery();
        IEnumerable<GetAuthorsDetailDto?> authorsToReturn = await _mediator.Send(getAuthorsDetailQuery);

        return Ok(authorsToReturn);
    }

    [HttpGet("{id}", Name = "GetAuthorById")]
    public async Task<ActionResult<GetAuthorDetailDto>> GetAuthorByIdAsync(int id)
    {
        GetAuthorDetailQuery getAuthorDetailQuery = new GetAuthorDetailQuery {Id = id};
        GetAuthorDetailDto? authorToReturn = await _mediator.Send(getAuthorDetailQuery);

        if (authorToReturn == null) return NotFound();

        return Ok(authorToReturn);
    }

    [HttpGet("with-courses")]
    public async Task<ActionResult<IEnumerable<AuthorForGetAuthorsWithCoursesDetailDto>>> GetAuthorsWithCoursesAsync()
    {
        GetAuthorsWithCoursesDetailQuery getAuthorsWithCoursesDetailQuery = new GetAuthorsWithCoursesDetailQuery();
        IEnumerable<AuthorForGetAuthorsWithCoursesDetailDto?> authorsToReturn = await _mediator.Send(getAuthorsWithCoursesDetailQuery);

        return Ok(authorsToReturn);
    }

    [HttpGet("with-courses/{id}", Name = "GetAuthorWithCoursesById")]
    public async Task<ActionResult<AuthorForGetAuthorWithCoursesDetailDto>> GetAuthorWithCoursesByIdAsync(int id)
    {
        GetAuthorWithCoursesDetailQuery getAuthorWithCoursesDetailQuery = new GetAuthorWithCoursesDetailQuery {Id = id};
        AuthorForGetAuthorWithCoursesDetailDto? authorToReturn = await _mediator.Send(getAuthorWithCoursesDetailQuery);

        if (authorToReturn == null) return NotFound();

        return Ok(authorToReturn);
    }

    [HttpPost]
    public async Task<ActionResult<CreateAuthorDto>> CreateAuthorAsync(CreateAuthorCommand createAuthorCommand) {

        CreateAuthorDto authorToReturn = await _mediator.Send(createAuthorCommand);
        
        return CreatedAtRoute
        (
            "GetAuthorById",
            new { id = authorToReturn.Id },
            authorToReturn
        );
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAuthorAsync(UpdateAuthorCommand updateAuthorCommand, int id)
    {
        if (id != updateAuthorCommand.Id) return BadRequest();

        bool result = await _mediator.Send(updateAuthorCommand);
        if(!result) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAuthorAsync(int id)
    {
        DeleteAuthorCommand deleteAuthorCommand = new DeleteAuthorCommand {Id = id};
        bool result = await _mediator.Send(deleteAuthorCommand);
        if(!result) return NotFound();
        return NoContent();
    }
}