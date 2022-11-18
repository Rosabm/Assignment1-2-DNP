using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class PostEfcDao :IPostDao
{
    private readonly EfcContext context;

    public PostEfcDao(EfcContext context)
    {
        this.context = context;
    }

    public async Task<Post> CreateAsync(Post post)
    {
        EntityEntry<Post> newPost = await context.posts.AddAsync(post);
        await context.SaveChangesAsync();
        return newPost.Entity;
    }

    public async Task<IEnumerable<Post>> GetAsync(SearchPostParametersDto searchParameters)
    {
        IQueryable<Post> postsQuery = context.posts.AsQueryable();
        IEnumerable<Post> result = await postsQuery.ToListAsync();
        return result;
    }

    public async Task<Post?> GetByIdAsync(int postId)
    {
        Post? existing = await context.posts.FirstOrDefaultAsync(p =>
            p.Id==postId);
        return existing;
    }

    public async  Task DeleteAsync(int id)
    {
       Post? existing = await GetByIdAsync(id);
        if (existing == null)
        {
            throw new Exception($"Post with id {id} not found");
        }

        context.posts.Remove(existing);
        await context.SaveChangesAsync();
    }
}