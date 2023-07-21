namespace UserProyect.Models;

public class UserData
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public Guid IdProfile { get; set; }

    public string ProfileName { get; set; }
}
