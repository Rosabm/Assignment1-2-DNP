using Domain.Models;

namespace WebApi.Services;

public interface IAuthService
{
     Task RegisterUser(User user);
     Task<User> ValidateUser(string username, string password);
}