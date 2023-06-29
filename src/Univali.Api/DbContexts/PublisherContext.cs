using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Univali.Api.Entities;

namespace Univali.Api.DbContexts;

public class PublisherContext : DbContext
{
    public DbSet<Publisher> Publishers {get; set;} = null!;
    public DbSet<Author> Authors {get; set;} = null!;
    public DbSet<Course> Courses {get; set;} = null!;

    //public DbSet<AuthorCourse> AuthorCourses {get; set;} = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(
            "Host=localhost;Database=Univali;Username=postgres;Password=123456"
        ).LogTo(Console.WriteLine,
        new[] {DbLoggerCategory.Database.Command.Name},
        LogLevel.Information)
        .EnableSensitiveDataLogging();
    }

    //public PublisherContext(DbContextOptions<PublisherContext> options)
    //: base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var author = modelBuilder.Entity<Author>();

        author
            .HasMany(a => a.Courses)
            .WithMany(c => c.Authors)
            .UsingEntity<AuthorCourse>(
                "AuthorsCourses",
                ac => ac.Property(ac => ac.CreatedOn).HasDefaultValueSql("NOW()"));

        author
            .Property(a => a.FirstName)
            .HasMaxLength(30)
            .IsRequired();

        author
            .Property(a => a.LastName)
            .HasMaxLength(30)
            .IsRequired();

        /*author
            .HasData(
                new Author {
                    Id = 2,
                    FirstName = "Autor",
                    LastName = "1"
                }
            );*/

        
        var course = modelBuilder.Entity<Course>();

        course
            .Property(c => c.Title)
            .HasMaxLength(60)
            .IsRequired();

        course
            .Property(c => c.Price)
            .HasPrecision(5,2)
            .HasColumnName("BasePrice")
            .IsRequired();

        course
            .Property(c => c.Description)
            .IsRequired(false);

        /*course
            .HasData(
                new Course {
                    Id = 1,
                    Title = "Curso",
                    Description = "Legal",
                    Price = 110.99
                }
            );*/


        var publisher = modelBuilder.Entity<Publisher>();

        publisher
            .Property(a => a.FirstName)
            .HasMaxLength(30)
            .IsRequired();

        publisher
            .Property(a => a.LastName)
            .HasMaxLength(30)
            .IsRequired();

        publisher
            .Property(p => p.Cpf)
            .HasMaxLength(11)
            .IsFixedLength();
        
        base.OnModelCreating(modelBuilder);
    }
}