namespace Domain.Models;

public class Comment
{
 
    public int Id { get; set; }
    public Post Post{ get; set; }
    public string Message { get; set; }
    public User Owner { get; private set; }
    
       public Comment( User owner,Post post, string message)
        {
            Post = post;
            Message = message;
            Owner = owner;
        }
       
       private Comment(){}

    
}