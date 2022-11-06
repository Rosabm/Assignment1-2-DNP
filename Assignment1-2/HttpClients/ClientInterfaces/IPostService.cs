using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IPostService
{
    Task CreateAsync(PostCreationDto dto); 
    Task<ICollection<Post>> GetAsync(
        string? userName,
        string? titleContains
    );
    
    Task<PostBasicDto> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}