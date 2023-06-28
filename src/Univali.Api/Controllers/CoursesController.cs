using Univali.Api.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Univali.Api.Features.Courses.Commands.CreateCourse;
using Univali.Api.Features.Courses.Commands.DeleteCourse;
using Univali.Api.Features.Courses.Commands.UpdateCourse;
using Univali.Api.Features.Courses.Queries.GetCourseDetail;
using Univali.Api.Features.Courses.Queries.GetCoursesDetail;
using Univali.Api.Features.Courses.Queries.GetCoursesWithAuthorsDetail;
using Univali.Api.Features.Courses.Queries.GetCourseWithAuthorsDetail;
using Univali.Api.Features.Courses.Commands.CreateCourseWithAuthors;

namespace Univali.Api.Controllers;

[ApiController]
[Route("api/courses")]
public class CoursesController : MainController {
    private readonly IMapper _mapper;
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMediator _mediator;
    public CoursesController (IMapper mapper, IPublisherRepository publisherRepository, IMediator mediator) {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _publisherRepository = publisherRepository ?? throw new ArgumentNullException(nameof(publisherRepository));
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

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

    [HttpPost]
    public async Task<ActionResult<CreateCourseDto>> CreateCourseAsync(CreateCourseCommand createCourseCommand) {

        CreateCourseDto courseToReturn = await _mediator.Send(createCourseCommand);
        
        return CreatedAtRoute
        (
            "GetCourseById",
            new { id = courseToReturn.Id },
            courseToReturn
        );
    }

    
    [HttpPost("with-authors")]
    public async Task<ActionResult<CourseForCreateCourseWithAuthorsDto>> CreateCourseWithAuthorsAsync(CreateCourseWithAuthorsCommand createCourseWithAuthorsCommand) {

        CourseForCreateCourseWithAuthorsDto courseToReturn = await _mediator.Send(createCourseWithAuthorsCommand);
        
        return CreatedAtRoute
        (
            "GetCourseWithAuthorsById",
            new { id = courseToReturn.Id },
            courseToReturn
        );
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCourseAsync(UpdateCourseCommand updateCourseCommand, int id)
    {
        if (id != updateCourseCommand.Id) return BadRequest();

        bool result = await _mediator.Send(updateCourseCommand);
        if(!result) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCourseAsync(int id)
    {
        DeleteCourseCommand deleteCourseCommand = new DeleteCourseCommand {Id = id};
        bool result = await _mediator.Send(deleteCourseCommand);
        if(!result) return NotFound();
        return NoContent();
    }
}