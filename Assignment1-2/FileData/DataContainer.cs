using Domain.Models;

namespace FileData;

public class DataContainer
{
    public ICollection<User> users { get; set; }
    public ICollection<Post> posts { get; set; }
    public ICollection<Comment> comments{ get; set; }
}