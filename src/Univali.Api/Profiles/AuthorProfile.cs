using AutoMapper;

namespace Univali.Api.Profiles;

public class AuthorProfile : Profile {
    public AuthorProfile() {
        //1º arg: objeto de origem
        //2º arg: objeto de destino

        CreateMap<Entities.Author, Entities.Author>();

        CreateMap<Entities.Author, Univali.Api.Features.Authors.Queries.GetAuthorDetail.GetAuthorDetailDto>();
        CreateMap<Entities.Author, Univali.Api.Features.Authors.Queries.GetAuthorsDetail.GetAuthorsDetailDto>();
        CreateMap<Entities.Author, Univali.Api.Features.Authors.Queries.GetAuthorsWithCoursesDetail.AuthorForGetAuthorsWithCoursesDetailDto>();
        CreateMap<Entities.Author, Univali.Api.Features.Authors.Queries.GetAuthorWithCoursesDetail.AuthorForGetAuthorWithCoursesDetailDto>();
        CreateMap<Univali.Api.Features.Authors.Commands.CreateAuthor.CreateAuthorCommand, Entities.Author>();
        CreateMap<Entities.Author, Univali.Api.Features.Authors.Commands.CreateAuthor.CreateAuthorDto>();
        CreateMap<Univali.Api.Features.Authors.Commands.UpdateAuthor.UpdateAuthorCommand, Entities.Author>();

        CreateMap<Entities.Author, Univali.Api.Features.Courses.Queries.GetCoursesWithAuthorsDetail.AuthorForGetCoursesWithAuthorsDetailDto>();
        CreateMap<Entities.Author, Univali.Api.Features.Courses.Queries.GetCourseWithAuthorsDetail.AuthorForGetCourseWithAuthorsDetailDto>();
        CreateMap<Entities.Author, Univali.Api.Features.Courses.Commands.CreateCourseWithAuthors.AuthorForCreateCourseWithAuthorsCommand>();
        CreateMap<Univali.Api.Features.Courses.Commands.CreateCourseWithAuthors.AuthorForCreateCourseWithAuthorsDto, Entities.Author>();
        CreateMap<Univali.Api.Features.Courses.Commands.CreateCourseWithAuthors.AuthorForCreateCourseWithAuthorsCommand, Entities.Author>();
        CreateMap<Entities.Author, Univali.Api.Features.Courses.Commands.CreateCourseWithAuthors.AuthorForCreateCourseWithAuthorsDto>();
    }
}