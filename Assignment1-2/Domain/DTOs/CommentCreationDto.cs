namespace Domain.DTOs;

public class CommentCreationDto
{

    public string OwnerUsername { get; }
    public int PostId { get; }
    public string Message { get; }
    
    public CommentCreationDto(string ownerUsername, int postId, string message)
    {
        OwnerUsername = ownerUsername;
        PostId = postId;
        Message = message;
    }

}