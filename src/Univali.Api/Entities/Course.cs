namespace Univali.Api.Entities;

public class Course {
    public int Id {get; set;}
    public string Title {get; set;} = string.Empty;
    public string Description {get; set;} = string.Empty;
    public double Price {get; set;}
    public Publisher Publisher {get; set;} = new();
    public int PublisherId {get; set;}
    public List<Author> Authors {get; set;} = new();

    public Course(string title, string description, double price)
    {
        Title = title;
        Description = description;
        Price = price;
    }

    public Course() {}
}