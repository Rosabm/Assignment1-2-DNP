using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentLogic Logic;

    public CommentController(ICommentLogic logic)
    {
        Logic = logic;
    }
    
    [HttpPost]
    public async Task<ActionResult<Comment>> CreateAsync(CommentCreationDto dto)
    {
        try
        {
            Comment comment = await Logic.CreateAsync(dto);
            return Ok(comment);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Comment>>> GetAsync()
    {
        try
        {  
            var comments = await Logic.GetAsync();
            return Ok(comments);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("{postId:int}")]
    public async Task<ActionResult<IEnumerable<Comment>>> GetByPost([FromRoute] int postId)
    {
        try
        {
            var result = await Logic.GetByPostAsync(postId);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    
}