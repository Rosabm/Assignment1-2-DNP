namespace Domain.Models;

public class Comment
{
 
    public int Id { get; set; }
    public int PostId{ get; set; }
    public string Message { get; set; }
    public User Owner { get; }
    
       public Comment( User owner,int postId, string message)
        {
            PostId = postId;
            Message = message;
            Owner = owner;
        }

    
}