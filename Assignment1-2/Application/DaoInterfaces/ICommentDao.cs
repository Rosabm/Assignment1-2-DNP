using Domain.Models;

namespace Application.DaoInterfaces;

public interface ICommentDao
{
    Task<Comment> CreateAsync(Comment comment);
    public Task<IEnumerable<Comment?>> GetAsync();
    public Task<IEnumerable<Comment?>> GetByPostAsync(int postId);
}