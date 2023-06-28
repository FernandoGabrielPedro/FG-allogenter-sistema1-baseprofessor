using AutoMapper;

namespace Univali.Api.Profiles;

public class CoursesProfile : Profile {
    public CoursesProfile() {
        //1º arg: objeto de origem
        //2º arg: objeto de destino

        CreateMap<Entities.Course, Entities.Course>();

        CreateMap<Entities.Course, Univali.Api.Features.Courses.Queries.GetCourseDetail.GetCourseDetailDto>();
        CreateMap<Entities.Course, Univali.Api.Features.Courses.Queries.GetCoursesDetail.GetCoursesDetailDto>();
        CreateMap<Entities.Course, Univali.Api.Features.Courses.Queries.GetCoursesWithAuthorsDetail.CourseForGetCoursesWithAuthorsDetailDto>();
        CreateMap<Entities.Course, Univali.Api.Features.Courses.Queries.GetCourseWithAuthorsDetail.CourseForGetCourseWithAuthorsDetailDto>();
        CreateMap<Univali.Api.Features.Courses.Commands.CreateCourse.CreateCourseCommand, Entities.Course>();
        CreateMap<Entities.Course, Univali.Api.Features.Courses.Commands.CreateCourse.CreateCourseDto>();
        CreateMap<Univali.Api.Features.Courses.Commands.UpdateCourse.UpdateCourseCommand, Entities.Course>();
        CreateMap<Entities.Course, Univali.Api.Features.Courses.Commands.CreateCourseWithAuthors.CreateCourseWithAuthorsCommand>();
        CreateMap<Univali.Api.Features.Courses.Commands.CreateCourseWithAuthors.CourseForCreateCourseWithAuthorsDto, Entities.Course>();
        CreateMap<Univali.Api.Features.Courses.Commands.CreateCourseWithAuthors.CreateCourseWithAuthorsCommand, Entities.Course>();
        CreateMap<Entities.Course, Univali.Api.Features.Courses.Commands.CreateCourseWithAuthors.CourseForCreateCourseWithAuthorsDto>();

        CreateMap<Entities.Course, Univali.Api.Features.Authors.Queries.GetAuthorsWithCoursesDetail.CourseForGetAuthorsWithCoursesDetailDto>();
        CreateMap<Entities.Course, Univali.Api.Features.Authors.Queries.GetAuthorWithCoursesDetail.CourseForGetAuthorWithCoursesDetailDto>();
    }
}