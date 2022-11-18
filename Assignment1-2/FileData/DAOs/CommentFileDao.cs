using Application.DaoInterfaces;
using Domain.Models;

namespace FileData.DAOs;

public class CommentFileDao : ICommentDao
{
    private readonly FileContext context;

    public CommentFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<Comment> CreateAsync(Comment comment)
    {
        int commentId = 1;
        if (context.Comments.Any())
        {
            commentId = context.Comments.Max(c => c.Id);
            commentId++;
        }

        comment.Id = commentId;

        context.Comments.Add(comment);
        context.SaveChanges();

        return Task.FromResult(comment);
    }

    public Task<IEnumerable<Comment?>> GetAsync()
    {
        IEnumerable<Comment> result = context.Comments.AsEnumerable();
        return Task.FromResult(result);
    }

    public Task<IEnumerable<Comment?>> GetByPostAsync(int postId)
    {
           // we know postId is unique
           IEnumerable<Comment> result = context.Comments.Where(c =>
                c.Post.Id == postId);
           return Task.FromResult(result);
    }
}