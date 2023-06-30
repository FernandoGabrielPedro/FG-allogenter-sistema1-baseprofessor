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
[Route("api/publishers/{publisherId}/courses")]
public class CoursesController : MainController {
    private readonly IMapper _mapper;
    private readonly IPublisherRepository _publisherRepository;
    private readonly IMediator _mediator;
    public CoursesController (IMapper mapper, IPublisherRepository publisherRepository, IMediator mediator) {
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
    */

    [HttpGet("with-authors")]
    public async Task<ActionResult<IEnumerable<CourseForGetCoursesWithAuthorsDetailDto>>> GetCoursesWithAuthorsAsync(int publisherId)
    {
        GetCoursesWithAuthorsDetailQuery getCoursesWithAuthorsDetailQuery = new GetCoursesWithAuthorsDetailQuery{PublisherId = publisherId};
        IEnumerable<CourseForGetCoursesWithAuthorsDetailDto?> coursesToReturn = await _mediator.Send(getCoursesWithAuthorsDetailQuery);

        return Ok(coursesToReturn);
    }

    [HttpGet("with-authors/{courseId}", Name = "GetCourseWithAuthorsById")]
    public async Task<ActionResult<CourseForGetCourseWithAuthorsDetailDto>> GetCourseWithAuthorsByIdAsync(int publisherId, int courseId)
    {
        GetCourseWithAuthorsDetailQuery getCourseDetailQuery = new GetCourseWithAuthorsDetailQuery {CourseId = courseId, PublisherId = publisherId};
        CourseForGetCourseWithAuthorsDetailDto? courseToReturn = await _mediator.Send(getCourseDetailQuery);

        if (courseToReturn == null) return NotFound();

        return Ok(courseToReturn);
    }

    /*[HttpPost]
    public async Task<ActionResult<CreateCourseDto>> CreateCourseAsync(CreateCourseCommand createCourseCommand) {

        CreateCourseDto courseToReturn = await _mediator.Send(createCourseCommand);
        
        return CreatedAtRoute
        (
            "GetCourseById",
            new { id = courseToReturn.Id },
            courseToReturn
        );
    }*/
    
    [HttpPost("with-authors")]
    public async Task<ActionResult<CourseForCreateCourseWithAuthorsDto>> CreateCourseWithAuthorsAsync(int publisherId, CreateCourseWithAuthorsCommand createCourseWithAuthorsCommand) {
        var publisher = await _publisherRepository.GetPublisherByIdAsync(publisherId);
        if(publisher == null) return BadRequest();

        createCourseWithAuthorsCommand.publisherId = publisherId;

        CourseForCreateCourseWithAuthorsDto courseToReturn = await _mediator.Send(createCourseWithAuthorsCommand);
        
        return CreatedAtRoute
        (
            "GetCourseWithAuthorsById",
            new { courseId = courseToReturn.Id, publisherId = courseToReturn.PublisherId},
            courseToReturn
        );
    }

    [HttpPut("{courseId}")]
    public async Task<ActionResult> UpdateCourseAsync(int publisherId, int courseId, CourseForUpdateCourseDto courseForUpdateCourseDto)
    {
        if (courseId != courseForUpdateCourseDto.Id) return BadRequest();

        UpdateCourseCommand updateCourseCommand= new UpdateCourseCommand {PublisherId = publisherId, CourseForUpdateCourseDto = courseForUpdateCourseDto};
        bool result = await _mediator.Send(updateCourseCommand);
        if(!result) return NotFound();

        return NoContent();
    }

    [HttpDelete("{courseId}")]
    public async Task<ActionResult> DeleteCourseAsync(int publisherId, int courseId)
    {
        DeleteCourseCommand deleteCourseCommand = new DeleteCourseCommand {PublisherId = publisherId, CourseId = courseId};
        bool result = await _mediator.Send(deleteCourseCommand);
        if(!result) return NotFound();
        return NoContent();
    }
}