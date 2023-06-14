namespace Univali.Api.Entities;

public class User {
    public int Id {get; set;}
    public string Name {get; set;} = String.Empty;
    public string Username {get; set;} = String.Empty;
    public string Password {get; set;} = String.Empty;
}