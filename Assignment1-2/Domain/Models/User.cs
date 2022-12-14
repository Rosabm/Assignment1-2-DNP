namespace Domain.Models;

public class User
{
    public User()
    {
    }

    public User(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }

    public string UserName { get; set; }
    public string Password { get; set; }
    public ICollection<Post> Posts { get; set; }
    public ICollection<Comment> Comments { get; set; }
}