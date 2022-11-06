using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class CommentLogic: ICommentLogic
{
    private readonly ICommentDao commentDao;
    private readonly IUserDao userDao;

    public CommentLogic(ICommentDao commentDao, IUserDao userDao)
    {
        this.commentDao = commentDao;
        this.userDao = userDao;
    }

    public async Task<Comment> CreateAsync(CommentCreationDto dto)
    {
        User? user = await userDao.GetByUsernameAsync(dto.OwnerUsername);
        if (user == null)
        {
            throw new Exception($"User with username {dto.OwnerUsername} was not found.");
        }
        
        Comment toCreate = new Comment(user,dto.PostId,dto.Message);
        Comment created = await commentDao.CreateAsync(toCreate);
        return created;
    }
    

    public Task<IEnumerable<Comment?>> GetAsync()
    {
        return commentDao.GetAsync();
    }

    public Task<IEnumerable<Comment?>> GetByPostAsync(int postId)
    {
        return commentDao.GetByPostAsync(postId);
    }
}