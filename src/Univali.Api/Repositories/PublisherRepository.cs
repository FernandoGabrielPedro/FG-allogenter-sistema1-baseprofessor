using Univali.Api.DbContexts;
using Univali.Api.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Univali.Api.Repositories;

public class PublisherRepository : IPublisherRepository {
    private readonly PublisherContext _context;
    private readonly IMapper _mapper;

    public PublisherRepository(PublisherContext customerContext, IMapper mapper) {
        _context = customerContext ?? throw new ArgumentNullException(nameof(customerContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<bool> SaveChangesAsync() {
        return (await _context.SaveChangesAsync() > 0);
    }

    public async Task<IEnumerable<Author>> GetAuthorsAsync() {
        return await _context.Authors.OrderBy(c => c.Id).ToListAsync();
    }
    public async Task<IEnumerable<Author>> GetAuthorsWithCoursesAsync() {
        return await _context.Authors.Include(a => a.Courses).OrderBy(c => c.Id).ToListAsync();
    }
    public async Task<Author?> GetAuthorByIdAsync(int id) {
        return await _context.Authors.FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task<Author?> GetAuthorWithCoursesByIdAsync(int id) {
        return await _context.Authors.Include(a => a.Courses).FirstOrDefaultAsync(c => c.Id == id);
    }
    public void CreateAuthor(Author authorEntity) {
        _context.Authors.Add(authorEntity);
    }
    public void UpdateAuthor(Author authorEntity, Author newAuthorValues) {
        _mapper.Map(authorEntity, newAuthorValues);
    }
    public void DeleteAuthor(Author authorEntity) {
        _context.Remove(authorEntity);
    }

    public async Task<IEnumerable<Course>> GetCoursesAsync() {
        return await _context.Courses.OrderBy(c => c.Id).ToListAsync();
    }
    public async Task<IEnumerable<Course>> GetCoursesWithAuthorsAsync() {
        return await _context.Courses.Include(c => c.Authors).OrderBy(c => c.Id).ToListAsync();
    }
    public async Task<Course?> GetCourseByIdAsync(int id) {
        return await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
    }
    public async Task<Course?> GetCourseWithAuthorsByIdAsync(int id) {
        return await _context.Courses.Include(c => c.Authors).FirstOrDefaultAsync(c => c.Id == id);
    }
    public void CreateCourse(Course courseEntity) {
        _context.Courses.Add(courseEntity);
    }
    public void UpdateCourse(Course courseEntity, Course newCourseValues) {
        _mapper.Map(courseEntity, newCourseValues);
    }
    public void DeleteCourse(Course courseEntity) {
        _context.Courses.Remove(courseEntity);
    }

    public async Task<IEnumerable<Publisher>> GetPublishersAsync() {
        return await _context.Publishers.OrderBy(c => c.Id).ToListAsync();
    }
    public async Task<IEnumerable<Publisher>> GetPublishersWithCoursesAsync() {
        return await _context.Publishers.Include(p => p.Courses).OrderBy(c => c.Id).ToListAsync();
    }
    public async Task<IEnumerable<Publisher>> GetPublishersWithCoursesWithAuthorsAsync() {
        return await _context.Publishers.Include(p => p.Courses).ThenInclude(c => c.Authors).OrderBy(c => c.Id).ToListAsync();
    }
    public async Task<Publisher?> GetPublisherByIdAsync(int id) {
        return await _context.Publishers.FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<Publisher?> GetPublisherWithCoursesByIdAsync(int id) {
        return await _context.Publishers.Include(p => p.Courses).FirstOrDefaultAsync(p => p.Id == id);
    }
    public async Task<Publisher?> GetPublisherWithCoursesWithAuthorsByIdAsync(int id) {
        return await _context.Publishers.Include(p => p.Courses).ThenInclude(c => c.Authors).FirstOrDefaultAsync(p => p.Id == id);
    }
    public void CreatePublisher(Publisher publisherEntity) {
        _context.Publishers.Add(publisherEntity);
    }
    public void UpdatePublisher(Publisher publisherEntity, Publisher newPublisherValues) {
        _mapper.Map(publisherEntity, newPublisherValues);
    }
    public void DeletePublisher(Publisher publisherEntity) {
        _context.Publishers.Remove(publisherEntity);
    }
}