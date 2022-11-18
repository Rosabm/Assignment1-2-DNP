namespace Domain.Models;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public User Owner { get; private set; }
    
    public ICollection<Comment> Comments { get; set; }


    public Post(User owner, string title, string body)
    {
        Owner = owner;
        Title = title;
        Body = body;
    }

    private Post()
    {
    }
}