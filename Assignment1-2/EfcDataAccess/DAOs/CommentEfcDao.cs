using Application.DaoInterfaces;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class CommentEfcDao : ICommentDao
{
    private readonly EfcContext context;

    public CommentEfcDao(EfcContext context)
    {
        this.context = context;
    }

    public async Task<Comment> CreateAsync(Comment comment)
    {
        EntityEntry<Comment> newComment = await context.comments.AddAsync(comment);
        await context.SaveChangesAsync();
        return newComment.Entity;
    }

    public async Task<IEnumerable<Comment?>> GetAsync()
    {
        IQueryable<Comment> commentsQuery = context.comments.AsQueryable();
        IEnumerable<Comment> result = await commentsQuery.ToListAsync();
        return result;
    }

    public async Task<IEnumerable<Comment?>> GetByPostAsync(int postId)
    {
        IQueryable<Comment> commentsQuery = context.comments.AsQueryable();
        commentsQuery = commentsQuery.Where(c => c.Post.Id==postId);
        IEnumerable<Comment> result = await commentsQuery.ToListAsync();
        return result;
    }
}