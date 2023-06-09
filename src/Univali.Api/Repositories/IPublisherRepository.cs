using Univali.Api.Entities;

namespace Univali.Api.Repositories;

public interface IPublisherRepository {
    Task<bool> SaveChangesAsync();

    Task<IEnumerable<Author>> GetAuthorsAsync();
    Task<IEnumerable<Author>> GetAuthorsWithCoursesAsync();
    Task<Author?> GetAuthorByIdAsync(int id);
    Task<Author?> GetAuthorWithCoursesByIdAsync(int id);
    void CreateAuthor(Author authorEntity);
    void UpdateAuthor(Author authorEntity, Author newAuthorValues);
    void DeleteAuthor(Author authorEntity);

    Task<IEnumerable<Course>> GetCoursesAsync();
    Task<IEnumerable<Course>> GetCoursesWithAuthorsAsync();
    Task<Course?> GetCourseByIdAsync(int id);
    Task<Course?> GetCourseWithAuthorsByIdAsync(int id);
    void CreateCourse(Course courseEntity);
    void UpdateCourse(Course courseEntity, Course newCourseValues);
    void DeleteCourse(Course courseEntity);

    Task<IEnumerable<Publisher>> GetPublishersAsync();
    Task<IEnumerable<Publisher>> GetPublishersWithCoursesAsync();
    Task<IEnumerable<Publisher>> GetPublishersWithCoursesWithAuthorsAsync();
    Task<Publisher?> GetPublisherByIdAsync(int id);
    Task<Publisher?> GetPublisherWithCoursesByIdAsync(int id);
    Task<Publisher?> GetPublisherWithCoursesWithAuthorsByIdAsync(int id);
    void CreatePublisher(Publisher publisherEntity);
    void UpdatePublisher(Publisher publisherEntity, Publisher newPublisherValues);
    void DeletePublisher(Publisher publisherEntity);
}