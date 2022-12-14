using System.ComponentModel.DataAnnotations;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace WebApi.Services;

public class AuthService : IAuthService
{
    private IUserLogic logic;
    private IEnumerable<User> users;


    public AuthService(IUserLogic logic)
    {
        this.logic = logic;
        users = logic.GetAsync().Result;
    }

   
    public Task<User> ValidateUser(string username, string password)
    {
        User? existingUser = users.FirstOrDefault(u => 
            u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));
        
        if (existingUser == null)
        {
            throw new Exception("User not found");
        }

        if (!existingUser.Password.Equals(password))
        {
            throw new Exception("Password mismatch");
        }

        return Task.FromResult(existingUser);
    }
    
    public Task RegisterUser(User user)
    {
        if (string.IsNullOrEmpty(user.UserName))
        {
            throw new ValidationException("Username cannot be null");
        }

        if (string.IsNullOrEmpty(user.Password))
        {
            throw new ValidationException("Password cannot be null");
        }
        // Do more user info validation here
        
        // save to persistence instead of list
        UserCreationDto dto = new UserCreationDto(user.UserName, user.Password);
        logic.CreateAsync(dto);
        
        return Task.CompletedTask;
    }
}